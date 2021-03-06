﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Testtypeparentinfo {

		protected static readonly cd.DAL.Testtypeparentinfo dal = new cd.DAL.Testtypeparentinfo();
		protected static readonly int itemCacheTimeout;

		static Testtypeparentinfo() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Testtypeparentinfo"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int Id) {
			var affrows = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(new TesttypeparentinfoInfo { Id = Id });
			return affrows;
		}

		#region enum _
		public enum _ {
			Id = 1, 
			Name
		}
		#endregion

		public static int Update(TesttypeparentinfoInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(TesttypeparentinfoInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Testtypeparentinfo.SqlUpdateBuild UpdateDiy(int Id) => new cd.DAL.Testtypeparentinfo.SqlUpdateBuild(new List<TesttypeparentinfoInfo> { new TesttypeparentinfoInfo { Id = Id } });
		public static cd.DAL.Testtypeparentinfo.SqlUpdateBuild UpdateDiy(List<TesttypeparentinfoInfo> dataSource) => new cd.DAL.Testtypeparentinfo.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Testtypeparentinfo.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Testtypeparentinfo.SqlUpdateBuild();

		public static TesttypeparentinfoInfo Insert(string Name) {
			return Insert(new TesttypeparentinfoInfo {
				Name = Name});
		}
		public static TesttypeparentinfoInfo Insert(TesttypeparentinfoInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(TesttypeparentinfoInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<TesttypeparentinfoInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Testtypeparentinfo:", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static TesttypeparentinfoInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("cd_BLL:Testtypeparentinfo:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TesttypeparentinfoInfo.Parse(str));

		public static List<TesttypeparentinfoInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(int Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new TesttypeparentinfoInfo { Id = Id });
			return affrows;
		}
		async public static Task<TesttypeparentinfoInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Testtypeparentinfo:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TesttypeparentinfoInfo.Parse(str));
		public static Task<int> UpdateAsync(TesttypeparentinfoInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(TesttypeparentinfoInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<TesttypeparentinfoInfo> InsertAsync(string Name) {
			return InsertAsync(new TesttypeparentinfoInfo {
				Name = Name});
		}
		async public static Task<TesttypeparentinfoInfo> InsertAsync(TesttypeparentinfoInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		internal static Task RemoveCacheAsync(TesttypeparentinfoInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<TesttypeparentinfoInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Testtypeparentinfo:", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<TesttypeparentinfoInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<TesttypeparentinfoInfo, SelectBuild> {
			public SelectBuild WhereId(params int[] Id) => this.Where1Or("a.`Id` = {0}", Id);
			public SelectBuild WhereName(params string[] Name) => this.Where1Or("a.`Name` = {0}", Name);
			public SelectBuild WhereNameLike(string pattern, bool isNotLike = false) => this.Where($@"a.`Name` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}