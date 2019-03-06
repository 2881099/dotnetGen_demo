using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Topicfields {

		protected static readonly cd.DAL.Topicfields dal = new cd.DAL.Topicfields();
		protected static readonly int itemCacheTimeout;

		static Topicfields() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Topicfields"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int TopicId) {
			var affrows = dal.Delete(TopicId);
			if (itemCacheTimeout > 0) RemoveCache(new TopicfieldsInfo { TopicId = TopicId });
			return affrows;
		}

		#region enum _
		public enum _ {
			TopicId = 1
		}
		#endregion

		public static int Update(TopicfieldsInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(TopicfieldsInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Topicfields.SqlUpdateBuild UpdateDiy(int TopicId) => new cd.DAL.Topicfields.SqlUpdateBuild(new List<TopicfieldsInfo> { new TopicfieldsInfo { TopicId = TopicId } });
		public static cd.DAL.Topicfields.SqlUpdateBuild UpdateDiy(List<TopicfieldsInfo> dataSource) => new cd.DAL.Topicfields.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Topicfields.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Topicfields.SqlUpdateBuild();

		public static TopicfieldsInfo Insert(int? TopicId) {
			return Insert(new TopicfieldsInfo {
				TopicId = TopicId});
		}
		public static TopicfieldsInfo Insert(TopicfieldsInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		public static int Insert(IEnumerable<TopicfieldsInfo> items) {
			var affrows = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(items);
			return affrows;
		}
		internal static void RemoveCache(TopicfieldsInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<TopicfieldsInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Topicfields:", item.TopicId);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static TopicfieldsInfo GetItem(int TopicId) => SqlHelper.CacheShell(string.Concat("cd_BLL:Topicfields:", TopicId), itemCacheTimeout, () => Select.WhereTopicId(TopicId).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TopicfieldsInfo.Parse(str));

		public static List<TopicfieldsInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(int TopicId) {
			var affrows = await dal.DeleteAsync(TopicId);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new TopicfieldsInfo { TopicId = TopicId });
			return affrows;
		}
		async public static Task<TopicfieldsInfo> GetItemAsync(int TopicId) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Topicfields:", TopicId), itemCacheTimeout, () => Select.WhereTopicId(TopicId).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TopicfieldsInfo.Parse(str));
		public static Task<int> UpdateAsync(TopicfieldsInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(TopicfieldsInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<TopicfieldsInfo> InsertAsync(int? TopicId) {
			return InsertAsync(new TopicfieldsInfo {
				TopicId = TopicId});
		}
		async public static Task<TopicfieldsInfo> InsertAsync(TopicfieldsInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		async public static Task<int> InsertAsync(IEnumerable<TopicfieldsInfo> items) {
			var affrows = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(items);
			return affrows;
		}
		internal static Task RemoveCacheAsync(TopicfieldsInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<TopicfieldsInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Topicfields:", item.TopicId);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<TopicfieldsInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<TopicfieldsInfo, SelectBuild> {
			public SelectBuild WhereTopicId(params int[] TopicId) => this.Where1Or("a.`TopicId` = {0}", TopicId);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}