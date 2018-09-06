using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class Song_tagInfo {
		#region fields
		private int? _Song_id;
		private SongInfo _obj_song;
		private int? _Tag_id;
		private TagInfo _obj_tag;
		#endregion

		public Song_tagInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Song_tag(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Song_id == null ? "null" : _Song_id.ToString(), "|",
				_Tag_id == null ? "null" : _Tag_id.ToString());
		}
		public static Song_tagInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 2, StringSplitOptions.None);
			if (ret.Length != 2) throw new Exception($"格式不正确，Song_tagInfo：{stringify}");
			Song_tagInfo item = new Song_tagInfo();
			if (string.Compare("null", ret[0]) != 0) item.Song_id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Tag_id = int.Parse(ret[1]);
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(Song_tagInfo).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("Song_id") ? string.Empty : string.Format(", Song_id : {0}", Song_id == null ? "null" : Song_id.ToString()), 
				__jsonIgnore.ContainsKey("Tag_id") ? string.Empty : string.Format(", Tag_id : {0}", Tag_id == null ? "null" : Tag_id.ToString()), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("Song_id")) ht["Song_id"] = Song_id;
			if (allField || !__jsonIgnore.ContainsKey("Tag_id")) ht["Tag_id"] = Tag_id;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		/// <summary>
		/// 歌曲
		/// </summary>
		[JsonProperty] public int? Song_id {
			get { return _Song_id; }
			set {
				if (_Song_id != value) _obj_song = null;
				_Song_id = value;
			}
		}
		public SongInfo Obj_song {
			get {
				if (_obj_song == null && _Song_id != null) _obj_song = BLL.Song.GetItem(_Song_id.Value);
				return _obj_song;
			}
			internal set { _obj_song = value; }
		}
		/// <summary>
		/// 标签
		/// </summary>
		[JsonProperty] public int? Tag_id {
			get { return _Tag_id; }
			set {
				if (_Tag_id != value) _obj_tag = null;
				_Tag_id = value;
			}
		}
		public TagInfo Obj_tag {
			get {
				if (_obj_tag == null && _Tag_id != null) _obj_tag = BLL.Tag.GetItem(_Tag_id.Value);
				return _obj_tag;
			}
			internal set { _obj_tag = value; }
		}
		#endregion

		public cd.DAL.Song_tag.SqlUpdateBuild UpdateDiy => _Song_id == null || _Tag_id == null ? null : BLL.Song_tag.UpdateDiy(new List<Song_tagInfo> { this });

		#region sync methods

		#endregion

		#region async methods

		#endregion
	}
}

