using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Testtypeinfo333 {

		protected static readonly cd.DAL.Testtypeinfo333 dal = new cd.DAL.Testtypeinfo333();
		protected static readonly int itemCacheTimeout;

		static Testtypeinfo333() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Testtypeinfo333"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}
		public static List<Testtypeinfo333Info> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		public static Task<List<Testtypeinfo333Info>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<Testtypeinfo333Info, SelectBuild> {
			public SelectBuild WhereGuid(params int?[] Guid) => this.Where1Or("a.`Guid` = {0}", Guid);
			public SelectBuild WhereName(params string[] Name) => this.Where1Or("a.`Name` = {0}", Name);
			public SelectBuild WhereNameLike(string pattern, bool isNotLike = false) => this.Where($@"a.`Name` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereParentId(params int?[] ParentId) => this.Where1Or("a.`ParentId` = {0}", ParentId);
			public SelectBuild WhereTimeRange(DateTime? begin) => base.Where("a.`Time` >= {0}", begin);
			public SelectBuild WhereTimeRange(DateTime? begin, DateTime? end) => end == null ? WhereTimeRange(begin) : base.Where("a.`Time` between {0} and {1}", begin, end);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}