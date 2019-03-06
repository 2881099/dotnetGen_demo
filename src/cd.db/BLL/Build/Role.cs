using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Role {

		protected static readonly cd.DAL.Role dal = new cd.DAL.Role();
		protected static readonly int itemCacheTimeout;

		static Role() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Role"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(uint Id) {
			var affrows = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(new RoleInfo { Id = Id });
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
			/// 角色名
			/// </summary>
			Name
		}
		#endregion

		public static int Update(RoleInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(RoleInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Role.SqlUpdateBuild UpdateDiy(uint Id) => new cd.DAL.Role.SqlUpdateBuild(new List<RoleInfo> { new RoleInfo { Id = Id } });
		public static cd.DAL.Role.SqlUpdateBuild UpdateDiy(List<RoleInfo> dataSource) => new cd.DAL.Role.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Role.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Role.SqlUpdateBuild();

		public static RoleInfo Insert(string Name) {
			return Insert(new RoleInfo {
				Name = Name});
		}
		public static RoleInfo Insert(RoleInfo item) {
			if (item.Create_time == null) item.Create_time = DateTime.Now;
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(RoleInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<RoleInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Role:", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static RoleInfo GetItem(uint Id) => SqlHelper.CacheShell(string.Concat("cd_BLL:Role:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : RoleInfo.Parse(str));

		public static List<RoleInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static SelectBuild SelectByDir(params DirInfo[] dirs) => Select.WhereDir(dirs);
		public static SelectBuild SelectByDir_id(params uint[] dir_ids) => Select.WhereDir_id(dir_ids);

		#region async
		async public static Task<int> DeleteAsync(uint Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new RoleInfo { Id = Id });
			return affrows;
		}
		async public static Task<RoleInfo> GetItemAsync(uint Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Role:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : RoleInfo.Parse(str));
		public static Task<int> UpdateAsync(RoleInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(RoleInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<RoleInfo> InsertAsync(string Name) {
			return InsertAsync(new RoleInfo {
				Name = Name});
		}
		async public static Task<RoleInfo> InsertAsync(RoleInfo item) {
			if (item.Create_time == null) item.Create_time = DateTime.Now;
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		internal static Task RemoveCacheAsync(RoleInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<RoleInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Role:", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<RoleInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<RoleInfo, SelectBuild> {
			public SelectBuild WhereDir(params DirInfo[] dirs) => WhereDir(dirs?.ToArray(), null);
			public SelectBuild WhereDir_id(params uint[] dir_ids) => WhereDir_id(dir_ids?.ToArray(), null);
			public SelectBuild WhereDir(DirInfo[] dirs, Action<Role_dir.SelectBuild> subCondition) => WhereDir_id(dirs?.Where<DirInfo>(a => a != null).Select<DirInfo, uint>(a => a.Id.Value).ToArray(), subCondition);
			public SelectBuild WhereDir_id(uint[] dir_ids, Action<Role_dir.SelectBuild> subCondition) {
				if (dir_ids == null || dir_ids.Length == 0) return this;
				Role_dir.SelectBuild subConditionSelect = Role_dir.Select.Where(string.Format("`role_id` = a . `id` AND `dir_id` IN ('{0}')", string.Join("','", dir_ids.Select(a => string.Concat(a).Replace("'", "''")))));
				subCondition?.Invoke(subConditionSelect);
				var subConditionSql = subConditionSelect.ToString("`role_id`").Replace(" a \r\nWHERE (", " WHERE (");
				if (subCondition != null) subConditionSql = subConditionSql.Replace("a.`", "`role_dir`.`");
				return base.Where($"EXISTS({subConditionSql})");
			}
			public SelectBuild WhereId(params uint[] Id) => this.Where1Or("a.`id` = {0}", Id);
			public SelectBuild WhereCreate_timeRange(DateTime? begin) => base.Where("a.`create_time` >= {0}", begin);
			public SelectBuild WhereCreate_timeRange(DateTime? begin, DateTime? end) => end == null ? WhereCreate_timeRange(begin) : base.Where("a.`create_time` between {0} and {1}", begin, end);
			/// <summary>
			/// 角色名，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereName(params string[] Name) => this.Where1Or("a.`name` = {0}", Name);
			public SelectBuild WhereNameLike(string pattern, bool isNotLike = false) => this.Where($@"a.`name` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}