using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Testtypeparentinfo23123 {

		protected static readonly cd.DAL.Testtypeparentinfo23123 dal = new cd.DAL.Testtypeparentinfo23123();
		protected static readonly int itemCacheTimeout;

		static Testtypeparentinfo23123() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Testtypeparentinfo23123"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}
		public static List<Testtypeparentinfo23123Info> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		public static Task<List<Testtypeparentinfo23123Info>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<Testtypeparentinfo23123Info, SelectBuild> {
			public SelectBuild WhereId(params int?[] Id) => this.Where1Or("a.`Id` = {0}", Id);
			public SelectBuild WhereName(params string[] Name) => this.Where1Or("a.`Name` = {0}", Name);
			public SelectBuild WhereNameLike(string pattern, bool isNotLike = false) => this.Where($@"a.`Name` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereTime2Range(DateTime? begin) => base.Where("a.`Time2` >= {0}", begin);
			public SelectBuild WhereTime2Range(DateTime? begin, DateTime? end) => end == null ? WhereTime2Range(begin) : base.Where("a.`Time2` between {0} and {1}", begin, end);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}