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
	public class Tb_topicController : BaseController {
		public Tb_topicController(ILogger<Tb_topicController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromQuery] string key, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Tb_topic.Select
				.Where(!string.IsNullOrEmpty(key), "a.Title like {0}", string.Concat("%", key, "%"));
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
			Tb_topicInfo item = await Tb_topic.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] int? Clicks, [FromForm] DateTime? CreateTime, [FromForm] ushort? Fusho, [FromForm] int? TestTypeInfoGuid, [FromForm] string Title, [FromForm] int? TypeGuid) {
			Tb_topicInfo item = new Tb_topicInfo();
			item.Clicks = Clicks;
			item.CreateTime = CreateTime;
			item.Fusho = Fusho;
			item.TestTypeInfoGuid = TestTypeInfoGuid;
			item.Title = Title;
			item.TypeGuid = TypeGuid;
			item = await Tb_topic.InsertAsync(item);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] int Id, [FromForm] int? Clicks, [FromForm] DateTime? CreateTime, [FromForm] ushort? Fusho, [FromForm] int? TestTypeInfoGuid, [FromForm] string Title, [FromForm] int? TypeGuid) {
			Tb_topicInfo item = await Tb_topic.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.Clicks = Clicks;
			item.CreateTime = CreateTime;
			item.Fusho = Fusho;
			item.TestTypeInfoGuid = TestTypeInfoGuid;
			item.Title = Title;
			item.TypeGuid = TypeGuid;
			int affrows = await Tb_topic.UpdateAsync(item);
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] int[] id) {
			int affrows = 0;
			foreach (int id2 in id)
				affrows += await Tb_topic.DeleteAsync(id2);
			if (affrows > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{affrows}");
			return APIReturn.失败;
		}
	}
}
