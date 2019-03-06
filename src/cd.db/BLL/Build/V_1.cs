using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class V_1 {

		protected static readonly cd.DAL.V_1 dal = new cd.DAL.V_1();
		protected static readonly int itemCacheTimeout;

		static V_1() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_V_1"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}
		public static List<V_1Info> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		public static Task<List<V_1Info>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<V_1Info, SelectBuild> {
			public SelectBuild WhereId(params uint?[] Id) => this.Where1Or("a.`id` = {0}", Id);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}