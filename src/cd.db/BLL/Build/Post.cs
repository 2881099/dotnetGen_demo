using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Post {

		protected static readonly cd.DAL.Post dal = new cd.DAL.Post();
		protected static readonly int itemCacheTimeout;

		static Post() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Post"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int Id) {
			var affrows = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(new PostInfo { Id = Id });
			return affrows;
		}
		public static int DeleteByTopic_id(uint? Topic_id) {
			return dal.DeleteByTopic_id(Topic_id);
		}

		public static int Update(PostInfo item) => dal.Update(item).ExecuteNonQuery();
		public static cd.DAL.Post.SqlUpdateBuild UpdateDiy(int Id) => new cd.DAL.Post.SqlUpdateBuild(new List<PostInfo> { new PostInfo { Id = Id } });
		public static cd.DAL.Post.SqlUpdateBuild UpdateDiy(List<PostInfo> dataSource) => new cd.DAL.Post.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Post.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Post.SqlUpdateBuild();

		public static PostInfo Insert(uint? Topic_id, string Content) {
			return Insert(new PostInfo {
				Topic_id = Topic_id, 
				Content = Content});
		}
		public static PostInfo Insert(PostInfo item) {
			if (item.Create_time == null) item.Create_time = DateTime.Now;
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(PostInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<PostInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Post_", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static PostInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("cd_BLL_Post_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : PostInfo.Parse(str));

		public static List<PostInfo> GetItems() => Select.ToList();
		public static PostSelectBuild Select => new PostSelectBuild(dal);
		public static PostSelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static List<PostInfo> GetItemsByTopic_id(params uint?[] Topic_id) => Select.WhereTopic_id(Topic_id).ToList();
		public static List<PostInfo> GetItemsByTopic_id(uint?[] Topic_id, int limit) => Select.WhereTopic_id(Topic_id).Limit(limit).ToList();
		public static PostSelectBuild SelectByTopic_id(params uint?[] Topic_id) => Select.WhereTopic_id(Topic_id);

		#region async
		public static Task<int> DeleteByTopic_idAsync(uint? Topic_id) {
			return dal.DeleteByTopic_idAsync(Topic_id);
		}
		async public static Task<int> DeleteAsync(int Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new PostInfo { Id = Id });
			return affrows;
		}
		async public static Task<PostInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL_Post_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : PostInfo.Parse(str));
		async public static Task<int> UpdateAsync(PostInfo item) => await dal.Update(item).ExecuteNonQueryAsync();

		public static Task<PostInfo> InsertAsync(uint? Topic_id, string Content) {
			return InsertAsync(new PostInfo {
				Topic_id = Topic_id, 
				Content = Content});
		}
		async public static Task<PostInfo> InsertAsync(PostInfo item) {
			if (item.Create_time == null) item.Create_time = DateTime.Now;
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async internal static Task RemoveCacheAsync(PostInfo item) => await RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<PostInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Post_", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<PostInfo>> GetItemsAsync() => Select.ToListAsync();
		public static Task<List<PostInfo>> GetItemsByTopic_idAsync(params uint?[] Topic_id) => Select.WhereTopic_id(Topic_id).ToListAsync();
		public static Task<List<PostInfo>> GetItemsByTopic_idAsync(uint?[] Topic_id, int limit) => Select.WhereTopic_id(Topic_id).Limit(limit).ToListAsync();
		#endregion
	}
	public partial class PostSelectBuild : SelectBuild<PostInfo, PostSelectBuild> {
		public PostSelectBuild WhereTopic_id(params uint?[] Topic_id) {
			return this.Where1Or("a.`topic_id` = {0}", Topic_id);
		}
		public PostSelectBuild WhereTopic_id(TopicSelectBuild select, bool isNotIn = false) {
			var opt = isNotIn ? "NOT IN" : "IN";
			return this.Where($"a.`topic_id` {opt} ({select.ToString("`id`")})");
		}
		public PostSelectBuild WhereId(params int[] Id) {
			return this.Where1Or("a.`id` = {0}", Id);
		}
		public PostSelectBuild WhereContent(params string[] Content) {
			return this.Where1Or("a.`content` = {0}", Content);
		}
		public PostSelectBuild WhereContentLike(params string[] Content) {
			if (Content == null || Content.Where(a => !string.IsNullOrEmpty(a)).Any() == false) return this;
			return this.Where1Or(@"a.`content` LIKE {0}", Content.Select(a => "%" + a + "%").ToArray());
		}
		public PostSelectBuild WhereCreate_timeRange(DateTime? begin) {
			return base.Where("a.`create_time` >= {0}", begin);
		}
		public PostSelectBuild WhereCreate_timeRange(DateTime? begin, DateTime? end) {
			if (end == null) return WhereCreate_timeRange(begin);
			return base.Where("a.`create_time` between {0} and {1}", begin, end);
		}
		public PostSelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
	}
}