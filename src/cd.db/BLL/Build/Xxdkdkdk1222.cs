using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Xxdkdkdk1222 {

		protected static readonly cd.DAL.Xxdkdkdk1222 dal = new cd.DAL.Xxdkdkdk1222();
		protected static readonly int itemCacheTimeout;

		static Xxdkdkdk1222() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Xxdkdkdk1222"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int Id22dd) {
			var affrows = dal.Delete(Id22dd);
			if (itemCacheTimeout > 0) RemoveCache(new Xxdkdkdk1222Info { Id22dd = Id22dd });
			return affrows;
		}

		#region enum _
		public enum _ {
			Id22dd = 1, 
			Name
		}
		#endregion

		public static int Update(Xxdkdkdk1222Info item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(Xxdkdkdk1222Info item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Xxdkdkdk1222.SqlUpdateBuild UpdateDiy(int Id22dd) => new cd.DAL.Xxdkdkdk1222.SqlUpdateBuild(new List<Xxdkdkdk1222Info> { new Xxdkdkdk1222Info { Id22dd = Id22dd } });
		public static cd.DAL.Xxdkdkdk1222.SqlUpdateBuild UpdateDiy(List<Xxdkdkdk1222Info> dataSource) => new cd.DAL.Xxdkdkdk1222.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Xxdkdkdk1222.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Xxdkdkdk1222.SqlUpdateBuild();

		public static Xxdkdkdk1222Info Insert(string Name) {
			return Insert(new Xxdkdkdk1222Info {
				Name = Name});
		}
		public static Xxdkdkdk1222Info Insert(Xxdkdkdk1222Info item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(Xxdkdkdk1222Info item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<Xxdkdkdk1222Info> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Xxdkdkdk1222:", item.Id22dd);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static Xxdkdkdk1222Info GetItem(int Id22dd) => SqlHelper.CacheShell(string.Concat("cd_BLL:Xxdkdkdk1222:", Id22dd), itemCacheTimeout, () => Select.WhereId22dd(Id22dd).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Xxdkdkdk1222Info.Parse(str));

		public static List<Xxdkdkdk1222Info> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(int Id22dd) {
			var affrows = await dal.DeleteAsync(Id22dd);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new Xxdkdkdk1222Info { Id22dd = Id22dd });
			return affrows;
		}
		async public static Task<Xxdkdkdk1222Info> GetItemAsync(int Id22dd) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Xxdkdkdk1222:", Id22dd), itemCacheTimeout, () => Select.WhereId22dd(Id22dd).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Xxdkdkdk1222Info.Parse(str));
		public static Task<int> UpdateAsync(Xxdkdkdk1222Info item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(Xxdkdkdk1222Info item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<Xxdkdkdk1222Info> InsertAsync(string Name) {
			return InsertAsync(new Xxdkdkdk1222Info {
				Name = Name});
		}
		async public static Task<Xxdkdkdk1222Info> InsertAsync(Xxdkdkdk1222Info item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		internal static Task RemoveCacheAsync(Xxdkdkdk1222Info item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<Xxdkdkdk1222Info> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Xxdkdkdk1222:", item.Id22dd);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<Xxdkdkdk1222Info>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<Xxdkdkdk1222Info, SelectBuild> {
			public SelectBuild WhereId22dd(params int[] Id22dd) => this.Where1Or("a.`Id22dd` = {0}", Id22dd);
			public SelectBuild WhereName(params string[] Name) => this.Where1Or("a.`Name` = {0}", Name);
			public SelectBuild WhereNameLike(string pattern, bool isNotLike = false) => this.Where($@"a.`Name` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}