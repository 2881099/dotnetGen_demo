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

		public static int Update(TopicInfo item) => dal.Update(item).ExecuteNonQuery();
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
		public static TopicSelectBuild Select => new TopicSelectBuild(dal);
		public static TopicSelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(uint Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new TopicInfo { Id = Id });
			return affrows;
		}
		async public static Task<TopicInfo> GetItemAsync(uint Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL_Topic_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TopicInfo.Parse(str));
		async public static Task<int> UpdateAsync(TopicInfo item) => await dal.Update(item).ExecuteNonQueryAsync();

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
		async internal static Task RemoveCacheAsync(TopicInfo item) => await RemoveCacheAsync(item == null ? null : new [] { item });
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
	}
	public partial class TopicSelectBuild : SelectBuild<TopicInfo, TopicSelectBuild> {
		public TopicSelectBuild WhereId(params uint[] Id) {
			return this.Where1Or("a.`id` = {0}", Id);
		}
		public TopicSelectBuild WhereCarddata(params string[] Carddata) {
			return this.Where1Or("a.`carddata` = {0}", Carddata);
		}
		public TopicSelectBuild WhereCarddataLike(params string[] Carddata) {
			if (Carddata == null || Carddata.Where(a => !string.IsNullOrEmpty(a)).Any() == false) return this;
			return this.Where1Or(@"a.`carddata` LIKE {0}", Carddata.Select(a => "%" + a + "%").ToArray());
		}
		public TopicSelectBuild WhereCardtype_IN(params TopicCARDTYPE?[] Cardtypes) {
			return this.Where1Or("a.`cardtype` = {0}", Cardtypes);
		}
		public TopicSelectBuild WhereCardtype(TopicCARDTYPE Cardtype1) {
			return this.WhereCardtype_IN(Cardtype1);
		}
		#region WhereCardtype
		public TopicSelectBuild WhereCardtype(TopicCARDTYPE Cardtype1, TopicCARDTYPE Cardtype2) {
			return this.WhereCardtype_IN(Cardtype1, Cardtype2);
		}
		public TopicSelectBuild WhereCardtype(TopicCARDTYPE Cardtype1, TopicCARDTYPE Cardtype2, TopicCARDTYPE Cardtype3) {
			return this.WhereCardtype_IN(Cardtype1, Cardtype2, Cardtype3);
		}
		public TopicSelectBuild WhereCardtype(TopicCARDTYPE Cardtype1, TopicCARDTYPE Cardtype2, TopicCARDTYPE Cardtype3, TopicCARDTYPE Cardtype4) {
			return this.WhereCardtype_IN(Cardtype1, Cardtype2, Cardtype3, Cardtype4);
		}
		public TopicSelectBuild WhereCardtype(TopicCARDTYPE Cardtype1, TopicCARDTYPE Cardtype2, TopicCARDTYPE Cardtype3, TopicCARDTYPE Cardtype4, TopicCARDTYPE Cardtype5) {
			return this.WhereCardtype_IN(Cardtype1, Cardtype2, Cardtype3, Cardtype4, Cardtype5);
		}
		#endregion
		public TopicSelectBuild WhereClicks(params ulong?[] Clicks) {
			return this.Where1Or("a.`clicks` = {0}", Clicks);
		}
		public TopicSelectBuild WhereContent(params string[] Content) {
			return this.Where1Or("a.`content` = {0}", Content);
		}
		public TopicSelectBuild WhereContentLike(params string[] Content) {
			if (Content == null || Content.Where(a => !string.IsNullOrEmpty(a)).Any() == false) return this;
			return this.Where1Or(@"a.`content` LIKE {0}", Content.Select(a => "%" + a + "%").ToArray());
		}
		public TopicSelectBuild WhereCreate_timeRange(DateTime? begin) {
			return base.Where("a.`create_time` >= {0}", begin);
		}
		public TopicSelectBuild WhereCreate_timeRange(DateTime? begin, DateTime? end) {
			if (end == null) return WhereCreate_timeRange(begin);
			return base.Where("a.`create_time` between {0} and {1}", begin, end);
		}
		public TopicSelectBuild WhereOrder_timeRange(DateTime? begin) {
			return base.Where("a.`order_time` >= {0}", begin);
		}
		public TopicSelectBuild WhereOrder_timeRange(DateTime? begin, DateTime? end) {
			if (end == null) return WhereOrder_timeRange(begin);
			return base.Where("a.`order_time` between {0} and {1}", begin, end);
		}
		public TopicSelectBuild WhereTest_addfiled(params byte?[] Test_addfiled) {
			return this.Where1Or("a.`test_addfiled` = {0}", Test_addfiled);
		}
		public TopicSelectBuild WhereTitle(params string[] Title) {
			return this.Where1Or("a.`title` = {0}", Title);
		}
		public TopicSelectBuild WhereTitleLike(params string[] Title) {
			if (Title == null || Title.Where(a => !string.IsNullOrEmpty(a)).Any() == false) return this;
			return this.Where1Or(@"a.`title` LIKE {0}", Title.Select(a => "%" + a + "%").ToArray());
		}
		public TopicSelectBuild WhereUpdate_timeRange(DateTime? begin) {
			return base.Where("a.`update_time` >= {0}", begin);
		}
		public TopicSelectBuild WhereUpdate_timeRange(DateTime? begin, DateTime? end) {
			if (end == null) return WhereUpdate_timeRange(begin);
			return base.Where("a.`update_time` between {0} and {1}", begin, end);
		}
		public TopicSelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
	}
}