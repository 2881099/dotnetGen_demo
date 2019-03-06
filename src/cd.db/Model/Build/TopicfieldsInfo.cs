using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class TopicfieldsInfo {
		#region fields
		private int? _TopicId;
		#endregion

		public TopicfieldsInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Topicfields(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_TopicId == null ? "null" : _TopicId.ToString());
		}
		public static TopicfieldsInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 1, StringSplitOptions.None);
			if (ret.Length != 1) throw new Exception($"格式不正确，TopicfieldsInfo：{stringify}");
			TopicfieldsInfo item = new TopicfieldsInfo();
			if (string.Compare("null", ret[0]) != 0) item.TopicId = int.Parse(ret[0]);
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(TopicfieldsInfo).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("TopicId") ? string.Empty : string.Format(", TopicId : {0}", TopicId == null ? "null" : TopicId.ToString()), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("TopicId")) ht["TopicId"] = TopicId;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		[JsonProperty] public int? TopicId {
			get { return _TopicId; }
			set { _TopicId = value; }
		}

		#endregion

		public cd.DAL.Topicfields.SqlUpdateBuild UpdateDiy => _TopicId == null ? null : BLL.Topicfields.UpdateDiy(new List<TopicfieldsInfo> { this });

		#region sync methods

		#endregion

		#region async methods

		#endregion
	}
}

