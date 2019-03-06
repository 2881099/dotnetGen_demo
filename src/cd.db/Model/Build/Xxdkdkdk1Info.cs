using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class Xxdkdkdk1Info {
		#region fields
		private int? _Id22;
		private int? _Id;
		private string _Name;
		#endregion

		public Xxdkdkdk1Info() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Xxdkdkdk1(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id22 == null ? "null" : _Id22.ToString(), "|",
				_Id == null ? "null" : _Id.ToString(), "|",
				_Name == null ? "null" : _Name.Replace("|", StringifySplit));
		}
		public static Xxdkdkdk1Info Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 3, StringSplitOptions.None);
			if (ret.Length != 3) throw new Exception($"格式不正确，Xxdkdkdk1Info：{stringify}");
			Xxdkdkdk1Info item = new Xxdkdkdk1Info();
			if (string.Compare("null", ret[0]) != 0) item.Id22 = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Id = int.Parse(ret[1]);
			if (string.Compare("null", ret[2]) != 0) item.Name = ret[2].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(Xxdkdkdk1Info).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("Id22") ? string.Empty : string.Format(", Id22 : {0}", Id22 == null ? "null" : Id22.ToString()), 
				__jsonIgnore.ContainsKey("Id") ? string.Empty : string.Format(", Id : {0}", Id == null ? "null" : Id.ToString()), 
				__jsonIgnore.ContainsKey("Name") ? string.Empty : string.Format(", Name : {0}", Name == null ? "null" : string.Format("'{0}'", Name.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Id22")) ht["Id22"] = Id22;
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
		[JsonProperty] public int? Id22 {
			get { return _Id22; }
			set { _Id22 = value; }
		}

		[JsonProperty] public int? Id {
			get { return _Id; }
			set { _Id = value; }
		}

		[JsonProperty] public string Name {
			get { return _Name; }
			set { _Name = value; }
		}

		#endregion

		public cd.DAL.Xxdkdkdk1.SqlUpdateBuild UpdateDiy => _Id22 == null ? null : BLL.Xxdkdkdk1.UpdateDiy(new List<Xxdkdkdk1Info> { this });

		#region sync methods

		public Xxdkdkdk1Info Save() {
			if (this.Id22 != null) {
				if (BLL.Xxdkdkdk1.Update(this) == 0) return BLL.Xxdkdkdk1.Insert(this);
				return this;
			}
			return BLL.Xxdkdkdk1.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<Xxdkdkdk1Info> SaveAsync() {
			if (this.Id22 != null) {
				if (await BLL.Xxdkdkdk1.UpdateAsync(this) == 0) return await BLL.Xxdkdkdk1.InsertAsync(this);
				return this;
			}
			return await BLL.Xxdkdkdk1.InsertAsync(this);
		}
		#endregion
	}
}

