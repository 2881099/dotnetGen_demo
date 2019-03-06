using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Tb_topic111333 {

		protected static readonly cd.DAL.Tb_topic111333 dal = new cd.DAL.Tb_topic111333();
		protected static readonly int itemCacheTimeout;

		static Tb_topic111333() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Tb_topic111333"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int Id) {
			var affrows = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(new Tb_topic111333Info { Id = Id });
			return affrows;
		}

		#region enum _
		public enum _ {
			Id = 1, 
			Clicks, 
			CreateTime, 
			TestTypeInfoGuid, 
			Title
		}
		#endregion

		public static int Update(Tb_topic111333Info item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(Tb_topic111333Info item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Tb_topic111333.SqlUpdateBuild UpdateDiy(int Id) => new cd.DAL.Tb_topic111333.SqlUpdateBuild(new List<Tb_topic111333Info> { new Tb_topic111333Info { Id = Id } });
		public static cd.DAL.Tb_topic111333.SqlUpdateBuild UpdateDiy(List<Tb_topic111333Info> dataSource) => new cd.DAL.Tb_topic111333.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Tb_topic111333.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Tb_topic111333.SqlUpdateBuild();

		public static Tb_topic111333Info Insert(int? Clicks, DateTime? CreateTime, int? TestTypeInfoGuid, string Title) {
			return Insert(new Tb_topic111333Info {
				Clicks = Clicks, 
				CreateTime = CreateTime, 
				TestTypeInfoGuid = TestTypeInfoGuid, 
				Title = Title});
		}
		public static Tb_topic111333Info Insert(Tb_topic111333Info item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(Tb_topic111333Info item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<Tb_topic111333Info> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Tb_topic111333:", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static Tb_topic111333Info GetItem(int Id) => SqlHelper.CacheShell(string.Concat("cd_BLL:Tb_topic111333:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Tb_topic111333Info.Parse(str));

		public static List<Tb_topic111333Info> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(int Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new Tb_topic111333Info { Id = Id });
			return affrows;
		}
		async public static Task<Tb_topic111333Info> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Tb_topic111333:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Tb_topic111333Info.Parse(str));
		public static Task<int> UpdateAsync(Tb_topic111333Info item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(Tb_topic111333Info item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<Tb_topic111333Info> InsertAsync(int? Clicks, DateTime? CreateTime, int? TestTypeInfoGuid, string Title) {
			return InsertAsync(new Tb_topic111333Info {
				Clicks = Clicks, 
				CreateTime = CreateTime, 
				TestTypeInfoGuid = TestTypeInfoGuid, 
				Title = Title});
		}
		async public static Task<Tb_topic111333Info> InsertAsync(Tb_topic111333Info item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		internal static Task RemoveCacheAsync(Tb_topic111333Info item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<Tb_topic111333Info> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Tb_topic111333:", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<Tb_topic111333Info>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<Tb_topic111333Info, SelectBuild> {
			public SelectBuild WhereId(params int[] Id) => this.Where1Or("a.`Id` = {0}", Id);
			public SelectBuild WhereClicks(params int?[] Clicks) => this.Where1Or("a.`Clicks` = {0}", Clicks);
			public SelectBuild WhereCreateTimeRange(DateTime? begin) => base.Where("a.`CreateTime` >= {0}", begin);
			public SelectBuild WhereCreateTimeRange(DateTime? begin, DateTime? end) => end == null ? WhereCreateTimeRange(begin) : base.Where("a.`CreateTime` between {0} and {1}", begin, end);
			public SelectBuild WhereTestTypeInfoGuid(params int?[] TestTypeInfoGuid) => this.Where1Or("a.`TestTypeInfoGuid` = {0}", TestTypeInfoGuid);
			public SelectBuild WhereTitle(params string[] Title) => this.Where1Or("a.`Title` = {0}", Title);
			public SelectBuild WhereTitleLike(string pattern, bool isNotLike = false) => this.Where($@"a.`Title` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}