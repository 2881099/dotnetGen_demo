using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Testtypeinfo : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`testtypeinfo`";
			internal static readonly string Field = "a.`Guid`, a.`Name`, a.`ParentId`, a.`SelfGuid`";
			internal static readonly string Sort = "a.`Guid`";
			internal static readonly string Returning = "";
			internal static readonly string Delete = "DELETE FROM `testtypeinfo` WHERE ";
			internal static readonly string InsertField = @"`Guid`, `Name`, `ParentId`, `SelfGuid`";
			internal static readonly string InsertValues = @"?Guid, ?Name, ?ParentId, ?SelfGuid";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `testtypeinfo`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(TesttypeinfoInfo item) {
			return new MySqlParameter[] {
				GetParameter("?Guid", MySqlDbType.Int32, 11, item.Guid), 
				GetParameter("?Name", MySqlDbType.VarChar, 255, item.Name), 
				GetParameter("?ParentId", MySqlDbType.Int32, 11, item.ParentId), 
				GetParameter("?SelfGuid", MySqlDbType.Int32, 11, item.SelfGuid)};
		}
		public TesttypeinfoInfo GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as TesttypeinfoInfo;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			TesttypeinfoInfo item = new TesttypeinfoInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Guid = (int?)dr.GetInt32(dataIndex); if (item.Guid == null) { dataIndex += 3; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Name = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.ParentId = (int?)dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.SelfGuid = (int?)dr.GetInt32(dataIndex);
			return item;
		}
		private void CopyItemAllField(TesttypeinfoInfo item, TesttypeinfoInfo newitem) {
			item.Guid = newitem.Guid;
			item.Name = newitem.Name;
			item.ParentId = newitem.ParentId;
			item.SelfGuid = newitem.SelfGuid;
		}
		#endregion

		public int Delete(int Guid) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`Guid` = ?Guid"), 
				GetParameter("?Guid", MySqlDbType.Int32, 11, Guid));
		}

		public SqlUpdateBuild Update(TesttypeinfoInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<TesttypeinfoInfo> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("Name") == false) sub.SetName(item.Name);
			if (ignore.ContainsKey("ParentId") == false) sub.SetParentId(item.ParentId);
			if (ignore.ContainsKey("SelfGuid") == false) sub.SetSelfGuid(item.SelfGuid);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<TesttypeinfoInfo> _dataSource;
			protected Dictionary<string, TesttypeinfoInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<TesttypeinfoInfo> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Guid}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`Guid` IN ({0})", _dataSource.Select(a => a.Guid).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Testtypeinfo.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Testtypeinfo.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Testtypeinfo.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Testtypeinfo.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetName(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Name = value;
				return this.Set("`Name`", $"?Name_{_parameters.Count}", 
					GetParameter($"?Name_{_parameters.Count}", MySqlDbType.VarChar, 255, value));
			}
			public SqlUpdateBuild SetParentId(int? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.ParentId = value;
				return this.Set("`ParentId`", $"?ParentId_{_parameters.Count}", 
					GetParameter($"?ParentId_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
			public SqlUpdateBuild SetParentIdIncrement(int value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.ParentId += value;
				return this.Set("`ParentId`", $"ifnull(`ParentId`, 0) + ?ParentId_{_parameters.Count}", 
					GetParameter($"?ParentId_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
			public SqlUpdateBuild SetSelfGuid(int? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.SelfGuid = value;
				return this.Set("`SelfGuid`", $"?SelfGuid_{_parameters.Count}", 
					GetParameter($"?SelfGuid_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
			public SqlUpdateBuild SetSelfGuidIncrement(int value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.SelfGuid += value;
				return this.Set("`SelfGuid`", $"ifnull(`SelfGuid`, 0) + ?SelfGuid_{_parameters.Count}", 
					GetParameter($"?SelfGuid_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
		}
		#endregion

		public TesttypeinfoInfo Insert(TesttypeinfoInfo item) {
			SqlHelper.ExecuteNonQuery(TSQL.Insert, GetParameters(item));
			return item;
		}
		public int Insert(IEnumerable<TesttypeinfoInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return SqlHelper.ExecuteNonQuery(mp.sql, mp.parms);
		}
		public (string sql, MySqlParameter[] parms) InsertMakeParam(IEnumerable<TesttypeinfoInfo> items) {
			var itemsArr = items?.Where(a => a != null).ToArray();
			if (itemsArr == null || itemsArr.Any() == false) return (null, null);
			var values = "";
			var parms = new MySqlParameter[itemsArr.Length * 4];
			for (var a = 0; a < itemsArr.Length; a++) {
				var item = itemsArr[a];
				values += $",({TSQL.InsertValues.Replace(", ", a + ", ")}{a})";
				var tmparms = GetParameters(item);
				for (var b = 0; b < tmparms.Length; b++) {
					tmparms[b].ParameterName += a;
					parms[a * 4 + b] = tmparms[b];
				}
			}
			return (string.Format(TSQL.InsertMultiFormat, values.Substring(1)), parms);
		}

		#region async
		async public Task<TesttypeinfoInfo> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as TesttypeinfoInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			TesttypeinfoInfo item = new TesttypeinfoInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Guid = (int?)dr.GetInt32(dataIndex); if (item.Guid == null) { dataIndex += 3; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Name = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.ParentId = (int?)dr.GetInt32(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.SelfGuid = (int?)dr.GetInt32(dataIndex);
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(int Guid) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`Guid` = ?Guid"), 
				GetParameter("?Guid", MySqlDbType.Int32, 11, Guid));
		}
		async public Task<TesttypeinfoInfo> InsertAsync(TesttypeinfoInfo item) {
			await SqlHelper.ExecuteNonQueryAsync(TSQL.Insert, GetParameters(item));
			return item;
		}
		async public Task<int> InsertAsync(IEnumerable<TesttypeinfoInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return 0;
			return await SqlHelper.ExecuteNonQueryAsync(mp.sql, mp.parms);
		}
		#endregion
	}
}