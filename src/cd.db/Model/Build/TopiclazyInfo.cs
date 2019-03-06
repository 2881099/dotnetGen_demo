using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class TopiclazyInfo {
		#region fields
		private int? _Id;
		private int? _Clicks;
		private DateTime? _CreateTime;
		private int? _TestTypeInfoGuid;
		private string _Title;
		#endregion

		public TopiclazyInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Topiclazy(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Clicks == null ? "null" : _Clicks.ToString(), "|",
				_CreateTime == null ? "null" : _CreateTime.Value.Ticks.ToString(), "|",
				_TestTypeInfoGuid == null ? "null" : _TestTypeInfoGuid.ToString(), "|",
				_Title == null ? "null" : _Title.Replace("|", StringifySplit));
		}
		public static TopiclazyInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 5, StringSplitOptions.None);
			if (ret.Length != 5) throw new Exception($"格式不正确，TopiclazyInfo：{stringify}");
			TopiclazyInfo item = new TopiclazyInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Clicks = int.Parse(ret[1]);
			if (string.Compare("null", ret[2]) != 0) item.CreateTime = new DateTime(long.Parse(ret[2]));
			if (string.Compare("null", ret[3]) != 0) item.TestTypeInfoGuid = int.Parse(ret[3]);
			if (string.Compare("null", ret[4]) != 0) item.Title = ret[4].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(TopiclazyInfo).GetField("JsonIgnore");
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
				__jsonIgnore.ContainsKey("Clicks") ? string.Empty : string.Format(", Clicks : {0}", Clicks == null ? "null" : Clicks.ToString()), 
				__jsonIgnore.ContainsKey("CreateTime") ? string.Empty : string.Format(", CreateTime : {0}", CreateTime == null ? "null" : CreateTime.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString()), 
				__jsonIgnore.ContainsKey("TestTypeInfoGuid") ? string.Empty : string.Format(", TestTypeInfoGuid : {0}", TestTypeInfoGuid == null ? "null" : TestTypeInfoGuid.ToString()), 
				__jsonIgnore.ContainsKey("Title") ? string.Empty : string.Format(", Title : {0}", Title == null ? "null" : string.Format("'{0}'", Title.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (allField || !__jsonIgnore.ContainsKey("Clicks")) ht["Clicks"] = Clicks;
			if (allField || !__jsonIgnore.ContainsKey("CreateTime")) ht["CreateTime"] = CreateTime;
			if (allField || !__jsonIgnore.ContainsKey("TestTypeInfoGuid")) ht["TestTypeInfoGuid"] = TestTypeInfoGuid;
			if (allField || !__jsonIgnore.ContainsKey("Title")) ht["Title"] = Title;
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

		[JsonProperty] public int? Clicks {
			get { return _Clicks; }
			set { _Clicks = value; }
		}

		[JsonProperty] public DateTime? CreateTime {
			get { return _CreateTime; }
			set { _CreateTime = value; }
		}

		[JsonProperty] public int? TestTypeInfoGuid {
			get { return _TestTypeInfoGuid; }
			set { _TestTypeInfoGuid = value; }
		}

		[JsonProperty] public string Title {
			get { return _Title; }
			set { _Title = value; }
		}

		#endregion

		public cd.DAL.Topiclazy.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Topiclazy.UpdateDiy(new List<TopiclazyInfo> { this });

		#region sync methods

		public TopiclazyInfo Save() {
			if (this.Id != null) {
				if (BLL.Topiclazy.Update(this) == 0) return BLL.Topiclazy.Insert(this);
				return this;
			}
			return BLL.Topiclazy.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<TopiclazyInfo> SaveAsync() {
			if (this.Id != null) {
				if (await BLL.Topiclazy.UpdateAsync(this) == 0) return await BLL.Topiclazy.InsertAsync(this);
				return this;
			}
			return await BLL.Topiclazy.InsertAsync(this);
		}
		#endregion
	}
}

