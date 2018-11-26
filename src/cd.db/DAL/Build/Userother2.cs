using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Userother2 : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`userother2`";
			internal static readonly string Field = "a.`userother_id`, a.`chinesename`, a.`xxxx`";
			internal static readonly string Sort = "a.`userother_id`";
			internal static readonly string Returning = "";
			internal static readonly string Delete = "DELETE FROM `userother2` WHERE ";
			internal static readonly string InsertField = @"`userother_id`, `chinesename`, `xxxx`";
			internal static readonly string InsertValues = @"?userother_id, ?chinesename, ?xxxx";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `userother2`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(Userother2Info item) {
			return new MySqlParameter[] {
				GetParameter("?userother_id", MySqlDbType.Int64, 20, item.Userother_id), 
				GetParameter("?chinesename", MySqlDbType.VarChar, 100, item.Chinesename), 
				GetParameter("?xxxx", MySqlDbType.VarChar, 255, item.Xxxx)};
		}
		public Userother2Info GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as Userother2Info;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			Userother2Info item = new Userother2Info();
			if (!dr.IsDBNull(++dataIndex)) item.Userother_id = (long?)dr.GetInt64(dataIndex); if (item.Userother_id == null) { dataIndex += 2; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Chinesename = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Xxxx = dr.GetString(dataIndex);
			return item;
		}
		private void CopyItemAllField(Userother2Info item, Userother2Info newitem) {
			item.Userother_id = newitem.Userother_id;
			item.Chinesename = newitem.Chinesename;
			item.Xxxx = newitem.Xxxx;
		}
		#endregion

		public int Delete(long Userother_id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`userother_id` = ?userother_id"), 
				GetParameter("?userother_id", MySqlDbType.Int64, 20, Userother_id));
		}
		public int DeleteByUserother_id(long? Userother_id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`userother_id` = ?userother_id"), 
				GetParameter("?userother_id", MySqlDbType.Int64, 20, Userother_id));
		}

		public SqlUpdateBuild Update(Userother2Info item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<Userother2Info> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("chinesename") == false) sub.SetChinesename(item.Chinesename);
			if (ignore.ContainsKey("xxxx") == false) sub.SetXxxx(item.Xxxx);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<Userother2Info> _dataSource;
			protected Dictionary<string, Userother2Info> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<Userother2Info> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Userother_id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`userother_id` IN ({0})", _dataSource.Select(a => a.Userother_id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Userother2.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Userother2.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Userother2.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Userother2.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetChinesename(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Chinesename = value;
				return this.Set("`chinesename`", $"?chinesename_{_parameters.Count}", 
					GetParameter($"?chinesename_{_parameters.Count}", MySqlDbType.VarChar, 100, value));
			}
			public SqlUpdateBuild SetXxxx(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Xxxx = value;
				return this.Set("`xxxx`", $"?xxxx_{_parameters.Count}", 
					GetParameter($"?xxxx_{_parameters.Count}", MySqlDbType.VarChar, 255, value));
			}
		}
		#endregion

		public Userother2Info Insert(Userother2Info item) {
			SqlHelper.ExecuteNonQuery(TSQL.Insert, GetParameters(item));
			return item;
		}
		public int Insert(IEnumerable<Userother2Info> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return SqlHelper.ExecuteNonQuery(mp.sql, mp.parms);
		}
		public (string sql, MySqlParameter[] parms) InsertMakeParam(IEnumerable<Userother2Info> items) {
			var itemsArr = items?.Where(a => a != null).ToArray();
			if (itemsArr == null || itemsArr.Any() == false) return (null, null);
			var values = "";
			var parms = new MySqlParameter[itemsArr.Length * 3];
			for (var a = 0; a < itemsArr.Length; a++) {
				var item = itemsArr[a];
				values += $",({TSQL.InsertValues.Replace(", ", a + ", ")}{a})";
				var tmparms = GetParameters(item);
				for (var b = 0; b < tmparms.Length; b++) {
					tmparms[b].ParameterName += a;
					parms[a * 3 + b] = tmparms[b];
				}
			}
			return (string.Format(TSQL.InsertMultiFormat, values.Substring(1)), parms);
		}

		#region async
		async public Task<Userother2Info> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as Userother2Info;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			Userother2Info item = new Userother2Info();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Userother_id = (long?)dr.GetInt64(dataIndex); if (item.Userother_id == null) { dataIndex += 2; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Chinesename = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Xxxx = dr.GetString(dataIndex);
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(long Userother_id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`userother_id` = ?userother_id"), 
				GetParameter("?userother_id", MySqlDbType.Int64, 20, Userother_id));
		}
		public Task<int> DeleteByUserother_idAsync(long? Userother_id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`userother_id` = ?userother_id"), 
				GetParameter("?userother_id", MySqlDbType.Int64, 20, Userother_id));
		}
		async public Task<Userother2Info> InsertAsync(Userother2Info item) {
			await SqlHelper.ExecuteNonQueryAsync(TSQL.Insert, GetParameters(item));
			return item;
		}
		async public Task<int> InsertAsync(IEnumerable<Userother2Info> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return await SqlHelper.ExecuteNonQueryAsync(mp.sql, mp.parms);
		}
		#endregion
	}
}