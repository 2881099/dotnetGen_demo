using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Post : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`post`";
			internal static readonly string Field = "a.`id`, a.`topic_id`, a.`content`, a.`create_time`";
			internal static readonly string Sort = "a.`id`";
			internal static readonly string Returning = "; SELECT LAST_INSERT_ID();";
			internal static readonly string Delete = "DELETE FROM `post` WHERE ";
			internal static readonly string InsertField = @"`topic_id`, `content`, `create_time`";
			internal static readonly string InsertValues = @"?topic_id, ?content, ?create_time";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `post`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(PostInfo item) {
			return new MySqlParameter[] {
				GetParameter("?id", MySqlDbType.Int32, 11, item.Id), 
				GetParameter("?topic_id", MySqlDbType.UInt32, 10, item.Topic_id), 
				GetParameter("?content", MySqlDbType.Text, -1, item.Content), 
				GetParameter("?create_time", MySqlDbType.DateTime, -1, item.Create_time)};
		}
		public PostInfo GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as PostInfo;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			PostInfo item = new PostInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Id = (int?)dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 3; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Topic_id = (uint?)dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Content = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Create_time = (DateTime?)dr.GetDateTime(dataIndex);
			return item;
		}
		private void CopyItemAllField(PostInfo item, PostInfo newitem) {
			item.Id = newitem.Id;
			item.Topic_id = newitem.Topic_id;
			item.Content = newitem.Content;
			item.Create_time = newitem.Create_time;
		}
		#endregion

		public int Delete(int Id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`id` = ?id"), 
				GetParameter("?id", MySqlDbType.Int32, 11, Id));
		}
		public int DeleteByTopic_id(uint? Topic_id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`topic_id` = ?topic_id"), 
				GetParameter("?topic_id", MySqlDbType.UInt32, 10, Topic_id));
		}

		public SqlUpdateBuild Update(PostInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<PostInfo> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("topic_id") == false) sub.SetTopic_id(item.Topic_id);
			if (ignore.ContainsKey("content") == false) sub.SetContent(item.Content);
			if (ignore.ContainsKey("create_time") == false) sub.SetCreate_time(item.Create_time);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<PostInfo> _dataSource;
			protected Dictionary<string, PostInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<PostInfo> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`id` IN ({0})", _dataSource.Select(a => a.Id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Post.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Post.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Post.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Post.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetTopic_id(uint? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Topic_id = value;
				return this.Set("`topic_id`", $"?topic_id_{_parameters.Count}", 
					GetParameter($"?topic_id_{_parameters.Count}", MySqlDbType.UInt32, 10, value));
			}
			public SqlUpdateBuild SetContent(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Content = value;
				return this.Set("`content`", $"?content_{_parameters.Count}", 
					GetParameter($"?content_{_parameters.Count}", MySqlDbType.Text, -1, value));
			}
			public SqlUpdateBuild SetCreate_time(DateTime? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Create_time = value;
				return this.Set("`create_time`", $"?create_time_{_parameters.Count}", 
					GetParameter($"?create_time_{_parameters.Count}", MySqlDbType.DateTime, -1, value));
			}
		}
		#endregion

		public PostInfo Insert(PostInfo item) {
			if (int.TryParse(string.Concat(SqlHelper.ExecuteScalar(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}

		#region async
		async public Task<PostInfo> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as PostInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			PostInfo item = new PostInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = (int?)dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 3; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Topic_id = (uint?)dr.GetInt32(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Content = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Create_time = (DateTime?)dr.GetDateTime(dataIndex);
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(int Id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`id` = ?id"), 
				GetParameter("?id", MySqlDbType.Int32, 11, Id));
		}
		public Task<int> DeleteByTopic_idAsync(uint? Topic_id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`topic_id` = ?topic_id"), 
				GetParameter("?topic_id", MySqlDbType.UInt32, 10, Topic_id));
		}
		async public Task<PostInfo> InsertAsync(PostInfo item) {
			if (int.TryParse(string.Concat(await SqlHelper.ExecuteScalarAsync(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}
		#endregion
	}
}