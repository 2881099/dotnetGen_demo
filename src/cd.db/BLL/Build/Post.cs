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

		#region enum _
		public enum _ {
			Id = 1, 
			/// <summary>
			/// 所属主题
			/// </summary>
			Topic_id, 
			/// <summary>
			/// 回复内容
			/// </summary>
			Content, 
			/// <summary>
			/// 创建时间
			/// </summary>
			Create_time
		}
		#endregion

		public static int Update(PostInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(PostInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
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
				keys[keysIdx++] = string.Concat("cd_BLL:Post:", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static PostInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("cd_BLL:Post:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : PostInfo.Parse(str));

		public static List<PostInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static List<PostInfo> GetItemsByTopic_id(params uint?[] Topic_id) => Select.WhereTopic_id(Topic_id).ToList();
		public static List<PostInfo> GetItemsByTopic_id(uint?[] Topic_id, int limit) => Select.WhereTopic_id(Topic_id).Limit(limit).ToList();
		public static SelectBuild SelectByTopic_id(params uint?[] Topic_id) => Select.WhereTopic_id(Topic_id);

		#region async
		public static Task<int> DeleteByTopic_idAsync(uint? Topic_id) {
			return dal.DeleteByTopic_idAsync(Topic_id);
		}
		async public static Task<int> DeleteAsync(int Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new PostInfo { Id = Id });
			return affrows;
		}
		async public static Task<PostInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Post:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : PostInfo.Parse(str));
		public static Task<int> UpdateAsync(PostInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(PostInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

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
		internal static Task RemoveCacheAsync(PostInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<PostInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Post:", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<PostInfo>> GetItemsAsync() => Select.ToListAsync();
		public static Task<List<PostInfo>> GetItemsByTopic_idAsync(params uint?[] Topic_id) => Select.WhereTopic_id(Topic_id).ToListAsync();
		public static Task<List<PostInfo>> GetItemsByTopic_idAsync(uint?[] Topic_id, int limit) => Select.WhereTopic_id(Topic_id).Limit(limit).ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<PostInfo, SelectBuild> {
			public SelectBuild WhereTopic_id(params uint?[] Topic_id) => this.Where1Or("a.`topic_id` = {0}", Topic_id);
			public SelectBuild WhereTopic_id(Topic.SelectBuild select, bool isNotIn = false) => this.Where($"a.`topic_id` {(isNotIn ? "NOT IN" : "IN")} ({select.ToString("`id`")})");
			public SelectBuild WhereId(params int[] Id) => this.Where1Or("a.`id` = {0}", Id);
			public SelectBuild WhereContentLike(string pattern, bool isNotLike = false) => this.Where($@"a.`content` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereCreate_timeRange(DateTime? begin) => base.Where("a.`create_time` >= {0}", begin);
			public SelectBuild WhereCreate_timeRange(DateTime? begin, DateTime? end) => end == null ? WhereCreate_timeRange(begin) : base.Where("a.`create_time` between {0} and {1}", begin, end);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}