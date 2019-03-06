using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Xxdkdkdk1222 : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`xxdkdkdk1222`";
			internal static readonly string Field = "a.`Id22dd`, a.`Name`";
			internal static readonly string Sort = "a.`Id22dd`";
			internal static readonly string Returning = "; SELECT LAST_INSERT_ID();";
			internal static readonly string Delete = "DELETE FROM `xxdkdkdk1222` WHERE ";
			internal static readonly string InsertField = @"`Name`";
			internal static readonly string InsertValues = @"?Name";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `xxdkdkdk1222`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(Xxdkdkdk1222Info item) {
			return new MySqlParameter[] {
				GetParameter("?Id22dd", MySqlDbType.Int32, 11, item.Id22dd), 
				GetParameter("?Name", MySqlDbType.VarChar, 101, item.Name)};
		}
		public Xxdkdkdk1222Info GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as Xxdkdkdk1222Info;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			Xxdkdkdk1222Info item = new Xxdkdkdk1222Info();
			if (!dr.IsDBNull(++dataIndex)) item.Id22dd = (int?)dr.GetInt32(dataIndex); if (item.Id22dd == null) { dataIndex += 1; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Name = dr.GetString(dataIndex);
			return item;
		}
		private void CopyItemAllField(Xxdkdkdk1222Info item, Xxdkdkdk1222Info newitem) {
			item.Id22dd = newitem.Id22dd;
			item.Name = newitem.Name;
		}
		#endregion

		public int Delete(int Id22dd) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`Id22dd` = ?Id22dd"), 
				GetParameter("?Id22dd", MySqlDbType.Int32, 11, Id22dd));
		}

		public SqlUpdateBuild Update(Xxdkdkdk1222Info item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<Xxdkdkdk1222Info> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("Name") == false) sub.SetName(item.Name);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<Xxdkdkdk1222Info> _dataSource;
			protected Dictionary<string, Xxdkdkdk1222Info> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<Xxdkdkdk1222Info> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Id22dd}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`Id22dd` IN ({0})", _dataSource.Select(a => a.Id22dd).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Xxdkdkdk1222.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Xxdkdkdk1222.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Xxdkdkdk1222.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Xxdkdkdk1222.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetName(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.Name = value;
				return this.Set("`Name`", $"?Name_{_parameters.Count}", 
					GetParameter($"?Name_{_parameters.Count}", MySqlDbType.VarChar, 101, value));
			}
		}
		#endregion

		public Xxdkdkdk1222Info Insert(Xxdkdkdk1222Info item) {
			if (int.TryParse(string.Concat(SqlHelper.ExecuteScalar(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id22dd = loc1;
			return item;
		}

		#region async
		async public Task<Xxdkdkdk1222Info> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as Xxdkdkdk1222Info;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			Xxdkdkdk1222Info item = new Xxdkdkdk1222Info();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id22dd = (int?)dr.GetInt32(dataIndex); if (item.Id22dd == null) { dataIndex += 1; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Name = dr.GetString(dataIndex);
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(int Id22dd) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`Id22dd` = ?Id22dd"), 
				GetParameter("?Id22dd", MySqlDbType.Int32, 11, Id22dd));
		}
		async public Task<Xxdkdkdk1222Info> InsertAsync(Xxdkdkdk1222Info item) {
			if (int.TryParse(string.Concat(await SqlHelper.ExecuteScalarAsync(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id22dd = loc1;
			return item;
		}
		#endregion
	}
}