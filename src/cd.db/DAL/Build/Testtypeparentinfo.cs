using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Testtypeparentinfo : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`testtypeparentinfo`";
			internal static readonly string Field = "a.`Id`, a.`Name`";
			internal static readonly string Sort = "a.`Id`";
			internal static readonly string Returning = "; SELECT LAST_INSERT_ID();";
			internal static readonly string Delete = "DELETE FROM `testtypeparentinfo` WHERE ";
			internal static readonly string InsertField = @"`Name`";
			internal static readonly string InsertValues = @"?Name";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `testtypeparentinfo`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(TesttypeparentinfoInfo item) {
			return new MySqlParameter[] {
				GetParameter("?Id", MySqlDbType.Int32, 11, item.Id), 
				GetParameter("?Name", MySqlDbType.VarChar, 255, item.Name)};
		}
		public TesttypeparentinfoInfo GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as TesttypeparentinfoInfo;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			TesttypeparentinfoInfo item = new TesttypeparentinfoInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Id = (int?)dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 1; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Name = dr.GetString(dataIndex);
			return item;
		}
		private void CopyItemAllField(TesttypeparentinfoInfo item, TesttypeparentinfoInfo newitem) {
			item.Id = newitem.Id;
			item.Name = newitem.Name;
		}
		#endregion

		public int Delete(int Id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`Id` = ?Id"), 
				GetParameter("?Id", MySqlDbType.Int32, 11, Id));
		}

		public SqlUpdateBuild Update(TesttypeparentinfoInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<TesttypeparentinfoInfo> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("Name") == false) sub.SetName(item.Name);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<TesttypeparentinfoInfo> _dataSource;
			protected Dictionary<string, TesttypeparentinfoInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<TesttypeparentinfoInfo> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`Id` IN ({0})", _dataSource.Select(a => a.Id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Testtypeparentinfo.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Testtypeparentinfo.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Testtypeparentinfo.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Testtypeparentinfo.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetName(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Name = value;
				return this.Set("`Name`", $"?Name_{_parameters.Count}", 
					GetParameter($"?Name_{_parameters.Count}", MySqlDbType.VarChar, 255, value));
			}
		}
		#endregion

		public TesttypeparentinfoInfo Insert(TesttypeparentinfoInfo item) {
			if (int.TryParse(string.Concat(SqlHelper.ExecuteScalar(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}

		#region async
		async public Task<TesttypeparentinfoInfo> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as TesttypeparentinfoInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			TesttypeparentinfoInfo item = new TesttypeparentinfoInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = (int?)dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 1; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Name = dr.GetString(dataIndex);
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(int Id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`Id` = ?Id"), 
				GetParameter("?Id", MySqlDbType.Int32, 11, Id));
		}
		async public Task<TesttypeparentinfoInfo> InsertAsync(TesttypeparentinfoInfo item) {
			if (int.TryParse(string.Concat(await SqlHelper.ExecuteScalarAsync(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}
		#endregion
	}
}