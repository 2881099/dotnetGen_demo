using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Topic2111sss : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`topic2111sss`";
			internal static readonly string Field = "a.`Id`, a.`Clicks`, a.`CreateTime`, a.`fusho`, a.`Title2`";
			internal static readonly string Sort = "a.`Id`";
			internal static readonly string Returning = "; SELECT LAST_INSERT_ID();";
			internal static readonly string Delete = "DELETE FROM `topic2111sss` WHERE ";
			internal static readonly string InsertField = @"`Clicks`, `CreateTime`, `fusho`, `Title2`";
			internal static readonly string InsertValues = @"?Clicks, ?CreateTime, ?fusho, ?Title2";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `topic2111sss`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(Topic2111sssInfo item) {
			return new MySqlParameter[] {
				GetParameter("?Id", MySqlDbType.UInt32, 10, item.Id), 
				GetParameter("?Clicks", MySqlDbType.Int32, 11, item.Clicks), 
				GetParameter("?CreateTime", MySqlDbType.DateTime, -1, item.CreateTime), 
				GetParameter("?fusho", MySqlDbType.UInt16, 5, item.Fusho), 
				GetParameter("?Title2", MySqlDbType.VarChar, 128, item.Title2)};
		}
		public Topic2111sssInfo GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as Topic2111sssInfo;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			Topic2111sssInfo item = new Topic2111sssInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Id = (uint?)dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 4; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Clicks = (int?)dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.CreateTime = (DateTime?)dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Fusho = (ushort?)dr.GetInt16(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Title2 = dr.GetString(dataIndex);
			return item;
		}
		private void CopyItemAllField(Topic2111sssInfo item, Topic2111sssInfo newitem) {
			item.Id = newitem.Id;
			item.Clicks = newitem.Clicks;
			item.CreateTime = newitem.CreateTime;
			item.Fusho = newitem.Fusho;
			item.Title2 = newitem.Title2;
		}
		#endregion

		public int Delete(uint Id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`Id` = ?Id"), 
				GetParameter("?Id", MySqlDbType.UInt32, 10, Id));
		}

		public SqlUpdateBuild Update(Topic2111sssInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<Topic2111sssInfo> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("Clicks") == false) sub.SetClicks(item.Clicks);
			if (ignore.ContainsKey("CreateTime") == false) sub.SetCreateTime(item.CreateTime);
			if (ignore.ContainsKey("fusho") == false) sub.SetFusho(item.Fusho);
			if (ignore.ContainsKey("Title2") == false) sub.SetTitle2(item.Title2);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<Topic2111sssInfo> _dataSource;
			protected Dictionary<string, Topic2111sssInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<Topic2111sssInfo> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`Id` IN ({0})", _dataSource.Select(a => a.Id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Topic2111sss.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Topic2111sss.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Topic2111sss.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Topic2111sss.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetClicks(int? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Clicks = value;
				return this.Set("`Clicks`", $"?Clicks_{_parameters.Count}", 
					GetParameter($"?Clicks_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
			public SqlUpdateBuild SetClicksIncrement(int value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Clicks += value;
				return this.Set("`Clicks`", $"ifnull(`Clicks`, 0) + ?Clicks_{_parameters.Count}", 
					GetParameter($"?Clicks_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
			public SqlUpdateBuild SetCreateTime(DateTime? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.CreateTime = value;
				return this.Set("`CreateTime`", $"?CreateTime_{_parameters.Count}", 
					GetParameter($"?CreateTime_{_parameters.Count}", MySqlDbType.DateTime, -1, value));
			}
			public SqlUpdateBuild SetFusho(ushort? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Fusho = value;
				return this.Set("`fusho`", $"?fusho_{_parameters.Count}", 
					GetParameter($"?fusho_{_parameters.Count}", MySqlDbType.UInt16, 5, value));
			}
			public SqlUpdateBuild SetFushoIncrement(short value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Fusho = (ushort?)((short?)item.Fusho + value);
				return this.Set("`fusho`", $"ifnull(`fusho`, 0) + ?fusho_{_parameters.Count}", 
					GetParameter($"?fusho_{_parameters.Count}", MySqlDbType.Int16, 5, value));
			}
			public SqlUpdateBuild SetTitle2(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Title2 = value;
				return this.Set("`Title2`", $"?Title2_{_parameters.Count}", 
					GetParameter($"?Title2_{_parameters.Count}", MySqlDbType.VarChar, 128, value));
			}
		}
		#endregion

		public Topic2111sssInfo Insert(Topic2111sssInfo item) {
			if (uint.TryParse(string.Concat(SqlHelper.ExecuteScalar(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}

		#region async
		async public Task<Topic2111sssInfo> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as Topic2111sssInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			Topic2111sssInfo item = new Topic2111sssInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = (uint?)dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 4; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Clicks = (int?)dr.GetInt32(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.CreateTime = (DateTime?)dr.GetDateTime(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Fusho = (ushort?)dr.GetInt16(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Title2 = dr.GetString(dataIndex);
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(uint Id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`Id` = ?Id"), 
				GetParameter("?Id", MySqlDbType.UInt32, 10, Id));
		}
		async public Task<Topic2111sssInfo> InsertAsync(Topic2111sssInfo item) {
			if (uint.TryParse(string.Concat(await SqlHelper.ExecuteScalarAsync(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}
		#endregion
	}
}