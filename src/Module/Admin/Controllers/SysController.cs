using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using cd.BLL;
using cd.Model;

namespace cd.Module.Admin.Controllers {
	[Route("[controller]")]
	[Obsolete]
	public class SysController : Controller {
		[HttpGet(@"connection")]
		public ContentResult Get_connection() {
			var sb = new StringBuilder();
			var pools = new List<MySql.Data.MySqlClient.MySqlConnectionPool>();
			pools.Add(SqlHelper.Pool);
			pools.AddRange(SqlHelper.SlavePools);
			for (var a = 0; a < pools.Count; a++) {
				var pool = pools[a];
				sb.AppendLine($@"【{pool.Policy.Name}】　　状态：{(pool.IsAvailable ? "正常" : $"[{pool.UnavailableTime}] {pool.UnavailableException.Message}")}
-------------------------------------------------------------------------------------------------------
{pool.StatisticsFullily}
");
			}
			return new ContentResult { ContentType = "text/plan;charset=utf-8", Content = sb.ToString() };
		}
		[HttpGet(@"connection/redis")]
		public ContentResult Get_connection_redis() {
			var sb = new StringBuilder();
			foreach(var pool in RedisHelper.Nodes.Values) {
				sb.AppendLine($@"【{pool.Policy.Name}】　　状态：{(pool.IsAvailable ? "正常" : $"[{pool.UnavailableTime}] {pool.UnavailableException.Message}")}
-------------------------------------------------------------------------------------------------------
Slots：{RedisHelper.Instance.SlotCache.Count}/16384, {pool.StatisticsFullily}
");
			}
			return new ContentResult { ContentType = "text/plan;charset=utf-8", Content = sb.ToString() };
		}

		[HttpGet(@"init_sysdir")]
		public APIReturn Get_init_sysdir() {
			/*
			if (Sysdir.SelectByParent_id(null).Count() > 0)
				return new APIReturn(-33, "本系统已经初始化过，页面没经过任何操作退出。");

			SysdirInfo dir1, dir2, dir3;
			dir1 = Sysdir.Insert(null, DateTime.Now, "运营管理", 1, null);

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "dir", 1, "/dir/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/dir/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/dir/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/dir/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/dir/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "post", 2, "/post/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/post/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/post/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/post/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/post/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "role", 3, "/role/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/role/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/role/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/role/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/role/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "role_dir", 4, "/role_dir/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/role_dir/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/role_dir/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/role_dir/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/role_dir/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "song", 5, "/song/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/song/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/song/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/song/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/song/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "song_tag", 6, "/song_tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/song_tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/song_tag/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/song_tag/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/song_tag/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "tag", 7, "/tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/tag/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/tag/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/tag/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "topic", 8, "/topic/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/topic/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/topic/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/topic/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/topic/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "topic_type", 9, "/topic_type/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/topic_type/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/topic_type/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/topic_type/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/topic_type/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "userother", 10, "/userother/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/userother/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/userother/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/userother/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/userother/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "userother2", 11, "/userother2/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/userother2/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/userother2/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/userother2/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/userother2/del");
			*/
			return new APIReturn(0, "管理目录已初始化完成。");
		}
	}
}
