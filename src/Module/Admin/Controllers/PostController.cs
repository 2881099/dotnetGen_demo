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
	public class PostController : BaseController {
		public PostController(ILogger<PostController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromServices]IConfiguration cfg, [FromQuery] string key, [FromQuery] uint?[] Topic_id, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Post.Select
				.Where(!string.IsNullOrEmpty(key), "a.content like {0}", string.Concat("%", key, "%"));
			if (Topic_id.Length > 0) select.WhereTopic_id(Topic_id);
			var items = await select.Count(out var count)
				.LeftJoin<Topic>("b", "b.id = a.topic_id").Page(page, limit).ToListAsync();
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
			PostInfo item = await Post.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] uint? Topic_id, [FromForm] string Content) {
			PostInfo item = new PostInfo();
			item.Topic_id = Topic_id;
			item.Content = Content;
			item.Create_time = DateTime.Now;
			item = await Post.InsertAsync(item);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] int Id, [FromForm] uint? Topic_id, [FromForm] string Content) {
			PostInfo item = await Post.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.Topic_id = Topic_id;
			item.Content = Content;
			item.Create_time = DateTime.Now;
			int affrows = await Post.UpdateAsync(item);
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] int[] id) {
			int affrows = 0;
			foreach (int id2 in id)
				affrows += await Post.DeleteAsync(id2);
			if (affrows > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{affrows}");
			return APIReturn.失败;
		}
	}
}
