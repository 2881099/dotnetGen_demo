using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class Topic_typeInfo {
		#region fields
		private int? _Id;
		private string _Name;
		#endregion

		public Topic_typeInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Topic_type(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Name == null ? "null" : _Name.Replace("|", StringifySplit));
		}
		public static Topic_typeInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 2, StringSplitOptions.None);
			if (ret.Length != 2) throw new Exception($"格式不正确，Topic_typeInfo：{stringify}");
			Topic_typeInfo item = new Topic_typeInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Name = ret[1].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(Topic_typeInfo).GetField("JsonIgnore");
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

		private List<TopicInfo> _obj_topics;
		public List<TopicInfo> Obj_topics => _obj_topics ?? (_obj_topics = BLL.Topic.SelectByTopic_type_id(_Id).Limit(500).ToList());
		#endregion

		public cd.DAL.Topic_type.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Topic_type.UpdateDiy(new List<Topic_typeInfo> { this });

		#region sync methods

		public Topic_typeInfo Save() {
			if (this.Id != null) {
				if (BLL.Topic_type.Update(this) == 0) return BLL.Topic_type.Insert(this);
				return this;
			}
			return BLL.Topic_type.Insert(this);
		}
		public TopicInfo AddTopic(string Carddata, TopicCARDTYPE? Cardtype, ulong? Clicks, string Content, DateTime? Order_time, byte? Test_addfiled, TopicTEST_SETFIELD? Test_setfield, string Title, int? Tyyp2_id) => AddTopic(new TopicInfo {
				Carddata = Carddata, 
				Cardtype = Cardtype, 
				Clicks = Clicks, 
				Content = Content, 
				Order_time = Order_time, 
				Test_addfiled = Test_addfiled, 
				Test_setfield = Test_setfield, 
				Title = Title, 
				Tyyp2_id = Tyyp2_id});
		public TopicInfo AddTopic(TopicInfo item) {
			item.Topic_type_id = this.Id;
			return BLL.Topic.Insert(item);
		}

		#endregion

		#region async methods

		async public Task<Topic_typeInfo> SaveAsync() {
			if (this.Id != null) {
				if (await BLL.Topic_type.UpdateAsync(this) == 0) return await BLL.Topic_type.InsertAsync(this);
				return this;
			}
			return await BLL.Topic_type.InsertAsync(this);
		}
		async public Task<TopicInfo> AddTopicAsync(string Carddata, TopicCARDTYPE? Cardtype, ulong? Clicks, string Content, DateTime? Order_time, byte? Test_addfiled, TopicTEST_SETFIELD? Test_setfield, string Title, int? Tyyp2_id) => await AddTopicAsync(new TopicInfo {
				Carddata = Carddata, 
				Cardtype = Cardtype, 
				Clicks = Clicks, 
				Content = Content, 
				Order_time = Order_time, 
				Test_addfiled = Test_addfiled, 
				Test_setfield = Test_setfield, 
				Title = Title, 
				Tyyp2_id = Tyyp2_id});
		async public Task<TopicInfo> AddTopicAsync(TopicInfo item) {
			item.Topic_type_id = this.Id;
			return await BLL.Topic.InsertAsync(item);
		}

		#endregion
	}
}

