using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Song_tag {

		protected static readonly cd.DAL.Song_tag dal = new cd.DAL.Song_tag();
		protected static readonly int itemCacheTimeout;

		static Song_tag() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Song_tag"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int Song_id, int Tag_id) {
			var affrows = dal.Delete(Song_id, Tag_id);
			if (itemCacheTimeout > 0) RemoveCache(new Song_tagInfo { Song_id = Song_id, Tag_id = Tag_id });
			return affrows;
		}
		public static int DeleteBySong_id(int? Song_id) {
			return dal.DeleteBySong_id(Song_id);
		}
		public static int DeleteByTag_id(int? Tag_id) {
			return dal.DeleteByTag_id(Tag_id);
		}

		#region enum _
		public enum _ {
			/// <summary>
			/// 歌曲
			/// </summary>
			Song_id = 1, 
			/// <summary>
			/// 标签
			/// </summary>
			Tag_id
		}
		#endregion

		public static int Update(Song_tagInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(Song_tagInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Song_tag.SqlUpdateBuild UpdateDiy(int Song_id, int Tag_id) => new cd.DAL.Song_tag.SqlUpdateBuild(new List<Song_tagInfo> { new Song_tagInfo { Song_id = Song_id, Tag_id = Tag_id } });
		public static cd.DAL.Song_tag.SqlUpdateBuild UpdateDiy(List<Song_tagInfo> dataSource) => new cd.DAL.Song_tag.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Song_tag.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Song_tag.SqlUpdateBuild();

		public static Song_tagInfo Insert(int? Song_id, int? Tag_id) {
			return Insert(new Song_tagInfo {
				Song_id = Song_id, 
				Tag_id = Tag_id});
		}
		public static Song_tagInfo Insert(Song_tagInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		public static int Insert(IEnumerable<Song_tagInfo> items) {
			var affrows = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(items);
			return affrows;
		}
		internal static void RemoveCache(Song_tagInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<Song_tagInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Song_tag_", item.Song_id, "_,_", item.Tag_id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static Song_tagInfo GetItem(int Song_id, int Tag_id) => SqlHelper.CacheShell(string.Concat("cd_BLL_Song_tag_", Song_id, "_,_", Tag_id), itemCacheTimeout, () => Select.WhereSong_id(Song_id).WhereTag_id(Tag_id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Song_tagInfo.Parse(str));

		public static List<Song_tagInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static List<Song_tagInfo> GetItemsBySong_id(params int?[] Song_id) => Select.WhereSong_id(Song_id).ToList();
		public static List<Song_tagInfo> GetItemsBySong_id(int?[] Song_id, int limit) => Select.WhereSong_id(Song_id).Limit(limit).ToList();
		public static SelectBuild SelectBySong_id(params int?[] Song_id) => Select.WhereSong_id(Song_id);
		public static List<Song_tagInfo> GetItemsByTag_id(params int?[] Tag_id) => Select.WhereTag_id(Tag_id).ToList();
		public static List<Song_tagInfo> GetItemsByTag_id(int?[] Tag_id, int limit) => Select.WhereTag_id(Tag_id).Limit(limit).ToList();
		public static SelectBuild SelectByTag_id(params int?[] Tag_id) => Select.WhereTag_id(Tag_id);

		#region async
		public static Task<int> DeleteByTag_idAsync(int? Tag_id) {
			return dal.DeleteByTag_idAsync(Tag_id);
		}
		public static Task<int> DeleteBySong_idAsync(int? Song_id) {
			return dal.DeleteBySong_idAsync(Song_id);
		}
		async public static Task<int> DeleteAsync(int Song_id, int Tag_id) {
			var affrows = await dal.DeleteAsync(Song_id, Tag_id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new Song_tagInfo { Song_id = Song_id, Tag_id = Tag_id });
			return affrows;
		}
		async public static Task<Song_tagInfo> GetItemAsync(int Song_id, int Tag_id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL_Song_tag_", Song_id, "_,_", Tag_id), itemCacheTimeout, () => Select.WhereSong_id(Song_id).WhereTag_id(Tag_id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Song_tagInfo.Parse(str));
		public static Task<int> UpdateAsync(Song_tagInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(Song_tagInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<Song_tagInfo> InsertAsync(int? Song_id, int? Tag_id) {
			return InsertAsync(new Song_tagInfo {
				Song_id = Song_id, 
				Tag_id = Tag_id});
		}
		async public static Task<Song_tagInfo> InsertAsync(Song_tagInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		async public static Task<int> InsertAsync(IEnumerable<Song_tagInfo> items) {
			var affrows = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(items);
			return affrows;
		}
		internal static Task RemoveCacheAsync(Song_tagInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<Song_tagInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Song_tag_", item.Song_id, "_,_", item.Tag_id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<Song_tagInfo>> GetItemsAsync() => Select.ToListAsync();
		public static Task<List<Song_tagInfo>> GetItemsBySong_idAsync(params int?[] Song_id) => Select.WhereSong_id(Song_id).ToListAsync();
		public static Task<List<Song_tagInfo>> GetItemsBySong_idAsync(int?[] Song_id, int limit) => Select.WhereSong_id(Song_id).Limit(limit).ToListAsync();
		public static Task<List<Song_tagInfo>> GetItemsByTag_idAsync(params int?[] Tag_id) => Select.WhereTag_id(Tag_id).ToListAsync();
		public static Task<List<Song_tagInfo>> GetItemsByTag_idAsync(int?[] Tag_id, int limit) => Select.WhereTag_id(Tag_id).Limit(limit).ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<Song_tagInfo, SelectBuild> {
			public SelectBuild WhereSong_id(params int?[] Song_id) => this.Where1Or("a.`song_id` = {0}", Song_id);
			public SelectBuild WhereSong_id(Song.SelectBuild select, bool isNotIn = false) => this.Where($"a.`song_id` {(isNotIn ? "NOT IN" : "IN")} ({select.ToString("`id`")})");
			public SelectBuild WhereTag_id(params int?[] Tag_id) => this.Where1Or("a.`tag_id` = {0}", Tag_id);
			public SelectBuild WhereTag_id(Tag.SelectBuild select, bool isNotIn = false) => this.Where($"a.`tag_id` {(isNotIn ? "NOT IN" : "IN")} ({select.ToString("`id`")})");
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}