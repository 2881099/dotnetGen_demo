using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using cd.Model;

public static partial class cdExtensionMethods {

	public static string ToJson(this PostInfo item) => string.Concat(item);
	public static string ToJson(this PostInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<PostInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this PostInfo[] items, Func<PostInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<PostInfo> items, Func<PostInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Post.SqlUpdateBuild UpdateDiy(this List<PostInfo> items) => cd.BLL.Post.UpdateDiy(items);

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

	public static string ToJson(this TopicInfo item) => string.Concat(item);
	public static string ToJson(this TopicInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<TopicInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this TopicInfo[] items, Func<TopicInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<TopicInfo> items, Func<TopicInfo, object> func = null) => GetBson(items, func);
	public static cd.DAL.Topic.SqlUpdateBuild UpdateDiy(this List<TopicInfo> items) => cd.BLL.Topic.UpdateDiy(items);

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