using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Song : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`song`";
			internal static readonly string Field = "a.`id`, a.`create_time`, a.`is_deleted`, a.`title`, a.`url`";
			internal static readonly string Sort = "a.`id`";
			internal static readonly string Returning = "; SELECT LAST_INSERT_ID();";
			internal static readonly string Delete = "DELETE FROM `song` WHERE ";
			internal static readonly string InsertField = @"`create_time`, `is_deleted`, `title`, `url`";
			internal static readonly string InsertValues = @"?create_time, ?is_deleted, ?title, ?url";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `song`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(SongInfo item) {
			return new MySqlParameter[] {
				GetParameter("?id", MySqlDbType.Int32, 11, item.Id), 
				GetParameter("?create_time", MySqlDbType.DateTime, -1, item.Create_time), 
				GetParameter("?is_deleted", MySqlDbType.Bit, 1, item.Is_deleted), 
				GetParameter("?title", MySqlDbType.VarChar, 255, item.Title), 
				GetParameter("?url", MySqlDbType.VarChar, 255, item.Url)};
		}
		public SongInfo GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as SongInfo;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			SongInfo item = new SongInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Id = (int?)dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 4; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Create_time = (DateTime?)dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Is_deleted = (bool?)dr.GetBoolean(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Title = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Url = dr.GetString(dataIndex);
			return item;
		}
		private void CopyItemAllField(SongInfo item, SongInfo newitem) {
			item.Id = newitem.Id;
			item.Create_time = newitem.Create_time;
			item.Is_deleted = newitem.Is_deleted;
			item.Title = newitem.Title;
			item.Url = newitem.Url;
		}
		#endregion

		public int Delete(int Id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`id` = ?id"), 
				GetParameter("?id", MySqlDbType.Int32, 11, Id));
		}

		public SqlUpdateBuild Update(SongInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<SongInfo> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("create_time") == false) sub.SetCreate_time(item.Create_time);
			if (ignore.ContainsKey("is_deleted") == false) sub.SetIs_deleted(item.Is_deleted);
			if (ignore.ContainsKey("title") == false) sub.SetTitle(item.Title);
			if (ignore.ContainsKey("url") == false) sub.SetUrl(item.Url);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<SongInfo> _dataSource;
			protected Dictionary<string, SongInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<SongInfo> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`id` IN ({0})", _dataSource.Select(a => a.Id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Song.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Song.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Song.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Song.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetCreate_time(DateTime? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Create_time = value;
				return this.Set("`create_time`", $"?create_time_{_parameters.Count}", 
					GetParameter($"?create_time_{_parameters.Count}", MySqlDbType.DateTime, -1, value));
			}
			public SqlUpdateBuild SetIs_deleted(bool? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Is_deleted = value;
				return this.Set("`is_deleted`", $"?is_deleted_{_parameters.Count}", 
					GetParameter($"?is_deleted_{_parameters.Count}", MySqlDbType.Bit, 1, value));
			}
			public SqlUpdateBuild SetTitle(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Title = value;
				return this.Set("`title`", $"?title_{_parameters.Count}", 
					GetParameter($"?title_{_parameters.Count}", MySqlDbType.VarChar, 255, value));
			}
			public SqlUpdateBuild SetUrl(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Url = value;
				return this.Set("`url`", $"?url_{_parameters.Count}", 
					GetParameter($"?url_{_parameters.Count}", MySqlDbType.VarChar, 255, value));
			}
		}
		#endregion

		public SongInfo Insert(SongInfo item) {
			if (int.TryParse(string.Concat(SqlHelper.ExecuteScalar(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}

		#region async
		async public Task<SongInfo> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as SongInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			SongInfo item = new SongInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = (int?)dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 4; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Create_time = (DateTime?)dr.GetDateTime(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Is_deleted = (bool?)dr.GetBoolean(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Title = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Url = dr.GetString(dataIndex);
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(int Id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`id` = ?id"), 
				GetParameter("?id", MySqlDbType.Int32, 11, Id));
		}
		async public Task<SongInfo> InsertAsync(SongInfo item) {
			if (int.TryParse(string.Concat(await SqlHelper.ExecuteScalarAsync(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}
		#endregion
	}
}