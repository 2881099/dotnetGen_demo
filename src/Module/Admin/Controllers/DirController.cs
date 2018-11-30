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
	public class DirController : BaseController {
		public DirController(ILogger<DirController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromQuery] string key, [FromQuery] uint[] Role_id, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Dir.Select
				.Where(!string.IsNullOrEmpty(key), "a.path like {0} or a.title like {0}", string.Concat("%", key, "%"));
			if (Role_id.Length > 0) select.WhereRole_id(Role_id);
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
			DirInfo item = await Dir.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] string Path, [FromForm] IFormFile Path_file, [FromForm] string Title, [FromForm] uint[] mn_Role) {
			DirInfo item = new DirInfo();
			if (Path_file != null) {
				item.Path = $"/upload/{Guid.NewGuid().ToString()}.png";
				using (FileStream fs = new FileStream(System.IO.Path.Combine(AppContext.BaseDirectory, item.Path), FileMode.Create)) Path_file.CopyTo(fs);
			} else
				item.Path = Path;
			item.Title = Title;
			item = await Dir.InsertAsync(item);
			//关联 Role
			foreach (uint mn_Role_in in mn_Role)
				item.FlagRole(mn_Role_in);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] uint Id, [FromForm] string Path, [FromForm] IFormFile Path_file, [FromForm] string Title, [FromForm] uint[] mn_Role) {
			DirInfo item = await Dir.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			if (!string.IsNullOrEmpty(item.Path) && (item.Path != Path || Path_file != null)) {
				string path = System.IO.Path.Combine(AppContext.BaseDirectory, item.Path);
				if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
			}
			if (Path_file != null) {
				item.Path = $"/upload/{Guid.NewGuid().ToString()}.png";
				using (FileStream fs = new FileStream(System.IO.Path.Combine(AppContext.BaseDirectory, item.Path), FileMode.Create)) Path_file.CopyTo(fs);
			} else
				item.Path = Path;
			item.Title = Title;
			int affrows = await Dir.UpdateAsync(item);
			//关联 Role
			if (mn_Role.Length == 0) {
				item.UnflagRoleALL();
			} else {
				List<uint> mn_Role_list = mn_Role.ToList();
				foreach (var Obj_role in item.Obj_roles) {
					int idx = mn_Role_list.FindIndex(a => a == Obj_role.Id);
					if (idx == -1) item.UnflagRole(Obj_role.Id);
					else mn_Role_list.RemoveAt(idx);
				}
				mn_Role_list.ForEach(a => item.FlagRole(a));
			}
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] uint[] id) {
			int affrows = 0;
			foreach (uint id2 in id)
				affrows += await Dir.DeleteAsync(id2);
			if (affrows > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{affrows}");
			return APIReturn.失败;
		}
	}
}
