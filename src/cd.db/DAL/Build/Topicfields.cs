using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Topicfields : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`topicfields`";
			internal static readonly string Field = "a.`TopicId`";
			internal static readonly string Sort = "a.`TopicId`";
			internal static readonly string Returning = "";
			internal static readonly string Delete = "DELETE FROM `topicfields` WHERE ";
			internal static readonly string InsertField = @"`TopicId`";
			internal static readonly string InsertValues = @"?TopicId";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `topicfields`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(TopicfieldsInfo item) {
			return new MySqlParameter[] {
				GetParameter("?TopicId", MySqlDbType.Int32, 11, item.TopicId)};
		}
		public TopicfieldsInfo GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as TopicfieldsInfo;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			TopicfieldsInfo item = new TopicfieldsInfo();
			if (!dr.IsDBNull(++dataIndex)) item.TopicId = (int?)dr.GetInt32(dataIndex); if (item.TopicId == null) { dataIndex += 0; return null; }
			return item;
		}
		private void CopyItemAllField(TopicfieldsInfo item, TopicfieldsInfo newitem) {
			item.TopicId = newitem.TopicId;
		}
		#endregion

		public int Delete(int TopicId) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`TopicId` = ?TopicId"), 
				GetParameter("?TopicId", MySqlDbType.Int32, 11, TopicId));
		}

		public SqlUpdateBuild Update(TopicfieldsInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<TopicfieldsInfo> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<TopicfieldsInfo> _dataSource;
			protected Dictionary<string, TopicfieldsInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<TopicfieldsInfo> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.TopicId}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`TopicId` IN ({0})", _dataSource.Select(a => a.TopicId).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Topicfields.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Topicfields.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Topicfields.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Topicfields.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
		}
		#endregion

		public TopicfieldsInfo Insert(TopicfieldsInfo item) {
			SqlHelper.ExecuteNonQuery(TSQL.Insert, GetParameters(item));
			return item;
		}
		public int Insert(IEnumerable<TopicfieldsInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return SqlHelper.ExecuteNonQuery(mp.sql, mp.parms);
		}
		public (string sql, MySqlParameter[] parms) InsertMakeParam(IEnumerable<TopicfieldsInfo> items) {
			var itemsArr = items?.Where(a => a != null).ToArray();
			if (itemsArr == null || itemsArr.Any() == false) return (null, null);
			var values = "";
			var parms = new MySqlParameter[itemsArr.Length * 1];
			for (var a = 0; a < itemsArr.Length; a++) {
				var item = itemsArr[a];
				values += $",({TSQL.InsertValues.Replace(", ", a + ", ")}{a})";
				var tmparms = GetParameters(item);
				for (var b = 0; b < tmparms.Length; b++) {
					tmparms[b].ParameterName += a;
					parms[a * 1 + b] = tmparms[b];
				}
			}
			return (string.Format(TSQL.InsertMultiFormat, values.Substring(1)), parms);
		}

		#region async
		async public Task<TopicfieldsInfo> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as TopicfieldsInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			TopicfieldsInfo item = new TopicfieldsInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TopicId = (int?)dr.GetInt32(dataIndex); if (item.TopicId == null) { dataIndex += 0; return (null, dataIndex); }
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(int TopicId) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`TopicId` = ?TopicId"), 
				GetParameter("?TopicId", MySqlDbType.Int32, 11, TopicId));
		}
		async public Task<TopicfieldsInfo> InsertAsync(TopicfieldsInfo item) {
			await SqlHelper.ExecuteNonQueryAsync(TSQL.Insert, GetParameters(item));
			return item;
		}
		async public Task<int> InsertAsync(IEnumerable<TopicfieldsInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return await SqlHelper.ExecuteNonQueryAsync(mp.sql, mp.parms);
		}
		#endregion
	}
}