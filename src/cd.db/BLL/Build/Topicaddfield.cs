using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Topicaddfield {

		protected static readonly cd.DAL.Topicaddfield dal = new cd.DAL.Topicaddfield();
		protected static readonly int itemCacheTimeout;

		static Topicaddfield() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Topicaddfield"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int Id) {
			var affrows = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(new TopicaddfieldInfo { Id = Id });
			return affrows;
		}

		#region enum _
		public enum _ {
			Id = 1, 
			Name, 
			Title222, 
			Xxxx
		}
		#endregion

		public static int Update(TopicaddfieldInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(TopicaddfieldInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Topicaddfield.SqlUpdateBuild UpdateDiy(int Id) => new cd.DAL.Topicaddfield.SqlUpdateBuild(new List<TopicaddfieldInfo> { new TopicaddfieldInfo { Id = Id } });
		public static cd.DAL.Topicaddfield.SqlUpdateBuild UpdateDiy(List<TopicaddfieldInfo> dataSource) => new cd.DAL.Topicaddfield.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Topicaddfield.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Topicaddfield.SqlUpdateBuild();

		public static TopicaddfieldInfo Insert(string Name, string Title222, string Xxxx) {
			return Insert(new TopicaddfieldInfo {
				Name = Name, 
				Title222 = Title222, 
				Xxxx = Xxxx});
		}
		public static TopicaddfieldInfo Insert(TopicaddfieldInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(TopicaddfieldInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<TopicaddfieldInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Topicaddfield:", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static TopicaddfieldInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("cd_BLL:Topicaddfield:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TopicaddfieldInfo.Parse(str));

		public static List<TopicaddfieldInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(int Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new TopicaddfieldInfo { Id = Id });
			return affrows;
		}
		async public static Task<TopicaddfieldInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Topicaddfield:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TopicaddfieldInfo.Parse(str));
		public static Task<int> UpdateAsync(TopicaddfieldInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(TopicaddfieldInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<TopicaddfieldInfo> InsertAsync(string Name, string Title222, string Xxxx) {
			return InsertAsync(new TopicaddfieldInfo {
				Name = Name, 
				Title222 = Title222, 
				Xxxx = Xxxx});
		}
		async public static Task<TopicaddfieldInfo> InsertAsync(TopicaddfieldInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		internal static Task RemoveCacheAsync(TopicaddfieldInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<TopicaddfieldInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Topicaddfield:", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<TopicaddfieldInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<TopicaddfieldInfo, SelectBuild> {
			public SelectBuild WhereId(params int[] Id) => this.Where1Or("a.`Id` = {0}", Id);
			public SelectBuild WhereName(params string[] Name) => this.Where1Or("a.`name` = {0}", Name);
			public SelectBuild WhereNameLike(string pattern, bool isNotLike = false) => this.Where($@"a.`name` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereTitle222(params string[] Title222) => this.Where1Or("a.`title222` = {0}", Title222);
			public SelectBuild WhereTitle222Like(string pattern, bool isNotLike = false) => this.Where($@"a.`title222` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereXxxx(params string[] Xxxx) => this.Where1Or("a.`xxxx` = {0}", Xxxx);
			public SelectBuild WhereXxxxLike(string pattern, bool isNotLike = false) => this.Where($@"a.`xxxx` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}