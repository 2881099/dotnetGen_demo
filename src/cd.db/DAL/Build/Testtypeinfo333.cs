using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Testtypeinfo333 : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`testtypeinfo333`";
			internal static readonly string Field = "a.`Guid`, a.`Name`, a.`ParentId`, a.`Time`";
			internal static readonly string Sort = "";
			internal static readonly string Returning = "";
			internal static readonly string Delete = "DELETE FROM `testtypeinfo333` WHERE ";
			internal static readonly string InsertField = @"`Guid`, `Name`, `ParentId`, `Time`";
			internal static readonly string InsertValues = @"?Guid, ?Name, ?ParentId, ?Time";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `testtypeinfo333`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(Testtypeinfo333Info item) {
			return new MySqlParameter[] {
				GetParameter("?Guid", MySqlDbType.Int32, 11, item.Guid), 
				GetParameter("?Name", MySqlDbType.VarChar, 255, item.Name), 
				GetParameter("?ParentId", MySqlDbType.Int32, 11, item.ParentId), 
				GetParameter("?Time", MySqlDbType.DateTime, -1, item.Time)};
		}
		public Testtypeinfo333Info GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as Testtypeinfo333Info;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			Testtypeinfo333Info item = new Testtypeinfo333Info();
			if (!dr.IsDBNull(++dataIndex)) item.Guid = (int?)dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Name = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.ParentId = (int?)dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Time = (DateTime?)dr.GetDateTime(dataIndex);
			return item;
		}
		private void CopyItemAllField(Testtypeinfo333Info item, Testtypeinfo333Info newitem) {
			item.Guid = newitem.Guid;
			item.Name = newitem.Name;
			item.ParentId = newitem.ParentId;
			item.Time = newitem.Time;
		}
		#endregion

		#region async
		async public Task<Testtypeinfo333Info> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as Testtypeinfo333Info;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			Testtypeinfo333Info item = new Testtypeinfo333Info();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Guid = (int?)dr.GetInt32(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Name = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.ParentId = (int?)dr.GetInt32(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Time = (DateTime?)dr.GetDateTime(dataIndex);
			return (item, dataIndex);
		}
		#endregion
	}
}