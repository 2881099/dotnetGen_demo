using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class TopicaddfieldInfo {
		#region fields
		private int? _Id;
		private string _Name;
		private string _Title222;
		private string _Xxxx;
		#endregion

		public TopicaddfieldInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Topicaddfield(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Name == null ? "null" : _Name.Replace("|", StringifySplit), "|",
				_Title222 == null ? "null" : _Title222.Replace("|", StringifySplit), "|",
				_Xxxx == null ? "null" : _Xxxx.Replace("|", StringifySplit));
		}
		public static TopicaddfieldInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 4, StringSplitOptions.None);
			if (ret.Length != 4) throw new Exception($"格式不正确，TopicaddfieldInfo：{stringify}");
			TopicaddfieldInfo item = new TopicaddfieldInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Name = ret[1].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[2]) != 0) item.Title222 = ret[2].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[3]) != 0) item.Xxxx = ret[3].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(TopicaddfieldInfo).GetField("JsonIgnore");
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
				__jsonIgnore.ContainsKey("Name") ? string.Empty : string.Format(", Name : {0}", Name == null ? "null" : string.Format("'{0}'", Name.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Title222") ? string.Empty : string.Format(", Title222 : {0}", Title222 == null ? "null" : string.Format("'{0}'", Title222.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Xxxx") ? string.Empty : string.Format(", Xxxx : {0}", Xxxx == null ? "null" : string.Format("'{0}'", Xxxx.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (allField || !__jsonIgnore.ContainsKey("Name")) ht["Name"] = Name;
			if (allField || !__jsonIgnore.ContainsKey("Title222")) ht["Title222"] = Title222;
			if (allField || !__jsonIgnore.ContainsKey("Xxxx")) ht["Xxxx"] = Xxxx;
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

		[JsonProperty] public string Name {
			get { return _Name; }
			set { _Name = value; }
		}

		[JsonProperty] public string Title222 {
			get { return _Title222; }
			set { _Title222 = value; }
		}

		[JsonProperty] public string Xxxx {
			get { return _Xxxx; }
			set { _Xxxx = value; }
		}

		#endregion

		public cd.DAL.Topicaddfield.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Topicaddfield.UpdateDiy(new List<TopicaddfieldInfo> { this });

		#region sync methods

		public TopicaddfieldInfo Save() {
			if (this.Id != null) {
				if (BLL.Topicaddfield.Update(this) == 0) return BLL.Topicaddfield.Insert(this);
				return this;
			}
			return BLL.Topicaddfield.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<TopicaddfieldInfo> SaveAsync() {
			if (this.Id != null) {
				if (await BLL.Topicaddfield.UpdateAsync(this) == 0) return await BLL.Topicaddfield.InsertAsync(this);
				return this;
			}
			return await BLL.Topicaddfield.InsertAsync(this);
		}
		#endregion
	}
}

