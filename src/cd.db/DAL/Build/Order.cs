using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Order : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`order`";
			internal static readonly string Field = "a.`OrderID`, a.`CustomerName`, a.`OrderTitle`, a.`TransactionDate`";
			internal static readonly string Sort = "a.`OrderID`";
			internal static readonly string Returning = "";
			internal static readonly string Delete = "DELETE FROM `order` WHERE ";
			internal static readonly string InsertField = @"`OrderID`, `CustomerName`, `OrderTitle`, `TransactionDate`";
			internal static readonly string InsertValues = @"?OrderID, ?CustomerName, ?OrderTitle, ?TransactionDate";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `order`(" + InsertField + ") VALUES{0}";
			internal static readonly string Insert = string.Format(InsertMultiFormat, $"({InsertValues}){Returning}");
		}
		#endregion

		#region common call
		protected static MySqlParameter GetParameter(string name, MySqlDbType type, int size, object value) {
			MySqlParameter parm = new MySqlParameter(name, type);
			if (size > 0) parm.Size = size;
			parm.Value = value;
			return parm;
		}
		protected static MySqlParameter[] GetParameters(OrderInfo item) {
			return new MySqlParameter[] {
				GetParameter("?OrderID", MySqlDbType.Int32, 11, item.OrderID), 
				GetParameter("?CustomerName", MySqlDbType.VarChar, 255, item.CustomerName), 
				GetParameter("?OrderTitle", MySqlDbType.VarChar, 255, item.OrderTitle), 
				GetParameter("?TransactionDate", MySqlDbType.DateTime, -1, item.TransactionDate)};
		}
		public OrderInfo GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as OrderInfo;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			OrderInfo item = new OrderInfo();
			if (!dr.IsDBNull(++dataIndex)) item.OrderID = (int?)dr.GetInt32(dataIndex); if (item.OrderID == null) { dataIndex += 3; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.CustomerName = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.OrderTitle = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TransactionDate = (DateTime?)dr.GetDateTime(dataIndex);
			return item;
		}
		private void CopyItemAllField(OrderInfo item, OrderInfo newitem) {
			item.OrderID = newitem.OrderID;
			item.CustomerName = newitem.CustomerName;
			item.OrderTitle = newitem.OrderTitle;
			item.TransactionDate = newitem.TransactionDate;
		}
		#endregion

		public int Delete(int OrderID) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`OrderID` = ?OrderID"), 
				GetParameter("?OrderID", MySqlDbType.Int32, 11, OrderID));
		}

		public SqlUpdateBuild Update(OrderInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<OrderInfo> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("CustomerName") == false) sub.SetCustomerName(item.CustomerName);
			if (ignore.ContainsKey("OrderTitle") == false) sub.SetOrderTitle(item.OrderTitle);
			if (ignore.ContainsKey("TransactionDate") == false) sub.SetTransactionDate(item.TransactionDate);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<OrderInfo> _dataSource;
			protected Dictionary<string, OrderInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<OrderInfo> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.OrderID}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`OrderID` IN ({0})", _dataSource.Select(a => a.OrderID).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Order.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Order.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Order.RemoveCacheAsync(_dataSource);
				return affrows;
			}
			public SqlUpdateBuild Where(string filterFormat, params object[] values) {
				if (!string.IsNullOrEmpty(_where)) _where = string.Concat(_where, " AND ");
				_where = string.Concat(_where, "(", SqlHelper.Addslashes(filterFormat, values), ")");
				return this;
			}
			public SqlUpdateBuild WhereExists<T>(SelectBuild<T> select) {
				return this.Where($"EXISTS({select.ToString("1")})");
			}
			public SqlUpdateBuild WhereNotExists<T>(SelectBuild<T> select) {
				return this.Where($"NOT EXISTS({select.ToString("1")})");
			}

			public SqlUpdateBuild Set(string field, string value, params MySqlParameter[] parms) {
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Order.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetCustomerName(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.CustomerName = value;
				return this.Set("`CustomerName`", $"?CustomerName_{_parameters.Count}", 
					GetParameter($"?CustomerName_{_parameters.Count}", MySqlDbType.VarChar, 255, value));
			}
			public SqlUpdateBuild SetOrderTitle(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.OrderTitle = value;
				return this.Set("`OrderTitle`", $"?OrderTitle_{_parameters.Count}", 
					GetParameter($"?OrderTitle_{_parameters.Count}", MySqlDbType.VarChar, 255, value));
			}
			public SqlUpdateBuild SetTransactionDate(DateTime? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TransactionDate = value;
				return this.Set("`TransactionDate`", $"?TransactionDate_{_parameters.Count}", 
					GetParameter($"?TransactionDate_{_parameters.Count}", MySqlDbType.DateTime, -1, value));
			}
		}
		#endregion

		public OrderInfo Insert(OrderInfo item) {
			SqlHelper.ExecuteNonQuery(TSQL.Insert, GetParameters(item));
			return item;
		}
		public int Insert(IEnumerable<OrderInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return SqlHelper.ExecuteNonQuery(mp.sql, mp.parms);
		}
		public (string sql, MySqlParameter[] parms) InsertMakeParam(IEnumerable<OrderInfo> items) {
			var itemsArr = items?.Where(a => a != null).ToArray();
			if (itemsArr == null || itemsArr.Any() == false) return (null, null);
			var values = "";
			var parms = new MySqlParameter[itemsArr.Length * 4];
			for (var a = 0; a < itemsArr.Length; a++) {
				var item = itemsArr[a];
				values += $",({TSQL.InsertValues.Replace(", ", a + ", ")}{a})";
				var tmparms = GetParameters(item);
				for (var b = 0; b < tmparms.Length; b++) {
					tmparms[b].ParameterName += a;
					parms[a * 4 + b] = tmparms[b];
				}
			}
			return (string.Format(TSQL.InsertMultiFormat, values.Substring(1)), parms);
		}

		#region async
		async public Task<OrderInfo> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as OrderInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			OrderInfo item = new OrderInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.OrderID = (int?)dr.GetInt32(dataIndex); if (item.OrderID == null) { dataIndex += 3; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.CustomerName = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.OrderTitle = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TransactionDate = (DateTime?)dr.GetDateTime(dataIndex);
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(int OrderID) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`OrderID` = ?OrderID"), 
				GetParameter("?OrderID", MySqlDbType.Int32, 11, OrderID));
		}
		async public Task<OrderInfo> InsertAsync(OrderInfo item) {
			await SqlHelper.ExecuteNonQueryAsync(TSQL.Insert, GetParameters(item));
			return item;
		}
		async public Task<int> InsertAsync(IEnumerable<OrderInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return await SqlHelper.ExecuteNonQueryAsync(mp.sql, mp.parms);
		}
		#endregion
	}
}