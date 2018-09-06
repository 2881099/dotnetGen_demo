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
	public class Song_tagController : BaseController {
		public Song_tagController(ILogger<Song_tagController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromServices]IConfiguration cfg, [FromQuery] int?[] Song_id, [FromQuery] int?[] Tag_id, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Song_tag.Select;
			if (Song_id.Length > 0) select.WhereSong_id(Song_id);
			if (Tag_id.Length > 0) select.WhereTag_id(Tag_id);
			var items = await select.Count(out var count)
				.LeftJoin<Song>("b", "b.id = a.song_id")
				.LeftJoin<Tag>("c", "c.id = a.tag_id").Page(page, limit).ToListAsync();
			ViewBag.items = items;
			ViewBag.count = count;
			return View();
		}

		[HttpGet(@"add")]
		public ActionResult Edit() {
			return View();
		}
		[HttpGet(@"edit")]
		async public Task<ActionResult> Edit([FromQuery] int Song_id, [FromQuery] int Tag_id) {
			Song_tagInfo item = await Song_tag.GetItemAsync(Song_id, Tag_id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] int? Song_id, [FromForm] int? Tag_id) {
			Song_tagInfo item = new Song_tagInfo();
			item.Song_id = Song_id;
			item.Tag_id = Tag_id;
			item = await Song_tag.InsertAsync(item);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] int Song_id, [FromQuery] int Tag_id) {
			Song_tagInfo item = await Song_tag.GetItemAsync(Song_id, Tag_id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			int affrows = await Song_tag.UpdateAsync(item);
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] string[] id) {
			int affrows = 0;
			foreach (string id2 in id) {
				string[] vs = id2.Split(',');
				affrows += await Song_tag.DeleteAsync(int.Parse(vs[0]), int.Parse(vs[1]));
			}
			if (affrows > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{affrows}");
			return APIReturn.失败;
		}
	}
}
