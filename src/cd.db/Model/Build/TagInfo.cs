using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class TagInfo {
		#region fields
		private int? _Id;
		private int? _Parent_id;
		private TagInfo _obj_tag;
		private string _Name;
		#endregion

		public TagInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Tag(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Parent_id == null ? "null" : _Parent_id.ToString(), "|",
				_Name == null ? "null" : _Name.Replace("|", StringifySplit));
		}
		public static TagInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 3, StringSplitOptions.None);
			if (ret.Length != 3) throw new Exception($"格式不正确，TagInfo：{stringify}");
			TagInfo item = new TagInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Parent_id = int.Parse(ret[1]);
			if (string.Compare("null", ret[2]) != 0) item.Name = ret[2].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(TagInfo).GetField("JsonIgnore");
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
				__jsonIgnore.ContainsKey("Parent_id") ? string.Empty : string.Format(", Parent_id : {0}", Parent_id == null ? "null" : Parent_id.ToString()), 
				__jsonIgnore.ContainsKey("Name") ? string.Empty : string.Format(", Name : {0}", Name == null ? "null" : string.Format("'{0}'", Name.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (allField || !__jsonIgnore.ContainsKey("Parent_id")) ht["Parent_id"] = Parent_id;
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

		/// <summary>
		/// 父标签
		/// </summary>
		[JsonProperty] public int? Parent_id {
			get { return _Parent_id; }
			set {
				if (_Parent_id != value) _obj_tag = null;
				_Parent_id = value;
			}
		}

		public TagInfo Obj_tag {
			get {
				if (_obj_tag == null && _Parent_id != null) _obj_tag = BLL.Tag.GetItem(_Parent_id.Value);
				return _obj_tag;
			}
			internal set { _obj_tag = value; }
		}

		/// <summary>
		/// 名称
		/// </summary>
		[JsonProperty] public string Name {
			get { return _Name; }
			set { _Name = value; }
		}

		private List<SongInfo> _obj_songs;
		public List<SongInfo> Obj_songs => _obj_songs ?? (_obj_songs = BLL.Song.SelectByTag_id(_Id.Value).ToList());
		private List<TagInfo> _obj_tags;
		public List<TagInfo> Obj_tags => _obj_tags ?? (_obj_tags = BLL.Tag.SelectByParent_id(_Id).Limit(500).ToList());
		#endregion

		public cd.DAL.Tag.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Tag.UpdateDiy(new List<TagInfo> { this });

		#region sync methods

		public TagInfo Save() {
			if (this.Id != null) {
				if (BLL.Tag.Update(this) == 0) return BLL.Tag.Insert(this);
				return this;
			}
			return BLL.Tag.Insert(this);
		}
		public Song_tagInfo FlagSong(SongInfo Song) => FlagSong(Song.Id);
		public Song_tagInfo FlagSong(int? Song_id) {
			Song_tagInfo item = BLL.Song_tag.GetItem(Song_id.Value, this.Id.Value);
			if (item == null) item = BLL.Song_tag.Insert(new Song_tagInfo {
				Song_id = Song_id, 
				Tag_id = this.Id});
			return item;
		}

		public int UnflagSong(SongInfo Song) => UnflagSong(Song.Id);
		public int UnflagSong(int? Song_id) => BLL.Song_tag.Delete(Song_id.Value, this.Id.Value);
		public int UnflagSongALL() => BLL.Song_tag.DeleteByTag_id(this.Id);

		public TagInfo AddTag(string Name) => AddTag(new TagInfo {
				Name = Name});
		public TagInfo AddTag(TagInfo item) {
			item.Parent_id = this.Id;
			return BLL.Tag.Insert(item);
		}

		#endregion

		#region async methods

		async public Task<TagInfo> SaveAsync() {
			if (this.Id != null) {
				if (await BLL.Tag.UpdateAsync(this) == 0) return await BLL.Tag.InsertAsync(this);
				return this;
			}
			return await BLL.Tag.InsertAsync(this);
		}
		async public Task<Song_tagInfo> FlagSongAsync(SongInfo Song) => await FlagSongAsync(Song.Id);
		async public Task<Song_tagInfo> FlagSongAsync(int? Song_id) {
			Song_tagInfo item = await BLL.Song_tag.GetItemAsync(Song_id.Value, this.Id.Value);
			if (item == null) item = await BLL.Song_tag.InsertAsync(new Song_tagInfo {
				Song_id = Song_id, 
				Tag_id = this.Id});
			return item;
		}

		async public Task<int> UnflagSongAsync(SongInfo Song) => await UnflagSongAsync(Song.Id);
		async public Task<int> UnflagSongAsync(int? Song_id) => await BLL.Song_tag.DeleteAsync(Song_id.Value, this.Id.Value);
		async public Task<int> UnflagSongALLAsync() => await BLL.Song_tag.DeleteByTag_idAsync(this.Id);

		async public Task<TagInfo> AddTagAsync(string Name) => await AddTagAsync(new TagInfo {
				Name = Name});
		async public Task<TagInfo> AddTagAsync(TagInfo item) {
			item.Parent_id = this.Id;
			return await BLL.Tag.InsertAsync(item);
		}

		#endregion
	}
}

