using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Role_dir : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`role_dir`";
			internal static readonly string Field = "a.`dir_id`, a.`role_id`";
			internal static readonly string Sort = "a.`dir_id`, a.`role_id`";
			internal static readonly string Returning = "";
			internal static readonly string Delete = "DELETE FROM `role_dir` WHERE ";
			internal static readonly string InsertField = @"`dir_id`, `role_id`";
			internal static readonly string InsertValues = @"?dir_id, ?role_id";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `role_dir`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(Role_dirInfo item) {
			return new MySqlParameter[] {
				GetParameter("?dir_id", MySqlDbType.UInt32, 10, item.Dir_id), 
				GetParameter("?role_id", MySqlDbType.UInt32, 10, item.Role_id)};
		}
		public Role_dirInfo GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as Role_dirInfo;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			Role_dirInfo item = new Role_dirInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Dir_id = (uint?)dr.GetInt32(dataIndex); if (item.Dir_id == null) { dataIndex += 1; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Role_id = (uint?)dr.GetInt32(dataIndex); if (item.Role_id == null) { dataIndex += 0; return null; }
			return item;
		}
		private void CopyItemAllField(Role_dirInfo item, Role_dirInfo newitem) {
			item.Dir_id = newitem.Dir_id;
			item.Role_id = newitem.Role_id;
		}
		#endregion

		public int Delete(uint Dir_id, uint Role_id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`dir_id` = ?dir_id AND `role_id` = ?role_id"), 
				GetParameter("?dir_id", MySqlDbType.UInt32, 10, Dir_id), 
				GetParameter("?role_id", MySqlDbType.UInt32, 10, Role_id));
		}
		public int DeleteByDir_id(uint? Dir_id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`dir_id` = ?dir_id"), 
				GetParameter("?dir_id", MySqlDbType.UInt32, 10, Dir_id));
		}
		public int DeleteByRole_id(uint? Role_id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`role_id` = ?role_id"), 
				GetParameter("?role_id", MySqlDbType.UInt32, 10, Role_id));
		}

		public SqlUpdateBuild Update(Role_dirInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<Role_dirInfo> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<Role_dirInfo> _dataSource;
			protected Dictionary<string, Role_dirInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<Role_dirInfo> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Dir_id}_{a.Role_id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`dir_id` IN ({0})", _dataSource.Select(a => a.Dir_id).Distinct())
						.Where(@"`role_id` IN ({0})", _dataSource.Select(a => a.Role_id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Role_dir.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Role_dir.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Role_dir.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Role_dir.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
		}
		#endregion

		public Role_dirInfo Insert(Role_dirInfo item) {
			SqlHelper.ExecuteNonQuery(TSQL.Insert, GetParameters(item));
			return item;
		}
		public int Insert(IEnumerable<Role_dirInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return SqlHelper.ExecuteNonQuery(mp.sql, mp.parms);
		}
		public (string sql, MySqlParameter[] parms) InsertMakeParam(IEnumerable<Role_dirInfo> items) {
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
		async public Task<Role_dirInfo> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as Role_dirInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			Role_dirInfo item = new Role_dirInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Dir_id = (uint?)dr.GetInt32(dataIndex); if (item.Dir_id == null) { dataIndex += 1; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Role_id = (uint?)dr.GetInt32(dataIndex); if (item.Role_id == null) { dataIndex += 0; return (null, dataIndex); }
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(uint Dir_id, uint Role_id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`dir_id` = ?dir_id AND `role_id` = ?role_id"), 
				GetParameter("?dir_id", MySqlDbType.UInt32, 10, Dir_id), 
				GetParameter("?role_id", MySqlDbType.UInt32, 10, Role_id));
		}
		public Task<int> DeleteByDir_idAsync(uint? Dir_id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`dir_id` = ?dir_id"), 
				GetParameter("?dir_id", MySqlDbType.UInt32, 10, Dir_id));
		}
		public Task<int> DeleteByRole_idAsync(uint? Role_id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`role_id` = ?role_id"), 
				GetParameter("?role_id", MySqlDbType.UInt32, 10, Role_id));
		}
		async public Task<Role_dirInfo> InsertAsync(Role_dirInfo item) {
			await SqlHelper.ExecuteNonQueryAsync(TSQL.Insert, GetParameters(item));
			return item;
		}
		async public Task<int> InsertAsync(IEnumerable<Role_dirInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return await SqlHelper.ExecuteNonQueryAsync(mp.sql, mp.parms);
		}
		#endregion
	}
}