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

		#region enum _
		public enum _ {
			Id = 1, 
			/// <summary>
			/// 父标签
			/// </summary>
			Parent_id, 
			/// <summary>
			/// 名称
			/// </summary>
			Name
		}
		#endregion

		public static int Update(TagInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(TagInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
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
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static List<TagInfo> GetItemsByParent_id(params int?[] Parent_id) => Select.WhereParent_id(Parent_id).ToList();
		public static List<TagInfo> GetItemsByParent_id(int?[] Parent_id, int limit) => Select.WhereParent_id(Parent_id).Limit(limit).ToList();
		public static SelectBuild SelectByParent_id(params int?[] Parent_id) => Select.WhereParent_id(Parent_id);
		public static SelectBuild SelectBySong(params SongInfo[] songs) => Select.WhereSong(songs);
		public static SelectBuild SelectBySong_id(params int[] song_ids) => Select.WhereSong_id(song_ids);

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
		public static Task<int> UpdateAsync(TagInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(TagInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

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
		internal static Task RemoveCacheAsync(TagInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
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

		public partial class SelectBuild : SelectBuild<TagInfo, SelectBuild> {
			public SelectBuild WhereParent_id(params int?[] Parent_id) => this.Where1Or("a.`parent_id` = {0}", Parent_id);
			public SelectBuild WhereParent_id(Tag.SelectBuild select, bool isNotIn = false) => this.Where($"a.`parent_id` {(isNotIn ? "NOT IN" : "IN")} ({select.ToString("`id`")})");
			public SelectBuild WhereSong(params SongInfo[] songs) => WhereSong(songs?.ToArray(), null);
			public SelectBuild WhereSong_id(params int[] song_ids) => WhereSong_id(song_ids?.ToArray(), null);
			public SelectBuild WhereSong(SongInfo[] songs, Action<Song_tag.SelectBuild> subCondition) => WhereSong_id(songs?.Where<SongInfo>(a => a != null).Select<SongInfo, int>(a => a.Id.Value).ToArray(), subCondition);
			public SelectBuild WhereSong_id(int[] song_ids, Action<Song_tag.SelectBuild> subCondition) {
				if (song_ids == null || song_ids.Length == 0) return this;
				Song_tag.SelectBuild subConditionSelect = Song_tag.Select.Where(string.Format("`tag_id` = a . `id` AND `song_id` IN ('{0}')", string.Join("','", song_ids.Select(a => string.Concat(a).Replace("'", "''")))));
				subCondition?.Invoke(subConditionSelect);
				var subConditionSql = subConditionSelect.ToString("`tag_id`").Replace(" a \r\nWHERE (", " WHERE (");
				if (subCondition != null) subConditionSql = subConditionSql.Replace("a.`", "`song_tag`.`");
				return base.Where($"EXISTS({subConditionSql})");
			}
			public SelectBuild WhereId(params int[] Id) => this.Where1Or("a.`id` = {0}", Id);
			/// <summary>
			/// 名称，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereName(params string[] Name) => this.Where1Or("a.`name` = {0}", Name);
			public SelectBuild WhereNameLike(string pattern, bool isNotLike = false) => this.Where($@"a.`name` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}