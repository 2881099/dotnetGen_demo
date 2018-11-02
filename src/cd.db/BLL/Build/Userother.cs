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

		public static int Update(UserotherInfo item) => dal.Update(item).ExecuteNonQuery();
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
		public static UserotherSelectBuild Select => new UserotherSelectBuild(dal);
		public static UserotherSelectBuild SelectAs(string alias = "a") => Select.As(alias);

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
		async public static Task<int> UpdateAsync(UserotherInfo item) => await dal.Update(item).ExecuteNonQueryAsync();

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
		async internal static Task RemoveCacheAsync(UserotherInfo item) => await RemoveCacheAsync(item == null ? null : new [] { item });
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
	}
	public partial class UserotherSelectBuild : SelectBuild<UserotherInfo, UserotherSelectBuild> {
		public UserotherSelectBuild WhereUserid(params string[] Userid) {
			return this.Where1Or("a.`userid` = {0}", Userid);
		}
		public UserotherSelectBuild WhereUseridLike(params string[] Userid) {
			if (Userid == null || Userid.Where(a => !string.IsNullOrEmpty(a)).Any() == false) return this;
			return this.Where1Or(@"a.`userid` LIKE {0}", Userid.Select(a => "%" + a + "%").ToArray());
		}
		public UserotherSelectBuild WhereChinesename(params string[] Chinesename) {
			return this.Where1Or("a.`chinesename` = {0}", Chinesename);
		}
		public UserotherSelectBuild WhereChinesenameLike(params string[] Chinesename) {
			if (Chinesename == null || Chinesename.Where(a => !string.IsNullOrEmpty(a)).Any() == false) return this;
			return this.Where1Or(@"a.`chinesename` LIKE {0}", Chinesename.Select(a => "%" + a + "%").ToArray());
		}
		public UserotherSelectBuild WhereCreatedRange(DateTime? begin) {
			return base.Where("a.`created` >= {0}", begin);
		}
		public UserotherSelectBuild WhereCreatedRange(DateTime? begin, DateTime? end) {
			if (end == null) return WhereCreatedRange(begin);
			return base.Where("a.`created` between {0} and {1}", begin, end);
		}
		public UserotherSelectBuild WhereDoctype(params string[] Doctype) {
			return this.Where1Or("a.`doctype` = {0}", Doctype);
		}
		public UserotherSelectBuild WhereDoctypeLike(params string[] Doctype) {
			if (Doctype == null || Doctype.Where(a => !string.IsNullOrEmpty(a)).Any() == false) return this;
			return this.Where1Or(@"a.`doctype` LIKE {0}", Doctype.Select(a => "%" + a + "%").ToArray());
		}
		public UserotherSelectBuild WhereEnglishname(params string[] Englishname) {
			return this.Where1Or("a.`englishname` = {0}", Englishname);
		}
		public UserotherSelectBuild WhereEnglishnameLike(params string[] Englishname) {
			if (Englishname == null || Englishname.Where(a => !string.IsNullOrEmpty(a)).Any() == false) return this;
			return this.Where1Or(@"a.`englishname` LIKE {0}", Englishname.Select(a => "%" + a + "%").ToArray());
		}
		public UserotherSelectBuild WhereHasverify(params bool?[] Hasverify) {
			return this.Where1Or("a.`hasverify` = {0}", Hasverify);
		}
		public UserotherSelectBuild WhereId(params long?[] Id) {
			return this.Where1Or("a.`id` = {0}", Id);
		}
		public UserotherSelectBuild WhereIdnumber(params string[] Idnumber) {
			return this.Where1Or("a.`idnumber` = {0}", Idnumber);
		}
		public UserotherSelectBuild WhereIdnumberLike(params string[] Idnumber) {
			if (Idnumber == null || Idnumber.Where(a => !string.IsNullOrEmpty(a)).Any() == false) return this;
			return this.Where1Or(@"a.`idnumber` LIKE {0}", Idnumber.Select(a => "%" + a + "%").ToArray());
		}
		public UserotherSelectBuild WhereImagesLike(params string[] Images) {
			if (Images == null || Images.Where(a => !string.IsNullOrEmpty(a)).Any() == false) return this;
			return this.Where1Or(@"a.`images` LIKE {0}", Images.Select(a => "%" + a + "%").ToArray());
		}
		public UserotherSelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
	}
}