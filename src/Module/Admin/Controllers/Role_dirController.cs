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
	public class Role_dirController : BaseController {
		public Role_dirController(ILogger<Role_dirController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromQuery] uint?[] Dir_id, [FromQuery] uint?[] Role_id, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Role_dir.Select;
			if (Dir_id.Length > 0) select.WhereDir_id(Dir_id);
			if (Role_id.Length > 0) select.WhereRole_id(Role_id);
			var items = await select.Count(out var count)
				.LeftJoin(a => a.Obj_dir.Id == a.Dir_id)
				.LeftJoin(a => a.Obj_role.Id == a.Role_id).Page(page, limit).ToListAsync();
			ViewBag.items = items;
			ViewBag.count = count;
			return View();
		}

		[HttpGet(@"add")]
		public ActionResult Edit() {
			return View();
		}
		[HttpGet(@"edit")]
		async public Task<ActionResult> Edit([FromQuery] uint Dir_id, [FromQuery] uint Role_id) {
			Role_dirInfo item = await Role_dir.GetItemAsync(Dir_id, Role_id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] uint? Dir_id, [FromForm] uint? Role_id) {
			Role_dirInfo item = new Role_dirInfo();
			item.Dir_id = Dir_id;
			item.Role_id = Role_id;
			item = await Role_dir.InsertAsync(item);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] uint Dir_id, [FromQuery] uint Role_id) {
			Role_dirInfo item = await Role_dir.GetItemAsync(Dir_id, Role_id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			int affrows = await Role_dir.UpdateAsync(item);
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] string[] id) {
			int affrows = 0;
			foreach (string id2 in id) {
				string[] vs = id2.Split(',');
				affrows += await Role_dir.DeleteAsync(uint.Parse(vs[0]), uint.Parse(vs[1]));
			}
			if (affrows > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{affrows}");
			return APIReturn.失败;
		}
	}
}
