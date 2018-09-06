using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Song {

		protected static readonly cd.DAL.Song dal = new cd.DAL.Song();
		protected static readonly int itemCacheTimeout;

		static Song() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Song"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int Id) {
			var affrows = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(new SongInfo { Id = Id });
			return affrows;
		}

		public static int Update(SongInfo item) => dal.Update(item).ExecuteNonQuery();
		public static cd.DAL.Song.SqlUpdateBuild UpdateDiy(int Id) => new cd.DAL.Song.SqlUpdateBuild(new List<SongInfo> { new SongInfo { Id = Id } });
		public static cd.DAL.Song.SqlUpdateBuild UpdateDiy(List<SongInfo> dataSource) => new cd.DAL.Song.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Song.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Song.SqlUpdateBuild();

		public static SongInfo Insert(string Title, string Url) {
			return Insert(new SongInfo {
				Title = Title, 
				Url = Url});
		}
		public static SongInfo Insert(SongInfo item) {
			if (item.Create_time == null) item.Create_time = DateTime.Now;
			if (item.Is_deleted == null) item.Is_deleted = false;
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(SongInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<SongInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Song_", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static SongInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("cd_BLL_Song_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : SongInfo.Parse(str));

		public static List<SongInfo> GetItems() => Select.ToList();
		public static SongSelectBuild SelectRaw => new SongSelectBuild(dal);
		/// <summary>
		/// 开启软删除功能，默认查询 is_deleted = false 的数据，查询所有使用 SelectRaw，软删除数据使用 Update is_deleted = true，物理删除数据使用 Delete 方法
		/// </summary>
		public static SongSelectBuild Select => SelectRaw.WhereIs_deleted(false);
		public static SongSelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static SongSelectBuild SelectByTag(params TagInfo[] tags) => Select.WhereTag(tags);
		public static SongSelectBuild SelectByTag_id(params int[] tag_ids) => Select.WhereTag_id(tag_ids);

		#region async
		async public static Task<int> DeleteAsync(int Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new SongInfo { Id = Id });
			return affrows;
		}
		async public static Task<SongInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL_Song_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : SongInfo.Parse(str));
		async public static Task<int> UpdateAsync(SongInfo item) => await dal.Update(item).ExecuteNonQueryAsync();

		public static Task<SongInfo> InsertAsync(string Title, string Url) {
			return InsertAsync(new SongInfo {
				Title = Title, 
				Url = Url});
		}
		async public static Task<SongInfo> InsertAsync(SongInfo item) {
			if (item.Create_time == null) item.Create_time = DateTime.Now;
			if (item.Is_deleted == null) item.Is_deleted = false;
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async internal static Task RemoveCacheAsync(SongInfo item) => await RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<SongInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Song_", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<SongInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion
	}
	public partial class SongSelectBuild : SelectBuild<SongInfo, SongSelectBuild> {
		public SongSelectBuild WhereTag(params TagInfo[] tags) => WhereTag(tags?.ToArray(), null);
		public SongSelectBuild WhereTag_id(params int[] tag_ids) => WhereTag_id(tag_ids?.ToArray(), null);
		public SongSelectBuild WhereTag(TagInfo[] tags, Action<Song_tagSelectBuild> subCondition) => WhereTag_id(tags?.Where<TagInfo>(a => a != null).Select<TagInfo, int>(a => a.Id.Value).ToArray(), subCondition);
		public SongSelectBuild WhereTag_id(int[] tag_ids, Action<Song_tagSelectBuild> subCondition) {
			if (tag_ids == null || tag_ids.Length == 0) return this;
			Song_tagSelectBuild subConditionSelect = Song_tag.Select.Where(string.Format("`song_id` = a . `id` AND `tag_id` IN ('{0}')", string.Join("','", tag_ids.Select(a => string.Concat(a).Replace("'", "''")))));
			if (subCondition != null) subCondition(subConditionSelect);
			var subConditionSql = subConditionSelect.ToString("`song_id`").Replace(" a \r\nWHERE (", " WHERE (");
			if (subCondition != null) subConditionSql = subConditionSql.Replace("a.`", "`song_tag`.`");
			return base.Where($"EXISTS({subConditionSql})");
		}
		public SongSelectBuild WhereId(params int[] Id) {
			return this.Where1Or("a.`id` = {0}", Id);
		}
		public SongSelectBuild WhereCreate_timeRange(DateTime? begin) {
			return base.Where("a.`create_time` >= {0}", begin);
		}
		public SongSelectBuild WhereCreate_timeRange(DateTime? begin, DateTime? end) {
			if (end == null) return WhereCreate_timeRange(begin);
			return base.Where("a.`create_time` between {0} and {1}", begin, end);
		}
		public SongSelectBuild WhereIs_deleted(params bool?[] Is_deleted) {
			return this.Where1Or("a.`is_deleted` = {0}", Is_deleted);
		}
		public SongSelectBuild WhereTitle(params string[] Title) {
			return this.Where1Or("a.`title` = {0}", Title);
		}
		public SongSelectBuild WhereTitleLike(params string[] Title) {
			if (Title == null || Title.Where(a => !string.IsNullOrEmpty(a)).Any() == false) return this;
			return this.Where1Or(@"a.`title` LIKE {0}", Title.Select(a => "%" + a + "%").ToArray());
		}
		public SongSelectBuild WhereUrl(params string[] Url) {
			return this.Where1Or("a.`url` = {0}", Url);
		}
		public SongSelectBuild WhereUrlLike(params string[] Url) {
			if (Url == null || Url.Where(a => !string.IsNullOrEmpty(a)).Any() == false) return this;
			return this.Where1Or(@"a.`url` LIKE {0}", Url.Select(a => "%" + a + "%").ToArray());
		}
		public SongSelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
	}
}