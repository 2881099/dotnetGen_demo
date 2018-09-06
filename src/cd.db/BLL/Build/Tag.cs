using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Tag {

		protected static readonly cd.DAL.Tag dal = new cd.DAL.Tag();
		protected static readonly int itemCacheTimeout;

		static Tag() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Tag"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int Id) {
			var affrows = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(new TagInfo { Id = Id });
			return affrows;
		}
		public static int DeleteByParent_id(int? Parent_id) {
			return dal.DeleteByParent_id(Parent_id);
		}

		public static int Update(TagInfo item) => dal.Update(item).ExecuteNonQuery();
		public static cd.DAL.Tag.SqlUpdateBuild UpdateDiy(int Id) => new cd.DAL.Tag.SqlUpdateBuild(new List<TagInfo> { new TagInfo { Id = Id } });
		public static cd.DAL.Tag.SqlUpdateBuild UpdateDiy(List<TagInfo> dataSource) => new cd.DAL.Tag.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Tag.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Tag.SqlUpdateBuild();

		public static TagInfo Insert(int? Parent_id, string Name) {
			return Insert(new TagInfo {
				Parent_id = Parent_id, 
				Name = Name});
		}
		public static TagInfo Insert(TagInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(TagInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<TagInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Tag_", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static TagInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("cd_BLL_Tag_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TagInfo.Parse(str));

		public static List<TagInfo> GetItems() => Select.ToList();
		public static TagSelectBuild Select => new TagSelectBuild(dal);
		public static TagSelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static List<TagInfo> GetItemsByParent_id(params int?[] Parent_id) => Select.WhereParent_id(Parent_id).ToList();
		public static List<TagInfo> GetItemsByParent_id(int?[] Parent_id, int limit) => Select.WhereParent_id(Parent_id).Limit(limit).ToList();
		public static TagSelectBuild SelectByParent_id(params int?[] Parent_id) => Select.WhereParent_id(Parent_id);
		public static TagSelectBuild SelectBySong(params SongInfo[] songs) => Select.WhereSong(songs);
		public static TagSelectBuild SelectBySong_id(params int[] song_ids) => Select.WhereSong_id(song_ids);

		#region async
		public static Task<int> DeleteByParent_idAsync(int? Parent_id) {
			return dal.DeleteByParent_idAsync(Parent_id);
		}
		async public static Task<int> DeleteAsync(int Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new TagInfo { Id = Id });
			return affrows;
		}
		async public static Task<TagInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL_Tag_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TagInfo.Parse(str));
		async public static Task<int> UpdateAsync(TagInfo item) => await dal.Update(item).ExecuteNonQueryAsync();

		public static Task<TagInfo> InsertAsync(int? Parent_id, string Name) {
			return InsertAsync(new TagInfo {
				Parent_id = Parent_id, 
				Name = Name});
		}
		async public static Task<TagInfo> InsertAsync(TagInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async internal static Task RemoveCacheAsync(TagInfo item) => await RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<TagInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Tag_", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<TagInfo>> GetItemsAsync() => Select.ToListAsync();
		public static Task<List<TagInfo>> GetItemsByParent_idAsync(params int?[] Parent_id) => Select.WhereParent_id(Parent_id).ToListAsync();
		public static Task<List<TagInfo>> GetItemsByParent_idAsync(int?[] Parent_id, int limit) => Select.WhereParent_id(Parent_id).Limit(limit).ToListAsync();
		#endregion
	}
	public partial class TagSelectBuild : SelectBuild<TagInfo, TagSelectBuild> {
		public TagSelectBuild WhereParent_id(params int?[] Parent_id) {
			return this.Where1Or("a.`parent_id` = {0}", Parent_id);
		}
		public TagSelectBuild WhereParent_id(TagSelectBuild select, bool isNotIn = false) {
			var opt = isNotIn ? "NOT IN" : "IN";
			return this.Where($"a.`parent_id` {opt} ({select.ToString("`id`")})");
		}
		public TagSelectBuild WhereSong(params SongInfo[] songs) => WhereSong(songs?.ToArray(), null);
		public TagSelectBuild WhereSong_id(params int[] song_ids) => WhereSong_id(song_ids?.ToArray(), null);
		public TagSelectBuild WhereSong(SongInfo[] songs, Action<Song_tagSelectBuild> subCondition) => WhereSong_id(songs?.Where<SongInfo>(a => a != null).Select<SongInfo, int>(a => a.Id.Value).ToArray(), subCondition);
		public TagSelectBuild WhereSong_id(int[] song_ids, Action<Song_tagSelectBuild> subCondition) {
			if (song_ids == null || song_ids.Length == 0) return this;
			Song_tagSelectBuild subConditionSelect = Song_tag.Select.Where(string.Format("`tag_id` = a . `id` AND `song_id` IN ('{0}')", string.Join("','", song_ids.Select(a => string.Concat(a).Replace("'", "''")))));
			if (subCondition != null) subCondition(subConditionSelect);
			var subConditionSql = subConditionSelect.ToString("`tag_id`").Replace(" a \r\nWHERE (", " WHERE (");
			if (subCondition != null) subConditionSql = subConditionSql.Replace("a.`", "`song_tag`.`");
			return base.Where($"EXISTS({subConditionSql})");
		}
		public TagSelectBuild WhereId(params int[] Id) {
			return this.Where1Or("a.`id` = {0}", Id);
		}
		public TagSelectBuild WhereName(params string[] Name) {
			return this.Where1Or("a.`name` = {0}", Name);
		}
		public TagSelectBuild WhereNameLike(params string[] Name) {
			if (Name == null || Name.Where(a => !string.IsNullOrEmpty(a)).Any() == false) return this;
			return this.Where1Or(@"a.`name` LIKE {0}", Name.Select(a => "%" + a + "%").ToArray());
		}
		public TagSelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
	}
}