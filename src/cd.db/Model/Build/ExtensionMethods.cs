using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using cd.Model;

public static partial class cdExtensionMethods {

	public static string ToJson(this CccccdddwwwInfo item) => string.Concat(item);
	public static string ToJson(this CccccdddwwwInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<CccccdddwwwInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this CccccdddwwwInfo[] items, Func<CccccdddwwwInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<CccccdddwwwInfo> items, Func<CccccdddwwwInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Cccccdddwww.SqlUpdateBuild UpdateDiy(this List<CccccdddwwwInfo> items) => cd.BLL.Cccccdddwww.UpdateDiy(items);

	public static string ToJson(this DirInfo item) => string.Concat(item);
	public static string ToJson(this DirInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<DirInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this DirInfo[] items, Func<DirInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<DirInfo> items, Func<DirInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Dir.SqlUpdateBuild UpdateDiy(this List<DirInfo> items) => cd.BLL.Dir.UpdateDiy(items);

	public static string ToJson(this NullaggretesttableInfo item) => string.Concat(item);
	public static string ToJson(this NullaggretesttableInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<NullaggretesttableInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this NullaggretesttableInfo[] items, Func<NullaggretesttableInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<NullaggretesttableInfo> items, Func<NullaggretesttableInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Nullaggretesttable.SqlUpdateBuild UpdateDiy(this List<NullaggretesttableInfo> items) => cd.BLL.Nullaggretesttable.UpdateDiy(items);

	public static string ToJson(this OrderInfo item) => string.Concat(item);
	public static string ToJson(this OrderInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<OrderInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this OrderInfo[] items, Func<OrderInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<OrderInfo> items, Func<OrderInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Order.SqlUpdateBuild UpdateDiy(this List<OrderInfo> items) => cd.BLL.Order.UpdateDiy(items);

	public static string ToJson(this OrderdetailInfo item) => string.Concat(item);
	public static string ToJson(this OrderdetailInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<OrderdetailInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this OrderdetailInfo[] items, Func<OrderdetailInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<OrderdetailInfo> items, Func<OrderdetailInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Orderdetail.SqlUpdateBuild UpdateDiy(this List<OrderdetailInfo> items) => cd.BLL.Orderdetail.UpdateDiy(items);

	public static string ToJson(this PostInfo item) => string.Concat(item);
	public static string ToJson(this PostInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<PostInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this PostInfo[] items, Func<PostInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<PostInfo> items, Func<PostInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Post.SqlUpdateBuild UpdateDiy(this List<PostInfo> items) => cd.BLL.Post.UpdateDiy(items);

	public static string ToJson(this RoleInfo item) => string.Concat(item);
	public static string ToJson(this RoleInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<RoleInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this RoleInfo[] items, Func<RoleInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<RoleInfo> items, Func<RoleInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Role.SqlUpdateBuild UpdateDiy(this List<RoleInfo> items) => cd.BLL.Role.UpdateDiy(items);

	public static string ToJson(this Role_dirInfo item) => string.Concat(item);
	public static string ToJson(this Role_dirInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<Role_dirInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this Role_dirInfo[] items, Func<Role_dirInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<Role_dirInfo> items, Func<Role_dirInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Role_dir.SqlUpdateBuild UpdateDiy(this List<Role_dirInfo> items) => cd.BLL.Role_dir.UpdateDiy(items);

	public static string ToJson(this SongInfo item) => string.Concat(item);
	public static string ToJson(this SongInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<SongInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this SongInfo[] items, Func<SongInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<SongInfo> items, Func<SongInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Song.SqlUpdateBuild UpdateDiy(this List<SongInfo> items) => cd.BLL.Song.UpdateDiy(items);

	public static string ToJson(this Song_tagInfo item) => string.Concat(item);
	public static string ToJson(this Song_tagInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<Song_tagInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this Song_tagInfo[] items, Func<Song_tagInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<Song_tagInfo> items, Func<Song_tagInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Song_tag.SqlUpdateBuild UpdateDiy(this List<Song_tagInfo> items) => cd.BLL.Song_tag.UpdateDiy(items);

	public static string ToJson(this TagInfo item) => string.Concat(item);
	public static string ToJson(this TagInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<TagInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this TagInfo[] items, Func<TagInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<TagInfo> items, Func<TagInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Tag.SqlUpdateBuild UpdateDiy(this List<TagInfo> items) => cd.BLL.Tag.UpdateDiy(items);

	public static string ToJson(this Tb_alltypeInfo item) => string.Concat(item);
	public static string ToJson(this Tb_alltypeInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<Tb_alltypeInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this Tb_alltypeInfo[] items, Func<Tb_alltypeInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<Tb_alltypeInfo> items, Func<Tb_alltypeInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Tb_alltype.SqlUpdateBuild UpdateDiy(this List<Tb_alltypeInfo> items) => cd.BLL.Tb_alltype.UpdateDiy(items);

	public static string ToJson(this Tb_topicInfo item) => string.Concat(item);
	public static string ToJson(this Tb_topicInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<Tb_topicInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this Tb_topicInfo[] items, Func<Tb_topicInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<Tb_topicInfo> items, Func<Tb_topicInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Tb_topic.SqlUpdateBuild UpdateDiy(this List<Tb_topicInfo> items) => cd.BLL.Tb_topic.UpdateDiy(items);

	public static string ToJson(this Tb_topic111333Info item) => string.Concat(item);
	public static string ToJson(this Tb_topic111333Info[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<Tb_topic111333Info> items) => GetJson(items);
	public static IDictionary[] ToBson(this Tb_topic111333Info[] items, Func<Tb_topic111333Info, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<Tb_topic111333Info> items, Func<Tb_topic111333Info, object> func = null) => GetBson(items, func);
	public static cd.DAL.Tb_topic111333.SqlUpdateBuild UpdateDiy(this List<Tb_topic111333Info> items) => cd.BLL.Tb_topic111333.UpdateDiy(items);

	public static string ToJson(this Tb_topic333Info item) => string.Concat(item);
	public static string ToJson(this Tb_topic333Info[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<Tb_topic333Info> items) => GetJson(items);
	public static IDictionary[] ToBson(this Tb_topic333Info[] items, Func<Tb_topic333Info, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<Tb_topic333Info> items, Func<Tb_topic333Info, object> func = null) => GetBson(items, func);
	public static cd.DAL.Tb_topic333.SqlUpdateBuild UpdateDiy(this List<Tb_topic333Info> items) => cd.BLL.Tb_topic333.UpdateDiy(items);

	public static string ToJson(this TesttypeinfoInfo item) => string.Concat(item);
	public static string ToJson(this TesttypeinfoInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<TesttypeinfoInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this TesttypeinfoInfo[] items, Func<TesttypeinfoInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<TesttypeinfoInfo> items, Func<TesttypeinfoInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Testtypeinfo.SqlUpdateBuild UpdateDiy(this List<TesttypeinfoInfo> items) => cd.BLL.Testtypeinfo.UpdateDiy(items);

	public static string ToJson(this Testtypeinfo333Info item) => string.Concat(item);
	public static string ToJson(this Testtypeinfo333Info[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<Testtypeinfo333Info> items) => GetJson(items);
	public static IDictionary[] ToBson(this Testtypeinfo333Info[] items, Func<Testtypeinfo333Info, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<Testtypeinfo333Info> items, Func<Testtypeinfo333Info, object> func = null) => GetBson(items, func);

	public static string ToJson(this TesttypeparentinfoInfo item) => string.Concat(item);
	public static string ToJson(this TesttypeparentinfoInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<TesttypeparentinfoInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this TesttypeparentinfoInfo[] items, Func<TesttypeparentinfoInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<TesttypeparentinfoInfo> items, Func<TesttypeparentinfoInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Testtypeparentinfo.SqlUpdateBuild UpdateDiy(this List<TesttypeparentinfoInfo> items) => cd.BLL.Testtypeparentinfo.UpdateDiy(items);

	public static string ToJson(this Testtypeparentinfo23123Info item) => string.Concat(item);
	public static string ToJson(this Testtypeparentinfo23123Info[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<Testtypeparentinfo23123Info> items) => GetJson(items);
	public static IDictionary[] ToBson(this Testtypeparentinfo23123Info[] items, Func<Testtypeparentinfo23123Info, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<Testtypeparentinfo23123Info> items, Func<Testtypeparentinfo23123Info, object> func = null) => GetBson(items, func);

	public static string ToJson(this TopicInfo item) => string.Concat(item);
	public static string ToJson(this TopicInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<TopicInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this TopicInfo[] items, Func<TopicInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<TopicInfo> items, Func<TopicInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Topic.SqlUpdateBuild UpdateDiy(this List<TopicInfo> items) => cd.BLL.Topic.UpdateDiy(items);

	public static string ToJson(this Topic_typeInfo item) => string.Concat(item);
	public static string ToJson(this Topic_typeInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<Topic_typeInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this Topic_typeInfo[] items, Func<Topic_typeInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<Topic_typeInfo> items, Func<Topic_typeInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Topic_type.SqlUpdateBuild UpdateDiy(this List<Topic_typeInfo> items) => cd.BLL.Topic_type.UpdateDiy(items);

	public static string ToJson(this Topic2111sssInfo item) => string.Concat(item);
	public static string ToJson(this Topic2111sssInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<Topic2111sssInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this Topic2111sssInfo[] items, Func<Topic2111sssInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<Topic2111sssInfo> items, Func<Topic2111sssInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Topic2111sss.SqlUpdateBuild UpdateDiy(this List<Topic2111sssInfo> items) => cd.BLL.Topic2111sss.UpdateDiy(items);

	public static string ToJson(this TopicaddfieldInfo item) => string.Concat(item);
	public static string ToJson(this TopicaddfieldInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<TopicaddfieldInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this TopicaddfieldInfo[] items, Func<TopicaddfieldInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<TopicaddfieldInfo> items, Func<TopicaddfieldInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Topicaddfield.SqlUpdateBuild UpdateDiy(this List<TopicaddfieldInfo> items) => cd.BLL.Topicaddfield.UpdateDiy(items);

	public static string ToJson(this TopicfieldsInfo item) => string.Concat(item);
	public static string ToJson(this TopicfieldsInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<TopicfieldsInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this TopicfieldsInfo[] items, Func<TopicfieldsInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<TopicfieldsInfo> items, Func<TopicfieldsInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Topicfields.SqlUpdateBuild UpdateDiy(this List<TopicfieldsInfo> items) => cd.BLL.Topicfields.UpdateDiy(items);

	public static string ToJson(this TopiclazyInfo item) => string.Concat(item);
	public static string ToJson(this TopiclazyInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<TopiclazyInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this TopiclazyInfo[] items, Func<TopiclazyInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<TopiclazyInfo> items, Func<TopiclazyInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Topiclazy.SqlUpdateBuild UpdateDiy(this List<TopiclazyInfo> items) => cd.BLL.Topiclazy.UpdateDiy(items);

	public static string ToJson(this UserotherInfo item) => string.Concat(item);
	public static string ToJson(this UserotherInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<UserotherInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this UserotherInfo[] items, Func<UserotherInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<UserotherInfo> items, Func<UserotherInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Userother.SqlUpdateBuild UpdateDiy(this List<UserotherInfo> items) => cd.BLL.Userother.UpdateDiy(items);

	public static string ToJson(this Userother2Info item) => string.Concat(item);
	public static string ToJson(this Userother2Info[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<Userother2Info> items) => GetJson(items);
	public static IDictionary[] ToBson(this Userother2Info[] items, Func<Userother2Info, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<Userother2Info> items, Func<Userother2Info, object> func = null) => GetBson(items, func);
	public static cd.DAL.Userother2.SqlUpdateBuild UpdateDiy(this List<Userother2Info> items) => cd.BLL.Userother2.UpdateDiy(items);

	public static string ToJson(this V_1Info item) => string.Concat(item);
	public static string ToJson(this V_1Info[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<V_1Info> items) => GetJson(items);
	public static IDictionary[] ToBson(this V_1Info[] items, Func<V_1Info, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<V_1Info> items, Func<V_1Info, object> func = null) => GetBson(items, func);

	public static string ToJson(this Xxdkdkdk1Info item) => string.Concat(item);
	public static string ToJson(this Xxdkdkdk1Info[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<Xxdkdkdk1Info> items) => GetJson(items);
	public static IDictionary[] ToBson(this Xxdkdkdk1Info[] items, Func<Xxdkdkdk1Info, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<Xxdkdkdk1Info> items, Func<Xxdkdkdk1Info, object> func = null) => GetBson(items, func);
	public static cd.DAL.Xxdkdkdk1.SqlUpdateBuild UpdateDiy(this List<Xxdkdkdk1Info> items) => cd.BLL.Xxdkdkdk1.UpdateDiy(items);

	public static string ToJson(this Xxdkdkdk1222Info item) => string.Concat(item);
	public static string ToJson(this Xxdkdkdk1222Info[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<Xxdkdkdk1222Info> items) => GetJson(items);
	public static IDictionary[] ToBson(this Xxdkdkdk1222Info[] items, Func<Xxdkdkdk1222Info, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<Xxdkdkdk1222Info> items, Func<Xxdkdkdk1222Info, object> func = null) => GetBson(items, func);
	public static cd.DAL.Xxdkdkdk1222.SqlUpdateBuild UpdateDiy(this List<Xxdkdkdk1222Info> items) => cd.BLL.Xxdkdkdk1222.UpdateDiy(items);

	public static string ToJson(this XxxInfo item) => string.Concat(item);
	public static string ToJson(this XxxInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<XxxInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this XxxInfo[] items, Func<XxxInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<XxxInfo> items, Func<XxxInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Xxx.SqlUpdateBuild UpdateDiy(this List<XxxInfo> items) => cd.BLL.Xxx.UpdateDiy(items);

	public static string ToJson(this XxxdddInfo item) => string.Concat(item);
	public static string ToJson(this XxxdddInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<XxxdddInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this XxxdddInfo[] items, Func<XxxdddInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<XxxdddInfo> items, Func<XxxdddInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Xxxddd.SqlUpdateBuild UpdateDiy(this List<XxxdddInfo> items) => cd.BLL.Xxxddd.UpdateDiy(items);

	public static string GetJson(IEnumerable items) {
		StringBuilder sb = new StringBuilder();
		sb.Append("[");
		IEnumerator ie = items.GetEnumerator();
		if (ie.MoveNext()) {
			while (true) {
				sb.Append(string.Concat(ie.Current));
				if (ie.MoveNext()) sb.Append(",");
				else break;
			}
		}
		sb.Append("]");
		return sb.ToString();
	}
	public static IDictionary[] GetBson(IEnumerable items, Delegate func = null) {
		List<IDictionary> ret = new List<IDictionary>();
		IEnumerator ie = items.GetEnumerator();
		while (ie.MoveNext()) {
			if (ie.Current == null) ret.Add(null);
			else if (func == null) ret.Add(ie.Current.GetType().GetMethod("ToBson").Invoke(ie.Current, new object[] { false }) as IDictionary);
			else {
				object obj = func.GetMethodInfo().Invoke(func.Target, new object[] { ie.Current });
				if (obj is IDictionary) ret.Add(obj as IDictionary);
				else {
					Hashtable ht = new Hashtable();
					PropertyInfo[] pis = obj.GetType().GetProperties();
					foreach (PropertyInfo pi in pis) ht[pi.Name] = pi.GetValue(obj);
					ret.Add(ht);
				}
			}
		}
		return ret.ToArray();
	}
}