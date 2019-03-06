using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Xxdkdkdk1 : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`xxdkdkdk1`";
			internal static readonly string Field = "a.`Id22`, a.`Id`, a.`name`";
			internal static readonly string Sort = "a.`Id22`";
			internal static readonly string Returning = "; SELECT LAST_INSERT_ID();";
			internal static readonly string Delete = "DELETE FROM `xxdkdkdk1` WHERE ";
			internal static readonly string InsertField = @"`Id`, `name`";
			internal static readonly string InsertValues = @"?Id, ?name";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `xxdkdkdk1`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(Xxdkdkdk1Info item) {
			return new MySqlParameter[] {
				GetParameter("?Id22", MySqlDbType.Int32, 11, item.Id22), 
				GetParameter("?Id", MySqlDbType.Int32, 11, item.Id), 
				GetParameter("?name", MySqlDbType.VarChar, 255, item.Name)};
		}
		public Xxdkdkdk1Info GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as Xxdkdkdk1Info;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			Xxdkdkdk1Info item = new Xxdkdkdk1Info();
			if (!dr.IsDBNull(++dataIndex)) item.Id22 = (int?)dr.GetInt32(dataIndex); if (item.Id22 == null) { dataIndex += 2; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Id = (int?)dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Name = dr.GetString(dataIndex);
			return item;
		}
		private void CopyItemAllField(Xxdkdkdk1Info item, Xxdkdkdk1Info newitem) {
			item.Id22 = newitem.Id22;
			item.Id = newitem.Id;
			item.Name = newitem.Name;
		}
		#endregion

		public int Delete(int Id22) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`Id22` = ?Id22"), 
				GetParameter("?Id22", MySqlDbType.Int32, 11, Id22));
		}

		public SqlUpdateBuild Update(Xxdkdkdk1Info item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<Xxdkdkdk1Info> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("Id") == false) sub.SetId(item.Id);
			if (ignore.ContainsKey("name") == false) sub.SetName(item.Name);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<Xxdkdkdk1Info> _dataSource;
			protected Dictionary<string, Xxdkdkdk1Info> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<Xxdkdkdk1Info> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Id22}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`Id22` IN ({0})", _dataSource.Select(a => a.Id22).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Xxdkdkdk1.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Xxdkdkdk1.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Xxdkdkdk1.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Xxdkdkdk1.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetId(int? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Id = value;
				return this.Set("`Id`", $"?Id_{_parameters.Count}", 
					GetParameter($"?Id_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
			public SqlUpdateBuild SetIdIncrement(int value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Id += value;
				return this.Set("`Id`", $"ifnull(`Id`, 0) + ?Id_{_parameters.Count}", 
					GetParameter($"?Id_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
			public SqlUpdateBuild SetName(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Name = value;
				return this.Set("`name`", $"?name_{_parameters.Count}", 
					GetParameter($"?name_{_parameters.Count}", MySqlDbType.VarChar, 255, value));
			}
		}
		#endregion

		public Xxdkdkdk1Info Insert(Xxdkdkdk1Info item) {
			if (int.TryParse(string.Concat(SqlHelper.ExecuteScalar(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id22 = loc1;
			return item;
		}

		#region async
		async public Task<Xxdkdkdk1Info> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as Xxdkdkdk1Info;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			Xxdkdkdk1Info item = new Xxdkdkdk1Info();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id22 = (int?)dr.GetInt32(dataIndex); if (item.Id22 == null) { dataIndex += 2; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = (int?)dr.GetInt32(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Name = dr.GetString(dataIndex);
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(int Id22) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`Id22` = ?Id22"), 
				GetParameter("?Id22", MySqlDbType.Int32, 11, Id22));
		}
		async public Task<Xxdkdkdk1Info> InsertAsync(Xxdkdkdk1Info item) {
			if (int.TryParse(string.Concat(await SqlHelper.ExecuteScalarAsync(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id22 = loc1;
			return item;
		}
		#endregion
	}
}