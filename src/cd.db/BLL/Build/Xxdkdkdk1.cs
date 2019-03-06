using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Xxdkdkdk1 {

		protected static readonly cd.DAL.Xxdkdkdk1 dal = new cd.DAL.Xxdkdkdk1();
		protected static readonly int itemCacheTimeout;

		static Xxdkdkdk1() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Xxdkdkdk1"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int Id22) {
			var affrows = dal.Delete(Id22);
			if (itemCacheTimeout > 0) RemoveCache(new Xxdkdkdk1Info { Id22 = Id22 });
			return affrows;
		}

		#region enum _
		public enum _ {
			Id22 = 1, 
			Id, 
			Name
		}
		#endregion

		public static int Update(Xxdkdkdk1Info item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(Xxdkdkdk1Info item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Xxdkdkdk1.SqlUpdateBuild UpdateDiy(int Id22) => new cd.DAL.Xxdkdkdk1.SqlUpdateBuild(new List<Xxdkdkdk1Info> { new Xxdkdkdk1Info { Id22 = Id22 } });
		public static cd.DAL.Xxdkdkdk1.SqlUpdateBuild UpdateDiy(List<Xxdkdkdk1Info> dataSource) => new cd.DAL.Xxdkdkdk1.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Xxdkdkdk1.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Xxdkdkdk1.SqlUpdateBuild();

		public static Xxdkdkdk1Info Insert(int? Id, string Name) {
			return Insert(new Xxdkdkdk1Info {
				Id = Id, 
				Name = Name});
		}
		public static Xxdkdkdk1Info Insert(Xxdkdkdk1Info item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(Xxdkdkdk1Info item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<Xxdkdkdk1Info> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Xxdkdkdk1:", item.Id22);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static Xxdkdkdk1Info GetItem(int Id22) => SqlHelper.CacheShell(string.Concat("cd_BLL:Xxdkdkdk1:", Id22), itemCacheTimeout, () => Select.WhereId22(Id22).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Xxdkdkdk1Info.Parse(str));

		public static List<Xxdkdkdk1Info> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(int Id22) {
			var affrows = await dal.DeleteAsync(Id22);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new Xxdkdkdk1Info { Id22 = Id22 });
			return affrows;
		}
		async public static Task<Xxdkdkdk1Info> GetItemAsync(int Id22) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Xxdkdkdk1:", Id22), itemCacheTimeout, () => Select.WhereId22(Id22).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Xxdkdkdk1Info.Parse(str));
		public static Task<int> UpdateAsync(Xxdkdkdk1Info item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(Xxdkdkdk1Info item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<Xxdkdkdk1Info> InsertAsync(int? Id, string Name) {
			return InsertAsync(new Xxdkdkdk1Info {
				Id = Id, 
				Name = Name});
		}
		async public static Task<Xxdkdkdk1Info> InsertAsync(Xxdkdkdk1Info item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		internal static Task RemoveCacheAsync(Xxdkdkdk1Info item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<Xxdkdkdk1Info> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Xxdkdkdk1:", item.Id22);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<Xxdkdkdk1Info>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<Xxdkdkdk1Info, SelectBuild> {
			public SelectBuild WhereId22(params int[] Id22) => this.Where1Or("a.`Id22` = {0}", Id22);
			public SelectBuild WhereId(params int?[] Id) => this.Where1Or("a.`Id` = {0}", Id);
			public SelectBuild WhereName(params string[] Name) => this.Where1Or("a.`name` = {0}", Name);
			public SelectBuild WhereNameLike(string pattern, bool isNotLike = false) => this.Where($@"a.`name` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}