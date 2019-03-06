using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Testtypeparentinfo23123 : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`testtypeparentinfo23123`";
			internal static readonly string Field = "a.`Id`, a.`Name`, a.`Time2`";
			internal static readonly string Sort = "";
			internal static readonly string Returning = "";
			internal static readonly string Delete = "DELETE FROM `testtypeparentinfo23123` WHERE ";
			internal static readonly string InsertField = @"`Id`, `Name`, `Time2`";
			internal static readonly string InsertValues = @"?Id, ?Name, ?Time2";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `testtypeparentinfo23123`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(Testtypeparentinfo23123Info item) {
			return new MySqlParameter[] {
				GetParameter("?Id", MySqlDbType.Int32, 11, item.Id), 
				GetParameter("?Name", MySqlDbType.VarChar, 255, item.Name), 
				GetParameter("?Time2", MySqlDbType.DateTime, -1, item.Time2)};
		}
		public Testtypeparentinfo23123Info GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as Testtypeparentinfo23123Info;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			Testtypeparentinfo23123Info item = new Testtypeparentinfo23123Info();
			if (!dr.IsDBNull(++dataIndex)) item.Id = (int?)dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Name = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Time2 = (DateTime?)dr.GetDateTime(dataIndex);
			return item;
		}
		private void CopyItemAllField(Testtypeparentinfo23123Info item, Testtypeparentinfo23123Info newitem) {
			item.Id = newitem.Id;
			item.Name = newitem.Name;
			item.Time2 = newitem.Time2;
		}
		#endregion

		#region async
		async public Task<Testtypeparentinfo23123Info> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as Testtypeparentinfo23123Info;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			Testtypeparentinfo23123Info item = new Testtypeparentinfo23123Info();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = (int?)dr.GetInt32(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Name = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Time2 = (DateTime?)dr.GetDateTime(dataIndex);
			return (item, dataIndex);
		}
		#endregion
	}
}