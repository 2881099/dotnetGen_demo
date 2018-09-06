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
	public class TagController : BaseController {
		public TagController(ILogger<TagController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromServices]IConfiguration cfg, [FromQuery] string key, [FromQuery] int?[] Parent_id, [FromQuery] int[] Song_id, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Tag.Select
				.Where(!string.IsNullOrEmpty(key), "a.name like {0}", string.Concat("%", key, "%"));
			if (Parent_id.Length > 0) select.WhereParent_id(Parent_id);
			if (Song_id.Length > 0) select.WhereSong_id(Song_id);
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
			TagInfo item = await Tag.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] int? Parent_id, [FromForm] string Name, [FromForm] int[] mn_Song) {
			TagInfo item = new TagInfo();
			item.Parent_id = Parent_id;
			item.Name = Name;
			item = await Tag.InsertAsync(item);
			//关联 Song
			foreach (int mn_Song_in in mn_Song)
				item.FlagSong(mn_Song_in);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] int Id, [FromForm] int? Parent_id, [FromForm] string Name, [FromForm] int[] mn_Song) {
			TagInfo item = await Tag.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.Parent_id = Parent_id;
			item.Name = Name;
			int affrows = await Tag.UpdateAsync(item);
			//关联 Song
			if (mn_Song.Length == 0) {
				item.UnflagSongALL();
			} else {
				List<int> mn_Song_list = mn_Song.ToList();
				foreach (var Obj_song in item.Obj_songs) {
					int idx = mn_Song_list.FindIndex(a => a == Obj_song.Id);
					if (idx == -1) item.UnflagSong(Obj_song.Id);
					else mn_Song_list.RemoveAt(idx);
				}
				mn_Song_list.ForEach(a => item.FlagSong(a));
			}
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] int[] id) {
			int affrows = 0;
			foreach (int id2 in id)
				affrows += await Tag.DeleteAsync(id2);
			if (affrows > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{affrows}");
			return APIReturn.失败;
		}
	}
}
