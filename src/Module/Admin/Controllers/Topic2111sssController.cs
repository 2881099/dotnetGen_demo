﻿using System;
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
	public class Topic2111sssController : BaseController {
		public Topic2111sssController(ILogger<Topic2111sssController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromQuery] string key, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Topic2111sss.Select
				.Where(!string.IsNullOrEmpty(key), "a.Title2 like {0}", string.Concat("%", key, "%"));
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
		async public Task<ActionResult> Edit([FromQuery] uint Id) {
			Topic2111sssInfo item = await Topic2111sss.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] int? Clicks, [FromForm] DateTime? CreateTime, [FromForm] ushort? Fusho, [FromForm] string Title2) {
			Topic2111sssInfo item = new Topic2111sssInfo();
			item.Clicks = Clicks;
			item.CreateTime = CreateTime;
			item.Fusho = Fusho;
			item.Title2 = Title2;
			item = await Topic2111sss.InsertAsync(item);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] uint Id, [FromForm] int? Clicks, [FromForm] DateTime? CreateTime, [FromForm] ushort? Fusho, [FromForm] string Title2) {
			Topic2111sssInfo item = await Topic2111sss.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.Clicks = Clicks;
			item.CreateTime = CreateTime;
			item.Fusho = Fusho;
			item.Title2 = Title2;
			int affrows = await Topic2111sss.UpdateAsync(item);
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] uint[] id) {
			int affrows = 0;
			foreach (uint id2 in id)
				affrows += await Topic2111sss.DeleteAsync(id2);
			if (affrows > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{affrows}");
			return APIReturn.失败;
		}
	}
}
