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
	public class SongController : BaseController {
		public SongController(ILogger<SongController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromServices]IConfiguration cfg, [FromQuery] string key, [FromQuery] int[] Tag_id, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Song.Select
				.Where(!string.IsNullOrEmpty(key), "a.title like {0} or a.url like {0}", string.Concat("%", key, "%"));
			if (Tag_id.Length > 0) select.WhereTag_id(Tag_id);
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
			SongInfo item = await Song.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] bool Is_deleted, [FromForm] string Title, [FromForm] string Url, [FromForm] int[] mn_Tag) {
			SongInfo item = new SongInfo();
			item.Create_time = DateTime.Now;
			item.Is_deleted = Is_deleted;
			item.Title = Title;
			item.Url = Url;
			item = await Song.InsertAsync(item);
			//关联 Tag
			foreach (int mn_Tag_in in mn_Tag)
				item.FlagTag(mn_Tag_in);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] int Id, [FromForm] bool Is_deleted, [FromForm] string Title, [FromForm] string Url, [FromForm] int[] mn_Tag) {
			SongInfo item = await Song.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.Create_time = DateTime.Now;
			item.Is_deleted = Is_deleted;
			item.Title = Title;
			item.Url = Url;
			int affrows = await Song.UpdateAsync(item);
			//关联 Tag
			if (mn_Tag.Length == 0) {
				item.UnflagTagALL();
			} else {
				List<int> mn_Tag_list = mn_Tag.ToList();
				foreach (var Obj_tag in item.Obj_tags) {
					int idx = mn_Tag_list.FindIndex(a => a == Obj_tag.Id);
					if (idx == -1) item.UnflagTag(Obj_tag.Id);
					else mn_Tag_list.RemoveAt(idx);
				}
				mn_Tag_list.ForEach(a => item.FlagTag(a));
			}
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] int[] id) {
			int affrows = 0;
			foreach (int id2 in id)
				affrows += await Song.DeleteAsync(id2);
			if (affrows > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{affrows}");
			return APIReturn.失败;
		}
	}
}
