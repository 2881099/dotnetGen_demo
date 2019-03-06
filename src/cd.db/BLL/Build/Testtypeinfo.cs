using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Testtypeinfo {

		protected static readonly cd.DAL.Testtypeinfo dal = new cd.DAL.Testtypeinfo();
		protected static readonly int itemCacheTimeout;

		static Testtypeinfo() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Testtypeinfo"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int Guid) {
			var affrows = dal.Delete(Guid);
			if (itemCacheTimeout > 0) RemoveCache(new TesttypeinfoInfo { Guid = Guid });
			return affrows;
		}

		#region enum _
		public enum _ {
			Guid = 1, 
			Name, 
			ParentId, 
			SelfGuid
		}
		#endregion

		public static int Update(TesttypeinfoInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(TesttypeinfoInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Testtypeinfo.SqlUpdateBuild UpdateDiy(int Guid) => new cd.DAL.Testtypeinfo.SqlUpdateBuild(new List<TesttypeinfoInfo> { new TesttypeinfoInfo { Guid = Guid } });
		public static cd.DAL.Testtypeinfo.SqlUpdateBuild UpdateDiy(List<TesttypeinfoInfo> dataSource) => new cd.DAL.Testtypeinfo.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Testtypeinfo.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Testtypeinfo.SqlUpdateBuild();

		public static TesttypeinfoInfo Insert(int? Guid, string Name, int? ParentId, int? SelfGuid) {
			return Insert(new TesttypeinfoInfo {
				Guid = Guid, 
				Name = Name, 
				ParentId = ParentId, 
				SelfGuid = SelfGuid});
		}
		public static TesttypeinfoInfo Insert(TesttypeinfoInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		public static int Insert(IEnumerable<TesttypeinfoInfo> items) {
			var affrows = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(items);
			return affrows;
		}
		internal static void RemoveCache(TesttypeinfoInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<TesttypeinfoInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Testtypeinfo:", item.Guid);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static TesttypeinfoInfo GetItem(int Guid) => SqlHelper.CacheShell(string.Concat("cd_BLL:Testtypeinfo:", Guid), itemCacheTimeout, () => Select.WhereGuid(Guid).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TesttypeinfoInfo.Parse(str));

		public static List<TesttypeinfoInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(int Guid) {
			var affrows = await dal.DeleteAsync(Guid);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new TesttypeinfoInfo { Guid = Guid });
			return affrows;
		}
		async public static Task<TesttypeinfoInfo> GetItemAsync(int Guid) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Testtypeinfo:", Guid), itemCacheTimeout, () => Select.WhereGuid(Guid).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TesttypeinfoInfo.Parse(str));
		public static Task<int> UpdateAsync(TesttypeinfoInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(TesttypeinfoInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<TesttypeinfoInfo> InsertAsync(int? Guid, string Name, int? ParentId, int? SelfGuid) {
			return InsertAsync(new TesttypeinfoInfo {
				Guid = Guid, 
				Name = Name, 
				ParentId = ParentId, 
				SelfGuid = SelfGuid});
		}
		async public static Task<TesttypeinfoInfo> InsertAsync(TesttypeinfoInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		async public static Task<int> InsertAsync(IEnumerable<TesttypeinfoInfo> items) {
			var affrows = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(items);
			return affrows;
		}
		internal static Task RemoveCacheAsync(TesttypeinfoInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<TesttypeinfoInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Testtypeinfo:", item.Guid);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<TesttypeinfoInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<TesttypeinfoInfo, SelectBuild> {
			public SelectBuild WhereGuid(params int[] Guid) => this.Where1Or("a.`Guid` = {0}", Guid);
			public SelectBuild WhereName(params string[] Name) => this.Where1Or("a.`Name` = {0}", Name);
			public SelectBuild WhereNameLike(string pattern, bool isNotLike = false) => this.Where($@"a.`Name` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereParentId(params int?[] ParentId) => this.Where1Or("a.`ParentId` = {0}", ParentId);
			public SelectBuild WhereSelfGuid(params int?[] SelfGuid) => this.Where1Or("a.`SelfGuid` = {0}", SelfGuid);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}