using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class Xxdkdkdk1222Info {
		#region fields
		private int? _Id22dd;
		private string _Name;
		#endregion

		public Xxdkdkdk1222Info() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Xxdkdkdk1222(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id22dd == null ? "null" : _Id22dd.ToString(), "|",
				_Name == null ? "null" : _Name.Replace("|", StringifySplit));
		}
		public static Xxdkdkdk1222Info Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 2, StringSplitOptions.None);
			if (ret.Length != 2) throw new Exception($"格式不正确，Xxdkdkdk1222Info：{stringify}");
			Xxdkdkdk1222Info item = new Xxdkdkdk1222Info();
			if (string.Compare("null", ret[0]) != 0) item.Id22dd = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Name = ret[1].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(Xxdkdkdk1222Info).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("Id22dd") ? string.Empty : string.Format(", Id22dd : {0}", Id22dd == null ? "null" : Id22dd.ToString()), 
				__jsonIgnore.ContainsKey("Name") ? string.Empty : string.Format(", Name : {0}", Name == null ? "null" : string.Format("'{0}'", Name.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Id22dd")) ht["Id22dd"] = Id22dd;
			if (allField || !__jsonIgnore.ContainsKey("Name")) ht["Name"] = Name;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		[JsonProperty] public int? Id22dd {
			get { return _Id22dd; }
			set { _Id22dd = value; }
		}

		[JsonProperty] public string Name {
			get { return _Name; }
			set { _Name = value; }
		}

		#endregion

		public cd.DAL.Xxdkdkdk1222.SqlUpdateBuild UpdateDiy => _Id22dd == null ? null : BLL.Xxdkdkdk1222.UpdateDiy(new List<Xxdkdkdk1222Info> { this });

		#region sync methods

		public Xxdkdkdk1222Info Save() {
			if (this.Id22dd != null) {
				if (BLL.Xxdkdkdk1222.Update(this) == 0) return BLL.Xxdkdkdk1222.Insert(this);
				return this;
			}
			return BLL.Xxdkdkdk1222.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<Xxdkdkdk1222Info> SaveAsync() {
			if (this.Id22dd != null) {
				if (await BLL.Xxdkdkdk1222.UpdateAsync(this) == 0) return await BLL.Xxdkdkdk1222.InsertAsync(this);
				return this;
			}
			return await BLL.Xxdkdkdk1222.InsertAsync(this);
		}
		#endregion
	}
}

