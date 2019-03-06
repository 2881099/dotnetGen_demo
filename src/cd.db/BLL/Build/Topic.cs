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
		public static int DeleteByTopic_type_id(int? Topic_type_id) {
			return dal.DeleteByTopic_type_id(Topic_type_id);
		}

		#region enum _
		public enum _ {
			Id = 1, 
			/// <summary>
			/// 类型id
			/// </summary>
			Topic_type_id, 
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
			/// set类型
			/// </summary>
			Test_setfield, 
			/// <summary>
			/// 标题
			/// </summary>
			Title, 
			/// <summary>
			/// 类型2
			/// </summary>
			Tyyp2_id, 
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
		public static TopicInfo Insert(int? Topic_type_id, string Carddata, TopicCARDTYPE? Cardtype, ulong? Clicks, string Content, DateTime? Order_time, byte? Test_addfiled, TopicTEST_SETFIELD? Test_setfield, string Title, int? Tyyp2_id) {
			return Insert(new TopicInfo {
				Topic_type_id = Topic_type_id, 
				Carddata = Carddata, 
				Cardtype = Cardtype, 
				Clicks = Clicks, 
				Content = Content, 
				Order_time = Order_time, 
				Test_addfiled = Test_addfiled, 
				Test_setfield = Test_setfield, 
				Title = Title, 
				Tyyp2_id = Tyyp2_id});
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
				keys[keysIdx++] = string.Concat("cd_BLL:Topic:", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static TopicInfo GetItem(uint Id) => SqlHelper.CacheShell(string.Concat("cd_BLL:Topic:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TopicInfo.Parse(str));

		public static List<TopicInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static List<TopicInfo> GetItemsByTopic_type_id(params int?[] Topic_type_id) => Select.WhereTopic_type_id(Topic_type_id).ToList();
		public static List<TopicInfo> GetItemsByTopic_type_id(int?[] Topic_type_id, int limit) => Select.WhereTopic_type_id(Topic_type_id).Limit(limit).ToList();
		public static SelectBuild SelectByTopic_type_id(params int?[] Topic_type_id) => Select.WhereTopic_type_id(Topic_type_id);

		#region async
		public static Task<int> DeleteByTopic_type_idAsync(int? Topic_type_id) {
			return dal.DeleteByTopic_type_idAsync(Topic_type_id);
		}
		async public static Task<int> DeleteAsync(uint Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new TopicInfo { Id = Id });
			return affrows;
		}
		async public static Task<TopicInfo> GetItemAsync(uint Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Topic:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : TopicInfo.Parse(str));
		public static Task<int> UpdateAsync(TopicInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(TopicInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Topic.Insert(TopicInfo item)
		/// </summary>
		[Obsolete]
		public static Task<TopicInfo> InsertAsync(int? Topic_type_id, string Carddata, TopicCARDTYPE? Cardtype, ulong? Clicks, string Content, DateTime? Order_time, byte? Test_addfiled, TopicTEST_SETFIELD? Test_setfield, string Title, int? Tyyp2_id) {
			return InsertAsync(new TopicInfo {
				Topic_type_id = Topic_type_id, 
				Carddata = Carddata, 
				Cardtype = Cardtype, 
				Clicks = Clicks, 
				Content = Content, 
				Order_time = Order_time, 
				Test_addfiled = Test_addfiled, 
				Test_setfield = Test_setfield, 
				Title = Title, 
				Tyyp2_id = Tyyp2_id});
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
				keys[keysIdx++] = string.Concat("cd_BLL:Topic:", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<TopicInfo>> GetItemsAsync() => Select.ToListAsync();
		public static Task<List<TopicInfo>> GetItemsByTopic_type_idAsync(params int?[] Topic_type_id) => Select.WhereTopic_type_id(Topic_type_id).ToListAsync();
		public static Task<List<TopicInfo>> GetItemsByTopic_type_idAsync(int?[] Topic_type_id, int limit) => Select.WhereTopic_type_id(Topic_type_id).Limit(limit).ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<TopicInfo, SelectBuild> {
			public SelectBuild WhereTopic_type_id(params int?[] Topic_type_id) => this.Where1Or("a.`topic_type_id` = {0}", Topic_type_id);
			public SelectBuild WhereTopic_type_id(Topic_type.SelectBuild select, bool isNotIn = false) => this.Where($"a.`topic_type_id` {(isNotIn ? "NOT IN" : "IN")} ({select.ToString("`id`")})");
			public SelectBuild WhereId(params uint[] Id) => this.Where1Or("a.`id` = {0}", Id);
			public SelectBuild WhereCarddataLike(string pattern, bool isNotLike = false) => this.Where($@"a.`carddata` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
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
			public SelectBuild WhereContentLike(string pattern, bool isNotLike = false) => this.Where($@"a.`content` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
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
			public SelectBuild WhereTest_setfield_IN(params TopicTEST_SETFIELD[] Test_setfields) => this.Where1Or("(a.`test_setfield` & {0}) = {0}", Test_setfields);
			/// <summary>
			/// set类型，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereTest_setfield(TopicTEST_SETFIELD Test_setfield1) => this.WhereTest_setfield_IN(Test_setfield1);
			#region WhereTest_setfield
			public SelectBuild WhereTest_setfield(TopicTEST_SETFIELD Test_setfield1, TopicTEST_SETFIELD Test_setfield2) => this.WhereTest_setfield_IN(Test_setfield1, Test_setfield2);
			public SelectBuild WhereTest_setfield(TopicTEST_SETFIELD Test_setfield1, TopicTEST_SETFIELD Test_setfield2, TopicTEST_SETFIELD Test_setfield3) => this.WhereTest_setfield_IN(Test_setfield1, Test_setfield2, Test_setfield3);
			public SelectBuild WhereTest_setfield(TopicTEST_SETFIELD Test_setfield1, TopicTEST_SETFIELD Test_setfield2, TopicTEST_SETFIELD Test_setfield3, TopicTEST_SETFIELD Test_setfield4) => this.WhereTest_setfield_IN(Test_setfield1, Test_setfield2, Test_setfield3, Test_setfield4);
			public SelectBuild WhereTest_setfield(TopicTEST_SETFIELD Test_setfield1, TopicTEST_SETFIELD Test_setfield2, TopicTEST_SETFIELD Test_setfield3, TopicTEST_SETFIELD Test_setfield4, TopicTEST_SETFIELD Test_setfield5) => this.WhereTest_setfield_IN(Test_setfield1, Test_setfield2, Test_setfield3, Test_setfield4, Test_setfield5);
			#endregion
			/// <summary>
			/// 标题，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereTitle(params string[] Title) => this.Where1Or("a.`title` = {0}", Title);
			public SelectBuild WhereTitleLike(string pattern, bool isNotLike = false) => this.Where($@"a.`title` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			/// <summary>
			/// 类型2，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereTyyp2_id(params int?[] Tyyp2_id) => this.Where1Or("a.`tyyp2_id` = {0}", Tyyp2_id);
			public SelectBuild WhereUpdate_timeRange(DateTime? begin) => base.Where("a.`update_time` >= {0}", begin);
			public SelectBuild WhereUpdate_timeRange(DateTime? begin, DateTime? end) => end == null ? WhereUpdate_timeRange(begin) : base.Where("a.`update_time` between {0} and {1}", begin, end);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}