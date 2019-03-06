using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Nullaggretesttable {

		protected static readonly cd.DAL.Nullaggretesttable dal = new cd.DAL.Nullaggretesttable();
		protected static readonly int itemCacheTimeout;

		static Nullaggretesttable() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Nullaggretesttable"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int Id) {
			var affrows = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(new NullaggretesttableInfo { Id = Id });
			return affrows;
		}

		#region enum _
		public enum _ {
			Id = 1
		}
		#endregion

		public static int Update(NullaggretesttableInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(NullaggretesttableInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Nullaggretesttable.SqlUpdateBuild UpdateDiy(int Id) => new cd.DAL.Nullaggretesttable.SqlUpdateBuild(new List<NullaggretesttableInfo> { new NullaggretesttableInfo { Id = Id } });
		public static cd.DAL.Nullaggretesttable.SqlUpdateBuild UpdateDiy(List<NullaggretesttableInfo> dataSource) => new cd.DAL.Nullaggretesttable.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Nullaggretesttable.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Nullaggretesttable.SqlUpdateBuild();

		public static NullaggretesttableInfo Insert() {
			return Insert(new NullaggretesttableInfo {});
		}
		public static NullaggretesttableInfo Insert(NullaggretesttableInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(NullaggretesttableInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<NullaggretesttableInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Nullaggretesttable:", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static NullaggretesttableInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("cd_BLL:Nullaggretesttable:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : NullaggretesttableInfo.Parse(str));

		public static List<NullaggretesttableInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(int Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new NullaggretesttableInfo { Id = Id });
			return affrows;
		}
		async public static Task<NullaggretesttableInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Nullaggretesttable:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : NullaggretesttableInfo.Parse(str));
		public static Task<int> UpdateAsync(NullaggretesttableInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(NullaggretesttableInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<NullaggretesttableInfo> InsertAsync() {
			return InsertAsync(new NullaggretesttableInfo {});
		}
		async public static Task<NullaggretesttableInfo> InsertAsync(NullaggretesttableInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		internal static Task RemoveCacheAsync(NullaggretesttableInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<NullaggretesttableInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Nullaggretesttable:", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<NullaggretesttableInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<NullaggretesttableInfo, SelectBuild> {
			public SelectBuild WhereId(params int[] Id) => this.Where1Or("a.`Id` = {0}", Id);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}