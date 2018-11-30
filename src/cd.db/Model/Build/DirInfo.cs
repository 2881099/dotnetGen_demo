using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class DirInfo {
		#region fields
		private uint? _Id;
		private string _Path;
		private string _Title;
		#endregion

		public DirInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Dir(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Path == null ? "null" : _Path.Replace("|", StringifySplit), "|",
				_Title == null ? "null" : _Title.Replace("|", StringifySplit));
		}
		public static DirInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 3, StringSplitOptions.None);
			if (ret.Length != 3) throw new Exception($"格式不正确，DirInfo：{stringify}");
			DirInfo item = new DirInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = uint.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Path = ret[1].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[2]) != 0) item.Title = ret[2].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(DirInfo).GetField("JsonIgnore");
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
				__jsonIgnore.ContainsKey("Path") ? string.Empty : string.Format(", Path : {0}", Path == null ? "null" : string.Format("'{0}'", Path.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Title") ? string.Empty : string.Format(", Title : {0}", Title == null ? "null" : string.Format("'{0}'", Title.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (allField || !__jsonIgnore.ContainsKey("Path")) ht["Path"] = Path;
			if (allField || !__jsonIgnore.ContainsKey("Title")) ht["Title"] = Title;
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

		/// <summary>
		/// HttpMethod + Path
		/// </summary>
		[JsonProperty] public string Path {
			get { return _Path; }
			set { _Path = value; }
		}

		/// <summary>
		/// 描述
		/// </summary>
		[JsonProperty] public string Title {
			get { return _Title; }
			set { _Title = value; }
		}

		private List<RoleInfo> _obj_roles;
		public List<RoleInfo> Obj_roles => _obj_roles ?? (_obj_roles = BLL.Role.SelectByDir_id(_Id.Value).ToList());
		#endregion

		public cd.DAL.Dir.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Dir.UpdateDiy(new List<DirInfo> { this });

		#region sync methods

		public DirInfo Save() {
			if (this.Id != null) {
				if (BLL.Dir.Update(this) == 0) return BLL.Dir.Insert(this);
				return this;
			}
			return BLL.Dir.Insert(this);
		}
		public Role_dirInfo FlagRole(RoleInfo Role) => FlagRole(Role.Id);
		public Role_dirInfo FlagRole(uint? Role_id) {
			Role_dirInfo item = BLL.Role_dir.GetItem(this.Id.Value, Role_id.Value);
			if (item == null) item = BLL.Role_dir.Insert(new Role_dirInfo {
				Dir_id = this.Id, 
				Role_id = Role_id});
			return item;
		}

		public int UnflagRole(RoleInfo Role) => UnflagRole(Role.Id);
		public int UnflagRole(uint? Role_id) => BLL.Role_dir.Delete(this.Id.Value, Role_id.Value);
		public int UnflagRoleALL() => BLL.Role_dir.DeleteByDir_id(this.Id);

		#endregion

		#region async methods

		async public Task<DirInfo> SaveAsync() {
			if (this.Id != null) {
				if (await BLL.Dir.UpdateAsync(this) == 0) return await BLL.Dir.InsertAsync(this);
				return this;
			}
			return await BLL.Dir.InsertAsync(this);
		}
		async public Task<Role_dirInfo> FlagRoleAsync(RoleInfo Role) => await FlagRoleAsync(Role.Id);
		async public Task<Role_dirInfo> FlagRoleAsync(uint? Role_id) {
			Role_dirInfo item = await BLL.Role_dir.GetItemAsync(this.Id.Value, Role_id.Value);
			if (item == null) item = await BLL.Role_dir.InsertAsync(new Role_dirInfo {
				Dir_id = this.Id, 
				Role_id = Role_id});
			return item;
		}

		async public Task<int> UnflagRoleAsync(RoleInfo Role) => await UnflagRoleAsync(Role.Id);
		async public Task<int> UnflagRoleAsync(uint? Role_id) => await BLL.Role_dir.DeleteAsync(this.Id.Value, Role_id.Value);
		async public Task<int> UnflagRoleALLAsync() => await BLL.Role_dir.DeleteByDir_idAsync(this.Id);

		#endregion
	}
}

