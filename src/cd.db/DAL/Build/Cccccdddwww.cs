using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Cccccdddwww : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`cccccdddwww`";
			internal static readonly string Field = "a.`Idx`, a.`name`";
			internal static readonly string Sort = "a.`Idx`";
			internal static readonly string Returning = "";
			internal static readonly string Delete = "DELETE FROM `cccccdddwww` WHERE ";
			internal static readonly string InsertField = @"`Idx`, `name`";
			internal static readonly string InsertValues = @"?Idx, ?name";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `cccccdddwww`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(CccccdddwwwInfo item) {
			return new MySqlParameter[] {
				GetParameter("?Idx", MySqlDbType.Int32, 11, item.Idx), 
				GetParameter("?name", MySqlDbType.VarChar, 100, item.Name)};
		}
		public CccccdddwwwInfo GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as CccccdddwwwInfo;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			CccccdddwwwInfo item = new CccccdddwwwInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Idx = (int?)dr.GetInt32(dataIndex); if (item.Idx == null) { dataIndex += 1; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Name = dr.GetString(dataIndex);
			return item;
		}
		private void CopyItemAllField(CccccdddwwwInfo item, CccccdddwwwInfo newitem) {
			item.Idx = newitem.Idx;
			item.Name = newitem.Name;
		}
		#endregion

		public int Delete(int Idx) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`Idx` = ?Idx"), 
				GetParameter("?Idx", MySqlDbType.Int32, 11, Idx));
		}

		public SqlUpdateBuild Update(CccccdddwwwInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<CccccdddwwwInfo> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("name") == false) sub.SetName(item.Name);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<CccccdddwwwInfo> _dataSource;
			protected Dictionary<string, CccccdddwwwInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<CccccdddwwwInfo> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Idx}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`Idx` IN ({0})", _dataSource.Select(a => a.Idx).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Cccccdddwww.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Cccccdddwww.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Cccccdddwww.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Cccccdddwww.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetName(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Name = value;
				return this.Set("`name`", $"?name_{_parameters.Count}", 
					GetParameter($"?name_{_parameters.Count}", MySqlDbType.VarChar, 100, value));
			}
		}
		#endregion

		public CccccdddwwwInfo Insert(CccccdddwwwInfo item) {
			SqlHelper.ExecuteNonQuery(TSQL.Insert, GetParameters(item));
			return item;
		}
		public int Insert(IEnumerable<CccccdddwwwInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return SqlHelper.ExecuteNonQuery(mp.sql, mp.parms);
		}
		public (string sql, MySqlParameter[] parms) InsertMakeParam(IEnumerable<CccccdddwwwInfo> items) {
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
		async public Task<CccccdddwwwInfo> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as CccccdddwwwInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			CccccdddwwwInfo item = new CccccdddwwwInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Idx = (int?)dr.GetInt32(dataIndex); if (item.Idx == null) { dataIndex += 1; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Name = dr.GetString(dataIndex);
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(int Idx) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`Idx` = ?Idx"), 
				GetParameter("?Idx", MySqlDbType.Int32, 11, Idx));
		}
		async public Task<CccccdddwwwInfo> InsertAsync(CccccdddwwwInfo item) {
			await SqlHelper.ExecuteNonQueryAsync(TSQL.Insert, GetParameters(item));
			return item;
		}
		async public Task<int> InsertAsync(IEnumerable<CccccdddwwwInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return await SqlHelper.ExecuteNonQueryAsync(mp.sql, mp.parms);
		}
		#endregion
	}
}