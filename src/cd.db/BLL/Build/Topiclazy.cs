using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Topiclazy {

		protected static readonly cd.DAL.Topiclazy dal = new cd.DAL.Topiclazy();
		protected static readonly int itemCacheTimeout;

		static Topiclazy() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Topiclazy"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int Id) {
			var affrows = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(new TopiclazyInfo { Id = Id });
			return affrows;
		}

		#region enum _
		public enum _ {
			Id = 1, 
			Clicks, 
			CreateTime, 
			TestTypeInfoGuid, 
			Title
		}
		#endregion

		public static int Update(TopiclazyInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(TopiclazyInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Topiclazy.SqlUpdateBuild UpdateDiy(int Id) => new cd.DAL.Topiclazy.SqlUpdateBuild(new List<TopiclazyInfo> { new TopiclazyInfo { Id = Id } });
		public static cd.DAL.Topiclazy.SqlUpdateBuild UpdateDiy(List<TopiclazyInfo> dataSource) => new cd.DAL.Topiclazy.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Topiclazy.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Topiclazy.SqlUpdateBuild();

		public static TopiclazyInfo Insert(int? Clicks, DateTime? CreateTime, int? TestTypeInfoGuid, string Title) {
			return Insert(new TopiclazyInfo {
				Clicks = Clicks, 
				CreateTime = CreateTime, 
				TestTypeInfoGuid = TestTypeInfoGuid, 
				Title = Title});
		}
		public static TopiclazyInfo Insert(TopiclazyInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(TopiclazyInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<TopiclazyInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Topiclazy:", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static TopiclazyInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("cd_BLL:Topiclazy:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TopiclazyInfo.Parse(str));

		public static List<TopiclazyInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(int Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new TopiclazyInfo { Id = Id });
			return affrows;
		}
		async public static Task<TopiclazyInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Topiclazy:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TopiclazyInfo.Parse(str));
		public static Task<int> UpdateAsync(TopiclazyInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(TopiclazyInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<TopiclazyInfo> InsertAsync(int? Clicks, DateTime? CreateTime, int? TestTypeInfoGuid, string Title) {
			return InsertAsync(new TopiclazyInfo {
				Clicks = Clicks, 
				CreateTime = CreateTime, 
				TestTypeInfoGuid = TestTypeInfoGuid, 
				Title = Title});
		}
		async public static Task<TopiclazyInfo> InsertAsync(TopiclazyInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		internal static Task RemoveCacheAsync(TopiclazyInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<TopiclazyInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Topiclazy:", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<TopiclazyInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<TopiclazyInfo, SelectBuild> {
			public SelectBuild WhereId(params int[] Id) => this.Where1Or("a.`Id` = {0}", Id);
			public SelectBuild WhereClicks(params int?[] Clicks) => this.Where1Or("a.`Clicks` = {0}", Clicks);
			public SelectBuild WhereCreateTimeRange(DateTime? begin) => base.Where("a.`CreateTime` >= {0}", begin);
			public SelectBuild WhereCreateTimeRange(DateTime? begin, DateTime? end) => end == null ? WhereCreateTimeRange(begin) : base.Where("a.`CreateTime` between {0} and {1}", begin, end);
			public SelectBuild WhereTestTypeInfoGuid(params int?[] TestTypeInfoGuid) => this.Where1Or("a.`TestTypeInfoGuid` = {0}", TestTypeInfoGuid);
			public SelectBuild WhereTitle(params string[] Title) => this.Where1Or("a.`Title` = {0}", Title);
			public SelectBuild WhereTitleLike(string pattern, bool isNotLike = false) => this.Where($@"a.`Title` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}