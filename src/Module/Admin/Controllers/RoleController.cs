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
	public class RoleController : BaseController {
		public RoleController(ILogger<RoleController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromQuery] string key, [FromQuery] uint[] Dir_id, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Role.Select
				.Where(!string.IsNullOrEmpty(key), "a.name like {0}", string.Concat("%", key, "%"));
			if (Dir_id.Length > 0) select.WhereDir_id(Dir_id);
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
			RoleInfo item = await Role.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] string Name, [FromForm] uint[] mn_Dir) {
			RoleInfo item = new RoleInfo();
			item.Create_time = DateTime.Now;
			item.Name = Name;
			item = await Role.InsertAsync(item);
			//关联 Dir
			foreach (uint mn_Dir_in in mn_Dir)
				item.FlagDir(mn_Dir_in);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] uint Id, [FromForm] string Name, [FromForm] uint[] mn_Dir) {
			RoleInfo item = await Role.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.Create_time = DateTime.Now;
			item.Name = Name;
			int affrows = await Role.UpdateAsync(item);
			//关联 Dir
			if (mn_Dir.Length == 0) {
				item.UnflagDirALL();
			} else {
				List<uint> mn_Dir_list = mn_Dir.ToList();
				foreach (var Obj_dir in item.Obj_dirs) {
					int idx = mn_Dir_list.FindIndex(a => a == Obj_dir.Id);
					if (idx == -1) item.UnflagDir(Obj_dir.Id);
					else mn_Dir_list.RemoveAt(idx);
				}
				mn_Dir_list.ForEach(a => item.FlagDir(a));
			}
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] uint[] id) {
			int affrows = 0;
			foreach (uint id2 in id)
				affrows += await Role.DeleteAsync(id2);
			if (affrows > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{affrows}");
			return APIReturn.失败;
		}
	}
}
