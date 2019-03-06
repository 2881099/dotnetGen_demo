using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.BLL {

	public partial class Tb_alltype {

		protected static readonly cd.DAL.Tb_alltype dal = new cd.DAL.Tb_alltype();
		protected static readonly int itemCacheTimeout;

		static Tb_alltype() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Tb_alltype"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static int Delete(int Id) {
			var affrows = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(new Tb_alltypeInfo { Id = Id });
			return affrows;
		}

		#region enum _
		public enum _ {
			Id = 1, 
			TestFieldBool, 
			TestFieldBool1111, 
			TestFieldBoolNullable, 
			TestFieldByte, 
			TestFieldByteNullable, 
			TestFieldBytes, 
			TestFieldDateTime, 
			TestFieldDateTimeNullable, 
			TestFieldDecimal, 
			TestFieldDecimalNullable, 
			TestFieldDouble, 
			TestFieldDoubleNullable, 
			TestFieldEnum1, 
			TestFieldEnum1Nullable, 
			TestFieldEnum2, 
			TestFieldEnum2Nullable, 
			TestFieldFloat, 
			TestFieldFloatNullable, 
			TestFieldGuid, 
			TestFieldGuidNullable, 
			TestFieldInt, 
			TestFieldIntNullable, 
			TestFieldLineString, 
			TestFieldLong, 
			TestFieldMultiLineString, 
			TestFieldMultiPoint, 
			TestFieldMultiPolygon, 
			TestFieldPoint, 
			TestFieldPolygon, 
			TestFieldSByte, 
			TestFieldSByteNullable, 
			TestFieldShort, 
			TestFieldShortNullable, 
			TestFieldString, 
			TestFieldTimeSpan, 
			TestFieldTimeSpanNullable, 
			TestFieldUInt, 
			TestFieldUIntNullable, 
			TestFieldULong, 
			TestFieldULongNullable, 
			TestFieldUShort, 
			TestFieldUShortNullable, 
			TestFielLongNullable
		}
		#endregion

		public static int Update(Tb_alltypeInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(Tb_alltypeInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static cd.DAL.Tb_alltype.SqlUpdateBuild UpdateDiy(int Id) => new cd.DAL.Tb_alltype.SqlUpdateBuild(new List<Tb_alltypeInfo> { new Tb_alltypeInfo { Id = Id } });
		public static cd.DAL.Tb_alltype.SqlUpdateBuild UpdateDiy(List<Tb_alltypeInfo> dataSource) => new cd.DAL.Tb_alltype.SqlUpdateBuild(dataSource);
		/// <summary>
		/// 用于批量更新，不会更新缓存
		/// </summary>
		public static cd.DAL.Tb_alltype.SqlUpdateBuild UpdateDiyDangerous => new cd.DAL.Tb_alltype.SqlUpdateBuild();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Tb_alltype.Insert(Tb_alltypeInfo item)
		/// </summary>
		[Obsolete]
		public static Tb_alltypeInfo Insert(bool? TestFieldBool, bool? TestFieldBool1111, bool? TestFieldBoolNullable, byte? TestFieldByte, byte? TestFieldByteNullable, byte[] TestFieldBytes, DateTime? TestFieldDateTime, DateTime? TestFieldDateTimeNullable, decimal? TestFieldDecimal, decimal? TestFieldDecimalNullable, double? TestFieldDouble, double? TestFieldDoubleNullable, Tb_alltypeTESTFIELDENUM1? TestFieldEnum1, Tb_alltypeTESTFIELDENUM1NULLABLE? TestFieldEnum1Nullable, Tb_alltypeTESTFIELDENUM2? TestFieldEnum2, Tb_alltypeTESTFIELDENUM2NULLABLE? TestFieldEnum2Nullable, float? TestFieldFloat, float? TestFieldFloatNullable, string TestFieldGuid, string TestFieldGuidNullable, int? TestFieldInt, int? TestFieldIntNullable, MygisLineString TestFieldLineString, long? TestFieldLong, MygisMultiLineString TestFieldMultiLineString, MygisMultiPoint TestFieldMultiPoint, MygisMultiPolygon TestFieldMultiPolygon, MygisPoint TestFieldPoint, MygisPolygon TestFieldPolygon, byte? TestFieldSByte, byte? TestFieldSByteNullable, short? TestFieldShort, short? TestFieldShortNullable, string TestFieldString, TimeSpan? TestFieldTimeSpan, TimeSpan? TestFieldTimeSpanNullable, uint? TestFieldUInt, uint? TestFieldUIntNullable, ulong? TestFieldULong, ulong? TestFieldULongNullable, ushort? TestFieldUShort, ushort? TestFieldUShortNullable, long? TestFielLongNullable) {
			return Insert(new Tb_alltypeInfo {
				TestFieldBool = TestFieldBool, 
				TestFieldBool1111 = TestFieldBool1111, 
				TestFieldBoolNullable = TestFieldBoolNullable, 
				TestFieldByte = TestFieldByte, 
				TestFieldByteNullable = TestFieldByteNullable, 
				TestFieldBytes = TestFieldBytes, 
				TestFieldDateTime = TestFieldDateTime, 
				TestFieldDateTimeNullable = TestFieldDateTimeNullable, 
				TestFieldDecimal = TestFieldDecimal, 
				TestFieldDecimalNullable = TestFieldDecimalNullable, 
				TestFieldDouble = TestFieldDouble, 
				TestFieldDoubleNullable = TestFieldDoubleNullable, 
				TestFieldEnum1 = TestFieldEnum1, 
				TestFieldEnum1Nullable = TestFieldEnum1Nullable, 
				TestFieldEnum2 = TestFieldEnum2, 
				TestFieldEnum2Nullable = TestFieldEnum2Nullable, 
				TestFieldFloat = TestFieldFloat, 
				TestFieldFloatNullable = TestFieldFloatNullable, 
				TestFieldGuid = TestFieldGuid, 
				TestFieldGuidNullable = TestFieldGuidNullable, 
				TestFieldInt = TestFieldInt, 
				TestFieldIntNullable = TestFieldIntNullable, 
				TestFieldLineString = TestFieldLineString, 
				TestFieldLong = TestFieldLong, 
				TestFieldMultiLineString = TestFieldMultiLineString, 
				TestFieldMultiPoint = TestFieldMultiPoint, 
				TestFieldMultiPolygon = TestFieldMultiPolygon, 
				TestFieldPoint = TestFieldPoint, 
				TestFieldPolygon = TestFieldPolygon, 
				TestFieldSByte = TestFieldSByte, 
				TestFieldSByteNullable = TestFieldSByteNullable, 
				TestFieldShort = TestFieldShort, 
				TestFieldShortNullable = TestFieldShortNullable, 
				TestFieldString = TestFieldString, 
				TestFieldTimeSpan = TestFieldTimeSpan, 
				TestFieldTimeSpanNullable = TestFieldTimeSpanNullable, 
				TestFieldUInt = TestFieldUInt, 
				TestFieldUIntNullable = TestFieldUIntNullable, 
				TestFieldULong = TestFieldULong, 
				TestFieldULongNullable = TestFieldULongNullable, 
				TestFieldUShort = TestFieldUShort, 
				TestFieldUShortNullable = TestFieldUShortNullable, 
				TestFielLongNullable = TestFielLongNullable});
		}
		public static Tb_alltypeInfo Insert(Tb_alltypeInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		internal static void RemoveCache(Tb_alltypeInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<Tb_alltypeInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Tb_alltype:", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static Tb_alltypeInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("cd_BLL:Tb_alltype:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Tb_alltypeInfo.Parse(str));

		public static List<Tb_alltypeInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<int> DeleteAsync(int Id) {
			var affrows = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(new Tb_alltypeInfo { Id = Id });
			return affrows;
		}
		async public static Task<Tb_alltypeInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("cd_BLL:Tb_alltype:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync(), item => item?.Stringify() ?? "null", str => str == "null" ? null : Tb_alltypeInfo.Parse(str));
		public static Task<int> UpdateAsync(Tb_alltypeInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(Tb_alltypeInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Tb_alltype.Insert(Tb_alltypeInfo item)
		/// </summary>
		[Obsolete]
		public static Task<Tb_alltypeInfo> InsertAsync(bool? TestFieldBool, bool? TestFieldBool1111, bool? TestFieldBoolNullable, byte? TestFieldByte, byte? TestFieldByteNullable, byte[] TestFieldBytes, DateTime? TestFieldDateTime, DateTime? TestFieldDateTimeNullable, decimal? TestFieldDecimal, decimal? TestFieldDecimalNullable, double? TestFieldDouble, double? TestFieldDoubleNullable, Tb_alltypeTESTFIELDENUM1? TestFieldEnum1, Tb_alltypeTESTFIELDENUM1NULLABLE? TestFieldEnum1Nullable, Tb_alltypeTESTFIELDENUM2? TestFieldEnum2, Tb_alltypeTESTFIELDENUM2NULLABLE? TestFieldEnum2Nullable, float? TestFieldFloat, float? TestFieldFloatNullable, string TestFieldGuid, string TestFieldGuidNullable, int? TestFieldInt, int? TestFieldIntNullable, MygisLineString TestFieldLineString, long? TestFieldLong, MygisMultiLineString TestFieldMultiLineString, MygisMultiPoint TestFieldMultiPoint, MygisMultiPolygon TestFieldMultiPolygon, MygisPoint TestFieldPoint, MygisPolygon TestFieldPolygon, byte? TestFieldSByte, byte? TestFieldSByteNullable, short? TestFieldShort, short? TestFieldShortNullable, string TestFieldString, TimeSpan? TestFieldTimeSpan, TimeSpan? TestFieldTimeSpanNullable, uint? TestFieldUInt, uint? TestFieldUIntNullable, ulong? TestFieldULong, ulong? TestFieldULongNullable, ushort? TestFieldUShort, ushort? TestFieldUShortNullable, long? TestFielLongNullable) {
			return InsertAsync(new Tb_alltypeInfo {
				TestFieldBool = TestFieldBool, 
				TestFieldBool1111 = TestFieldBool1111, 
				TestFieldBoolNullable = TestFieldBoolNullable, 
				TestFieldByte = TestFieldByte, 
				TestFieldByteNullable = TestFieldByteNullable, 
				TestFieldBytes = TestFieldBytes, 
				TestFieldDateTime = TestFieldDateTime, 
				TestFieldDateTimeNullable = TestFieldDateTimeNullable, 
				TestFieldDecimal = TestFieldDecimal, 
				TestFieldDecimalNullable = TestFieldDecimalNullable, 
				TestFieldDouble = TestFieldDouble, 
				TestFieldDoubleNullable = TestFieldDoubleNullable, 
				TestFieldEnum1 = TestFieldEnum1, 
				TestFieldEnum1Nullable = TestFieldEnum1Nullable, 
				TestFieldEnum2 = TestFieldEnum2, 
				TestFieldEnum2Nullable = TestFieldEnum2Nullable, 
				TestFieldFloat = TestFieldFloat, 
				TestFieldFloatNullable = TestFieldFloatNullable, 
				TestFieldGuid = TestFieldGuid, 
				TestFieldGuidNullable = TestFieldGuidNullable, 
				TestFieldInt = TestFieldInt, 
				TestFieldIntNullable = TestFieldIntNullable, 
				TestFieldLineString = TestFieldLineString, 
				TestFieldLong = TestFieldLong, 
				TestFieldMultiLineString = TestFieldMultiLineString, 
				TestFieldMultiPoint = TestFieldMultiPoint, 
				TestFieldMultiPolygon = TestFieldMultiPolygon, 
				TestFieldPoint = TestFieldPoint, 
				TestFieldPolygon = TestFieldPolygon, 
				TestFieldSByte = TestFieldSByte, 
				TestFieldSByteNullable = TestFieldSByteNullable, 
				TestFieldShort = TestFieldShort, 
				TestFieldShortNullable = TestFieldShortNullable, 
				TestFieldString = TestFieldString, 
				TestFieldTimeSpan = TestFieldTimeSpan, 
				TestFieldTimeSpanNullable = TestFieldTimeSpanNullable, 
				TestFieldUInt = TestFieldUInt, 
				TestFieldUIntNullable = TestFieldUIntNullable, 
				TestFieldULong = TestFieldULong, 
				TestFieldULongNullable = TestFieldULongNullable, 
				TestFieldUShort = TestFieldUShort, 
				TestFieldUShortNullable = TestFieldUShortNullable, 
				TestFielLongNullable = TestFielLongNullable});
		}
		async public static Task<Tb_alltypeInfo> InsertAsync(Tb_alltypeInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		internal static Task RemoveCacheAsync(Tb_alltypeInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<Tb_alltypeInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("cd_BLL:Tb_alltype:", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<Tb_alltypeInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<Tb_alltypeInfo, SelectBuild> {
			public SelectBuild WhereId(params int[] Id) => this.Where1Or("a.`Id` = {0}", Id);
			public SelectBuild WhereTestFieldBool(params bool?[] TestFieldBool) => this.Where1Or("a.`testFieldBool` = {0}", TestFieldBool);
			public SelectBuild WhereTestFieldBool1111(params bool?[] TestFieldBool1111) => this.Where1Or("a.`testFieldBool1111` = {0}", TestFieldBool1111);
			public SelectBuild WhereTestFieldBoolNullable(params bool?[] TestFieldBoolNullable) => this.Where1Or("a.`testFieldBoolNullable` = {0}", TestFieldBoolNullable);
			public SelectBuild WhereTestFieldByte(params byte?[] TestFieldByte) => this.Where1Or("a.`testFieldByte` = {0}", TestFieldByte);
			public SelectBuild WhereTestFieldByteNullable(params byte?[] TestFieldByteNullable) => this.Where1Or("a.`testFieldByteNullable` = {0}", TestFieldByteNullable);
			public SelectBuild WhereTestFieldDateTimeRange(DateTime? begin) => base.Where("a.`testFieldDateTime` >= {0}", begin);
			public SelectBuild WhereTestFieldDateTimeRange(DateTime? begin, DateTime? end) => end == null ? WhereTestFieldDateTimeRange(begin) : base.Where("a.`testFieldDateTime` between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldDateTimeNullableRange(DateTime? begin) => base.Where("a.`testFieldDateTimeNullable` >= {0}", begin);
			public SelectBuild WhereTestFieldDateTimeNullableRange(DateTime? begin, DateTime? end) => end == null ? WhereTestFieldDateTimeNullableRange(begin) : base.Where("a.`testFieldDateTimeNullable` between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldDecimal(params decimal?[] TestFieldDecimal) => this.Where1Or("a.`testFieldDecimal` = {0}", TestFieldDecimal);
			public SelectBuild WhereTestFieldDecimalRange(decimal? begin) => base.Where("a.`testFieldDecimal` >= {0}", begin);
			public SelectBuild WhereTestFieldDecimalRange(decimal? begin, decimal? end) => end == null ? WhereTestFieldDecimalRange(begin) : base.Where("a.`testFieldDecimal` between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldDecimalNullable(params decimal?[] TestFieldDecimalNullable) => this.Where1Or("a.`testFieldDecimalNullable` = {0}", TestFieldDecimalNullable);
			public SelectBuild WhereTestFieldDecimalNullableRange(decimal? begin) => base.Where("a.`testFieldDecimalNullable` >= {0}", begin);
			public SelectBuild WhereTestFieldDecimalNullableRange(decimal? begin, decimal? end) => end == null ? WhereTestFieldDecimalNullableRange(begin) : base.Where("a.`testFieldDecimalNullable` between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldDouble(params double?[] TestFieldDouble) => this.Where1Or("a.`testFieldDouble` = {0}", TestFieldDouble);
			public SelectBuild WhereTestFieldDoubleRange(double? begin) => base.Where("a.`testFieldDouble` >= {0}", begin);
			public SelectBuild WhereTestFieldDoubleRange(double? begin, double? end) => end == null ? WhereTestFieldDoubleRange(begin) : base.Where("a.`testFieldDouble` between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldDoubleNullable(params double?[] TestFieldDoubleNullable) => this.Where1Or("a.`testFieldDoubleNullable` = {0}", TestFieldDoubleNullable);
			public SelectBuild WhereTestFieldDoubleNullableRange(double? begin) => base.Where("a.`testFieldDoubleNullable` >= {0}", begin);
			public SelectBuild WhereTestFieldDoubleNullableRange(double? begin, double? end) => end == null ? WhereTestFieldDoubleNullableRange(begin) : base.Where("a.`testFieldDoubleNullable` between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldEnum1_IN(params Tb_alltypeTESTFIELDENUM1?[] TestFieldEnum1s) => this.Where1Or("a.`testFieldEnum1` = {0}", TestFieldEnum1s);
			public SelectBuild WhereTestFieldEnum1(Tb_alltypeTESTFIELDENUM1 TestFieldEnum11) => this.WhereTestFieldEnum1_IN(TestFieldEnum11);
			#region WhereTestFieldEnum1
			public SelectBuild WhereTestFieldEnum1(Tb_alltypeTESTFIELDENUM1 TestFieldEnum11, Tb_alltypeTESTFIELDENUM1 TestFieldEnum12) => this.WhereTestFieldEnum1_IN(TestFieldEnum11, TestFieldEnum12);
			public SelectBuild WhereTestFieldEnum1(Tb_alltypeTESTFIELDENUM1 TestFieldEnum11, Tb_alltypeTESTFIELDENUM1 TestFieldEnum12, Tb_alltypeTESTFIELDENUM1 TestFieldEnum13) => this.WhereTestFieldEnum1_IN(TestFieldEnum11, TestFieldEnum12, TestFieldEnum13);
			public SelectBuild WhereTestFieldEnum1(Tb_alltypeTESTFIELDENUM1 TestFieldEnum11, Tb_alltypeTESTFIELDENUM1 TestFieldEnum12, Tb_alltypeTESTFIELDENUM1 TestFieldEnum13, Tb_alltypeTESTFIELDENUM1 TestFieldEnum14) => this.WhereTestFieldEnum1_IN(TestFieldEnum11, TestFieldEnum12, TestFieldEnum13, TestFieldEnum14);
			public SelectBuild WhereTestFieldEnum1(Tb_alltypeTESTFIELDENUM1 TestFieldEnum11, Tb_alltypeTESTFIELDENUM1 TestFieldEnum12, Tb_alltypeTESTFIELDENUM1 TestFieldEnum13, Tb_alltypeTESTFIELDENUM1 TestFieldEnum14, Tb_alltypeTESTFIELDENUM1 TestFieldEnum15) => this.WhereTestFieldEnum1_IN(TestFieldEnum11, TestFieldEnum12, TestFieldEnum13, TestFieldEnum14, TestFieldEnum15);
			#endregion
			public SelectBuild WhereTestFieldEnum1Nullable_IN(params Tb_alltypeTESTFIELDENUM1NULLABLE?[] TestFieldEnum1Nullables) => this.Where1Or("a.`testFieldEnum1Nullable` = {0}", TestFieldEnum1Nullables);
			public SelectBuild WhereTestFieldEnum1Nullable(Tb_alltypeTESTFIELDENUM1NULLABLE TestFieldEnum1Nullable1) => this.WhereTestFieldEnum1Nullable_IN(TestFieldEnum1Nullable1);
			#region WhereTestFieldEnum1Nullable
			public SelectBuild WhereTestFieldEnum1Nullable(Tb_alltypeTESTFIELDENUM1NULLABLE TestFieldEnum1Nullable1, Tb_alltypeTESTFIELDENUM1NULLABLE TestFieldEnum1Nullable2) => this.WhereTestFieldEnum1Nullable_IN(TestFieldEnum1Nullable1, TestFieldEnum1Nullable2);
			public SelectBuild WhereTestFieldEnum1Nullable(Tb_alltypeTESTFIELDENUM1NULLABLE TestFieldEnum1Nullable1, Tb_alltypeTESTFIELDENUM1NULLABLE TestFieldEnum1Nullable2, Tb_alltypeTESTFIELDENUM1NULLABLE TestFieldEnum1Nullable3) => this.WhereTestFieldEnum1Nullable_IN(TestFieldEnum1Nullable1, TestFieldEnum1Nullable2, TestFieldEnum1Nullable3);
			public SelectBuild WhereTestFieldEnum1Nullable(Tb_alltypeTESTFIELDENUM1NULLABLE TestFieldEnum1Nullable1, Tb_alltypeTESTFIELDENUM1NULLABLE TestFieldEnum1Nullable2, Tb_alltypeTESTFIELDENUM1NULLABLE TestFieldEnum1Nullable3, Tb_alltypeTESTFIELDENUM1NULLABLE TestFieldEnum1Nullable4) => this.WhereTestFieldEnum1Nullable_IN(TestFieldEnum1Nullable1, TestFieldEnum1Nullable2, TestFieldEnum1Nullable3, TestFieldEnum1Nullable4);
			public SelectBuild WhereTestFieldEnum1Nullable(Tb_alltypeTESTFIELDENUM1NULLABLE TestFieldEnum1Nullable1, Tb_alltypeTESTFIELDENUM1NULLABLE TestFieldEnum1Nullable2, Tb_alltypeTESTFIELDENUM1NULLABLE TestFieldEnum1Nullable3, Tb_alltypeTESTFIELDENUM1NULLABLE TestFieldEnum1Nullable4, Tb_alltypeTESTFIELDENUM1NULLABLE TestFieldEnum1Nullable5) => this.WhereTestFieldEnum1Nullable_IN(TestFieldEnum1Nullable1, TestFieldEnum1Nullable2, TestFieldEnum1Nullable3, TestFieldEnum1Nullable4, TestFieldEnum1Nullable5);
			#endregion
			public SelectBuild WhereTestFieldEnum2_IN(params Tb_alltypeTESTFIELDENUM2[] TestFieldEnum2s) => this.Where1Or("(a.`testFieldEnum2` & {0}) = {0}", TestFieldEnum2s);
			public SelectBuild WhereTestFieldEnum2(Tb_alltypeTESTFIELDENUM2 TestFieldEnum21) => this.WhereTestFieldEnum2_IN(TestFieldEnum21);
			#region WhereTestFieldEnum2
			public SelectBuild WhereTestFieldEnum2(Tb_alltypeTESTFIELDENUM2 TestFieldEnum21, Tb_alltypeTESTFIELDENUM2 TestFieldEnum22) => this.WhereTestFieldEnum2_IN(TestFieldEnum21, TestFieldEnum22);
			public SelectBuild WhereTestFieldEnum2(Tb_alltypeTESTFIELDENUM2 TestFieldEnum21, Tb_alltypeTESTFIELDENUM2 TestFieldEnum22, Tb_alltypeTESTFIELDENUM2 TestFieldEnum23) => this.WhereTestFieldEnum2_IN(TestFieldEnum21, TestFieldEnum22, TestFieldEnum23);
			public SelectBuild WhereTestFieldEnum2(Tb_alltypeTESTFIELDENUM2 TestFieldEnum21, Tb_alltypeTESTFIELDENUM2 TestFieldEnum22, Tb_alltypeTESTFIELDENUM2 TestFieldEnum23, Tb_alltypeTESTFIELDENUM2 TestFieldEnum24) => this.WhereTestFieldEnum2_IN(TestFieldEnum21, TestFieldEnum22, TestFieldEnum23, TestFieldEnum24);
			public SelectBuild WhereTestFieldEnum2(Tb_alltypeTESTFIELDENUM2 TestFieldEnum21, Tb_alltypeTESTFIELDENUM2 TestFieldEnum22, Tb_alltypeTESTFIELDENUM2 TestFieldEnum23, Tb_alltypeTESTFIELDENUM2 TestFieldEnum24, Tb_alltypeTESTFIELDENUM2 TestFieldEnum25) => this.WhereTestFieldEnum2_IN(TestFieldEnum21, TestFieldEnum22, TestFieldEnum23, TestFieldEnum24, TestFieldEnum25);
			#endregion
			public SelectBuild WhereTestFieldEnum2Nullable_IN(params Tb_alltypeTESTFIELDENUM2NULLABLE[] TestFieldEnum2Nullables) => this.Where1Or("(a.`testFieldEnum2Nullable` & {0}) = {0}", TestFieldEnum2Nullables);
			public SelectBuild WhereTestFieldEnum2Nullable(Tb_alltypeTESTFIELDENUM2NULLABLE TestFieldEnum2Nullable1) => this.WhereTestFieldEnum2Nullable_IN(TestFieldEnum2Nullable1);
			#region WhereTestFieldEnum2Nullable
			public SelectBuild WhereTestFieldEnum2Nullable(Tb_alltypeTESTFIELDENUM2NULLABLE TestFieldEnum2Nullable1, Tb_alltypeTESTFIELDENUM2NULLABLE TestFieldEnum2Nullable2) => this.WhereTestFieldEnum2Nullable_IN(TestFieldEnum2Nullable1, TestFieldEnum2Nullable2);
			public SelectBuild WhereTestFieldEnum2Nullable(Tb_alltypeTESTFIELDENUM2NULLABLE TestFieldEnum2Nullable1, Tb_alltypeTESTFIELDENUM2NULLABLE TestFieldEnum2Nullable2, Tb_alltypeTESTFIELDENUM2NULLABLE TestFieldEnum2Nullable3) => this.WhereTestFieldEnum2Nullable_IN(TestFieldEnum2Nullable1, TestFieldEnum2Nullable2, TestFieldEnum2Nullable3);
			public SelectBuild WhereTestFieldEnum2Nullable(Tb_alltypeTESTFIELDENUM2NULLABLE TestFieldEnum2Nullable1, Tb_alltypeTESTFIELDENUM2NULLABLE TestFieldEnum2Nullable2, Tb_alltypeTESTFIELDENUM2NULLABLE TestFieldEnum2Nullable3, Tb_alltypeTESTFIELDENUM2NULLABLE TestFieldEnum2Nullable4) => this.WhereTestFieldEnum2Nullable_IN(TestFieldEnum2Nullable1, TestFieldEnum2Nullable2, TestFieldEnum2Nullable3, TestFieldEnum2Nullable4);
			public SelectBuild WhereTestFieldEnum2Nullable(Tb_alltypeTESTFIELDENUM2NULLABLE TestFieldEnum2Nullable1, Tb_alltypeTESTFIELDENUM2NULLABLE TestFieldEnum2Nullable2, Tb_alltypeTESTFIELDENUM2NULLABLE TestFieldEnum2Nullable3, Tb_alltypeTESTFIELDENUM2NULLABLE TestFieldEnum2Nullable4, Tb_alltypeTESTFIELDENUM2NULLABLE TestFieldEnum2Nullable5) => this.WhereTestFieldEnum2Nullable_IN(TestFieldEnum2Nullable1, TestFieldEnum2Nullable2, TestFieldEnum2Nullable3, TestFieldEnum2Nullable4, TestFieldEnum2Nullable5);
			#endregion
			public SelectBuild WhereTestFieldFloat(params float?[] TestFieldFloat) => this.Where1Or("a.`testFieldFloat` = {0}", TestFieldFloat);
			public SelectBuild WhereTestFieldFloatRange(float? begin) => base.Where("a.`testFieldFloat` >= {0}", begin);
			public SelectBuild WhereTestFieldFloatRange(float? begin, float? end) => end == null ? WhereTestFieldFloatRange(begin) : base.Where("a.`testFieldFloat` between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldFloatNullable(params float?[] TestFieldFloatNullable) => this.Where1Or("a.`testFieldFloatNullable` = {0}", TestFieldFloatNullable);
			public SelectBuild WhereTestFieldFloatNullableRange(float? begin) => base.Where("a.`testFieldFloatNullable` >= {0}", begin);
			public SelectBuild WhereTestFieldFloatNullableRange(float? begin, float? end) => end == null ? WhereTestFieldFloatNullableRange(begin) : base.Where("a.`testFieldFloatNullable` between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldGuid(params string[] TestFieldGuid) => this.Where1Or("a.`testFieldGuid` = {0}", TestFieldGuid);
			public SelectBuild WhereTestFieldGuidLike(string pattern, bool isNotLike = false) => this.Where($@"a.`testFieldGuid` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereTestFieldGuidNullable(params string[] TestFieldGuidNullable) => this.Where1Or("a.`testFieldGuidNullable` = {0}", TestFieldGuidNullable);
			public SelectBuild WhereTestFieldGuidNullableLike(string pattern, bool isNotLike = false) => this.Where($@"a.`testFieldGuidNullable` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereTestFieldInt(params int?[] TestFieldInt) => this.Where1Or("a.`testFieldInt` = {0}", TestFieldInt);
			public SelectBuild WhereTestFieldIntNullable(params int?[] TestFieldIntNullable) => this.Where1Or("a.`testFieldIntNullable` = {0}", TestFieldIntNullable);
			public SelectBuild WhereTestFieldLong(params long?[] TestFieldLong) => this.Where1Or("a.`testFieldLong` = {0}", TestFieldLong);
			/// <summary>
			/// 查找地理位置多少米范围内的记录，距离由近到远排序
			/// </summary>
			/// <param name="point">经纬度</param>
			/// <param name="meter">米(=0时无限制)</param>
			/// <returns></returns>
			public SelectBuild WhereTestFieldPointMbrContains(MygisPoint point, double meter = 0) => this.Where(meter > 0, @"MBRContains(LineString(
  Point({0} + 10 / ( 111.1 / COS(RADIANS({1}))), {1} + 10 / 111.1),
  Point({0} - 10 / ( 111.1 / COS(RADIANS({1}))), {1} - 10 / 111.1)), a.`testFieldPoint`)", point.X, point.Y, meter);
			public SelectBuild WhereTestFieldSByte(params byte?[] TestFieldSByte) => this.Where1Or("a.`testFieldSByte` = {0}", TestFieldSByte);
			public SelectBuild WhereTestFieldSByteNullable(params byte?[] TestFieldSByteNullable) => this.Where1Or("a.`testFieldSByteNullable` = {0}", TestFieldSByteNullable);
			public SelectBuild WhereTestFieldShort(params short?[] TestFieldShort) => this.Where1Or("a.`testFieldShort` = {0}", TestFieldShort);
			public SelectBuild WhereTestFieldShortNullable(params short?[] TestFieldShortNullable) => this.Where1Or("a.`testFieldShortNullable` = {0}", TestFieldShortNullable);
			public SelectBuild WhereTestFieldString(params string[] TestFieldString) => this.Where1Or("a.`testFieldString` = {0}", TestFieldString);
			public SelectBuild WhereTestFieldStringLike(string pattern, bool isNotLike = false) => this.Where($@"a.`testFieldString` {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereTestFieldTimeSpanRange(TimeSpan? begin) => base.Where("a.`testFieldTimeSpan` >= {0}", begin);
			public SelectBuild WhereTestFieldTimeSpanRange(TimeSpan? begin, TimeSpan? end) => end == null ? WhereTestFieldTimeSpanRange(begin) : base.Where("a.`testFieldTimeSpan` between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldTimeSpanNullableRange(TimeSpan? begin) => base.Where("a.`testFieldTimeSpanNullable` >= {0}", begin);
			public SelectBuild WhereTestFieldTimeSpanNullableRange(TimeSpan? begin, TimeSpan? end) => end == null ? WhereTestFieldTimeSpanNullableRange(begin) : base.Where("a.`testFieldTimeSpanNullable` between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldUInt(params uint?[] TestFieldUInt) => this.Where1Or("a.`testFieldUInt` = {0}", TestFieldUInt);
			public SelectBuild WhereTestFieldUIntNullable(params uint?[] TestFieldUIntNullable) => this.Where1Or("a.`testFieldUIntNullable` = {0}", TestFieldUIntNullable);
			public SelectBuild WhereTestFieldULong(params ulong?[] TestFieldULong) => this.Where1Or("a.`testFieldULong` = {0}", TestFieldULong);
			public SelectBuild WhereTestFieldULongNullable(params ulong?[] TestFieldULongNullable) => this.Where1Or("a.`testFieldULongNullable` = {0}", TestFieldULongNullable);
			public SelectBuild WhereTestFieldUShort(params ushort?[] TestFieldUShort) => this.Where1Or("a.`testFieldUShort` = {0}", TestFieldUShort);
			public SelectBuild WhereTestFieldUShortNullable(params ushort?[] TestFieldUShortNullable) => this.Where1Or("a.`testFieldUShortNullable` = {0}", TestFieldUShortNullable);
			public SelectBuild WhereTestFielLongNullable(params long?[] TestFielLongNullable) => this.Where1Or("a.`testFielLongNullable` = {0}", TestFielLongNullable);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}