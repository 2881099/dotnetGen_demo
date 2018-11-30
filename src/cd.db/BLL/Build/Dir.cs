using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Dir {

		protected static readonly cd.DAL.Dir dal = new cd.DAL.Dir();
		protected static readonly int itemCacheTimeout;

		static Dir() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Dir"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(uint Id) {
			var affrows = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(GetItem(Id));
			return affrows;
		}
		public static int DeleteByPath(string Path) {
			var affrows = dal.DeleteByPath(Path);
			if (itemCacheTimeout > 0) RemoveCache(GetItemByPath(Path));
			return affrows;
		}

		#region enum _
		public enum _ {
			Id = 1, 
			/// <summary>
			/// HttpMethod + Path
			/// </summary>
			Path, 
			/// <summary>
			/// 描述
			/// </summary>
			Title
		}
		#endregion

		public static int Update(DirInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(DirInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Dir.SqlUpdateBuild UpdateDiy(uint Id) => new cd.DAL.Dir.SqlUpdateBuild(new List<DirInfo> { itemCacheTimeout > 0 ? new DirInfo { Id = Id,  } : GetItem(Id) });
		public static cd.DAL.Dir.SqlUpdateBuild UpdateDiy(List<DirInfo> dataSource) => new cd.DAL.Dir.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Dir.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Dir.SqlUpdateBuild();

		public static DirInfo Insert(string Path, string Title) {
			return Insert(new DirInfo {
				Path = Path, 
				Title = Title});
		}
		public static DirInfo Insert(DirInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(DirInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<DirInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 2];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Dir_", item.Id);
				keys[keysIdx++] = string.Concat("cd_BLL_DirByPath_", item.Path);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static DirInfo GetItem(uint Id) => SqlHelper.CacheShell(string.Concat("cd_BLL_Dir_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : DirInfo.Parse(str));
		public static DirInfo GetItemByPath(string Path) => SqlHelper.CacheShell(string.Concat("cd_BLL_DirByPath_", Path), itemCacheTimeout, () => Select.WherePath(Path).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : DirInfo.Parse(str));

		public static List<DirInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static SelectBuild SelectByRole(params RoleInfo[] roles) => Select.WhereRole(roles);
		public static SelectBuild SelectByRole_id(params uint[] role_ids) => Select.WhereRole_id(role_ids);

		#region async
		async public static Task<int> DeleteAsync(uint Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(GetItem(Id));
			return affrows;
		}
		async public static Task<DirInfo> GetItemAsync(uint Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL_Dir_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : DirInfo.Parse(str));
		async public static Task<int> DeleteByPathAsync(string Path) {
			var affrows = await dal.DeleteByPathAsync(Path);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(GetItemByPath(Path));
			return affrows;
		}
		async public static Task<DirInfo> GetItemByPathAsync(string Path) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL_DirByPath_", Path), itemCacheTimeout, () => Select.WherePath(Path).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : DirInfo.Parse(str));
		public static Task<int> UpdateAsync(DirInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(DirInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<DirInfo> InsertAsync(string Path, string Title) {
			return InsertAsync(new DirInfo {
				Path = Path, 
				Title = Title});
		}
		async public static Task<DirInfo> InsertAsync(DirInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		internal static Task RemoveCacheAsync(DirInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<DirInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 2];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Dir_", item.Id);
				keys[keysIdx++] = string.Concat("cd_BLL_DirByPath_", item.Path);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<DirInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<DirInfo, SelectBuild> {
			public SelectBuild WhereRole(params RoleInfo[] roles) => WhereRole(roles?.ToArray(), null);
			public SelectBuild WhereRole_id(params uint[] role_ids) => WhereRole_id(role_ids?.ToArray(), null);
			public SelectBuild WhereRole(RoleInfo[] roles, Action<Role_dir.SelectBuild> subCondition) => WhereRole_id(roles?.Where<RoleInfo>(a => a != null).Select<RoleInfo, uint>(a => a.Id.Value).ToArray(), subCondition);
			public SelectBuild WhereRole_id(uint[] role_ids, Action<Role_dir.SelectBuild> subCondition) {
				if (role_ids == null || role_ids.Length == 0) return this;
				Role_dir.SelectBuild subConditionSelect = Role_dir.Select.Where(string.Format("`dir_id` = a . `id` AND `role_id` IN ('{0}')", string.Join("','", role_ids.Select(a => string.Concat(a).Replace("'", "''")))));
				subCondition?.Invoke(subConditionSelect);
				var subConditionSql = subConditionSelect.ToString("`dir_id`").Replace(" a \r\nWHERE (", " WHERE (");
				if (subCondition != null) subConditionSql = subConditionSql.Replace("a.`", "`role_dir`.`");
				return base.Where($"EXISTS({subConditionSql})");
			}
			public SelectBuild WhereId(params uint[] Id) => this.Where1Or("a.`id` = {0}", Id);
			/// <summary>
			/// HttpMethod + Path，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WherePath(params string[] Path) => this.Where1Or("a.`path` = {0}", Path);
			public SelectBuild WherePathLike(string pattern, bool isNotLike = false) => this.Where($@"a.`path` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			/// <summary>
			/// 描述，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereTitle(params string[] Title) => this.Where1Or("a.`title` = {0}", Title);
			public SelectBuild WhereTitleLike(string pattern, bool isNotLike = false) => this.Where($@"a.`title` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}