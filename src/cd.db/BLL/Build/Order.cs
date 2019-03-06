using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Order {

		protected static readonly cd.DAL.Order dal = new cd.DAL.Order();
		protected static readonly int itemCacheTimeout;

		static Order() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Order"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int OrderID) {
			var affrows = dal.Delete(OrderID);
			if (itemCacheTimeout > 0) RemoveCache(new OrderInfo { OrderID = OrderID });
			return affrows;
		}

		#region enum _
		public enum _ {
			OrderID = 1, 
			CustomerName, 
			OrderTitle, 
			TransactionDate
		}
		#endregion

		public static int Update(OrderInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(OrderInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Order.SqlUpdateBuild UpdateDiy(int OrderID) => new cd.DAL.Order.SqlUpdateBuild(new List<OrderInfo> { new OrderInfo { OrderID = OrderID } });
		public static cd.DAL.Order.SqlUpdateBuild UpdateDiy(List<OrderInfo> dataSource) => new cd.DAL.Order.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Order.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Order.SqlUpdateBuild();

		public static OrderInfo Insert(int? OrderID, string CustomerName, string OrderTitle, DateTime? TransactionDate) {
			return Insert(new OrderInfo {
				OrderID = OrderID, 
				CustomerName = CustomerName, 
				OrderTitle = OrderTitle, 
				TransactionDate = TransactionDate});
		}
		public static OrderInfo Insert(OrderInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		public static int Insert(IEnumerable<OrderInfo> items) {
			var affrows = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(items);
			return affrows;
		}
		internal static void RemoveCache(OrderInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<OrderInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Order:", item.OrderID);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static OrderInfo GetItem(int OrderID) => SqlHelper.CacheShell(string.Concat("cd_BLL:Order:", OrderID), itemCacheTimeout, () => Select.WhereOrderID(OrderID).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : OrderInfo.Parse(str));

		public static List<OrderInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(int OrderID) {
			var affrows = await dal.DeleteAsync(OrderID);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new OrderInfo { OrderID = OrderID });
			return affrows;
		}
		async public static Task<OrderInfo> GetItemAsync(int OrderID) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Order:", OrderID), itemCacheTimeout, () => Select.WhereOrderID(OrderID).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : OrderInfo.Parse(str));
		public static Task<int> UpdateAsync(OrderInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(OrderInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<OrderInfo> InsertAsync(int? OrderID, string CustomerName, string OrderTitle, DateTime? TransactionDate) {
			return InsertAsync(new OrderInfo {
				OrderID = OrderID, 
				CustomerName = CustomerName, 
				OrderTitle = OrderTitle, 
				TransactionDate = TransactionDate});
		}
		async public static Task<OrderInfo> InsertAsync(OrderInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="items">集合</param>
		/// <returns>影响的行数</returns>
		async public static Task<int> InsertAsync(IEnumerable<OrderInfo> items) {
			var affrows = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(items);
			return affrows;
		}
		internal static Task RemoveCacheAsync(OrderInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<OrderInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Order:", item.OrderID);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<OrderInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<OrderInfo, SelectBuild> {
			public SelectBuild WhereOrderID(params int[] OrderID) => this.Where1Or("a.`OrderID` = {0}", OrderID);
			public SelectBuild WhereCustomerName(params string[] CustomerName) => this.Where1Or("a.`CustomerName` = {0}", CustomerName);
			public SelectBuild WhereCustomerNameLike(string pattern, bool isNotLike = false) => this.Where($@"a.`CustomerName` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereOrderTitle(params string[] OrderTitle) => this.Where1Or("a.`OrderTitle` = {0}", OrderTitle);
			public SelectBuild WhereOrderTitleLike(string pattern, bool isNotLike = false) => this.Where($@"a.`OrderTitle` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereTransactionDateRange(DateTime? begin) => base.Where("a.`TransactionDate` >= {0}", begin);
			public SelectBuild WhereTransactionDateRange(DateTime? begin, DateTime? end) => end == null ? WhereTransactionDateRange(begin) : base.Where("a.`TransactionDate` between {0} and {1}", begin, end);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}