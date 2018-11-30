using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Topic : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`topic`";
			internal static readonly string Field = "a.`id`, a.`topic_type_id`, a.`carddata`, a.`cardtype`+0, a.`clicks`, a.`content`, a.`create_time`, a.`order_time`, a.`test_addfiled`, a.`title`, a.`update_time`";
			internal static readonly string Sort = "a.`id`";
			internal static readonly string Returning = "; SELECT LAST_INSERT_ID();";
			internal static readonly string Delete = "DELETE FROM `topic` WHERE ";
			internal static readonly string InsertField = @"`topic_type_id`, `carddata`, `cardtype`, `clicks`, `content`, `create_time`, `order_time`, `test_addfiled`, `title`, `update_time`";
			internal static readonly string InsertValues = @"?topic_type_id, ?carddata, ?cardtype, ?clicks, ?content, ?create_time, ?order_time, ?test_addfiled, ?title, ?update_time";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `topic`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(TopicInfo item) {
			return new MySqlParameter[] {
				GetParameter("?id", MySqlDbType.UInt32, 10, item.Id), 
				GetParameter("?topic_type_id", MySqlDbType.Int32, 11, item.Topic_type_id), 
				GetParameter("?carddata", MySqlDbType.Text, -1, item.Carddata), 
				GetParameter("?cardtype", MySqlDbType.Enum, -1, item.Cardtype?.ToInt64()), 
				GetParameter("?clicks", MySqlDbType.UInt64, 20, item.Clicks), 
				GetParameter("?content", MySqlDbType.Text, -1, item.Content), 
				GetParameter("?create_time", MySqlDbType.DateTime, -1, item.Create_time), 
				GetParameter("?order_time", MySqlDbType.DateTime, -1, item.Order_time), 
				GetParameter("?test_addfiled", MySqlDbType.Byte, 4, item.Test_addfiled), 
				GetParameter("?title", MySqlDbType.VarChar, 255, item.Title), 
				GetParameter("?update_time", MySqlDbType.DateTime, -1, item.Update_time)};
		}
		public TopicInfo GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as TopicInfo;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			TopicInfo item = new TopicInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Id = (uint?)dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 10; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Topic_type_id = (int?)dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Carddata = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Cardtype = (TopicCARDTYPE?)dr.GetInt64(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Clicks = (ulong?)dr.GetInt64(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Content = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Create_time = (DateTime?)dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Order_time = (DateTime?)dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Test_addfiled = (byte?)dr.GetByte(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Title = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Update_time = (DateTime?)dr.GetDateTime(dataIndex);
			return item;
		}
		private void CopyItemAllField(TopicInfo item, TopicInfo newitem) {
			item.Id = newitem.Id;
			item.Topic_type_id = newitem.Topic_type_id;
			item.Carddata = newitem.Carddata;
			item.Cardtype = newitem.Cardtype;
			item.Clicks = newitem.Clicks;
			item.Content = newitem.Content;
			item.Create_time = newitem.Create_time;
			item.Order_time = newitem.Order_time;
			item.Test_addfiled = newitem.Test_addfiled;
			item.Title = newitem.Title;
			item.Update_time = newitem.Update_time;
		}
		#endregion

		public int Delete(uint Id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`id` = ?id"), 
				GetParameter("?id", MySqlDbType.UInt32, 10, Id));
		}
		public int DeleteByTopic_type_id(int? Topic_type_id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`topic_type_id` = ?topic_type_id"), 
				GetParameter("?topic_type_id", MySqlDbType.Int32, 11, Topic_type_id));
		}

		public SqlUpdateBuild Update(TopicInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<TopicInfo> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("topic_type_id") == false) sub.SetTopic_type_id(item.Topic_type_id);
			if (ignore.ContainsKey("carddata") == false) sub.SetCarddata(item.Carddata);
			if (ignore.ContainsKey("cardtype") == false) sub.SetCardtype(item.Cardtype);
			if (ignore.ContainsKey("clicks") == false) sub.SetClicks(item.Clicks);
			if (ignore.ContainsKey("content") == false) sub.SetContent(item.Content);
			if (ignore.ContainsKey("create_time") == false) sub.SetCreate_time(item.Create_time);
			if (ignore.ContainsKey("order_time") == false) sub.SetOrder_time(item.Order_time);
			if (ignore.ContainsKey("test_addfiled") == false) sub.SetTest_addfiled(item.Test_addfiled);
			if (ignore.ContainsKey("title") == false) sub.SetTitle(item.Title);
			if (ignore.ContainsKey("update_time") == false) sub.SetUpdate_time(item.Update_time);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<TopicInfo> _dataSource;
			protected Dictionary<string, TopicInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<TopicInfo> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`id` IN ({0})", _dataSource.Select(a => a.Id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Topic.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Topic.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Topic.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Topic.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetTopic_type_id(int? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Topic_type_id = value;
				return this.Set("`topic_type_id`", $"?topic_type_id_{_parameters.Count}", 
					GetParameter($"?topic_type_id_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
			public SqlUpdateBuild SetCarddata(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Carddata = value;
				return this.Set("`carddata`", $"?carddata_{_parameters.Count}", 
					GetParameter($"?carddata_{_parameters.Count}", MySqlDbType.Text, -1, value));
			}
			public SqlUpdateBuild SetCardtype(TopicCARDTYPE? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Cardtype = value;
				return this.Set("`cardtype`", $"?cardtype_{_parameters.Count}", 
					GetParameter($"?cardtype_{_parameters.Count}", MySqlDbType.Enum, -1, value?.ToInt64()));
			}
			public SqlUpdateBuild SetClicks(ulong? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Clicks = value;
				return this.Set("`clicks`", $"?clicks_{_parameters.Count}", 
					GetParameter($"?clicks_{_parameters.Count}", MySqlDbType.UInt64, 20, value));
			}
			public SqlUpdateBuild SetClicksIncrement(long value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Clicks = (ulong?)((long?)item.Clicks + value);
				return this.Set("`clicks`", $"ifnull(`clicks`, 0) + ?clicks_{_parameters.Count}", 
					GetParameter($"?clicks_{_parameters.Count}", MySqlDbType.Int64, 20, value));
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
			public SqlUpdateBuild SetOrder_time(DateTime? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Order_time = value;
				return this.Set("`order_time`", $"?order_time_{_parameters.Count}", 
					GetParameter($"?order_time_{_parameters.Count}", MySqlDbType.DateTime, -1, value));
			}
			public SqlUpdateBuild SetTest_addfiled(byte? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Test_addfiled = value;
				return this.Set("`test_addfiled`", $"?test_addfiled_{_parameters.Count}", 
					GetParameter($"?test_addfiled_{_parameters.Count}", MySqlDbType.Byte, 4, value));
			}
			public SqlUpdateBuild SetTest_addfiledIncrement(byte value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Test_addfiled += value;
				return this.Set("`test_addfiled`", $"ifnull(`test_addfiled`, 0) + ?test_addfiled_{_parameters.Count}", 
					GetParameter($"?test_addfiled_{_parameters.Count}", MySqlDbType.Byte, 4, value));
			}
			public SqlUpdateBuild SetTitle(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Title = value;
				return this.Set("`title`", $"?title_{_parameters.Count}", 
					GetParameter($"?title_{_parameters.Count}", MySqlDbType.VarChar, 255, value));
			}
			public SqlUpdateBuild SetUpdate_time(DateTime? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Update_time = value;
				return this.Set("`update_time`", $"?update_time_{_parameters.Count}", 
					GetParameter($"?update_time_{_parameters.Count}", MySqlDbType.DateTime, -1, value));
			}
		}
		#endregion

		public TopicInfo Insert(TopicInfo item) {
			if (uint.TryParse(string.Concat(SqlHelper.ExecuteScalar(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}

		#region async
		async public Task<TopicInfo> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as TopicInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			TopicInfo item = new TopicInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = (uint?)dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 10; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Topic_type_id = (int?)dr.GetInt32(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Carddata = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Cardtype = (TopicCARDTYPE?)dr.GetInt64(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Clicks = (ulong?)dr.GetInt64(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Content = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Create_time = (DateTime?)dr.GetDateTime(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Order_time = (DateTime?)dr.GetDateTime(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Test_addfiled = (byte?)dr.GetByte(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Title = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Update_time = (DateTime?)dr.GetDateTime(dataIndex);
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(uint Id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`id` = ?id"), 
				GetParameter("?id", MySqlDbType.UInt32, 10, Id));
		}
		public Task<int> DeleteByTopic_type_idAsync(int? Topic_type_id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`topic_type_id` = ?topic_type_id"), 
				GetParameter("?topic_type_id", MySqlDbType.Int32, 11, Topic_type_id));
		}
		async public Task<TopicInfo> InsertAsync(TopicInfo item) {
			if (uint.TryParse(string.Concat(await SqlHelper.ExecuteScalarAsync(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}
		#endregion
	}
}