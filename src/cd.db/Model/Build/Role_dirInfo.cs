using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class Role_dirInfo {
		#region fields
		private uint? _Dir_id;
		private DirInfo _obj_dir;
		private uint? _Role_id;
		private RoleInfo _obj_role;
		#endregion

		public Role_dirInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Role_dir(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Dir_id == null ? "null" : _Dir_id.ToString(), "|",
				_Role_id == null ? "null" : _Role_id.ToString());
		}
		public static Role_dirInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 2, StringSplitOptions.None);
			if (ret.Length != 2) throw new Exception($"格式不正确，Role_dirInfo：{stringify}");
			Role_dirInfo item = new Role_dirInfo();
			if (string.Compare("null", ret[0]) != 0) item.Dir_id = uint.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Role_id = uint.Parse(ret[1]);
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(Role_dirInfo).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("Dir_id") ? string.Empty : string.Format(", Dir_id : {0}", Dir_id == null ? "null" : Dir_id.ToString()), 
				__jsonIgnore.ContainsKey("Role_id") ? string.Empty : string.Format(", Role_id : {0}", Role_id == null ? "null" : Role_id.ToString()), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Dir_id")) ht["Dir_id"] = Dir_id;
			if (allField || !__jsonIgnore.ContainsKey("Role_id")) ht["Role_id"] = Role_id;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		[JsonProperty] public uint? Dir_id {
			get { return _Dir_id; }
			set {
				if (_Dir_id != value) _obj_dir = null;
				_Dir_id = value;
			}
		}

		public DirInfo Obj_dir {
			get {
				if (_obj_dir == null && _Dir_id != null) _obj_dir = BLL.Dir.GetItem(_Dir_id.Value);
				return _obj_dir;
			}
			internal set { _obj_dir = value; }
		}

		[JsonProperty] public uint? Role_id {
			get { return _Role_id; }
			set {
				if (_Role_id != value) _obj_role = null;
				_Role_id = value;
			}
		}

		public RoleInfo Obj_role {
			get {
				if (_obj_role == null && _Role_id != null) _obj_role = BLL.Role.GetItem(_Role_id.Value);
				return _obj_role;
			}
			internal set { _obj_role = value; }
		}

		#endregion

		public cd.DAL.Role_dir.SqlUpdateBuild UpdateDiy => _Dir_id == null || _Role_id == null ? null : BLL.Role_dir.UpdateDiy(new List<Role_dirInfo> { this });

		#region sync methods

		#endregion

		#region async methods

		#endregion
	}
}

