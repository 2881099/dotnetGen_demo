using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class PostInfo {
		#region fields
		private int? _Id;
		private uint? _Topic_id;
		private TopicInfo _obj_topic;
		private string _Content;
		private DateTime? _Create_time;
		#endregion

		public PostInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Post(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Topic_id == null ? "null" : _Topic_id.ToString(), "|",
				_Content == null ? "null" : _Content.Replace("|", StringifySplit), "|",
				_Create_time == null ? "null" : _Create_time.Value.Ticks.ToString());
		}
		public static PostInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 4, StringSplitOptions.None);
			if (ret.Length != 4) throw new Exception($"格式不正确，PostInfo：{stringify}");
			PostInfo item = new PostInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Topic_id = uint.Parse(ret[1]);
			if (string.Compare("null", ret[2]) != 0) item.Content = ret[2].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[3]) != 0) item.Create_time = new DateTime(long.Parse(ret[3]));
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(PostInfo).GetField("JsonIgnore");
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
				__jsonIgnore.ContainsKey("Topic_id") ? string.Empty : string.Format(", Topic_id : {0}", Topic_id == null ? "null" : Topic_id.ToString()), 
				__jsonIgnore.ContainsKey("Content") ? string.Empty : string.Format(", Content : {0}", Content == null ? "null" : string.Format("'{0}'", Content.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Create_time") ? string.Empty : string.Format(", Create_time : {0}", Create_time == null ? "null" : Create_time.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString()), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (allField || !__jsonIgnore.ContainsKey("Topic_id")) ht["Topic_id"] = Topic_id;
			if (allField || !__jsonIgnore.ContainsKey("Content")) ht["Content"] = Content;
			if (allField || !__jsonIgnore.ContainsKey("Create_time")) ht["Create_time"] = Create_time;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		[JsonProperty] public int? Id {
			get { return _Id; }
			set { _Id = value; }
		}
		/// <summary>
		/// 所属主题
		/// </summary>
		[JsonProperty] public uint? Topic_id {
			get { return _Topic_id; }
			set {
				if (_Topic_id != value) _obj_topic = null;
				_Topic_id = value;
			}
		}
		public TopicInfo Obj_topic {
			get {
				if (_obj_topic == null && _Topic_id != null) _obj_topic = BLL.Topic.GetItem(_Topic_id.Value);
				return _obj_topic;
			}
			internal set { _obj_topic = value; }
		}
		/// <summary>
		/// 回复内容
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
		#endregion

		public cd.DAL.Post.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Post.UpdateDiy(new List<PostInfo> { this });

		#region sync methods

		public PostInfo Save() {
			if (this.Id != null) {
				if (BLL.Post.Update(this) == 0) return BLL.Post.Insert(this);
				return this;
			}
			this.Create_time = DateTime.Now;
			return BLL.Post.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<PostInfo> SaveAsync() {
			if (this.Id != null) {
				if (await BLL.Post.UpdateAsync(this) == 0) return await BLL.Post.InsertAsync(this);
				return this;
			}
			this.Create_time = DateTime.Now;
			return await BLL.Post.InsertAsync(this);
		}
		#endregion
	}
}

