using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class Topic2111sssInfo {
		#region fields
		private uint? _Id;
		private int? _Clicks;
		private DateTime? _CreateTime;
		private ushort? _Fusho;
		private string _Title2;
		#endregion

		public Topic2111sssInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Topic2111sss(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Clicks == null ? "null" : _Clicks.ToString(), "|",
				_CreateTime == null ? "null" : _CreateTime.Value.Ticks.ToString(), "|",
				_Fusho == null ? "null" : _Fusho.ToString(), "|",
				_Title2 == null ? "null" : _Title2.Replace("|", StringifySplit));
		}
		public static Topic2111sssInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 5, StringSplitOptions.None);
			if (ret.Length != 5) throw new Exception($"格式不正确，Topic2111sssInfo：{stringify}");
			Topic2111sssInfo item = new Topic2111sssInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = uint.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Clicks = int.Parse(ret[1]);
			if (string.Compare("null", ret[2]) != 0) item.CreateTime = new DateTime(long.Parse(ret[2]));
			if (string.Compare("null", ret[3]) != 0) item.Fusho = ushort.Parse(ret[3]);
			if (string.Compare("null", ret[4]) != 0) item.Title2 = ret[4].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(Topic2111sssInfo).GetField("JsonIgnore");
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
				__jsonIgnore.ContainsKey("Fusho") ? string.Empty : string.Format(", Fusho : {0}", Fusho == null ? "null" : Fusho.ToString()), 
				__jsonIgnore.ContainsKey("Title2") ? string.Empty : string.Format(", Title2 : {0}", Title2 == null ? "null" : string.Format("'{0}'", Title2.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (allField || !__jsonIgnore.ContainsKey("Clicks")) ht["Clicks"] = Clicks;
			if (allField || !__jsonIgnore.ContainsKey("CreateTime")) ht["CreateTime"] = CreateTime;
			if (allField || !__jsonIgnore.ContainsKey("Fusho")) ht["Fusho"] = Fusho;
			if (allField || !__jsonIgnore.ContainsKey("Title2")) ht["Title2"] = Title2;
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

		[JsonProperty] public int? Clicks {
			get { return _Clicks; }
			set { _Clicks = value; }
		}

		[JsonProperty] public DateTime? CreateTime {
			get { return _CreateTime; }
			set { _CreateTime = value; }
		}

		[JsonProperty] public ushort? Fusho {
			get { return _Fusho; }
			set { _Fusho = value; }
		}

		[JsonProperty] public string Title2 {
			get { return _Title2; }
			set { _Title2 = value; }
		}

		#endregion

		public cd.DAL.Topic2111sss.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Topic2111sss.UpdateDiy(new List<Topic2111sssInfo> { this });

		#region sync methods

		public Topic2111sssInfo Save() {
			if (this.Id != null) {
				if (BLL.Topic2111sss.Update(this) == 0) return BLL.Topic2111sss.Insert(this);
				return this;
			}
			return BLL.Topic2111sss.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<Topic2111sssInfo> SaveAsync() {
			if (this.Id != null) {
				if (await BLL.Topic2111sss.UpdateAsync(this) == 0) return await BLL.Topic2111sss.InsertAsync(this);
				return this;
			}
			return await BLL.Topic2111sss.InsertAsync(this);
		}
		#endregion
	}
}

