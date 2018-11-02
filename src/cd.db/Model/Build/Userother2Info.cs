using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class Userother2Info {
		#region fields
		private long? _Userother_id;
		private UserotherInfo _obj_userother;
		private string _Chinesename;
		private string _Xxxx;
		#endregion

		public Userother2Info() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Userother2(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Userother_id == null ? "null" : _Userother_id.ToString(), "|",
				_Chinesename == null ? "null" : _Chinesename.Replace("|", StringifySplit), "|",
				_Xxxx == null ? "null" : _Xxxx.Replace("|", StringifySplit));
		}
		public static Userother2Info Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 3, StringSplitOptions.None);
			if (ret.Length != 3) throw new Exception($"格式不正确，Userother2Info：{stringify}");
			Userother2Info item = new Userother2Info();
			if (string.Compare("null", ret[0]) != 0) item.Userother_id = long.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Chinesename = ret[1].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[2]) != 0) item.Xxxx = ret[2].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(Userother2Info).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("Userother_id") ? string.Empty : string.Format(", Userother_id : {0}", Userother_id == null ? "null" : Userother_id.ToString()), 
				__jsonIgnore.ContainsKey("Chinesename") ? string.Empty : string.Format(", Chinesename : {0}", Chinesename == null ? "null" : string.Format("'{0}'", Chinesename.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Xxxx") ? string.Empty : string.Format(", Xxxx : {0}", Xxxx == null ? "null" : string.Format("'{0}'", Xxxx.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Userother_id")) ht["Userother_id"] = Userother_id;
			if (allField || !__jsonIgnore.ContainsKey("Chinesename")) ht["Chinesename"] = Chinesename;
			if (allField || !__jsonIgnore.ContainsKey("Xxxx")) ht["Xxxx"] = Xxxx;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		[JsonProperty] public long? Userother_id {
			get { return _Userother_id; }
			set {
				if (_Userother_id != value) _obj_userother = null;
				_Userother_id = value;
			}
		}

		public UserotherInfo Obj_userother {
			get {
				if (_obj_userother == null && _Userother_id != null) _obj_userother = BLL.Userother.GetItemById(_Userother_id.Value);
				return _obj_userother;
			}
			internal set { _obj_userother = value; }
		}

		[JsonProperty] public string Chinesename {
			get { return _Chinesename; }
			set { _Chinesename = value; }
		}

		[JsonProperty] public string Xxxx {
			get { return _Xxxx; }
			set { _Xxxx = value; }
		}

		#endregion

		public cd.DAL.Userother2.SqlUpdateBuild UpdateDiy => _Userother_id == null ? null : BLL.Userother2.UpdateDiy(new List<Userother2Info> { this });

		#region sync methods

		public Userother2Info Save() {
			if (this.Userother_id != null) {
				if (BLL.Userother2.Update(this) == 0) return BLL.Userother2.Insert(this);
				return this;
			}
			return BLL.Userother2.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<Userother2Info> SaveAsync() {
			if (this.Userother_id != null) {
				if (await BLL.Userother2.UpdateAsync(this) == 0) return await BLL.Userother2.InsertAsync(this);
				return this;
			}
			return await BLL.Userother2.InsertAsync(this);
		}
		#endregion
	}
}

