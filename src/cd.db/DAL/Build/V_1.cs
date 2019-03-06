using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class V_1 : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`v_1`";
			internal static readonly string Field = "a.`id`";
			internal static readonly string Sort = "";
			internal static readonly string Returning = "";
			internal static readonly string Delete = "DELETE FROM `v_1` WHERE ";
			internal static readonly string InsertField = @"`id`";
			internal static readonly string InsertValues = @"?id";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `v_1`(" + InsertField + ") VALUES{0}";
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
		protected static MySqlParameter[] GetParameters(V_1Info item) {
			return new MySqlParameter[] {
				GetParameter("?id", MySqlDbType.UInt32, 10, item.Id)};
		}
		public V_1Info GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as V_1Info;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			V_1Info item = new V_1Info();
			if (!dr.IsDBNull(++dataIndex)) item.Id = (uint?)dr.GetInt32(dataIndex);
			return item;
		}
		private void CopyItemAllField(V_1Info item, V_1Info newitem) {
			item.Id = newitem.Id;
		}
		#endregion

		#region async
		async public Task<V_1Info> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as V_1Info;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			V_1Info item = new V_1Info();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = (uint?)dr.GetInt32(dataIndex);
			return (item, dataIndex);
		}
		#endregion
	}
}