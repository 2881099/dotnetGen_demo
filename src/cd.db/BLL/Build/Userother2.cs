using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Userother2 {

		protected static readonly cd.DAL.Userother2 dal = new cd.DAL.Userother2();
		protected static readonly int itemCacheTimeout;

		static Userother2() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Userother2"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(long Userother_id) {
			var affrows = dal.Delete(Userother_id);
			if (itemCacheTimeout > 0) RemoveCache(new Userother2Info { Userother_id = Userother_id });
			return affrows;
		}
		public static int DeleteByUserother_id(long? Userother_id) {
			return dal.DeleteByUserother_id(Userother_id);
		}

		#region enum _
		public enum _ {
			Userother_id = 1, 
			Chinesename, 
			Xxxx
		}
		#endregion

		public static int Update(Userother2Info item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(Userother2Info item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Userother2.SqlUpdateBuild UpdateDiy(long Userother_id) => new cd.DAL.Userother2.SqlUpdateBuild(new List<Userother2Info> { new Userother2Info { Userother_id = Userother_id } });
		public static cd.DAL.Userother2.SqlUpdateBuild UpdateDiy(List<Userother2Info> dataSource) => new cd.DAL.Userother2.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Userother2.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Userother2.SqlUpdateBuild();

		public static Userother2Info Insert(long? Userother_id, string Chinesename, string Xxxx) {
			return Insert(new Userother2Info {
				Userother_id = Userother_id, 
				Chinesename = Chinesename, 
				Xxxx = Xxxx});
		}
		public static Userother2Info Insert(Userother2Info item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		public static int Insert(IEnumerable<Userother2Info> items) {
			var affrows = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(items);
			return affrows;
		}
		internal static void RemoveCache(Userother2Info item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<Userother2Info> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Userother2_", item.Userother_id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static Userother2Info GetItem(long Userother_id) => SqlHelper.CacheShell(string.Concat("cd_BLL_Userother2_", Userother_id), itemCacheTimeout, () => Select.WhereUserother_id(Userother_id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Userother2Info.Parse(str));

		public static List<Userother2Info> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static List<Userother2Info> GetItemsByUserother_id(params long?[] Userother_id) => Select.WhereUserother_id(Userother_id).ToList();
		public static List<Userother2Info> GetItemsByUserother_id(long?[] Userother_id, int limit) => Select.WhereUserother_id(Userother_id).Limit(limit).ToList();
		public static SelectBuild SelectByUserother_id(params long?[] Userother_id) => Select.WhereUserother_id(Userother_id);

		#region async
		public static Task<int> DeleteByUserother_idAsync(long? Userother_id) {
			return dal.DeleteByUserother_idAsync(Userother_id);
		}
		async public static Task<int> DeleteAsync(long Userother_id) {
			var affrows = await dal.DeleteAsync(Userother_id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new Userother2Info { Userother_id = Userother_id });
			return affrows;
		}
		async public static Task<Userother2Info> GetItemAsync(long Userother_id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL_Userother2_", Userother_id), itemCacheTimeout, () => Select.WhereUserother_id(Userother_id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Userother2Info.Parse(str));
		public static Task<int> UpdateAsync(Userother2Info item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(Userother2Info item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<Userother2Info> InsertAsync(long? Userother_id, string Chinesename, string Xxxx) {
			return InsertAsync(new Userother2Info {
				Userother_id = Userother_id, 
				Chinesename = Chinesename, 
				Xxxx = Xxxx});
		}
		async public static Task<Userother2Info> InsertAsync(Userother2Info item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		async public static Task<int> InsertAsync(IEnumerable<Userother2Info> items) {
			var affrows = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(items);
			return affrows;
		}
		internal static Task RemoveCacheAsync(Userother2Info item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<Userother2Info> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Userother2_", item.Userother_id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<Userother2Info>> GetItemsAsync() => Select.ToListAsync();
		public static Task<List<Userother2Info>> GetItemsByUserother_idAsync(params long?[] Userother_id) => Select.WhereUserother_id(Userother_id).ToListAsync();
		public static Task<List<Userother2Info>> GetItemsByUserother_idAsync(long?[] Userother_id, int limit) => Select.WhereUserother_id(Userother_id).Limit(limit).ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<Userother2Info, SelectBuild> {
			public SelectBuild WhereUserother_id(params long?[] Userother_id) => this.Where1Or("a.`userother_id` = {0}", Userother_id);
			public SelectBuild WhereUserother_id(Userother.SelectBuild select, bool isNotIn = false) => this.Where($"a.`userother_id` {(isNotIn ? "NOT IN" : "IN")} ({select.ToString("`id`")})");
			public SelectBuild WhereChinesename(params string[] Chinesename) => this.Where1Or("a.`chinesename` = {0}", Chinesename);
			public SelectBuild WhereChinesenameLike(string pattern, bool isNotLike = false) => this.Where($@"a.`chinesename` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild WhereXxxx(params string[] Xxxx) => this.Where1Or("a.`xxxx` = {0}", Xxxx);
			public SelectBuild WhereXxxxLike(string pattern, bool isNotLike = false) => this.Where($@"a.`xxxx` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}