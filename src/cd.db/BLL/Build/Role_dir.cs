using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Role_dir {

		protected static readonly cd.DAL.Role_dir dal = new cd.DAL.Role_dir();
		protected static readonly int itemCacheTimeout;

		static Role_dir() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Role_dir"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(uint Dir_id, uint Role_id) {
			var affrows = dal.Delete(Dir_id, Role_id);
			if (itemCacheTimeout > 0) RemoveCache(new Role_dirInfo { Dir_id = Dir_id, Role_id = Role_id });
			return affrows;
		}
		public static int DeleteByDir_id(uint? Dir_id) {
			return dal.DeleteByDir_id(Dir_id);
		}
		public static int DeleteByRole_id(uint? Role_id) {
			return dal.DeleteByRole_id(Role_id);
		}

		#region enum _
		public enum _ {
			Dir_id = 1, 
			Role_id
		}
		#endregion

		public static int Update(Role_dirInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(Role_dirInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Role_dir.SqlUpdateBuild UpdateDiy(uint Dir_id, uint Role_id) => new cd.DAL.Role_dir.SqlUpdateBuild(new List<Role_dirInfo> { new Role_dirInfo { Dir_id = Dir_id, Role_id = Role_id } });
		public static cd.DAL.Role_dir.SqlUpdateBuild UpdateDiy(List<Role_dirInfo> dataSource) => new cd.DAL.Role_dir.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Role_dir.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Role_dir.SqlUpdateBuild();

		public static Role_dirInfo Insert(uint? Dir_id, uint? Role_id) {
			return Insert(new Role_dirInfo {
				Dir_id = Dir_id, 
				Role_id = Role_id});
		}
		public static Role_dirInfo Insert(Role_dirInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		public static int Insert(IEnumerable<Role_dirInfo> items) {
			var affrows = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(items);
			return affrows;
		}
		internal static void RemoveCache(Role_dirInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<Role_dirInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Role_dir_", item.Dir_id, "_,_", item.Role_id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static Role_dirInfo GetItem(uint Dir_id, uint Role_id) => SqlHelper.CacheShell(string.Concat("cd_BLL_Role_dir_", Dir_id, "_,_", Role_id), itemCacheTimeout, () => Select.WhereDir_id(Dir_id).WhereRole_id(Role_id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Role_dirInfo.Parse(str));

		public static List<Role_dirInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static List<Role_dirInfo> GetItemsByDir_id(params uint?[] Dir_id) => Select.WhereDir_id(Dir_id).ToList();
		public static List<Role_dirInfo> GetItemsByDir_id(uint?[] Dir_id, int limit) => Select.WhereDir_id(Dir_id).Limit(limit).ToList();
		public static SelectBuild SelectByDir_id(params uint?[] Dir_id) => Select.WhereDir_id(Dir_id);
		public static List<Role_dirInfo> GetItemsByRole_id(params uint?[] Role_id) => Select.WhereRole_id(Role_id).ToList();
		public static List<Role_dirInfo> GetItemsByRole_id(uint?[] Role_id, int limit) => Select.WhereRole_id(Role_id).Limit(limit).ToList();
		public static SelectBuild SelectByRole_id(params uint?[] Role_id) => Select.WhereRole_id(Role_id);

		#region async
		public static Task<int> DeleteByRole_idAsync(uint? Role_id) {
			return dal.DeleteByRole_idAsync(Role_id);
		}
		public static Task<int> DeleteByDir_idAsync(uint? Dir_id) {
			return dal.DeleteByDir_idAsync(Dir_id);
		}
		async public static Task<int> DeleteAsync(uint Dir_id, uint Role_id) {
			var affrows = await dal.DeleteAsync(Dir_id, Role_id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new Role_dirInfo { Dir_id = Dir_id, Role_id = Role_id });
			return affrows;
		}
		async public static Task<Role_dirInfo> GetItemAsync(uint Dir_id, uint Role_id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL_Role_dir_", Dir_id, "_,_", Role_id), itemCacheTimeout, () => Select.WhereDir_id(Dir_id).WhereRole_id(Role_id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Role_dirInfo.Parse(str));
		public static Task<int> UpdateAsync(Role_dirInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(Role_dirInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<Role_dirInfo> InsertAsync(uint? Dir_id, uint? Role_id) {
			return InsertAsync(new Role_dirInfo {
				Dir_id = Dir_id, 
				Role_id = Role_id});
		}
		async public static Task<Role_dirInfo> InsertAsync(Role_dirInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		async public static Task<int> InsertAsync(IEnumerable<Role_dirInfo> items) {
			var affrows = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(items);
			return affrows;
		}
		internal static Task RemoveCacheAsync(Role_dirInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<Role_dirInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Role_dir_", item.Dir_id, "_,_", item.Role_id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<Role_dirInfo>> GetItemsAsync() => Select.ToListAsync();
		public static Task<List<Role_dirInfo>> GetItemsByDir_idAsync(params uint?[] Dir_id) => Select.WhereDir_id(Dir_id).ToListAsync();
		public static Task<List<Role_dirInfo>> GetItemsByDir_idAsync(uint?[] Dir_id, int limit) => Select.WhereDir_id(Dir_id).Limit(limit).ToListAsync();
		public static Task<List<Role_dirInfo>> GetItemsByRole_idAsync(params uint?[] Role_id) => Select.WhereRole_id(Role_id).ToListAsync();
		public static Task<List<Role_dirInfo>> GetItemsByRole_idAsync(uint?[] Role_id, int limit) => Select.WhereRole_id(Role_id).Limit(limit).ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<Role_dirInfo, SelectBuild> {
			public SelectBuild WhereDir_id(params uint?[] Dir_id) => this.Where1Or("a.`dir_id` = {0}", Dir_id);
			public SelectBuild WhereDir_id(Dir.SelectBuild select, bool isNotIn = false) => this.Where($"a.`dir_id` {(isNotIn ? "NOT IN" : "IN")} ({select.ToString("`id`")})");
			public SelectBuild WhereRole_id(params uint?[] Role_id) => this.Where1Or("a.`role_id` = {0}", Role_id);
			public SelectBuild WhereRole_id(Role.SelectBuild select, bool isNotIn = false) => this.Where($"a.`role_id` {(isNotIn ? "NOT IN" : "IN")} ({select.ToString("`id`")})");
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}