using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Userother {

		protected static readonly cd.DAL.Userother dal = new cd.DAL.Userother();
		protected static readonly int itemCacheTimeout;

		static Userother() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Userother"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(string Userid) {
			var affrows = dal.Delete(Userid);
			if (itemCacheTimeout > 0) RemoveCache(GetItem(Userid));
			return affrows;
		}
		public static int DeleteById(long Id) {
			var affrows = dal.DeleteById(Id);
			if (itemCacheTimeout > 0) RemoveCache(GetItemById(Id));
			return affrows;
		}

		#region enum _
		public enum _ {
			Userid = 1, 
			Chinesename, 
			Created, 
			Doctype, 
			Englishname, 
			/// <summary>
			/// 是否验证
			/// </summary>
			Hasverify, 
			Id, 
			Idnumber, 
			Images
		}
		#endregion

		public static int Update(UserotherInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(UserotherInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Userother.SqlUpdateBuild UpdateDiy(string Userid) => new cd.DAL.Userother.SqlUpdateBuild(new List<UserotherInfo> { itemCacheTimeout > 0 ? new UserotherInfo { Userid = Userid,  } : GetItem(Userid) });
		public static cd.DAL.Userother.SqlUpdateBuild UpdateDiy(List<UserotherInfo> dataSource) => new cd.DAL.Userother.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Userother.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Userother.SqlUpdateBuild();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Userother.Insert(UserotherInfo item)
		/// </summary>
		[Obsolete]
		public static UserotherInfo Insert(string Userid, string Chinesename, DateTime? Created, string Doctype, string Englishname, bool? Hasverify, string Idnumber, string Images) {
			return Insert(new UserotherInfo {
				Userid = Userid, 
				Chinesename = Chinesename, 
				Created = Created, 
				Doctype = Doctype, 
				Englishname = Englishname, 
				Hasverify = Hasverify, 
				Idnumber = Idnumber, 
				Images = Images});
		}
		public static UserotherInfo Insert(UserotherInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(UserotherInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<UserotherInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 2];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Userother_", item.Userid);
				keys[keysIdx++] = string.Concat("cd_BLL_UserotherById_", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static UserotherInfo GetItem(string Userid) => SqlHelper.CacheShell(string.Concat("cd_BLL_Userother_", Userid), itemCacheTimeout, () => Select.WhereUserid(Userid).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : UserotherInfo.Parse(str));
		public static UserotherInfo GetItemById(long Id) => SqlHelper.CacheShell(string.Concat("cd_BLL_UserotherById_", Id), itemCacheTimeout, () => Select.WhereId(new long?(Id)).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : UserotherInfo.Parse(str));

		public static List<UserotherInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(string Userid) {
			var affrows = await dal.DeleteAsync(Userid);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(GetItem(Userid));
			return affrows;
		}
		async public static Task<UserotherInfo> GetItemAsync(string Userid) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL_Userother_", Userid), itemCacheTimeout, () => Select.WhereUserid(Userid).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : UserotherInfo.Parse(str));
		async public static Task<int> DeleteByIdAsync(long Id) {
			var affrows = await dal.DeleteByIdAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(GetItemById(Id));
			return affrows;
		}
		async public static Task<UserotherInfo> GetItemByIdAsync(long Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL_UserotherById_", Id), itemCacheTimeout, () => Select.WhereId(new long?(Id)).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : UserotherInfo.Parse(str));
		public static Task<int> UpdateAsync(UserotherInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(UserotherInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Userother.Insert(UserotherInfo item)
		/// </summary>
		[Obsolete]
		public static Task<UserotherInfo> InsertAsync(string Userid, string Chinesename, DateTime? Created, string Doctype, string Englishname, bool? Hasverify, string Idnumber, string Images) {
			return InsertAsync(new UserotherInfo {
				Userid = Userid, 
				Chinesename = Chinesename, 
				Created = Created, 
				Doctype = Doctype, 
				Englishname = Englishname, 
				Hasverify = Hasverify, 
				Idnumber = Idnumber, 
				Images = Images});
		}
		async public static Task<UserotherInfo> InsertAsync(UserotherInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		internal static Task RemoveCacheAsync(UserotherInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<UserotherInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 2];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL_Userother_", item.Userid);
				keys[keysIdx++] = string.Concat("cd_BLL_UserotherById_", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<UserotherInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<UserotherInfo, SelectBuild> {
			public SelectBuild WhereUserid(params string[] Userid) => this.Where1Or("a.`userid` = {0}", Userid);
			public SelectBuild WhereUseridLike(string pattern, bool isNotLike = false) => this.Where($@"a.`userid` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild WhereChinesename(params string[] Chinesename) => this.Where1Or("a.`chinesename` = {0}", Chinesename);
			public SelectBuild WhereChinesenameLike(string pattern, bool isNotLike = false) => this.Where($@"a.`chinesename` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild WhereCreatedRange(DateTime? begin) => base.Where("a.`created` >= {0}", begin);
			public SelectBuild WhereCreatedRange(DateTime? begin, DateTime? end) => end == null ? WhereCreatedRange(begin) : base.Where("a.`created` between {0} and {1}", begin, end);
			public SelectBuild WhereDoctype(params string[] Doctype) => this.Where1Or("a.`doctype` = {0}", Doctype);
			public SelectBuild WhereDoctypeLike(string pattern, bool isNotLike = false) => this.Where($@"a.`doctype` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild WhereEnglishname(params string[] Englishname) => this.Where1Or("a.`englishname` = {0}", Englishname);
			public SelectBuild WhereEnglishnameLike(string pattern, bool isNotLike = false) => this.Where($@"a.`englishname` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			/// <summary>
			/// 是否验证，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereHasverify(params bool?[] Hasverify) => this.Where1Or("a.`hasverify` = {0}", Hasverify);
			public SelectBuild WhereId(params long?[] Id) => this.Where1Or("a.`id` = {0}", Id);
			public SelectBuild WhereIdnumber(params string[] Idnumber) => this.Where1Or("a.`idnumber` = {0}", Idnumber);
			public SelectBuild WhereIdnumberLike(string pattern, bool isNotLike = false) => this.Where($@"a.`idnumber` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild WhereImagesLike(string pattern, bool isNotLike = false) => this.Where($@"a.`images` {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}