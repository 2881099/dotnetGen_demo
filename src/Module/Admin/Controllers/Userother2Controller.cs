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
	public class Userother2Controller : BaseController {
		public Userother2Controller(ILogger<Userother2Controller> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromServices]IConfiguration cfg, [FromQuery] string key, [FromQuery] long?[] Userother_id, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Userother2.Select
				.Where(!string.IsNullOrEmpty(key), "a.chinesename like {0} or a.xxxx like {0}", string.Concat("%", key, "%"));
			if (Userother_id.Length > 0) select.WhereUserother_id(Userother_id);
			var items = await select.Count(out var count)
				.LeftJoin<Userother>("b", "b.id = a.userother_id").Page(page, limit).ToListAsync();
			ViewBag.items = items;
			ViewBag.count = count;
			return View();
		}

		[HttpGet(@"add")]
		public ActionResult Edit() {
			return View();
		}
		[HttpGet(@"edit")]
		async public Task<ActionResult> Edit([FromQuery] long Userother_id) {
			Userother2Info item = await Userother2.GetItemAsync(Userother_id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] long? Userother_id, [FromForm] string Chinesename, [FromForm] string Xxxx) {
			Userother2Info item = new Userother2Info();
			item.Userother_id = Userother_id;
			item.Chinesename = Chinesename;
			item.Xxxx = Xxxx;
			item = await Userother2.InsertAsync(item);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] long Userother_id, [FromForm] string Chinesename, [FromForm] string Xxxx) {
			Userother2Info item = await Userother2.GetItemAsync(Userother_id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.Chinesename = Chinesename;
			item.Xxxx = Xxxx;
			int affrows = await Userother2.UpdateAsync(item);
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] long[] id) {
			int affrows = 0;
			foreach (long id2 in id)
				affrows += await Userother2.DeleteAsync(id2);
			if (affrows > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{affrows}");
			return APIReturn.失败;
		}
	}
}
