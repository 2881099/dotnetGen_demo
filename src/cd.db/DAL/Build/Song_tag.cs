using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Song_tag : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`song_tag`";
			internal static readonly string Field = "a.`song_id`, a.`tag_id`";
			internal static readonly string Sort = "a.`song_id`, a.`tag_id`";
			internal static readonly string Returning = "";
			internal static readonly string Delete = "DELETE FROM `song_tag` WHERE ";
			internal static readonly string InsertField = @"`song_id`, `tag_id`";
			internal static readonly string InsertValues = @"?song_id, ?tag_id";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `song_tag`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(Song_tagInfo item) {
			return new MySqlParameter[] {
				GetParameter("?song_id", MySqlDbType.Int32, 11, item.Song_id), 
				GetParameter("?tag_id", MySqlDbType.Int32, 11, item.Tag_id)};
		}
		public Song_tagInfo GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as Song_tagInfo;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			Song_tagInfo item = new Song_tagInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Song_id = (int?)dr.GetInt32(dataIndex); if (item.Song_id == null) { dataIndex += 1; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Tag_id = (int?)dr.GetInt32(dataIndex); if (item.Tag_id == null) { dataIndex += 0; return null; }
			return item;
		}
		private void CopyItemAllField(Song_tagInfo item, Song_tagInfo newitem) {
			item.Song_id = newitem.Song_id;
			item.Tag_id = newitem.Tag_id;
		}
		#endregion

		public int Delete(int Song_id, int Tag_id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`song_id` = ?song_id AND `tag_id` = ?tag_id"), 
				GetParameter("?song_id", MySqlDbType.Int32, 11, Song_id), 
				GetParameter("?tag_id", MySqlDbType.Int32, 11, Tag_id));
		}
		public int DeleteBySong_id(int? Song_id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`song_id` = ?song_id"), 
				GetParameter("?song_id", MySqlDbType.Int32, 11, Song_id));
		}
		public int DeleteByTag_id(int? Tag_id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`tag_id` = ?tag_id"), 
				GetParameter("?tag_id", MySqlDbType.Int32, 11, Tag_id));
		}

		public SqlUpdateBuild Update(Song_tagInfo item) {
			return new SqlUpdateBuild(new List<Song_tagInfo> { item });
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<Song_tagInfo> _dataSource;
			protected Dictionary<string, Song_tagInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<Song_tagInfo> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Song_id}_{a.Tag_id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`song_id` IN ({0})", _dataSource.Select(a => a.Song_id).Distinct())
						.Where(@"`tag_id` IN ({0})", _dataSource.Select(a => a.Tag_id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Song_tag.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Song_tag.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Song_tag.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Song_tag.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
		}
		#endregion

		public Song_tagInfo Insert(Song_tagInfo item) {
			SqlHelper.ExecuteNonQuery(TSQL.Insert, GetParameters(item));
			return item;
		}
		public int Insert(IEnumerable<Song_tagInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return SqlHelper.ExecuteNonQuery(mp.sql, mp.parms);
		}
		public (string sql, MySqlParameter[] parms) InsertMakeParam(IEnumerable<Song_tagInfo> items) {
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
		async public Task<Song_tagInfo> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as Song_tagInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			Song_tagInfo item = new Song_tagInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Song_id = (int?)dr.GetInt32(dataIndex); if (item.Song_id == null) { dataIndex += 1; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Tag_id = (int?)dr.GetInt32(dataIndex); if (item.Tag_id == null) { dataIndex += 0; return (null, dataIndex); }
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(int Song_id, int Tag_id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`song_id` = ?song_id AND `tag_id` = ?tag_id"), 
				GetParameter("?song_id", MySqlDbType.Int32, 11, Song_id), 
				GetParameter("?tag_id", MySqlDbType.Int32, 11, Tag_id));
		}
		public Task<int> DeleteBySong_idAsync(int? Song_id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`song_id` = ?song_id"), 
				GetParameter("?song_id", MySqlDbType.Int32, 11, Song_id));
		}
		public Task<int> DeleteByTag_idAsync(int? Tag_id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`tag_id` = ?tag_id"), 
				GetParameter("?tag_id", MySqlDbType.Int32, 11, Tag_id));
		}
		async public Task<Song_tagInfo> InsertAsync(Song_tagInfo item) {
			await SqlHelper.ExecuteNonQueryAsync(TSQL.Insert, GetParameters(item));
			return item;
		}
		async public Task<int> InsertAsync(IEnumerable<Song_tagInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return await SqlHelper.ExecuteNonQueryAsync(mp.sql, mp.parms);
		}
		#endregion
	}
}