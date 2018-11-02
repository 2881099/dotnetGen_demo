using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class UserotherInfo {
		#region fields
		private string _Userid;
		private string _Chinesename;
		private DateTime? _Created;
		private string _Doctype;
		private string _Englishname;
		private bool? _Hasverify;
		private long? _Id;
		private string _Idnumber;
		private string _Images;
		#endregion

		public UserotherInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Userother(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Userid == null ? "null" : _Userid.Replace("|", StringifySplit), "|",
				_Chinesename == null ? "null" : _Chinesename.Replace("|", StringifySplit), "|",
				_Created == null ? "null" : _Created.Value.Ticks.ToString(), "|",
				_Doctype == null ? "null" : _Doctype.Replace("|", StringifySplit), "|",
				_Englishname == null ? "null" : _Englishname.Replace("|", StringifySplit), "|",
				_Hasverify == null ? "null" : (_Hasverify == true ? "1" : "0"), "|",
				_Id == null ? "null" : _Id.ToString(), "|",
				_Idnumber == null ? "null" : _Idnumber.Replace("|", StringifySplit), "|",
				_Images == null ? "null" : _Images.Replace("|", StringifySplit));
		}
		public static UserotherInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 9, StringSplitOptions.None);
			if (ret.Length != 9) throw new Exception($"格式不正确，UserotherInfo：{stringify}");
			UserotherInfo item = new UserotherInfo();
			if (string.Compare("null", ret[0]) != 0) item.Userid = ret[0].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[1]) != 0) item.Chinesename = ret[1].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[2]) != 0) item.Created = new DateTime(long.Parse(ret[2]));
			if (string.Compare("null", ret[3]) != 0) item.Doctype = ret[3].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[4]) != 0) item.Englishname = ret[4].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[5]) != 0) item.Hasverify = ret[5] == "1";
			if (string.Compare("null", ret[6]) != 0) item.Id = long.Parse(ret[6]);
			if (string.Compare("null", ret[7]) != 0) item.Idnumber = ret[7].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[8]) != 0) item.Images = ret[8].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(UserotherInfo).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("Userid") ? string.Empty : string.Format(", Userid : {0}", Userid == null ? "null" : string.Format("'{0}'", Userid.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Chinesename") ? string.Empty : string.Format(", Chinesename : {0}", Chinesename == null ? "null" : string.Format("'{0}'", Chinesename.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Created") ? string.Empty : string.Format(", Created : {0}", Created == null ? "null" : Created.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString()), 
				__jsonIgnore.ContainsKey("Doctype") ? string.Empty : string.Format(", Doctype : {0}", Doctype == null ? "null" : string.Format("'{0}'", Doctype.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Englishname") ? string.Empty : string.Format(", Englishname : {0}", Englishname == null ? "null" : string.Format("'{0}'", Englishname.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Hasverify") ? string.Empty : string.Format(", Hasverify : {0}", Hasverify == null ? "null" : (Hasverify == true ? "true" : "false")), 
				__jsonIgnore.ContainsKey("Id") ? string.Empty : string.Format(", Id : {0}", Id == null ? "null" : Id.ToString()), 
				__jsonIgnore.ContainsKey("Idnumber") ? string.Empty : string.Format(", Idnumber : {0}", Idnumber == null ? "null" : string.Format("'{0}'", Idnumber.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Images") ? string.Empty : string.Format(", Images : {0}", Images == null ? "null" : string.Format("'{0}'", Images.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Userid")) ht["Userid"] = Userid;
			if (allField || !__jsonIgnore.ContainsKey("Chinesename")) ht["Chinesename"] = Chinesename;
			if (allField || !__jsonIgnore.ContainsKey("Created")) ht["Created"] = Created;
			if (allField || !__jsonIgnore.ContainsKey("Doctype")) ht["Doctype"] = Doctype;
			if (allField || !__jsonIgnore.ContainsKey("Englishname")) ht["Englishname"] = Englishname;
			if (allField || !__jsonIgnore.ContainsKey("Hasverify")) ht["Hasverify"] = Hasverify;
			if (allField || !__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (allField || !__jsonIgnore.ContainsKey("Idnumber")) ht["Idnumber"] = Idnumber;
			if (allField || !__jsonIgnore.ContainsKey("Images")) ht["Images"] = Images;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		[JsonProperty] public string Userid {
			get { return _Userid; }
			set { _Userid = value; }
		}

		[JsonProperty] public string Chinesename {
			get { return _Chinesename; }
			set { _Chinesename = value; }
		}

		[JsonProperty] public DateTime? Created {
			get { return _Created; }
			set { _Created = value; }
		}

		[JsonProperty] public string Doctype {
			get { return _Doctype; }
			set { _Doctype = value; }
		}

		[JsonProperty] public string Englishname {
			get { return _Englishname; }
			set { _Englishname = value; }
		}

		/// <summary>
		/// 是否验证
		/// </summary>
		[JsonProperty] public bool? Hasverify {
			get { return _Hasverify; }
			set { _Hasverify = value; }
		}

		[JsonProperty] public long? Id {
			get { return _Id; }
			set { _Id = value; }
		}

		[JsonProperty] public string Idnumber {
			get { return _Idnumber; }
			set { _Idnumber = value; }
		}

		[JsonProperty] public string Images {
			get { return _Images; }
			set { _Images = value; }
		}

		private Userother2Info _obj_userother2;
		public Userother2Info Obj_userother2 {
			get { return _obj_userother2 ?? (_Id == null ? null : (_obj_userother2 = BLL.Userother2.GetItem(_Id.Value))); }
			internal set { _obj_userother2 = value; }
		}
		#endregion

		public cd.DAL.Userother.SqlUpdateBuild UpdateDiy => BLL.Userother.UpdateDiy(new List<UserotherInfo> { this });

		#region sync methods

		public UserotherInfo Save() {
			if (this.Userid != null) {
				if (BLL.Userother.Update(this) == 0) return BLL.Userother.Insert(this);
				return this;
			}
			return BLL.Userother.Insert(this);
		}
		public Userother2Info AddUserother2(string Chinesename, string Xxxx) => AddUserother2(new Userother2Info {
				Chinesename = Chinesename, 
				Xxxx = Xxxx});
		public Userother2Info AddUserother2(Userother2Info item) {
			item.Userother_id = this.Id;
			return BLL.Userother2.Insert(item);
		}

		#endregion

		#region async methods

		async public Task<UserotherInfo> SaveAsync() {
			if (this.Userid != null) {
				if (await BLL.Userother.UpdateAsync(this) == 0) return await BLL.Userother.InsertAsync(this);
				return this;
			}
			return await BLL.Userother.InsertAsync(this);
		}
		async public Task<Userother2Info> AddUserother2Async(string Chinesename, string Xxxx) => await AddUserother2Async(new Userother2Info {
				Chinesename = Chinesename, 
				Xxxx = Xxxx});
		async public Task<Userother2Info> AddUserother2Async(Userother2Info item) {
			item.Userother_id = this.Id;
			return await BLL.Userother2.InsertAsync(item);
		}

		#endregion
	}
}

