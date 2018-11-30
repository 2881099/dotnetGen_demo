using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class RoleInfo {
		#region fields
		private uint? _Id;
		private DateTime? _Create_time;
		private string _Name;
		#endregion

		public RoleInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Role(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Create_time == null ? "null" : _Create_time.Value.Ticks.ToString(), "|",
				_Name == null ? "null" : _Name.Replace("|", StringifySplit));
		}
		public static RoleInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 3, StringSplitOptions.None);
			if (ret.Length != 3) throw new Exception($"格式不正确，RoleInfo：{stringify}");
			RoleInfo item = new RoleInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = uint.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Create_time = new DateTime(long.Parse(ret[1]));
			if (string.Compare("null", ret[2]) != 0) item.Name = ret[2].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(RoleInfo).GetField("JsonIgnore");
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
				__jsonIgnore.ContainsKey("Create_time") ? string.Empty : string.Format(", Create_time : {0}", Create_time == null ? "null" : Create_time.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString()), 
				__jsonIgnore.ContainsKey("Name") ? string.Empty : string.Format(", Name : {0}", Name == null ? "null" : string.Format("'{0}'", Name.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (allField || !__jsonIgnore.ContainsKey("Create_time")) ht["Create_time"] = Create_time;
			if (allField || !__jsonIgnore.ContainsKey("Name")) ht["Name"] = Name;
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
		/// 创建时间
		/// </summary>
		[JsonProperty] public DateTime? Create_time {
			get { return _Create_time; }
			set { _Create_time = value; }
		}

		/// <summary>
		/// 角色名
		/// </summary>
		[JsonProperty] public string Name {
			get { return _Name; }
			set { _Name = value; }
		}

		private List<DirInfo> _obj_dirs;
		public List<DirInfo> Obj_dirs => _obj_dirs ?? (_obj_dirs = BLL.Dir.SelectByRole_id(_Id.Value).ToList());
		#endregion

		public cd.DAL.Role.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Role.UpdateDiy(new List<RoleInfo> { this });

		#region sync methods

		public RoleInfo Save() {
			if (this.Id != null) {
				if (BLL.Role.Update(this) == 0) return BLL.Role.Insert(this);
				return this;
			}
			this.Create_time = DateTime.Now;
			return BLL.Role.Insert(this);
		}
		public Role_dirInfo FlagDir(DirInfo Dir) => FlagDir(Dir.Id);
		public Role_dirInfo FlagDir(uint? Dir_id) {
			Role_dirInfo item = BLL.Role_dir.GetItem(Dir_id.Value, this.Id.Value);
			if (item == null) item = BLL.Role_dir.Insert(new Role_dirInfo {
				Dir_id = Dir_id, 
				Role_id = this.Id});
			return item;
		}

		public int UnflagDir(DirInfo Dir) => UnflagDir(Dir.Id);
		public int UnflagDir(uint? Dir_id) => BLL.Role_dir.Delete(Dir_id.Value, this.Id.Value);
		public int UnflagDirALL() => BLL.Role_dir.DeleteByRole_id(this.Id);

		#endregion

		#region async methods

		async public Task<RoleInfo> SaveAsync() {
			if (this.Id != null) {
				if (await BLL.Role.UpdateAsync(this) == 0) return await BLL.Role.InsertAsync(this);
				return this;
			}
			this.Create_time = DateTime.Now;
			return await BLL.Role.InsertAsync(this);
		}
		async public Task<Role_dirInfo> FlagDirAsync(DirInfo Dir) => await FlagDirAsync(Dir.Id);
		async public Task<Role_dirInfo> FlagDirAsync(uint? Dir_id) {
			Role_dirInfo item = await BLL.Role_dir.GetItemAsync(Dir_id.Value, this.Id.Value);
			if (item == null) item = await BLL.Role_dir.InsertAsync(new Role_dirInfo {
				Dir_id = Dir_id, 
				Role_id = this.Id});
			return item;
		}

		async public Task<int> UnflagDirAsync(DirInfo Dir) => await UnflagDirAsync(Dir.Id);
		async public Task<int> UnflagDirAsync(uint? Dir_id) => await BLL.Role_dir.DeleteAsync(Dir_id.Value, this.Id.Value);
		async public Task<int> UnflagDirALLAsync() => await BLL.Role_dir.DeleteByRole_idAsync(this.Id);

		#endregion
	}
}

