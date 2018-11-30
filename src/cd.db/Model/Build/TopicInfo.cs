using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class TopicInfo {
		#region fields
		private uint? _Id;
		private int? _Topic_type_id;
		private Topic_typeInfo _obj_topic_type;
		private string _Carddata;
		private TopicCARDTYPE? _Cardtype;
		private ulong? _Clicks;
		private string _Content;
		private DateTime? _Create_time;
		private DateTime? _Order_time;
		private byte? _Test_addfiled;
		private string _Title;
		private DateTime? _Update_time;
		#endregion

		public TopicInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Topic(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Topic_type_id == null ? "null" : _Topic_type_id.ToString(), "|",
				_Carddata == null ? "null" : _Carddata.Replace("|", StringifySplit), "|",
				_Cardtype == null ? "null" : _Cardtype.ToInt64().ToString(), "|",
				_Clicks == null ? "null" : _Clicks.ToString(), "|",
				_Content == null ? "null" : _Content.Replace("|", StringifySplit), "|",
				_Create_time == null ? "null" : _Create_time.Value.Ticks.ToString(), "|",
				_Order_time == null ? "null" : _Order_time.Value.Ticks.ToString(), "|",
				_Test_addfiled == null ? "null" : _Test_addfiled.ToString(), "|",
				_Title == null ? "null" : _Title.Replace("|", StringifySplit), "|",
				_Update_time == null ? "null" : _Update_time.Value.Ticks.ToString());
		}
		public static TopicInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 11, StringSplitOptions.None);
			if (ret.Length != 11) throw new Exception($"格式不正确，TopicInfo：{stringify}");
			TopicInfo item = new TopicInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = uint.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Topic_type_id = int.Parse(ret[1]);
			if (string.Compare("null", ret[2]) != 0) item.Carddata = ret[2].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[3]) != 0) item.Cardtype = (TopicCARDTYPE)long.Parse(ret[3]);
			if (string.Compare("null", ret[4]) != 0) item.Clicks = ulong.Parse(ret[4]);
			if (string.Compare("null", ret[5]) != 0) item.Content = ret[5].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[6]) != 0) item.Create_time = new DateTime(long.Parse(ret[6]));
			if (string.Compare("null", ret[7]) != 0) item.Order_time = new DateTime(long.Parse(ret[7]));
			if (string.Compare("null", ret[8]) != 0) item.Test_addfiled = byte.Parse(ret[8]);
			if (string.Compare("null", ret[9]) != 0) item.Title = ret[9].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[10]) != 0) item.Update_time = new DateTime(long.Parse(ret[10]));
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(TopicInfo).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("Id") ? string.Empty : string.Format(", Id : {0}", Id == null ? "null" : Id.ToString()), 
				__jsonIgnore.ContainsKey("Topic_type_id") ? string.Empty : string.Format(", Topic_type_id : {0}", Topic_type_id == null ? "null" : Topic_type_id.ToString()), 
				__jsonIgnore.ContainsKey("Carddata") ? string.Empty : string.Format(", Carddata : {0}", Carddata == null ? "null" : string.Format("'{0}'", Carddata.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Cardtype") ? string.Empty : string.Format(", Cardtype : {0}", Cardtype == null ? "null" : string.Format("'{0}'", Cardtype.ToDescriptionOrString().Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Clicks") ? string.Empty : string.Format(", Clicks : {0}", Clicks == null ? "null" : Clicks.ToString()), 
				__jsonIgnore.ContainsKey("Content") ? string.Empty : string.Format(", Content : {0}", Content == null ? "null" : string.Format("'{0}'", Content.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Create_time") ? string.Empty : string.Format(", Create_time : {0}", Create_time == null ? "null" : Create_time.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString()), 
				__jsonIgnore.ContainsKey("Order_time") ? string.Empty : string.Format(", Order_time : {0}", Order_time == null ? "null" : Order_time.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString()), 
				__jsonIgnore.ContainsKey("Test_addfiled") ? string.Empty : string.Format(", Test_addfiled : {0}", Test_addfiled == null ? "null" : Test_addfiled.ToString()), 
				__jsonIgnore.ContainsKey("Title") ? string.Empty : string.Format(", Title : {0}", Title == null ? "null" : string.Format("'{0}'", Title.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Update_time") ? string.Empty : string.Format(", Update_time : {0}", Update_time == null ? "null" : Update_time.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString()), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (allField || !__jsonIgnore.ContainsKey("Topic_type_id")) ht["Topic_type_id"] = Topic_type_id;
			if (allField || !__jsonIgnore.ContainsKey("Carddata")) ht["Carddata"] = Carddata;
			if (allField || !__jsonIgnore.ContainsKey("Cardtype")) ht["Cardtype"] = Cardtype?.ToDescriptionOrString();
			if (allField || !__jsonIgnore.ContainsKey("Clicks")) ht["Clicks"] = Clicks;
			if (allField || !__jsonIgnore.ContainsKey("Content")) ht["Content"] = Content;
			if (allField || !__jsonIgnore.ContainsKey("Create_time")) ht["Create_time"] = Create_time;
			if (allField || !__jsonIgnore.ContainsKey("Order_time")) ht["Order_time"] = Order_time;
			if (allField || !__jsonIgnore.ContainsKey("Test_addfiled")) ht["Test_addfiled"] = Test_addfiled;
			if (allField || !__jsonIgnore.ContainsKey("Title")) ht["Title"] = Title;
			if (allField || !__jsonIgnore.ContainsKey("Update_time")) ht["Update_time"] = Update_time;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		[JsonProperty] public uint? Id {
			get { return _Id; }
			set { _Id = value; }
		}

		/// <summary>
		/// 类型id
		/// </summary>
		[JsonProperty] public int? Topic_type_id {
			get { return _Topic_type_id; }
			set {
				if (_Topic_type_id != value) _obj_topic_type = null;
				_Topic_type_id = value;
			}
		}

		public Topic_typeInfo Obj_topic_type {
			get {
				if (_obj_topic_type == null && _Topic_type_id != null) _obj_topic_type = BLL.Topic_type.GetItem(_Topic_type_id.Value);
				return _obj_topic_type;
			}
			internal set { _obj_topic_type = value; }
		}

		/// <summary>
		/// 卡片渲染数据
		/// </summary>
		[JsonProperty] public string Carddata {
			get { return _Carddata; }
			set { _Carddata = value; }
		}

		/// <summary>
		/// 卡片类型
		/// </summary>
		[JsonProperty] public TopicCARDTYPE? Cardtype {
			get { return _Cardtype; }
			set { _Cardtype = value; }
		}

		/// <summary>
		/// 点击次数
		/// </summary>
		[JsonProperty] public ulong? Clicks {
			get { return _Clicks; }
			set { _Clicks = value; }
		}

		/// <summary>
		/// 内容
		/// </summary>
		[JsonProperty] public string Content {
			get { return _Content; }
			set { _Content = value; }
		}

		/// <summary>
		/// 创建时间
		/// </summary>
		[JsonProperty] public DateTime? Create_time {
			get { return _Create_time; }
			set { _Create_time = value; }
		}

		/// <summary>
		/// 排序时间
		/// </summary>
		[JsonProperty] public DateTime? Order_time {
			get { return _Order_time; }
			set { _Order_time = value; }
		}

		/// <summary>
		/// 测试添加的字段
		/// 
		/// 换行
		/// 
		/// sdgsdg
		/// </summary>
		[JsonProperty] public byte? Test_addfiled {
			get { return _Test_addfiled; }
			set { _Test_addfiled = value; }
		}

		/// <summary>
		/// 标题
		/// </summary>
		[JsonProperty] public string Title {
			get { return _Title; }
			set { _Title = value; }
		}

		/// <summary>
		/// 修改时间
		/// </summary>
		[JsonProperty] public DateTime? Update_time {
			get { return _Update_time; }
			set { _Update_time = value; }
		}

		private List<PostInfo> _obj_posts;
		public List<PostInfo> Obj_posts => _obj_posts ?? (_obj_posts = BLL.Post.SelectByTopic_id(_Id).Limit(500).ToList());
		#endregion

		public cd.DAL.Topic.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Topic.UpdateDiy(new List<TopicInfo> { this });

		#region sync methods

		public TopicInfo Save() {
			this.Update_time = DateTime.Now;
			if (this.Id != null) {
				if (BLL.Topic.Update(this) == 0) return BLL.Topic.Insert(this);
				return this;
			}
			this.Create_time = DateTime.Now;
			return BLL.Topic.Insert(this);
		}
		public PostInfo AddPost(string Content) => AddPost(new PostInfo {
				Content = Content});
		public PostInfo AddPost(PostInfo item) {
			item.Topic_id = this.Id;
			return BLL.Post.Insert(item);
		}

		#endregion

		#region async methods

		async public Task<TopicInfo> SaveAsync() {
			this.Update_time = DateTime.Now;
			if (this.Id != null) {
				if (await BLL.Topic.UpdateAsync(this) == 0) return await BLL.Topic.InsertAsync(this);
				return this;
			}
			this.Create_time = DateTime.Now;
			return await BLL.Topic.InsertAsync(this);
		}
		async public Task<PostInfo> AddPostAsync(string Content) => await AddPostAsync(new PostInfo {
				Content = Content});
		async public Task<PostInfo> AddPostAsync(PostInfo item) {
			item.Topic_id = this.Id;
			return await BLL.Post.InsertAsync(item);
		}

		#endregion
	}
	public enum TopicCARDTYPE {
		视频 = 1, 图文01, 图文02, 链接
	}
}

