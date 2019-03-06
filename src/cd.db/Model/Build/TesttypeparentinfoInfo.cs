using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class TesttypeparentinfoInfo {
		#region fields
		private int? _Id;
		private string _Name;
		#endregion

		public TesttypeparentinfoInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Testtypeparentinfo(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Name == null ? "null" : _Name.Replace("|", StringifySplit));
		}
		public static TesttypeparentinfoInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 2, StringSplitOptions.None);
			if (ret.Length != 2) throw new Exception($"格式不正确，TesttypeparentinfoInfo：{stringify}");
			TesttypeparentinfoInfo item = new TesttypeparentinfoInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Name = ret[1].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(TesttypeparentinfoInfo).GetField("JsonIgnore");
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
				__jsonIgnore.ContainsKey("Name") ? string.Empty : string.Format(", Name : {0}", Name == null ? "null" : string.Format("'{0}'", Name.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (allField || !__jsonIgnore.ContainsKey("Name")) ht["Name"] = Name;
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

		#endregion

		public cd.DAL.Testtypeparentinfo.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Testtypeparentinfo.UpdateDiy(new List<TesttypeparentinfoInfo> { this });

		#region sync methods

		public TesttypeparentinfoInfo Save() {
			if (this.Id != null) {
				if (BLL.Testtypeparentinfo.Update(this) == 0) return BLL.Testtypeparentinfo.Insert(this);
				return this;
			}
			return BLL.Testtypeparentinfo.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<TesttypeparentinfoInfo> SaveAsync() {
			if (this.Id != null) {
				if (await BLL.Testtypeparentinfo.UpdateAsync(this) == 0) return await BLL.Testtypeparentinfo.InsertAsync(this);
				return this;
			}
			return await BLL.Testtypeparentinfo.InsertAsync(this);
		}
		#endregion
	}
}

