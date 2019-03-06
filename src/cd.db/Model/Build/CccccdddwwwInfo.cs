using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class CccccdddwwwInfo {
		#region fields
		private int? _Idx;
		private string _Name;
		#endregion

		public CccccdddwwwInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Cccccdddwww(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Idx == null ? "null" : _Idx.ToString(), "|",
				_Name == null ? "null" : _Name.Replace("|", StringifySplit));
		}
		public static CccccdddwwwInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 2, StringSplitOptions.None);
			if (ret.Length != 2) throw new Exception($"格式不正确，CccccdddwwwInfo：{stringify}");
			CccccdddwwwInfo item = new CccccdddwwwInfo();
			if (string.Compare("null", ret[0]) != 0) item.Idx = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Name = ret[1].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(CccccdddwwwInfo).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("Idx") ? string.Empty : string.Format(", Idx : {0}", Idx == null ? "null" : Idx.ToString()), 
				__jsonIgnore.ContainsKey("Name") ? string.Empty : string.Format(", Name : {0}", Name == null ? "null" : string.Format("'{0}'", Name.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Idx")) ht["Idx"] = Idx;
			if (allField || !__jsonIgnore.ContainsKey("Name")) ht["Name"] = Name;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		[JsonProperty] public int? Idx {
			get { return _Idx; }
			set { _Idx = value; }
		}

		[JsonProperty] public string Name {
			get { return _Name; }
			set { _Name = value; }
		}

		#endregion

		public cd.DAL.Cccccdddwww.SqlUpdateBuild UpdateDiy => _Idx == null ? null : BLL.Cccccdddwww.UpdateDiy(new List<CccccdddwwwInfo> { this });

		#region sync methods

		public CccccdddwwwInfo Save() {
			if (this.Idx != null) {
				if (BLL.Cccccdddwww.Update(this) == 0) return BLL.Cccccdddwww.Insert(this);
				return this;
			}
			return BLL.Cccccdddwww.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<CccccdddwwwInfo> SaveAsync() {
			if (this.Idx != null) {
				if (await BLL.Cccccdddwww.UpdateAsync(this) == 0) return await BLL.Cccccdddwww.InsertAsync(this);
				return this;
			}
			return await BLL.Cccccdddwww.InsertAsync(this);
		}
		#endregion
	}
}

