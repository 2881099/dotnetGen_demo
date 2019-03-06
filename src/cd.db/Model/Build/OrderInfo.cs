using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cd.Model {

	[JsonObject(MemberSerialization.OptIn)]
	public partial class OrderInfo {
		#region fields
		private int? _OrderID;
		private string _CustomerName;
		private string _OrderTitle;
		private DateTime? _TransactionDate;
		#endregion

		public OrderInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Order(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_OrderID == null ? "null" : _OrderID.ToString(), "|",
				_CustomerName == null ? "null" : _CustomerName.Replace("|", StringifySplit), "|",
				_OrderTitle == null ? "null" : _OrderTitle.Replace("|", StringifySplit), "|",
				_TransactionDate == null ? "null" : _TransactionDate.Value.Ticks.ToString());
		}
		public static OrderInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 4, StringSplitOptions.None);
			if (ret.Length != 4) throw new Exception($"格式不正确，OrderInfo：{stringify}");
			OrderInfo item = new OrderInfo();
			if (string.Compare("null", ret[0]) != 0) item.OrderID = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.CustomerName = ret[1].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[2]) != 0) item.OrderTitle = ret[2].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[3]) != 0) item.TransactionDate = new DateTime(long.Parse(ret[3]));
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(OrderInfo).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("OrderID") ? string.Empty : string.Format(", OrderID : {0}", OrderID == null ? "null" : OrderID.ToString()), 
				__jsonIgnore.ContainsKey("CustomerName") ? string.Empty : string.Format(", CustomerName : {0}", CustomerName == null ? "null" : string.Format("'{0}'", CustomerName.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("OrderTitle") ? string.Empty : string.Format(", OrderTitle : {0}", OrderTitle == null ? "null" : string.Format("'{0}'", OrderTitle.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("TransactionDate") ? string.Empty : string.Format(", TransactionDate : {0}", TransactionDate == null ? "null" : TransactionDate.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString()), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (allField || !__jsonIgnore.ContainsKey("OrderID")) ht["OrderID"] = OrderID;
			if (allField || !__jsonIgnore.ContainsKey("CustomerName")) ht["CustomerName"] = CustomerName;
			if (allField || !__jsonIgnore.ContainsKey("OrderTitle")) ht["OrderTitle"] = OrderTitle;
			if (allField || !__jsonIgnore.ContainsKey("TransactionDate")) ht["TransactionDate"] = TransactionDate;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		[JsonProperty] public int? OrderID {
			get { return _OrderID; }
			set { _OrderID = value; }
		}

		[JsonProperty] public string CustomerName {
			get { return _CustomerName; }
			set { _CustomerName = value; }
		}

		[JsonProperty] public string OrderTitle {
			get { return _OrderTitle; }
			set { _OrderTitle = value; }
		}

		[JsonProperty] public DateTime? TransactionDate {
			get { return _TransactionDate; }
			set { _TransactionDate = value; }
		}

		#endregion

		public cd.DAL.Order.SqlUpdateBuild UpdateDiy => _OrderID == null ? null : BLL.Order.UpdateDiy(new List<OrderInfo> { this });

		#region sync methods

		public OrderInfo Save() {
			if (this.OrderID != null) {
				if (BLL.Order.Update(this) == 0) return BLL.Order.Insert(this);
				return this;
			}
			return BLL.Order.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<OrderInfo> SaveAsync() {
			if (this.OrderID != null) {
				if (await BLL.Order.UpdateAsync(this) == 0) return await BLL.Order.InsertAsync(this);
				return this;
			}
			return await BLL.Order.InsertAsync(this);
		}
		#endregion
	}
}

