using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Tb_topic {

		protected static readonly cd.DAL.Tb_topic dal = new cd.DAL.Tb_topic();
		protected static readonly int itemCacheTimeout;

		static Tb_topic() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Tb_topic"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int Id) {
			var affrows = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(new Tb_topicInfo { Id = Id });
			return affrows;
		}

		#region enum _
		public enum _ {
			Id = 1, 
			Clicks, 
			CreateTime, 
			Fusho, 
			TestTypeInfoGuid, 
			Title, 
			TypeGuid
		}
		#endregion

		public static int Update(Tb_topicInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(Tb_topicInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Tb_topic.SqlUpdateBuild UpdateDiy(int Id) => new cd.DAL.Tb_topic.SqlUpdateBuild(new List<Tb_topicInfo> { new Tb_topicInfo { Id = Id } });
		public static cd.DAL.Tb_topic.SqlUpdateBuild UpdateDiy(List<Tb_topicInfo> dataSource) => new cd.DAL.Tb_topic.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Tb_topic.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Tb_topic.SqlUpdateBuild();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Tb_topic.Insert(Tb_topicInfo item)
		/// </summary>
		[Obsolete]
		public static Tb_topicInfo Insert(int? Clicks, DateTime? CreateTime, ushort? Fusho, int? TestTypeInfoGuid, string Title, int? TypeGuid) {
			return Insert(new Tb_topicInfo {
				Clicks = Clicks, 
				CreateTime = CreateTime, 
				Fusho = Fusho, 
				TestTypeInfoGuid = TestTypeInfoGuid, 
				Title = Title, 
				TypeGuid = TypeGuid});
		}
		public static Tb_topicInfo Insert(Tb_topicInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(Tb_topicInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<Tb_topicInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Tb_topic:", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static Tb_topicInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("cd_BLL:Tb_topic:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Tb_topicInfo.Parse(str));

		public static List<Tb_topicInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(int Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new Tb_topicInfo { Id = Id });
			return affrows;
		}
		async public static Task<Tb_topicInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Tb_topic:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Tb_topicInfo.Parse(str));
		public static Task<int> UpdateAsync(Tb_topicInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(Tb_topicInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Tb_topic.Insert(Tb_topicInfo item)
		/// </summary>
		[Obsolete]
		public static Task<Tb_topicInfo> InsertAsync(int? Clicks, DateTime? CreateTime, ushort? Fusho, int? TestTypeInfoGuid, string Title, int? TypeGuid) {
			return InsertAsync(new Tb_topicInfo {
				Clicks = Clicks, 
				CreateTime = CreateTime, 
				Fusho = Fusho, 
				TestTypeInfoGuid = TestTypeInfoGuid, 
				Title = Title, 
				TypeGuid = TypeGuid});
		}
		async public static Task<Tb_topicInfo> InsertAsync(Tb_topicInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		internal static Task RemoveCacheAsync(Tb_topicInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<Tb_topicInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Tb_topic:", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<Tb_topicInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<Tb_topicInfo, SelectBuild> {
			public SelectBuild WhereId(params int[] Id) => this.Where1Or("a.`Id` = {0}", Id);
			public SelectBuild WhereClicks(params int?[] Clicks) => this.Where1Or("a.`Clicks` = {0}", Clicks);
			public SelectBuild WhereCreateTimeRange(DateTime? begin) => base.Where("a.`CreateTime` >= {0}", begin);
			public SelectBuild WhereCreateTimeRange(DateTime? begin, DateTime? end) => end == null ? WhereCreateTimeRange(begin) : base.Where("a.`CreateTime` between {0} and {1}", begin, end);
			public SelectBuild WhereFusho(params ushort?[] Fusho) => this.Where1Or("a.`fusho` = {0}", Fusho);
			public SelectBuild WhereTestTypeInfoGuid(params int?[] TestTypeInfoGuid) => this.Where1Or("a.`TestTypeInfoGuid` = {0}", TestTypeInfoGuid);
			public SelectBuild WhereTitle(params string[] Title) => this.Where1Or("a.`Title` = {0}", Title);
			public SelectBuild WhereTitleLike(string pattern, bool isNotLike = false) => this.Where($@"a.`Title` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereTypeGuid(params int?[] TypeGuid) => this.Where1Or("a.`TypeGuid` = {0}", TypeGuid);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}