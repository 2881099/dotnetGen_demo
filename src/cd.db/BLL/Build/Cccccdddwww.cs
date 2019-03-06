using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Cccccdddwww {

		protected static readonly cd.DAL.Cccccdddwww dal = new cd.DAL.Cccccdddwww();
		protected static readonly int itemCacheTimeout;

		static Cccccdddwww() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Cccccdddwww"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int Idx) {
			var affrows = dal.Delete(Idx);
			if (itemCacheTimeout > 0) RemoveCache(new CccccdddwwwInfo { Idx = Idx });
			return affrows;
		}

		#region enum _
		public enum _ {
			Idx = 1, 
			Name
		}
		#endregion

		public static int Update(CccccdddwwwInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(CccccdddwwwInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Cccccdddwww.SqlUpdateBuild UpdateDiy(int Idx) => new cd.DAL.Cccccdddwww.SqlUpdateBuild(new List<CccccdddwwwInfo> { new CccccdddwwwInfo { Idx = Idx } });
		public static cd.DAL.Cccccdddwww.SqlUpdateBuild UpdateDiy(List<CccccdddwwwInfo> dataSource) => new cd.DAL.Cccccdddwww.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Cccccdddwww.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Cccccdddwww.SqlUpdateBuild();

		public static CccccdddwwwInfo Insert(int? Idx, string Name) {
			return Insert(new CccccdddwwwInfo {
				Idx = Idx, 
				Name = Name});
		}
		public static CccccdddwwwInfo Insert(CccccdddwwwInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		public static int Insert(IEnumerable<CccccdddwwwInfo> items) {
			var affrows = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(items);
			return affrows;
		}
		internal static void RemoveCache(CccccdddwwwInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<CccccdddwwwInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Cccccdddwww:", item.Idx);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static CccccdddwwwInfo GetItem(int Idx) => SqlHelper.CacheShell(string.Concat("cd_BLL:Cccccdddwww:", Idx), itemCacheTimeout, () => Select.WhereIdx(Idx).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : CccccdddwwwInfo.Parse(str));

		public static List<CccccdddwwwInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(int Idx) {
			var affrows = await dal.DeleteAsync(Idx);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new CccccdddwwwInfo { Idx = Idx });
			return affrows;
		}
		async public static Task<CccccdddwwwInfo> GetItemAsync(int Idx) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Cccccdddwww:", Idx), itemCacheTimeout, () => Select.WhereIdx(Idx).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : CccccdddwwwInfo.Parse(str));
		public static Task<int> UpdateAsync(CccccdddwwwInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(CccccdddwwwInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<CccccdddwwwInfo> InsertAsync(int? Idx, string Name) {
			return InsertAsync(new CccccdddwwwInfo {
				Idx = Idx, 
				Name = Name});
		}
		async public static Task<CccccdddwwwInfo> InsertAsync(CccccdddwwwInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		async public static Task<int> InsertAsync(IEnumerable<CccccdddwwwInfo> items) {
			var affrows = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(items);
			return affrows;
		}
		internal static Task RemoveCacheAsync(CccccdddwwwInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<CccccdddwwwInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Cccccdddwww:", item.Idx);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<CccccdddwwwInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<CccccdddwwwInfo, SelectBuild> {
			public SelectBuild WhereIdx(params int[] Idx) => this.Where1Or("a.`Idx` = {0}", Idx);
			public SelectBuild WhereName(params string[] Name) => this.Where1Or("a.`name` = {0}", Name);
			public SelectBuild WhereNameLike(string pattern, bool isNotLike = false) => this.Where($@"a.`name` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}