using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Topic2111sss {

		protected static readonly cd.DAL.Topic2111sss dal = new cd.DAL.Topic2111sss();
		protected static readonly int itemCacheTimeout;

		static Topic2111sss() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Topic2111sss"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(uint Id) {
			var affrows = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(new Topic2111sssInfo { Id = Id });
			return affrows;
		}

		#region enum _
		public enum _ {
			Id = 1, 
			Clicks, 
			CreateTime, 
			Fusho, 
			Title2
		}
		#endregion

		public static int Update(Topic2111sssInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(Topic2111sssInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Topic2111sss.SqlUpdateBuild UpdateDiy(uint Id) => new cd.DAL.Topic2111sss.SqlUpdateBuild(new List<Topic2111sssInfo> { new Topic2111sssInfo { Id = Id } });
		public static cd.DAL.Topic2111sss.SqlUpdateBuild UpdateDiy(List<Topic2111sssInfo> dataSource) => new cd.DAL.Topic2111sss.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Topic2111sss.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Topic2111sss.SqlUpdateBuild();

		public static Topic2111sssInfo Insert(int? Clicks, DateTime? CreateTime, ushort? Fusho, string Title2) {
			return Insert(new Topic2111sssInfo {
				Clicks = Clicks, 
				CreateTime = CreateTime, 
				Fusho = Fusho, 
				Title2 = Title2});
		}
		public static Topic2111sssInfo Insert(Topic2111sssInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(Topic2111sssInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<Topic2111sssInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Topic2111sss:", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static Topic2111sssInfo GetItem(uint Id) => SqlHelper.CacheShell(string.Concat("cd_BLL:Topic2111sss:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Topic2111sssInfo.Parse(str));

		public static List<Topic2111sssInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(uint Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new Topic2111sssInfo { Id = Id });
			return affrows;
		}
		async public static Task<Topic2111sssInfo> GetItemAsync(uint Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Topic2111sss:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Topic2111sssInfo.Parse(str));
		public static Task<int> UpdateAsync(Topic2111sssInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(Topic2111sssInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<Topic2111sssInfo> InsertAsync(int? Clicks, DateTime? CreateTime, ushort? Fusho, string Title2) {
			return InsertAsync(new Topic2111sssInfo {
				Clicks = Clicks, 
				CreateTime = CreateTime, 
				Fusho = Fusho, 
				Title2 = Title2});
		}
		async public static Task<Topic2111sssInfo> InsertAsync(Topic2111sssInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		internal static Task RemoveCacheAsync(Topic2111sssInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<Topic2111sssInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Topic2111sss:", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<Topic2111sssInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<Topic2111sssInfo, SelectBuild> {
			public SelectBuild WhereId(params uint[] Id) => this.Where1Or("a.`Id` = {0}", Id);
			public SelectBuild WhereClicks(params int?[] Clicks) => this.Where1Or("a.`Clicks` = {0}", Clicks);
			public SelectBuild WhereCreateTimeRange(DateTime? begin) => base.Where("a.`CreateTime` >= {0}", begin);
			public SelectBuild WhereCreateTimeRange(DateTime? begin, DateTime? end) => end == null ? WhereCreateTimeRange(begin) : base.Where("a.`CreateTime` between {0} and {1}", begin, end);
			public SelectBuild WhereFusho(params ushort?[] Fusho) => this.Where1Or("a.`fusho` = {0}", Fusho);
			public SelectBuild WhereTitle2(params string[] Title2) => this.Where1Or("a.`Title2` = {0}", Title2);
			public SelectBuild WhereTitle2Like(string pattern, bool isNotLike = false) => this.Where($@"a.`Title2` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}