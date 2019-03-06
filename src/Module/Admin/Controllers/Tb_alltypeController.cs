using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using cd.BLL;
using cd.Model;

namespace cd.Module.Admin.Controllers {
	[Route("[controller]")]
	public class Tb_alltypeController : BaseController {
		public Tb_alltypeController(ILogger<Tb_alltypeController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromQuery] string key, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Tb_alltype.Select
				.Where(!string.IsNullOrEmpty(key), "a.testFieldGuid like {0} or a.testFieldGuidNullable like {0} or a.testFieldString like {0}", string.Concat("%", key, "%"));
			var items = await select.Count(out var count).Page(page, limit).ToListAsync();
			ViewBag.items = items;
			ViewBag.count = count;
			return View();
		}

		[HttpGet(@"add")]
		public ActionResult Edit() {
			return View();
		}
		[HttpGet(@"edit")]
		async public Task<ActionResult> Edit([FromQuery] int Id) {
			Tb_alltypeInfo item = await Tb_alltype.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] bool TestFieldBool, [FromForm] bool TestFieldBool1111, [FromForm] bool TestFieldBoolNullable, [FromForm] byte? TestFieldByte, [FromForm] byte? TestFieldByteNullable, [FromForm] byte[] TestFieldBytes, [FromForm] DateTime? TestFieldDateTime, [FromForm] DateTime? TestFieldDateTimeNullable, [FromForm] decimal? TestFieldDecimal, [FromForm] decimal? TestFieldDecimalNullable, [FromForm] double? TestFieldDouble, [FromForm] double? TestFieldDoubleNullable, [FromForm] Tb_alltypeTESTFIELDENUM1? TestFieldEnum1, [FromForm] Tb_alltypeTESTFIELDENUM1NULLABLE? TestFieldEnum1Nullable, [FromForm] Tb_alltypeTESTFIELDENUM2[] TestFieldEnum2, [FromForm] Tb_alltypeTESTFIELDENUM2NULLABLE[] TestFieldEnum2Nullable, [FromForm] float? TestFieldFloat, [FromForm] float? TestFieldFloatNullable, [FromForm] string TestFieldGuid, [FromForm] string TestFieldGuidNullable, [FromForm] int? TestFieldInt, [FromForm] int? TestFieldIntNullable, [FromForm] MygisLineString TestFieldLineString, [FromForm] long? TestFieldLong, [FromForm] MygisMultiLineString TestFieldMultiLineString, [FromForm] MygisMultiPoint TestFieldMultiPoint, [FromForm] MygisMultiPolygon TestFieldMultiPolygon, [FromForm] MygisPoint TestFieldPoint, [FromForm] MygisPolygon TestFieldPolygon, [FromForm] byte? TestFieldSByte, [FromForm] byte? TestFieldSByteNullable, [FromForm] short? TestFieldShort, [FromForm] short? TestFieldShortNullable, [FromForm] string TestFieldString, [FromForm] TimeSpan? TestFieldTimeSpan, [FromForm] TimeSpan? TestFieldTimeSpanNullable, [FromForm] uint? TestFieldUInt, [FromForm] uint? TestFieldUIntNullable, [FromForm] ulong? TestFieldULong, [FromForm] ulong? TestFieldULongNullable, [FromForm] ushort? TestFieldUShort, [FromForm] ushort? TestFieldUShortNullable, [FromForm] long? TestFielLongNullable) {
			Tb_alltypeInfo item = new Tb_alltypeInfo();
			item.TestFieldBool = TestFieldBool;
			item.TestFieldBool1111 = TestFieldBool1111;
			item.TestFieldBoolNullable = TestFieldBoolNullable;
			item.TestFieldByte = TestFieldByte;
			item.TestFieldByteNullable = TestFieldByteNullable;
			item.TestFieldBytes = TestFieldBytes;
			item.TestFieldDateTime = TestFieldDateTime;
			item.TestFieldDateTimeNullable = TestFieldDateTimeNullable;
			item.TestFieldDecimal = TestFieldDecimal;
			item.TestFieldDecimalNullable = TestFieldDecimalNullable;
			item.TestFieldDouble = TestFieldDouble;
			item.TestFieldDoubleNullable = TestFieldDoubleNullable;
			item.TestFieldEnum1 = TestFieldEnum1;
			item.TestFieldEnum1Nullable = TestFieldEnum1Nullable;
			item.TestFieldEnum2 = null;
			TestFieldEnum2?.ToList().ForEach(a => item.TestFieldEnum2 = (item.TestFieldEnum2 ?? 0) | a);
			item.TestFieldEnum2Nullable = null;
			TestFieldEnum2Nullable?.ToList().ForEach(a => item.TestFieldEnum2Nullable = (item.TestFieldEnum2Nullable ?? 0) | a);
			item.TestFieldFloat = TestFieldFloat;
			item.TestFieldFloatNullable = TestFieldFloatNullable;
			item.TestFieldGuid = TestFieldGuid;
			item.TestFieldGuidNullable = TestFieldGuidNullable;
			item.TestFieldInt = TestFieldInt;
			item.TestFieldIntNullable = TestFieldIntNullable;
			item.TestFieldLineString = TestFieldLineString;
			item.TestFieldLong = TestFieldLong;
			item.TestFieldMultiLineString = TestFieldMultiLineString;
			item.TestFieldMultiPoint = TestFieldMultiPoint;
			item.TestFieldMultiPolygon = TestFieldMultiPolygon;
			item.TestFieldPoint = TestFieldPoint;
			item.TestFieldPolygon = TestFieldPolygon;
			item.TestFieldSByte = TestFieldSByte;
			item.TestFieldSByteNullable = TestFieldSByteNullable;
			item.TestFieldShort = TestFieldShort;
			item.TestFieldShortNullable = TestFieldShortNullable;
			item.TestFieldString = TestFieldString;
			item.TestFieldTimeSpan = TestFieldTimeSpan;
			item.TestFieldTimeSpanNullable = TestFieldTimeSpanNullable;
			item.TestFieldUInt = TestFieldUInt;
			item.TestFieldUIntNullable = TestFieldUIntNullable;
			item.TestFieldULong = TestFieldULong;
			item.TestFieldULongNullable = TestFieldULongNullable;
			item.TestFieldUShort = TestFieldUShort;
			item.TestFieldUShortNullable = TestFieldUShortNullable;
			item.TestFielLongNullable = TestFielLongNullable;
			item = await Tb_alltype.InsertAsync(item);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] int Id, [FromForm] bool TestFieldBool, [FromForm] bool TestFieldBool1111, [FromForm] bool TestFieldBoolNullable, [FromForm] byte? TestFieldByte, [FromForm] byte? TestFieldByteNullable, [FromForm] byte[] TestFieldBytes, [FromForm] DateTime? TestFieldDateTime, [FromForm] DateTime? TestFieldDateTimeNullable, [FromForm] decimal? TestFieldDecimal, [FromForm] decimal? TestFieldDecimalNullable, [FromForm] double? TestFieldDouble, [FromForm] double? TestFieldDoubleNullable, [FromForm] Tb_alltypeTESTFIELDENUM1? TestFieldEnum1, [FromForm] Tb_alltypeTESTFIELDENUM1NULLABLE? TestFieldEnum1Nullable, [FromForm] Tb_alltypeTESTFIELDENUM2[] TestFieldEnum2, [FromForm] Tb_alltypeTESTFIELDENUM2NULLABLE[] TestFieldEnum2Nullable, [FromForm] float? TestFieldFloat, [FromForm] float? TestFieldFloatNullable, [FromForm] string TestFieldGuid, [FromForm] string TestFieldGuidNullable, [FromForm] int? TestFieldInt, [FromForm] int? TestFieldIntNullable, [FromForm] MygisLineString TestFieldLineString, [FromForm] long? TestFieldLong, [FromForm] MygisMultiLineString TestFieldMultiLineString, [FromForm] MygisMultiPoint TestFieldMultiPoint, [FromForm] MygisMultiPolygon TestFieldMultiPolygon, [FromForm] MygisPoint TestFieldPoint, [FromForm] MygisPolygon TestFieldPolygon, [FromForm] byte? TestFieldSByte, [FromForm] byte? TestFieldSByteNullable, [FromForm] short? TestFieldShort, [FromForm] short? TestFieldShortNullable, [FromForm] string TestFieldString, [FromForm] TimeSpan? TestFieldTimeSpan, [FromForm] TimeSpan? TestFieldTimeSpanNullable, [FromForm] uint? TestFieldUInt, [FromForm] uint? TestFieldUIntNullable, [FromForm] ulong? TestFieldULong, [FromForm] ulong? TestFieldULongNullable, [FromForm] ushort? TestFieldUShort, [FromForm] ushort? TestFieldUShortNullable, [FromForm] long? TestFielLongNullable) {
			Tb_alltypeInfo item = await Tb_alltype.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.TestFieldBool = TestFieldBool;
			item.TestFieldBool1111 = TestFieldBool1111;
			item.TestFieldBoolNullable = TestFieldBoolNullable;
			item.TestFieldByte = TestFieldByte;
			item.TestFieldByteNullable = TestFieldByteNullable;
			item.TestFieldBytes = TestFieldBytes;
			item.TestFieldDateTime = TestFieldDateTime;
			item.TestFieldDateTimeNullable = TestFieldDateTimeNullable;
			item.TestFieldDecimal = TestFieldDecimal;
			item.TestFieldDecimalNullable = TestFieldDecimalNullable;
			item.TestFieldDouble = TestFieldDouble;
			item.TestFieldDoubleNullable = TestFieldDoubleNullable;
			item.TestFieldEnum1 = TestFieldEnum1;
			item.TestFieldEnum1Nullable = TestFieldEnum1Nullable;
			item.TestFieldEnum2 = null;
			TestFieldEnum2?.ToList().ForEach(a => item.TestFieldEnum2 = (item.TestFieldEnum2 ?? 0) | a);
			item.TestFieldEnum2Nullable = null;
			TestFieldEnum2Nullable?.ToList().ForEach(a => item.TestFieldEnum2Nullable = (item.TestFieldEnum2Nullable ?? 0) | a);
			item.TestFieldFloat = TestFieldFloat;
			item.TestFieldFloatNullable = TestFieldFloatNullable;
			item.TestFieldGuid = TestFieldGuid;
			item.TestFieldGuidNullable = TestFieldGuidNullable;
			item.TestFieldInt = TestFieldInt;
			item.TestFieldIntNullable = TestFieldIntNullable;
			item.TestFieldLineString = TestFieldLineString;
			item.TestFieldLong = TestFieldLong;
			item.TestFieldMultiLineString = TestFieldMultiLineString;
			item.TestFieldMultiPoint = TestFieldMultiPoint;
			item.TestFieldMultiPolygon = TestFieldMultiPolygon;
			item.TestFieldPoint = TestFieldPoint;
			item.TestFieldPolygon = TestFieldPolygon;
			item.TestFieldSByte = TestFieldSByte;
			item.TestFieldSByteNullable = TestFieldSByteNullable;
			item.TestFieldShort = TestFieldShort;
			item.TestFieldShortNullable = TestFieldShortNullable;
			item.TestFieldString = TestFieldString;
			item.TestFieldTimeSpan = TestFieldTimeSpan;
			item.TestFieldTimeSpanNullable = TestFieldTimeSpanNullable;
			item.TestFieldUInt = TestFieldUInt;
			item.TestFieldUIntNullable = TestFieldUIntNullable;
			item.TestFieldULong = TestFieldULong;
			item.TestFieldULongNullable = TestFieldULongNullable;
			item.TestFieldUShort = TestFieldUShort;
			item.TestFieldUShortNullable = TestFieldUShortNullable;
			item.TestFielLongNullable = TestFielLongNullable;
			int affrows = await Tb_alltype.UpdateAsync(item);
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] int[] id) {
			int affrows = 0;
			foreach (int id2 in id)
				affrows += await Tb_alltype.DeleteAsync(id2);
			if (affrows > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{affrows}");
			return APIReturn.失败;
		}
	}
}
