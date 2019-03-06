using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class Testtypeinfo333Info {
		#region fields
		private int? _Guid;
		private string _Name;
		private int? _ParentId;
		private DateTime? _Time;
		#endregion

		public Testtypeinfo333Info() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Testtypeinfo333(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Guid == null ? "null" : _Guid.ToString(), "|",
				_Name == null ? "null" : _Name.Replace("|", StringifySplit), "|",
				_ParentId == null ? "null" : _ParentId.ToString(), "|",
				_Time == null ? "null" : _Time.Value.Ticks.ToString());
		}
		public static Testtypeinfo333Info Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 4, StringSplitOptions.None);
			if (ret.Length != 4) throw new Exception($"格式不正确，Testtypeinfo333Info：{stringify}");
			Testtypeinfo333Info item = new Testtypeinfo333Info();
			if (string.Compare("null", ret[0]) != 0) item.Guid = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Name = ret[1].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[2]) != 0) item.ParentId = int.Parse(ret[2]);
			if (string.Compare("null", ret[3]) != 0) item.Time = new DateTime(long.Parse(ret[3]));
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(Testtypeinfo333Info).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("Guid") ? string.Empty : string.Format(", Guid : {0}", Guid == null ? "null" : Guid.ToString()), 
				__jsonIgnore.ContainsKey("Name") ? string.Empty : string.Format(", Name : {0}", Name == null ? "null" : string.Format("'{0}'", Name.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("ParentId") ? string.Empty : string.Format(", ParentId : {0}", ParentId == null ? "null" : ParentId.ToString()), 
				__jsonIgnore.ContainsKey("Time") ? string.Empty : string.Format(", Time : {0}", Time == null ? "null" : Time.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString()), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Guid")) ht["Guid"] = Guid;
			if (allField || !__jsonIgnore.ContainsKey("Name")) ht["Name"] = Name;
			if (allField || !__jsonIgnore.ContainsKey("ParentId")) ht["ParentId"] = ParentId;
			if (allField || !__jsonIgnore.ContainsKey("Time")) ht["Time"] = Time;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		[JsonProperty] public int? Guid {
			get { return _Guid; }
			set { _Guid = value; }
		}

		[JsonProperty] public string Name {
			get { return _Name; }
			set { _Name = value; }
		}

		[JsonProperty] public int? ParentId {
			get { return _ParentId; }
			set { _ParentId = value; }
		}

		[JsonProperty] public DateTime? Time {
			get { return _Time; }
			set { _Time = value; }
		}

		#endregion

		#region sync methods

		#endregion

		#region async methods

		#endregion
	}
}

