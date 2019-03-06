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

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "cccccdddwww", 1, "/cccccdddwww/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/cccccdddwww/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/cccccdddwww/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/cccccdddwww/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/cccccdddwww/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "dir", 2, "/dir/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/dir/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/dir/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/dir/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/dir/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "nullaggretesttable", 3, "/nullaggretesttable/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/nullaggretesttable/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/nullaggretesttable/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/nullaggretesttable/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/nullaggretesttable/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "order", 4, "/order/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/order/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/order/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/order/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/order/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "orderdetail", 5, "/orderdetail/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/orderdetail/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/orderdetail/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/orderdetail/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/orderdetail/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "post", 6, "/post/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/post/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/post/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/post/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/post/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "role", 7, "/role/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/role/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/role/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/role/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/role/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "role_dir", 8, "/role_dir/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/role_dir/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/role_dir/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/role_dir/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/role_dir/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "song", 9, "/song/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/song/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/song/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/song/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/song/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "song_tag", 10, "/song_tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/song_tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/song_tag/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/song_tag/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/song_tag/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "tag", 11, "/tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/tag/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/tag/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/tag/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "tb_alltype", 12, "/tb_alltype/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/tb_alltype/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/tb_alltype/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/tb_alltype/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/tb_alltype/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "tb_topic", 13, "/tb_topic/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/tb_topic/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/tb_topic/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/tb_topic/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/tb_topic/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "tb_topic111333", 14, "/tb_topic111333/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/tb_topic111333/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/tb_topic111333/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/tb_topic111333/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/tb_topic111333/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "tb_topic333", 15, "/tb_topic333/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/tb_topic333/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/tb_topic333/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/tb_topic333/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/tb_topic333/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "testtypeinfo", 16, "/testtypeinfo/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/testtypeinfo/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/testtypeinfo/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/testtypeinfo/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/testtypeinfo/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "testtypeparentinfo", 17, "/testtypeparentinfo/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/testtypeparentinfo/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/testtypeparentinfo/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/testtypeparentinfo/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/testtypeparentinfo/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "topic", 18, "/topic/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/topic/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/topic/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/topic/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/topic/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "topic_type", 19, "/topic_type/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/topic_type/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/topic_type/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/topic_type/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/topic_type/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "topic2111sss", 20, "/topic2111sss/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/topic2111sss/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/topic2111sss/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/topic2111sss/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/topic2111sss/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "topicaddfield", 21, "/topicaddfield/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/topicaddfield/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/topicaddfield/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/topicaddfield/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/topicaddfield/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "topicfields", 22, "/topicfields/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/topicfields/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/topicfields/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/topicfields/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/topicfields/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "topiclazy", 23, "/topiclazy/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/topiclazy/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/topiclazy/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/topiclazy/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/topiclazy/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "userother", 24, "/userother/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/userother/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/userother/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/userother/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/userother/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "userother2", 25, "/userother2/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/userother2/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/userother2/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/userother2/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/userother2/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "xxdkdkdk1", 26, "/xxdkdkdk1/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/xxdkdkdk1/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/xxdkdkdk1/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/xxdkdkdk1/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/xxdkdkdk1/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "xxdkdkdk1222", 27, "/xxdkdkdk1222/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/xxdkdkdk1222/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/xxdkdkdk1222/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/xxdkdkdk1222/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/xxdkdkdk1222/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "xxx", 28, "/xxx/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/xxx/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/xxx/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/xxx/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/xxx/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "xxxddd", 29, "/xxxddd/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/xxxddd/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/xxxddd/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/xxxddd/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/xxxddd/del");
			*/
			return new APIReturn(0, "管理目录已初始化完成。");
		}
	}
}
