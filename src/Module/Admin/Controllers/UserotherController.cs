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
	public class UserotherController : BaseController {
		public UserotherController(ILogger<UserotherController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromQuery] string key, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Userother.Select
				.Where(!string.IsNullOrEmpty(key), "a.userid like {0} or a.chinesename like {0} or a.doctype like {0} or a.englishname like {0} or a.idnumber like {0} or a.images like {0}", string.Concat("%", key, "%"));
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
		async public Task<ActionResult> Edit([FromQuery] string Userid) {
			UserotherInfo item = await Userother.GetItemAsync(Userid);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] string Userid, [FromForm] string Chinesename, [FromForm] DateTime? Created, [FromForm] string Doctype, [FromForm] string Englishname, [FromForm] bool Hasverify, [FromForm] string Idnumber, [FromForm] string Images) {
			UserotherInfo item = new UserotherInfo();
			item.Userid = Userid;
			item.Chinesename = Chinesename;
			item.Created = Created;
			item.Doctype = Doctype;
			item.Englishname = Englishname;
			item.Hasverify = Hasverify;
			item.Idnumber = Idnumber;
			item.Images = Images;
			item = await Userother.InsertAsync(item);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] string Userid, [FromForm] string Chinesename, [FromForm] DateTime? Created, [FromForm] string Doctype, [FromForm] string Englishname, [FromForm] bool Hasverify, [FromForm] string Idnumber, [FromForm] string Images) {
			UserotherInfo item = await Userother.GetItemAsync(Userid);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.Chinesename = Chinesename;
			item.Created = Created;
			item.Doctype = Doctype;
			item.Englishname = Englishname;
			item.Hasverify = Hasverify;
			item.Idnumber = Idnumber;
			item.Images = Images;
			int affrows = await Userother.UpdateAsync(item);
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] string[] id) {
			int affrows = 0;
			foreach (string id2 in id)
				affrows += await Userother.DeleteAsync(id2);
			if (affrows > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{affrows}");
			return APIReturn.失败;
		}
	}
}
