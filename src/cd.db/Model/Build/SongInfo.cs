using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class SongInfo {
		#region fields
		private int? _Id;
		private DateTime? _Create_time;
		private bool? _Is_deleted;
		private string _Title;
		private string _Url;
		#endregion

		public SongInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Song(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Create_time == null ? "null" : _Create_time.Value.Ticks.ToString(), "|",
				_Is_deleted == null ? "null" : (_Is_deleted == true ? "1" : "0"), "|",
				_Title == null ? "null" : _Title.Replace("|", StringifySplit), "|",
				_Url == null ? "null" : _Url.Replace("|", StringifySplit));
		}
		public static SongInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 5, StringSplitOptions.None);
			if (ret.Length != 5) throw new Exception($"格式不正确，SongInfo：{stringify}");
			SongInfo item = new SongInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Create_time = new DateTime(long.Parse(ret[1]));
			if (string.Compare("null", ret[2]) != 0) item.Is_deleted = ret[2] == "1";
			if (string.Compare("null", ret[3]) != 0) item.Title = ret[3].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[4]) != 0) item.Url = ret[4].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(SongInfo).GetField("JsonIgnore");
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
				__jsonIgnore.ContainsKey("Is_deleted") ? string.Empty : string.Format(", Is_deleted : {0}", Is_deleted == null ? "null" : (Is_deleted == true ? "true" : "false")), 
				__jsonIgnore.ContainsKey("Title") ? string.Empty : string.Format(", Title : {0}", Title == null ? "null" : string.Format("'{0}'", Title.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Url") ? string.Empty : string.Format(", Url : {0}", Url == null ? "null" : string.Format("'{0}'", Url.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (allField || !__jsonIgnore.ContainsKey("Create_time")) ht["Create_time"] = Create_time;
			if (allField || !__jsonIgnore.ContainsKey("Is_deleted")) ht["Is_deleted"] = Is_deleted;
			if (allField || !__jsonIgnore.ContainsKey("Title")) ht["Title"] = Title;
			if (allField || !__jsonIgnore.ContainsKey("Url")) ht["Url"] = Url;
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

		/// <summary>
		/// 创建时间
		/// </summary>
		[JsonProperty] public DateTime? Create_time {
			get { return _Create_time; }
			set { _Create_time = value; }
		}

		/// <summary>
		/// 软删除
		/// </summary>
		[JsonProperty] public bool? Is_deleted {
			get { return _Is_deleted; }
			set { _Is_deleted = value; }
		}

		/// <summary>
		/// 歌名
		/// </summary>
		[JsonProperty] public string Title {
			get { return _Title; }
			set { _Title = value; }
		}

		/// <summary>
		/// 地址
		/// </summary>
		[JsonProperty] public string Url {
			get { return _Url; }
			set { _Url = value; }
		}

		private List<TagInfo> _obj_tags;
		public List<TagInfo> Obj_tags => _obj_tags ?? (_obj_tags = BLL.Tag.SelectBySong_id(_Id.Value).ToList());
		#endregion

		public cd.DAL.Song.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Song.UpdateDiy(new List<SongInfo> { this });

		#region sync methods

		public SongInfo Save() {
			if (this.Id != null) {
				if (BLL.Song.Update(this) == 0) return BLL.Song.Insert(this);
				return this;
			}
			this.Create_time = DateTime.Now;
			return BLL.Song.Insert(this);
		}
		public Song_tagInfo FlagTag(TagInfo Tag) => FlagTag(Tag.Id);
		public Song_tagInfo FlagTag(int? Tag_id) {
			Song_tagInfo item = BLL.Song_tag.GetItem(this.Id.Value, Tag_id.Value);
			if (item == null) item = BLL.Song_tag.Insert(new Song_tagInfo {
				Song_id = this.Id, 
				Tag_id = Tag_id});
			return item;
		}

		public int UnflagTag(TagInfo Tag) => UnflagTag(Tag.Id);
		public int UnflagTag(int? Tag_id) => BLL.Song_tag.Delete(this.Id.Value, Tag_id.Value);
		public int UnflagTagALL() => BLL.Song_tag.DeleteBySong_id(this.Id);

		#endregion

		#region async methods

		async public Task<SongInfo> SaveAsync() {
			if (this.Id != null) {
				if (await BLL.Song.UpdateAsync(this) == 0) return await BLL.Song.InsertAsync(this);
				return this;
			}
			this.Create_time = DateTime.Now;
			return await BLL.Song.InsertAsync(this);
		}
		async public Task<Song_tagInfo> FlagTagAsync(TagInfo Tag) => await FlagTagAsync(Tag.Id);
		async public Task<Song_tagInfo> FlagTagAsync(int? Tag_id) {
			Song_tagInfo item = await BLL.Song_tag.GetItemAsync(this.Id.Value, Tag_id.Value);
			if (item == null) item = await BLL.Song_tag.InsertAsync(new Song_tagInfo {
				Song_id = this.Id, 
				Tag_id = Tag_id});
			return item;
		}

		async public Task<int> UnflagTagAsync(TagInfo Tag) => await UnflagTagAsync(Tag.Id);
		async public Task<int> UnflagTagAsync(int? Tag_id) => await BLL.Song_tag.DeleteAsync(this.Id.Value, Tag_id.Value);
		async public Task<int> UnflagTagALLAsync() => await BLL.Song_tag.DeleteBySong_idAsync(this.Id);

		#endregion
	}
}

