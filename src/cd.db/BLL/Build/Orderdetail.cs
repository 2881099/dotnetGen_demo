using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Orderdetail {

		protected static readonly cd.DAL.Orderdetail dal = new cd.DAL.Orderdetail();
		protected static readonly int itemCacheTimeout;

		static Orderdetail() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Orderdetail"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int DetailId) {
			var affrows = dal.Delete(DetailId);
			if (itemCacheTimeout > 0) RemoveCache(new OrderdetailInfo { DetailId = DetailId });
			return affrows;
		}

		#region enum _
		public enum _ {
			DetailId = 1, 
			OrderId
		}
		#endregion

		public static int Update(OrderdetailInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(OrderdetailInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Orderdetail.SqlUpdateBuild UpdateDiy(int DetailId) => new cd.DAL.Orderdetail.SqlUpdateBuild(new List<OrderdetailInfo> { new OrderdetailInfo { DetailId = DetailId } });
		public static cd.DAL.Orderdetail.SqlUpdateBuild UpdateDiy(List<OrderdetailInfo> dataSource) => new cd.DAL.Orderdetail.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Orderdetail.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Orderdetail.SqlUpdateBuild();

		public static OrderdetailInfo Insert(int? DetailId, int? OrderId) {
			return Insert(new OrderdetailInfo {
				DetailId = DetailId, 
				OrderId = OrderId});
		}
		public static OrderdetailInfo Insert(OrderdetailInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		public static int Insert(IEnumerable<OrderdetailInfo> items) {
			var affrows = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(items);
			return affrows;
		}
		internal static void RemoveCache(OrderdetailInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<OrderdetailInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Orderdetail:", item.DetailId);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static OrderdetailInfo GetItem(int DetailId) => SqlHelper.CacheShell(string.Concat("cd_BLL:Orderdetail:", DetailId), itemCacheTimeout, () => Select.WhereDetailId(DetailId).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : OrderdetailInfo.Parse(str));

		public static List<OrderdetailInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(int DetailId) {
			var affrows = await dal.DeleteAsync(DetailId);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new OrderdetailInfo { DetailId = DetailId });
			return affrows;
		}
		async public static Task<OrderdetailInfo> GetItemAsync(int DetailId) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Orderdetail:", DetailId), itemCacheTimeout, () => Select.WhereDetailId(DetailId).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : OrderdetailInfo.Parse(str));
		public static Task<int> UpdateAsync(OrderdetailInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(OrderdetailInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<OrderdetailInfo> InsertAsync(int? DetailId, int? OrderId) {
			return InsertAsync(new OrderdetailInfo {
				DetailId = DetailId, 
				OrderId = OrderId});
		}
		async public static Task<OrderdetailInfo> InsertAsync(OrderdetailInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		async public static Task<int> InsertAsync(IEnumerable<OrderdetailInfo> items) {
			var affrows = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(items);
			return affrows;
		}
		internal static Task RemoveCacheAsync(OrderdetailInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<OrderdetailInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Orderdetail:", item.DetailId);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<OrderdetailInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<OrderdetailInfo, SelectBuild> {
			public SelectBuild WhereDetailId(params int[] DetailId) => this.Where1Or("a.`DetailId` = {0}", DetailId);
			public SelectBuild WhereOrderId(params int?[] OrderId) => this.Where1Or("a.`OrderId` = {0}", OrderId);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}