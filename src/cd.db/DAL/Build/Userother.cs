using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Userother : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`userother`";
			internal static readonly string Field = "a.`userid`, a.`chinesename`, a.`created`, a.`doctype`, a.`englishname`, a.`hasverify`, a.`id`, a.`idnumber`, a.`images`";
			internal static readonly string Sort = "a.`userid`";
			internal static readonly string Returning = "; SELECT LAST_INSERT_ID();";
			internal static readonly string Delete = "DELETE FROM `userother` WHERE ";
			internal static readonly string InsertField = @"`userid`, `chinesename`, `created`, `doctype`, `englishname`, `hasverify`, `idnumber`, `images`";
			internal static readonly string InsertValues = @"?userid, ?chinesename, ?created, ?doctype, ?englishname, ?hasverify, ?idnumber, ?images";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `userother`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(UserotherInfo item) {
			return new MySqlParameter[] {
				GetParameter("?userid", MySqlDbType.VarChar, 100, item.Userid), 
				GetParameter("?chinesename", MySqlDbType.VarChar, 100, item.Chinesename), 
				GetParameter("?created", MySqlDbType.DateTime, -1, item.Created), 
				GetParameter("?doctype", MySqlDbType.VarChar, 100, item.Doctype), 
				GetParameter("?englishname", MySqlDbType.VarChar, 100, item.Englishname), 
				GetParameter("?hasverify", MySqlDbType.Bit, 1, item.Hasverify), 
				GetParameter("?id", MySqlDbType.Int64, 20, item.Id), 
				GetParameter("?idnumber", MySqlDbType.VarChar, 100, item.Idnumber), 
				GetParameter("?images", MySqlDbType.VarChar, 500, item.Images)};
		}
		public UserotherInfo GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as UserotherInfo;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			UserotherInfo item = new UserotherInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Userid = dr.GetString(dataIndex); if (item.Userid == null) { dataIndex += 8; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Chinesename = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Created = (DateTime?)dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Doctype = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Englishname = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Hasverify = (bool?)dr.GetBoolean(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Id = (long?)dr.GetInt64(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Idnumber = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Images = dr.GetString(dataIndex);
			return item;
		}
		private void CopyItemAllField(UserotherInfo item, UserotherInfo newitem) {
			item.Userid = newitem.Userid;
			item.Chinesename = newitem.Chinesename;
			item.Created = newitem.Created;
			item.Doctype = newitem.Doctype;
			item.Englishname = newitem.Englishname;
			item.Hasverify = newitem.Hasverify;
			item.Id = newitem.Id;
			item.Idnumber = newitem.Idnumber;
			item.Images = newitem.Images;
		}
		#endregion

		public int Delete(string Userid) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`userid` = ?userid"), 
				GetParameter("?userid", MySqlDbType.VarChar, 100, Userid));
		}
		public int DeleteById(long Id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`id` = ?id"), 
				GetParameter("?id", MySqlDbType.Int64, 20, Id));
		}

		public SqlUpdateBuild Update(UserotherInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<UserotherInfo> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("chinesename") == false) sub.SetChinesename(item.Chinesename);
			if (ignore.ContainsKey("created") == false) sub.SetCreated(item.Created);
			if (ignore.ContainsKey("doctype") == false) sub.SetDoctype(item.Doctype);
			if (ignore.ContainsKey("englishname") == false) sub.SetEnglishname(item.Englishname);
			if (ignore.ContainsKey("hasverify") == false) sub.SetHasverify(item.Hasverify);
			if (ignore.ContainsKey("idnumber") == false) sub.SetIdnumber(item.Idnumber);
			if (ignore.ContainsKey("images") == false) sub.SetImages(item.Images);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<UserotherInfo> _dataSource;
			protected Dictionary<string, UserotherInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<UserotherInfo> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Userid}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`userid` IN ({0})", _dataSource.Select(a => a.Userid).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Userother.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Userother.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Userother.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Userother.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetChinesename(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Chinesename = value;
				return this.Set("`chinesename`", $"?chinesename_{_parameters.Count}", 
					GetParameter($"?chinesename_{_parameters.Count}", MySqlDbType.VarChar, 100, value));
			}
			public SqlUpdateBuild SetCreated(DateTime? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Created = value;
				return this.Set("`created`", $"?created_{_parameters.Count}", 
					GetParameter($"?created_{_parameters.Count}", MySqlDbType.DateTime, -1, value));
			}
			public SqlUpdateBuild SetDoctype(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Doctype = value;
				return this.Set("`doctype`", $"?doctype_{_parameters.Count}", 
					GetParameter($"?doctype_{_parameters.Count}", MySqlDbType.VarChar, 100, value));
			}
			public SqlUpdateBuild SetEnglishname(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Englishname = value;
				return this.Set("`englishname`", $"?englishname_{_parameters.Count}", 
					GetParameter($"?englishname_{_parameters.Count}", MySqlDbType.VarChar, 100, value));
			}
			public SqlUpdateBuild SetHasverify(bool? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Hasverify = value;
				return this.Set("`hasverify`", $"?hasverify_{_parameters.Count}", 
					GetParameter($"?hasverify_{_parameters.Count}", MySqlDbType.Bit, 1, value));
			}
			public SqlUpdateBuild SetIdnumber(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Idnumber = value;
				return this.Set("`idnumber`", $"?idnumber_{_parameters.Count}", 
					GetParameter($"?idnumber_{_parameters.Count}", MySqlDbType.VarChar, 100, value));
			}
			public SqlUpdateBuild SetImages(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Images = value;
				return this.Set("`images`", $"?images_{_parameters.Count}", 
					GetParameter($"?images_{_parameters.Count}", MySqlDbType.VarChar, 500, value));
			}
		}
		#endregion

		public UserotherInfo Insert(UserotherInfo item) {
			if (long.TryParse(string.Concat(SqlHelper.ExecuteScalar(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}

		#region async
		async public Task<UserotherInfo> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as UserotherInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			UserotherInfo item = new UserotherInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Userid = dr.GetString(dataIndex); if (item.Userid == null) { dataIndex += 8; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Chinesename = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Created = (DateTime?)dr.GetDateTime(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Doctype = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Englishname = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Hasverify = (bool?)dr.GetBoolean(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = (long?)dr.GetInt64(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Idnumber = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Images = dr.GetString(dataIndex);
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(string Userid) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`userid` = ?userid"), 
				GetParameter("?userid", MySqlDbType.VarChar, 100, Userid));
		}
		public Task<int> DeleteByIdAsync(long Id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`id` = ?id"), 
				GetParameter("?id", MySqlDbType.Int64, 20, Id));
		}
		async public Task<UserotherInfo> InsertAsync(UserotherInfo item) {
			if (long.TryParse(string.Concat(await SqlHelper.ExecuteScalarAsync(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}
		#endregion
	}
}