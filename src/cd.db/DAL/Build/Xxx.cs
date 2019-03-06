using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Xxx : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`xxx`";
			internal static readonly string Field = "a.`Id`, a.`CreateTime`, a.`Title`, a.`TypeGuid`";
			internal static readonly string Sort = "a.`Id`";
			internal static readonly string Returning = "; SELECT LAST_INSERT_ID();";
			internal static readonly string Delete = "DELETE FROM `xxx` WHERE ";
			internal static readonly string InsertField = @"`CreateTime`, `Title`, `TypeGuid`";
			internal static readonly string InsertValues = @"?CreateTime, ?Title, ?TypeGuid";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `xxx`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(XxxInfo item) {
			return new MySqlParameter[] {
				GetParameter("?Id", MySqlDbType.Int32, 11, item.Id), 
				GetParameter("?CreateTime", MySqlDbType.DateTime, -1, item.CreateTime), 
				GetParameter("?Title", MySqlDbType.VarChar, 255, item.Title), 
				GetParameter("?TypeGuid", MySqlDbType.Int32, 11, item.TypeGuid)};
		}
		public XxxInfo GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as XxxInfo;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			XxxInfo item = new XxxInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Id = (int?)dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 3; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.CreateTime = (DateTime?)dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Title = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TypeGuid = (int?)dr.GetInt32(dataIndex);
			return item;
		}
		private void CopyItemAllField(XxxInfo item, XxxInfo newitem) {
			item.Id = newitem.Id;
			item.CreateTime = newitem.CreateTime;
			item.Title = newitem.Title;
			item.TypeGuid = newitem.TypeGuid;
		}
		#endregion

		public int Delete(int Id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`Id` = ?Id"), 
				GetParameter("?Id", MySqlDbType.Int32, 11, Id));
		}

		public SqlUpdateBuild Update(XxxInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<XxxInfo> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("CreateTime") == false) sub.SetCreateTime(item.CreateTime);
			if (ignore.ContainsKey("Title") == false) sub.SetTitle(item.Title);
			if (ignore.ContainsKey("TypeGuid") == false) sub.SetTypeGuid(item.TypeGuid);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<XxxInfo> _dataSource;
			protected Dictionary<string, XxxInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<XxxInfo> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`Id` IN ({0})", _dataSource.Select(a => a.Id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Xxx.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Xxx.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Xxx.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Xxx.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetCreateTime(DateTime? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.CreateTime = value;
				return this.Set("`CreateTime`", $"?CreateTime_{_parameters.Count}", 
					GetParameter($"?CreateTime_{_parameters.Count}", MySqlDbType.DateTime, -1, value));
			}
			public SqlUpdateBuild SetTitle(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Title = value;
				return this.Set("`Title`", $"?Title_{_parameters.Count}", 
					GetParameter($"?Title_{_parameters.Count}", MySqlDbType.VarChar, 255, value));
			}
			public SqlUpdateBuild SetTypeGuid(int? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TypeGuid = value;
				return this.Set("`TypeGuid`", $"?TypeGuid_{_parameters.Count}", 
					GetParameter($"?TypeGuid_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
			public SqlUpdateBuild SetTypeGuidIncrement(int value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TypeGuid += value;
				return this.Set("`TypeGuid`", $"ifnull(`TypeGuid`, 0) + ?TypeGuid_{_parameters.Count}", 
					GetParameter($"?TypeGuid_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
		}
		#endregion

		public XxxInfo Insert(XxxInfo item) {
			if (int.TryParse(string.Concat(SqlHelper.ExecuteScalar(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}

		#region async
		async public Task<XxxInfo> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as XxxInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			XxxInfo item = new XxxInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = (int?)dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 3; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.CreateTime = (DateTime?)dr.GetDateTime(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Title = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TypeGuid = (int?)dr.GetInt32(dataIndex);
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(int Id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`Id` = ?Id"), 
				GetParameter("?Id", MySqlDbType.Int32, 11, Id));
		}
		async public Task<XxxInfo> InsertAsync(XxxInfo item) {
			if (int.TryParse(string.Concat(await SqlHelper.ExecuteScalarAsync(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}
		#endregion
	}
}