using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Topic {

		protected static readonly cd.DAL.Topic dal = new cd.DAL.Topic();
		protected static readonly int itemCacheTimeout;

		static Topic() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Topic"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(uint Id) {
			var affrows = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(new TopicInfo { Id = Id });
			return affrows;
		}

		#region enum _
		public enum _ {
			Id = 1, 
			/// <summary>
			/// 卡片渲染数据
			/// </summary>
			Carddata, 
			/// <summary>
			/// 卡片类型
			/// </summary>
			Cardtype, 
			/// <summary>
			/// 点击次数
			/// </summary>
			Clicks, 
			/// <summary>
			/// 内容
			/// </summary>
			Content, 
			/// <summary>
			/// 创建时间
			/// </summary>
			Create_time, 
			/// <summary>
			/// 排序时间
			/// </summary>
			Order_time, 
			/// <summary>
			/// 测试添加的字段
			/// 
			/// 换行
			/// 
			/// sdgsdg
			/// </summary>
			Test_addfiled, 
			/// <summary>
			/// 标题
			/// </summary>
			Title, 
			/// <summary>
			/// 修改时间
			/// </summary>
			Update_time
		}
		#endregion

		public static int Update(TopicInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(TopicInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Topic.SqlUpdateBuild UpdateDiy(uint Id) => new cd.DAL.Topic.SqlUpdateBuild(new List<TopicInfo> { new TopicInfo { Id = Id } });
		public static cd.DAL.Topic.SqlUpdateBuild UpdateDiy(List<TopicInfo> dataSource) => new cd.DAL.Topic.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Topic.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Topic.SqlUpdateBuild();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Topic.Insert(TopicInfo item)
		/// </summary>
		[Obsolete]
		public static TopicInfo Insert(string Carddata, TopicCARDTYPE? Cardtype, ulong? Clicks, string Content, DateTime? Order_time, byte? Test_addfiled, string Title) {
			return Insert(new TopicInfo {
				Carddata = Carddata, 
				Cardtype = Cardtype, 
				Clicks = Clicks, 
				Content = Content, 
				Order_time = Order_time, 
				Test_addfiled = Test_addfiled, 
				Title = Title});
		}
		public static TopicInfo Insert(TopicInfo item) {
			if (item.Create_time == null) item.Create_time = DateTime.Now;
			if (item.Update_time == null) item.Update_time = DateTime.Now;
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(TopicInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<TopicInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Topic_", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static TopicInfo GetItem(uint Id) => SqlHelper.CacheShell(string.Concat("cd_BLL_Topic_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TopicInfo.Parse(str));

		public static List<TopicInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(uint Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new TopicInfo { Id = Id });
			return affrows;
		}
		async public static Task<TopicInfo> GetItemAsync(uint Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL_Topic_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TopicInfo.Parse(str));
		public static Task<int> UpdateAsync(TopicInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(TopicInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Topic.Insert(TopicInfo item)
		/// </summary>
		[Obsolete]
		public static Task<TopicInfo> InsertAsync(string Carddata, TopicCARDTYPE? Cardtype, ulong? Clicks, string Content, DateTime? Order_time, byte? Test_addfiled, string Title) {
			return InsertAsync(new TopicInfo {
				Carddata = Carddata, 
				Cardtype = Cardtype, 
				Clicks = Clicks, 
				Content = Content, 
				Order_time = Order_time, 
				Test_addfiled = Test_addfiled, 
				Title = Title});
		}
		async public static Task<TopicInfo> InsertAsync(TopicInfo item) {
			if (item.Create_time == null) item.Create_time = DateTime.Now;
			if (item.Update_time == null) item.Update_time = DateTime.Now;
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		internal static Task RemoveCacheAsync(TopicInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<TopicInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Topic_", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<TopicInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<TopicInfo, SelectBuild> {
			public SelectBuild WhereId(params uint[] Id) => this.Where1Or("a.`id` = {0}", Id);
			public SelectBuild WhereCarddataLike(string pattern, bool isNotLike = false) => this.Where($@"a.`carddata` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild WhereCardtype_IN(params TopicCARDTYPE?[] Cardtypes) => this.Where1Or("a.`cardtype` = {0}", Cardtypes);
			/// <summary>
			/// 卡片类型，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereCardtype(TopicCARDTYPE Cardtype1) => this.WhereCardtype_IN(Cardtype1);
			#region WhereCardtype
			public SelectBuild WhereCardtype(TopicCARDTYPE Cardtype1, TopicCARDTYPE Cardtype2) => this.WhereCardtype_IN(Cardtype1, Cardtype2);
			public SelectBuild WhereCardtype(TopicCARDTYPE Cardtype1, TopicCARDTYPE Cardtype2, TopicCARDTYPE Cardtype3) => this.WhereCardtype_IN(Cardtype1, Cardtype2, Cardtype3);
			public SelectBuild WhereCardtype(TopicCARDTYPE Cardtype1, TopicCARDTYPE Cardtype2, TopicCARDTYPE Cardtype3, TopicCARDTYPE Cardtype4) => this.WhereCardtype_IN(Cardtype1, Cardtype2, Cardtype3, Cardtype4);
			public SelectBuild WhereCardtype(TopicCARDTYPE Cardtype1, TopicCARDTYPE Cardtype2, TopicCARDTYPE Cardtype3, TopicCARDTYPE Cardtype4, TopicCARDTYPE Cardtype5) => this.WhereCardtype_IN(Cardtype1, Cardtype2, Cardtype3, Cardtype4, Cardtype5);
			#endregion
			/// <summary>
			/// 点击次数，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereClicks(params ulong?[] Clicks) => this.Where1Or("a.`clicks` = {0}", Clicks);
			public SelectBuild WhereContentLike(string pattern, bool isNotLike = false) => this.Where($@"a.`content` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild WhereCreate_timeRange(DateTime? begin) => base.Where("a.`create_time` >= {0}", begin);
			public SelectBuild WhereCreate_timeRange(DateTime? begin, DateTime? end) => end == null ? WhereCreate_timeRange(begin) : base.Where("a.`create_time` between {0} and {1}", begin, end);
			public SelectBuild WhereOrder_timeRange(DateTime? begin) => base.Where("a.`order_time` >= {0}", begin);
			public SelectBuild WhereOrder_timeRange(DateTime? begin, DateTime? end) => end == null ? WhereOrder_timeRange(begin) : base.Where("a.`order_time` between {0} and {1}", begin, end);
			/// <summary>
			/// 测试添加的字段
			/// 
			/// 换行
			/// 
			/// sdgsdg，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereTest_addfiled(params byte?[] Test_addfiled) => this.Where1Or("a.`test_addfiled` = {0}", Test_addfiled);
			/// <summary>
			/// 标题，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereTitle(params string[] Title) => this.Where1Or("a.`title` = {0}", Title);
			public SelectBuild WhereTitleLike(string pattern, bool isNotLike = false) => this.Where($@"a.`title` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild WhereUpdate_timeRange(DateTime? begin) => base.Where("a.`update_time` >= {0}", begin);
			public SelectBuild WhereUpdate_timeRange(DateTime? begin, DateTime? end) => end == null ? WhereUpdate_timeRange(begin) : base.Where("a.`update_time` between {0} and {1}", begin, end);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}