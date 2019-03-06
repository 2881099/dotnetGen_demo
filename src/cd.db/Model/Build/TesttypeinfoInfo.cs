using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class TesttypeinfoInfo {
		#region fields
		private int? _Guid;
		private string _Name;
		private int? _ParentId;
		private int? _SelfGuid;
		#endregion

		public TesttypeinfoInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Testtypeinfo(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Guid == null ? "null" : _Guid.ToString(), "|",
				_Name == null ? "null" : _Name.Replace("|", StringifySplit), "|",
				_ParentId == null ? "null" : _ParentId.ToString(), "|",
				_SelfGuid == null ? "null" : _SelfGuid.ToString());
		}
		public static TesttypeinfoInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 4, StringSplitOptions.None);
			if (ret.Length != 4) throw new Exception($"格式不正确，TesttypeinfoInfo：{stringify}");
			TesttypeinfoInfo item = new TesttypeinfoInfo();
			if (string.Compare("null", ret[0]) != 0) item.Guid = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Name = ret[1].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[2]) != 0) item.ParentId = int.Parse(ret[2]);
			if (string.Compare("null", ret[3]) != 0) item.SelfGuid = int.Parse(ret[3]);
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(TesttypeinfoInfo).GetField("JsonIgnore");
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
				__jsonIgnore.ContainsKey("SelfGuid") ? string.Empty : string.Format(", SelfGuid : {0}", SelfGuid == null ? "null" : SelfGuid.ToString()), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Guid")) ht["Guid"] = Guid;
			if (allField || !__jsonIgnore.ContainsKey("Name")) ht["Name"] = Name;
			if (allField || !__jsonIgnore.ContainsKey("ParentId")) ht["ParentId"] = ParentId;
			if (allField || !__jsonIgnore.ContainsKey("SelfGuid")) ht["SelfGuid"] = SelfGuid;
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

		[JsonProperty] public int? SelfGuid {
			get { return _SelfGuid; }
			set { _SelfGuid = value; }
		}

		#endregion

		public cd.DAL.Testtypeinfo.SqlUpdateBuild UpdateDiy => _Guid == null ? null : BLL.Testtypeinfo.UpdateDiy(new List<TesttypeinfoInfo> { this });

		#region sync methods

		public TesttypeinfoInfo Save() {
			if (this.Guid != null) {
				if (BLL.Testtypeinfo.Update(this) == 0) return BLL.Testtypeinfo.Insert(this);
				return this;
			}
			return BLL.Testtypeinfo.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<TesttypeinfoInfo> SaveAsync() {
			if (this.Guid != null) {
				if (await BLL.Testtypeinfo.UpdateAsync(this) == 0) return await BLL.Testtypeinfo.InsertAsync(this);
				return this;
			}
			return await BLL.Testtypeinfo.InsertAsync(this);
		}
		#endregion
	}
}

