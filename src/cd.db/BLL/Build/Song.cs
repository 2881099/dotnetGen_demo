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

		#region enum _
		public enum _ {
			Id = 1, 
			/// <summary>
			/// 创建时间
			/// </summary>
			Create_time, 
			/// <summary>
			/// 软删除
			/// </summary>
			Is_deleted, 
			/// <summary>
			/// 歌名
			/// </summary>
			Title, 
			/// <summary>
			/// 地址
			/// </summary>
			Url
		}
		#endregion

		public static int Update(SongInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(SongInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
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
		public static SelectBuild SelectRaw => new SelectBuild(dal);
		/// <summary>
		/// 开启软删除功能，默认查询 is_deleted = false 的数据，查询所有使用 SelectRaw，软删除数据使用 Update is_deleted = true，物理删除数据使用 Delete 方法
		/// </summary>
		public static SelectBuild Select => SelectRaw.WhereIs_deleted(false);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static SelectBuild SelectByTag(params TagInfo[] tags) => Select.WhereTag(tags);
		public static SelectBuild SelectByTag_id(params int[] tag_ids) => Select.WhereTag_id(tag_ids);

		#region async
		async public static Task<int> DeleteAsync(int Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new SongInfo { Id = Id });
			return affrows;
		}
		async public static Task<SongInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL_Song_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : SongInfo.Parse(str));
		public static Task<int> UpdateAsync(SongInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(SongInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

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
		internal static Task RemoveCacheAsync(SongInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
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

		public partial class SelectBuild : SelectBuild<SongInfo, SelectBuild> {
			public SelectBuild WhereTag(params TagInfo[] tags) => WhereTag(tags?.ToArray(), null);
			public SelectBuild WhereTag_id(params int[] tag_ids) => WhereTag_id(tag_ids?.ToArray(), null);
			public SelectBuild WhereTag(TagInfo[] tags, Action<Song_tag.SelectBuild> subCondition) => WhereTag_id(tags?.Where<TagInfo>(a => a != null).Select<TagInfo, int>(a => a.Id.Value).ToArray(), subCondition);
			public SelectBuild WhereTag_id(int[] tag_ids, Action<Song_tag.SelectBuild> subCondition) {
				if (tag_ids == null || tag_ids.Length == 0) return this;
				Song_tag.SelectBuild subConditionSelect = Song_tag.Select.Where(string.Format("`song_id` = a . `id` AND `tag_id` IN ('{0}')", string.Join("','", tag_ids.Select(a => string.Concat(a).Replace("'", "''")))));
				subCondition?.Invoke(subConditionSelect);
				var subConditionSql = subConditionSelect.ToString("`song_id`").Replace(" a \r\nWHERE (", " WHERE (");
				if (subCondition != null) subConditionSql = subConditionSql.Replace("a.`", "`song_tag`.`");
				return base.Where($"EXISTS({subConditionSql})");
			}
			public SelectBuild WhereId(params int[] Id) => this.Where1Or("a.`id` = {0}", Id);
			public SelectBuild WhereCreate_timeRange(DateTime? begin) => base.Where("a.`create_time` >= {0}", begin);
			public SelectBuild WhereCreate_timeRange(DateTime? begin, DateTime? end) => end == null ? WhereCreate_timeRange(begin) : base.Where("a.`create_time` between {0} and {1}", begin, end);
			/// <summary>
			/// 软删除，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereIs_deleted(params bool?[] Is_deleted) => this.Where1Or("a.`is_deleted` = {0}", Is_deleted);
			/// <summary>
			/// 歌名，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereTitle(params string[] Title) => this.Where1Or("a.`title` = {0}", Title);
			public SelectBuild WhereTitleLike(string pattern, bool isNotLike = false) => this.Where($@"a.`title` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			/// <summary>
			/// 地址，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereUrl(params string[] Url) => this.Where1Or("a.`url` = {0}", Url);
			public SelectBuild WhereUrlLike(string pattern, bool isNotLike = false) => this.Where($@"a.`url` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}