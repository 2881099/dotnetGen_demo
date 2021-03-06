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
	public class TopicController : BaseController {
		public TopicController(ILogger<TopicController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromQuery] string key, [FromQuery] int?[] Topic_type_id, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Topic.Select
				.Where(!string.IsNullOrEmpty(key), "a.carddata like {0} or a.content like {0} or a.title like {0}", string.Concat("%", key, "%"));
			if (Topic_type_id.Length > 0) select.WhereTopic_type_id(Topic_type_id);
			var items = await select.Count(out var count)
				.LeftJoin(a => a.Obj_topic_type.Id == a.Topic_type_id).Page(page, limit).ToListAsync();
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
			TopicInfo item = await Topic.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] int? Topic_type_id, [FromForm] string Carddata, [FromForm] TopicCARDTYPE? Cardtype, [FromForm] ulong? Clicks, [FromForm] string Content, [FromForm] DateTime? Order_time, [FromForm] byte? Test_addfiled, [FromForm] TopicTEST_SETFIELD[] Test_setfield, [FromForm] string Title, [FromForm] int? Tyyp2_id) {
			TopicInfo item = new TopicInfo();
			item.Topic_type_id = Topic_type_id;
			item.Carddata = Carddata;
			item.Cardtype = Cardtype;
			item.Clicks = Clicks;
			item.Content = Content;
			item.Create_time = DateTime.Now;
			item.Order_time = Order_time;
			item.Test_addfiled = Test_addfiled;
			item.Test_setfield = null;
			Test_setfield?.ToList().ForEach(a => item.Test_setfield = (item.Test_setfield ?? 0) | a);
			item.Title = Title;
			item.Tyyp2_id = Tyyp2_id;
			item.Update_time = DateTime.Now;
			item = await Topic.InsertAsync(item);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] uint Id, [FromForm] int? Topic_type_id, [FromForm] string Carddata, [FromForm] TopicCARDTYPE? Cardtype, [FromForm] ulong? Clicks, [FromForm] string Content, [FromForm] DateTime? Order_time, [FromForm] byte? Test_addfiled, [FromForm] TopicTEST_SETFIELD[] Test_setfield, [FromForm] string Title, [FromForm] int? Tyyp2_id) {
			TopicInfo item = await Topic.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.Topic_type_id = Topic_type_id;
			item.Carddata = Carddata;
			item.Cardtype = Cardtype;
			item.Clicks = Clicks;
			item.Content = Content;
			item.Create_time = DateTime.Now;
			item.Order_time = Order_time;
			item.Test_addfiled = Test_addfiled;
			item.Test_setfield = null;
			Test_setfield?.ToList().ForEach(a => item.Test_setfield = (item.Test_setfield ?? 0) | a);
			item.Title = Title;
			item.Tyyp2_id = Tyyp2_id;
			item.Update_time = DateTime.Now;
			int affrows = await Topic.UpdateAsync(item);
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] uint[] id) {
			int affrows = 0;
			foreach (uint id2 in id)
				affrows += await Topic.DeleteAsync(id2);
			if (affrows > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{affrows}");
			return APIReturn.失败;
		}
	}
}
