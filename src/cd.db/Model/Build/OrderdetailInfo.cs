using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class OrderdetailInfo {
		#region fields
		private int? _DetailId;
		private int? _OrderId;
		#endregion

		public OrderdetailInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Orderdetail(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_DetailId == null ? "null" : _DetailId.ToString(), "|",
				_OrderId == null ? "null" : _OrderId.ToString());
		}
		public static OrderdetailInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 2, StringSplitOptions.None);
			if (ret.Length != 2) throw new Exception($"格式不正确，OrderdetailInfo：{stringify}");
			OrderdetailInfo item = new OrderdetailInfo();
			if (string.Compare("null", ret[0]) != 0) item.DetailId = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.OrderId = int.Parse(ret[1]);
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(OrderdetailInfo).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("DetailId") ? string.Empty : string.Format(", DetailId : {0}", DetailId == null ? "null" : DetailId.ToString()), 
				__jsonIgnore.ContainsKey("OrderId") ? string.Empty : string.Format(", OrderId : {0}", OrderId == null ? "null" : OrderId.ToString()), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("DetailId")) ht["DetailId"] = DetailId;
			if (allField || !__jsonIgnore.ContainsKey("OrderId")) ht["OrderId"] = OrderId;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		[JsonProperty] public int? DetailId {
			get { return _DetailId; }
			set { _DetailId = value; }
		}

		[JsonProperty] public int? OrderId {
			get { return _OrderId; }
			set { _OrderId = value; }
		}

		#endregion

		public cd.DAL.Orderdetail.SqlUpdateBuild UpdateDiy => _DetailId == null ? null : BLL.Orderdetail.UpdateDiy(new List<OrderdetailInfo> { this });

		#region sync methods

		public OrderdetailInfo Save() {
			if (this.DetailId != null) {
				if (BLL.Orderdetail.Update(this) == 0) return BLL.Orderdetail.Insert(this);
				return this;
			}
			return BLL.Orderdetail.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<OrderdetailInfo> SaveAsync() {
			if (this.DetailId != null) {
				if (await BLL.Orderdetail.UpdateAsync(this) == 0) return await BLL.Orderdetail.InsertAsync(this);
				return this;
			}
			return await BLL.Orderdetail.InsertAsync(this);
		}
		#endregion
	}
}

