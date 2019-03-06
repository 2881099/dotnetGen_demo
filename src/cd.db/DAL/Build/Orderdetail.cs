using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Orderdetail : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`orderdetail`";
			internal static readonly string Field = "a.`DetailId`, a.`OrderId`";
			internal static readonly string Sort = "a.`DetailId`";
			internal static readonly string Returning = "";
			internal static readonly string Delete = "DELETE FROM `orderdetail` WHERE ";
			internal static readonly string InsertField = @"`DetailId`, `OrderId`";
			internal static readonly string InsertValues = @"?DetailId, ?OrderId";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `orderdetail`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(OrderdetailInfo item) {
			return new MySqlParameter[] {
				GetParameter("?DetailId", MySqlDbType.Int32, 11, item.DetailId), 
				GetParameter("?OrderId", MySqlDbType.Int32, 11, item.OrderId)};
		}
		public OrderdetailInfo GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as OrderdetailInfo;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			OrderdetailInfo item = new OrderdetailInfo();
			if (!dr.IsDBNull(++dataIndex)) item.DetailId = (int?)dr.GetInt32(dataIndex); if (item.DetailId == null) { dataIndex += 1; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.OrderId = (int?)dr.GetInt32(dataIndex);
			return item;
		}
		private void CopyItemAllField(OrderdetailInfo item, OrderdetailInfo newitem) {
			item.DetailId = newitem.DetailId;
			item.OrderId = newitem.OrderId;
		}
		#endregion

		public int Delete(int DetailId) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`DetailId` = ?DetailId"), 
				GetParameter("?DetailId", MySqlDbType.Int32, 11, DetailId));
		}

		public SqlUpdateBuild Update(OrderdetailInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<OrderdetailInfo> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("OrderId") == false) sub.SetOrderId(item.OrderId);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<OrderdetailInfo> _dataSource;
			protected Dictionary<string, OrderdetailInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<OrderdetailInfo> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.DetailId}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`DetailId` IN ({0})", _dataSource.Select(a => a.DetailId).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Orderdetail.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Orderdetail.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Orderdetail.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Orderdetail.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetOrderId(int? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.OrderId = value;
				return this.Set("`OrderId`", $"?OrderId_{_parameters.Count}", 
					GetParameter($"?OrderId_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
			public SqlUpdateBuild SetOrderIdIncrement(int value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.OrderId += value;
				return this.Set("`OrderId`", $"ifnull(`OrderId`, 0) + ?OrderId_{_parameters.Count}", 
					GetParameter($"?OrderId_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
		}
		#endregion

		public OrderdetailInfo Insert(OrderdetailInfo item) {
			SqlHelper.ExecuteNonQuery(TSQL.Insert, GetParameters(item));
			return item;
		}
		public int Insert(IEnumerable<OrderdetailInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return SqlHelper.ExecuteNonQuery(mp.sql, mp.parms);
		}
		public (string sql, MySqlParameter[] parms) InsertMakeParam(IEnumerable<OrderdetailInfo> items) {
			var itemsArr = items?.Where(a => a != null).ToArray();
			if (itemsArr == null || itemsArr.Any() == false) return (null, null);
			var values = "";
			var parms = new MySqlParameter[itemsArr.Length * 2];
			for (var a = 0; a < itemsArr.Length; a++) {
				var item = itemsArr[a];
				values += $",({TSQL.InsertValues.Replace(", ", a + ", ")}{a})";
				var tmparms = GetParameters(item);
				for (var b = 0; b < tmparms.Length; b++) {
					tmparms[b].ParameterName += a;
					parms[a * 2 + b] = tmparms[b];
				}
			}
			return (string.Format(TSQL.InsertMultiFormat, values.Substring(1)), parms);
		}

		#region async
		async public Task<OrderdetailInfo> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as OrderdetailInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			OrderdetailInfo item = new OrderdetailInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.DetailId = (int?)dr.GetInt32(dataIndex); if (item.DetailId == null) { dataIndex += 1; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.OrderId = (int?)dr.GetInt32(dataIndex);
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(int DetailId) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`DetailId` = ?DetailId"), 
				GetParameter("?DetailId", MySqlDbType.Int32, 11, DetailId));
		}
		async public Task<OrderdetailInfo> InsertAsync(OrderdetailInfo item) {
			await SqlHelper.ExecuteNonQueryAsync(TSQL.Insert, GetParameters(item));
			return item;
		}
		async public Task<int> InsertAsync(IEnumerable<OrderdetailInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return await SqlHelper.ExecuteNonQueryAsync(mp.sql, mp.parms);
		}
		#endregion
	}
}