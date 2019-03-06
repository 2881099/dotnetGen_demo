using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using cd.Model;

namespace cd.DAL {

	public partial class Tb_alltype : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "`tb_alltype`";
			internal static readonly string Field = "a.`Id`, a.`testFieldBool`, a.`testFieldBool1111`, a.`testFieldBoolNullable`, a.`testFieldByte`, a.`testFieldByteNullable`, a.`testFieldBytes`, a.`testFieldDateTime`, a.`testFieldDateTimeNullable`, a.`testFieldDecimal`, a.`testFieldDecimalNullable`, a.`testFieldDouble`, a.`testFieldDoubleNullable`, a.`testFieldEnum1`+0, a.`testFieldEnum1Nullable`+0, a.`testFieldEnum2`+0, a.`testFieldEnum2Nullable`+0, a.`testFieldFloat`, a.`testFieldFloatNullable`, a.`testFieldGuid`, a.`testFieldGuidNullable`, a.`testFieldInt`, a.`testFieldIntNullable`, AsText(a.`testFieldLineString`), a.`testFieldLong`, AsText(a.`testFieldMultiLineString`), AsText(a.`testFieldMultiPoint`), AsText(a.`testFieldMultiPolygon`), AsText(a.`testFieldPoint`), AsText(a.`testFieldPolygon`), a.`testFieldSByte`, a.`testFieldSByteNullable`, a.`testFieldShort`, a.`testFieldShortNullable`, a.`testFieldString`, a.`testFieldTimeSpan`, a.`testFieldTimeSpanNullable`, a.`testFieldUInt`, a.`testFieldUIntNullable`, a.`testFieldULong`, a.`testFieldULongNullable`, a.`testFieldUShort`, a.`testFieldUShortNullable`, a.`testFielLongNullable`";
			internal static readonly string Sort = "a.`Id`";
			internal static readonly string Returning = "; SELECT LAST_INSERT_ID();";
			internal static readonly string Delete = "DELETE FROM `tb_alltype` WHERE ";
			internal static readonly string InsertField = @"`testFieldBool`, `testFieldBool1111`, `testFieldBoolNullable`, `testFieldByte`, `testFieldByteNullable`, `testFieldBytes`, `testFieldDateTime`, `testFieldDateTimeNullable`, `testFieldDecimal`, `testFieldDecimalNullable`, `testFieldDouble`, `testFieldDoubleNullable`, `testFieldEnum1`, `testFieldEnum1Nullable`, `testFieldEnum2`, `testFieldEnum2Nullable`, `testFieldFloat`, `testFieldFloatNullable`, `testFieldGuid`, `testFieldGuidNullable`, `testFieldInt`, `testFieldIntNullable`, `testFieldLineString`, `testFieldLong`, `testFieldMultiLineString`, `testFieldMultiPoint`, `testFieldMultiPolygon`, `testFieldPoint`, `testFieldPolygon`, `testFieldSByte`, `testFieldSByteNullable`, `testFieldShort`, `testFieldShortNullable`, `testFieldString`, `testFieldTimeSpan`, `testFieldTimeSpanNullable`, `testFieldUInt`, `testFieldUIntNullable`, `testFieldULong`, `testFieldULongNullable`, `testFieldUShort`, `testFieldUShortNullable`, `testFielLongNullable`";
			internal static readonly string InsertValues = @"?testFieldBool, ?testFieldBool1111, ?testFieldBoolNullable, ?testFieldByte, ?testFieldByteNullable, ?testFieldBytes, ?testFieldDateTime, ?testFieldDateTimeNullable, ?testFieldDecimal, ?testFieldDecimalNullable, ?testFieldDouble, ?testFieldDoubleNullable, ?testFieldEnum1, ?testFieldEnum1Nullable, ?testFieldEnum2, ?testFieldEnum2Nullable, ?testFieldFloat, ?testFieldFloatNullable, ?testFieldGuid, ?testFieldGuidNullable, ?testFieldInt, ?testFieldIntNullable, ST_GeomFromText(?testFieldLineString), ?testFieldLong, ST_GeomFromText(?testFieldMultiLineString), ST_GeomFromText(?testFieldMultiPoint), ST_GeomFromText(?testFieldMultiPolygon), ST_GeomFromText(?testFieldPoint), ST_GeomFromText(?testFieldPolygon), ?testFieldSByte, ?testFieldSByteNullable, ?testFieldShort, ?testFieldShortNullable, ?testFieldString, ?testFieldTimeSpan, ?testFieldTimeSpanNullable, ?testFieldUInt, ?testFieldUIntNullable, ?testFieldULong, ?testFieldULongNullable, ?testFieldUShort, ?testFieldUShortNullable, ?testFielLongNullable";
			internal static readonly string InsertMultiFormat = @"INSERT INTO `tb_alltype`(" + InsertField + ") VALUES{0}";
			internal static readonly string Insert = string.Format(InsertMultiFormat, $"({InsertValues}){Returning}");
		}
		#endregion

		#region common call
		protected static MySqlParameter GetParameter(string name, MySqlDbType type, int size, object value) {
			MySqlParameter parm = new MySqlParameter(name, type);
			if (size > 0) parm.Size = size;
			parm.Value = value;
			return parm;
		}
		protected static MySqlParameter[] GetParameters(Tb_alltypeInfo item) {
			return new MySqlParameter[] {
				GetParameter("?Id", MySqlDbType.Int32, 11, item.Id), 
				GetParameter("?testFieldBool", MySqlDbType.Bit, 1, item.TestFieldBool), 
				GetParameter("?testFieldBool1111", MySqlDbType.Bit, 1, item.TestFieldBool1111), 
				GetParameter("?testFieldBoolNullable", MySqlDbType.Bit, 1, item.TestFieldBoolNullable), 
				GetParameter("?testFieldByte", MySqlDbType.UByte, 3, item.TestFieldByte), 
				GetParameter("?testFieldByteNullable", MySqlDbType.UByte, 3, item.TestFieldByteNullable), 
				GetParameter("?testFieldBytes", MySqlDbType.VarBinary, 255, item.TestFieldBytes), 
				GetParameter("?testFieldDateTime", MySqlDbType.DateTime, -1, item.TestFieldDateTime), 
				GetParameter("?testFieldDateTimeNullable", MySqlDbType.DateTime, -1, item.TestFieldDateTimeNullable), 
				GetParameter("?testFieldDecimal", MySqlDbType.Decimal, 10, item.TestFieldDecimal), 
				GetParameter("?testFieldDecimalNullable", MySqlDbType.Decimal, 10, item.TestFieldDecimalNullable), 
				GetParameter("?testFieldDouble", MySqlDbType.Double, -1, item.TestFieldDouble), 
				GetParameter("?testFieldDoubleNullable", MySqlDbType.Double, -1, item.TestFieldDoubleNullable), 
				GetParameter("?testFieldEnum1", MySqlDbType.Enum, -1, item.TestFieldEnum1?.ToInt64()), 
				GetParameter("?testFieldEnum1Nullable", MySqlDbType.Enum, -1, item.TestFieldEnum1Nullable?.ToInt64()), 
				GetParameter("?testFieldEnum2", MySqlDbType.Set, -1, item.TestFieldEnum2?.ToInt64()), 
				GetParameter("?testFieldEnum2Nullable", MySqlDbType.Set, -1, item.TestFieldEnum2Nullable?.ToInt64()), 
				GetParameter("?testFieldFloat", MySqlDbType.Float, -1, item.TestFieldFloat), 
				GetParameter("?testFieldFloatNullable", MySqlDbType.Float, -1, item.TestFieldFloatNullable), 
				GetParameter("?testFieldGuid", MySqlDbType.String, 36, item.TestFieldGuid), 
				GetParameter("?testFieldGuidNullable", MySqlDbType.String, 36, item.TestFieldGuidNullable), 
				GetParameter("?testFieldInt", MySqlDbType.Int32, 11, item.TestFieldInt), 
				GetParameter("?testFieldIntNullable", MySqlDbType.Int32, 11, item.TestFieldIntNullable), 
				GetParameter("?testFieldLineString", MySqlDbType.Text, -1, item.TestFieldLineString.AsText()), 
				GetParameter("?testFieldLong", MySqlDbType.Int64, 20, item.TestFieldLong), 
				GetParameter("?testFieldMultiLineString", MySqlDbType.Text, -1, item.TestFieldMultiLineString.AsText()), 
				GetParameter("?testFieldMultiPoint", MySqlDbType.Text, -1, item.TestFieldMultiPoint.AsText()), 
				GetParameter("?testFieldMultiPolygon", MySqlDbType.Text, -1, item.TestFieldMultiPolygon.AsText()), 
				GetParameter("?testFieldPoint", MySqlDbType.Text, -1, item.TestFieldPoint.AsText()), 
				GetParameter("?testFieldPolygon", MySqlDbType.Text, -1, item.TestFieldPolygon.AsText()), 
				GetParameter("?testFieldSByte", MySqlDbType.Byte, 3, item.TestFieldSByte), 
				GetParameter("?testFieldSByteNullable", MySqlDbType.Byte, 3, item.TestFieldSByteNullable), 
				GetParameter("?testFieldShort", MySqlDbType.Int16, 6, item.TestFieldShort), 
				GetParameter("?testFieldShortNullable", MySqlDbType.Int16, 6, item.TestFieldShortNullable), 
				GetParameter("?testFieldString", MySqlDbType.VarChar, 255, item.TestFieldString), 
				GetParameter("?testFieldTimeSpan", MySqlDbType.Time, -1, item.TestFieldTimeSpan), 
				GetParameter("?testFieldTimeSpanNullable", MySqlDbType.Time, -1, item.TestFieldTimeSpanNullable), 
				GetParameter("?testFieldUInt", MySqlDbType.UInt32, 10, item.TestFieldUInt), 
				GetParameter("?testFieldUIntNullable", MySqlDbType.UInt32, 10, item.TestFieldUIntNullable), 
				GetParameter("?testFieldULong", MySqlDbType.UInt64, 20, item.TestFieldULong), 
				GetParameter("?testFieldULongNullable", MySqlDbType.UInt64, 20, item.TestFieldULongNullable), 
				GetParameter("?testFieldUShort", MySqlDbType.UInt16, 5, item.TestFieldUShort), 
				GetParameter("?testFieldUShortNullable", MySqlDbType.UInt16, 5, item.TestFieldUShortNullable), 
				GetParameter("?testFielLongNullable", MySqlDbType.Int64, 20, item.TestFielLongNullable)};
		}
		public Tb_alltypeInfo GetItem(IDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as Tb_alltypeInfo;
		}
		public object GetItem(IDataReader dr, ref int dataIndex) {
			Tb_alltypeInfo item = new Tb_alltypeInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Id = (int?)dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 43; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldBool = (bool?)dr.GetBoolean(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldBool1111 = (bool?)dr.GetBoolean(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldBoolNullable = (bool?)dr.GetBoolean(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldByte = (byte?)dr.GetByte(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldByteNullable = (byte?)dr.GetByte(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldBytes = dr.GetValue(dataIndex) as byte[];
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldDateTime = (DateTime?)dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldDateTimeNullable = (DateTime?)dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldDecimal = (decimal?)dr.GetDecimal(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldDecimalNullable = (decimal?)dr.GetDecimal(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldDouble = (double?)dr.GetDouble(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldDoubleNullable = (double?)dr.GetDouble(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldEnum1 = (Tb_alltypeTESTFIELDENUM1?)dr.GetInt64(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldEnum1Nullable = (Tb_alltypeTESTFIELDENUM1NULLABLE?)dr.GetInt64(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldEnum2 = (Tb_alltypeTESTFIELDENUM2?)dr.GetInt64(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldEnum2Nullable = (Tb_alltypeTESTFIELDENUM2NULLABLE?)dr.GetInt64(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldFloat = (float?)dr.GetFloat(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldFloatNullable = (float?)dr.GetFloat(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldGuid = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldGuidNullable = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldInt = (int?)dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldIntNullable = (int?)dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldLineString = MygisGeometry.Parse(dr.GetString(dataIndex)) as MygisLineString;
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldLong = (long?)dr.GetInt64(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldMultiLineString = MygisGeometry.Parse(dr.GetString(dataIndex)) as MygisMultiLineString;
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldMultiPoint = MygisGeometry.Parse(dr.GetString(dataIndex)) as MygisMultiPoint;
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldMultiPolygon = MygisGeometry.Parse(dr.GetString(dataIndex)) as MygisMultiPolygon;
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldPoint = MygisGeometry.Parse(dr.GetString(dataIndex)) as MygisPoint;
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldPolygon = MygisGeometry.Parse(dr.GetString(dataIndex)) as MygisPolygon;
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldSByte = (byte?)dr.GetByte(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldSByteNullable = (byte?)dr.GetByte(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldShort = (short?)dr.GetInt16(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldShortNullable = (short?)dr.GetInt16(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldString = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldTimeSpan = (TimeSpan?)dr.GetValue(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldTimeSpanNullable = (TimeSpan?)dr.GetValue(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldUInt = (uint?)dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldUIntNullable = (uint?)dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldULong = (ulong?)dr.GetInt64(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldULongNullable = (ulong?)dr.GetInt64(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldUShort = (ushort?)dr.GetInt16(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldUShortNullable = (ushort?)dr.GetInt16(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFielLongNullable = (long?)dr.GetInt64(dataIndex);
			return item;
		}
		private void CopyItemAllField(Tb_alltypeInfo item, Tb_alltypeInfo newitem) {
			item.Id = newitem.Id;
			item.TestFieldBool = newitem.TestFieldBool;
			item.TestFieldBool1111 = newitem.TestFieldBool1111;
			item.TestFieldBoolNullable = newitem.TestFieldBoolNullable;
			item.TestFieldByte = newitem.TestFieldByte;
			item.TestFieldByteNullable = newitem.TestFieldByteNullable;
			item.TestFieldBytes = newitem.TestFieldBytes;
			item.TestFieldDateTime = newitem.TestFieldDateTime;
			item.TestFieldDateTimeNullable = newitem.TestFieldDateTimeNullable;
			item.TestFieldDecimal = newitem.TestFieldDecimal;
			item.TestFieldDecimalNullable = newitem.TestFieldDecimalNullable;
			item.TestFieldDouble = newitem.TestFieldDouble;
			item.TestFieldDoubleNullable = newitem.TestFieldDoubleNullable;
			item.TestFieldEnum1 = newitem.TestFieldEnum1;
			item.TestFieldEnum1Nullable = newitem.TestFieldEnum1Nullable;
			item.TestFieldEnum2 = newitem.TestFieldEnum2;
			item.TestFieldEnum2Nullable = newitem.TestFieldEnum2Nullable;
			item.TestFieldFloat = newitem.TestFieldFloat;
			item.TestFieldFloatNullable = newitem.TestFieldFloatNullable;
			item.TestFieldGuid = newitem.TestFieldGuid;
			item.TestFieldGuidNullable = newitem.TestFieldGuidNullable;
			item.TestFieldInt = newitem.TestFieldInt;
			item.TestFieldIntNullable = newitem.TestFieldIntNullable;
			item.TestFieldLineString = newitem.TestFieldLineString;
			item.TestFieldLong = newitem.TestFieldLong;
			item.TestFieldMultiLineString = newitem.TestFieldMultiLineString;
			item.TestFieldMultiPoint = newitem.TestFieldMultiPoint;
			item.TestFieldMultiPolygon = newitem.TestFieldMultiPolygon;
			item.TestFieldPoint = newitem.TestFieldPoint;
			item.TestFieldPolygon = newitem.TestFieldPolygon;
			item.TestFieldSByte = newitem.TestFieldSByte;
			item.TestFieldSByteNullable = newitem.TestFieldSByteNullable;
			item.TestFieldShort = newitem.TestFieldShort;
			item.TestFieldShortNullable = newitem.TestFieldShortNullable;
			item.TestFieldString = newitem.TestFieldString;
			item.TestFieldTimeSpan = newitem.TestFieldTimeSpan;
			item.TestFieldTimeSpanNullable = newitem.TestFieldTimeSpanNullable;
			item.TestFieldUInt = newitem.TestFieldUInt;
			item.TestFieldUIntNullable = newitem.TestFieldUIntNullable;
			item.TestFieldULong = newitem.TestFieldULong;
			item.TestFieldULongNullable = newitem.TestFieldULongNullable;
			item.TestFieldUShort = newitem.TestFieldUShort;
			item.TestFieldUShortNullable = newitem.TestFieldUShortNullable;
			item.TestFielLongNullable = newitem.TestFielLongNullable;
		}
		#endregion

		public int Delete(int Id) {
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, "`Id` = ?Id"), 
				GetParameter("?Id", MySqlDbType.Int32, 11, Id));
		}

		public SqlUpdateBuild Update(Tb_alltypeInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<Tb_alltypeInfo> { item });
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("testFieldBool") == false) sub.SetTestFieldBool(item.TestFieldBool);
			if (ignore.ContainsKey("testFieldBool1111") == false) sub.SetTestFieldBool1111(item.TestFieldBool1111);
			if (ignore.ContainsKey("testFieldBoolNullable") == false) sub.SetTestFieldBoolNullable(item.TestFieldBoolNullable);
			if (ignore.ContainsKey("testFieldByte") == false) sub.SetTestFieldByte(item.TestFieldByte);
			if (ignore.ContainsKey("testFieldByteNullable") == false) sub.SetTestFieldByteNullable(item.TestFieldByteNullable);
			if (ignore.ContainsKey("testFieldBytes") == false) sub.SetTestFieldBytes(item.TestFieldBytes);
			if (ignore.ContainsKey("testFieldDateTime") == false) sub.SetTestFieldDateTime(item.TestFieldDateTime);
			if (ignore.ContainsKey("testFieldDateTimeNullable") == false) sub.SetTestFieldDateTimeNullable(item.TestFieldDateTimeNullable);
			if (ignore.ContainsKey("testFieldDecimal") == false) sub.SetTestFieldDecimal(item.TestFieldDecimal);
			if (ignore.ContainsKey("testFieldDecimalNullable") == false) sub.SetTestFieldDecimalNullable(item.TestFieldDecimalNullable);
			if (ignore.ContainsKey("testFieldDouble") == false) sub.SetTestFieldDouble(item.TestFieldDouble);
			if (ignore.ContainsKey("testFieldDoubleNullable") == false) sub.SetTestFieldDoubleNullable(item.TestFieldDoubleNullable);
			if (ignore.ContainsKey("testFieldEnum1") == false) sub.SetTestFieldEnum1(item.TestFieldEnum1);
			if (ignore.ContainsKey("testFieldEnum1Nullable") == false) sub.SetTestFieldEnum1Nullable(item.TestFieldEnum1Nullable);
			if (ignore.ContainsKey("testFieldEnum2") == false) sub.SetTestFieldEnum2(item.TestFieldEnum2);
			if (ignore.ContainsKey("testFieldEnum2Nullable") == false) sub.SetTestFieldEnum2Nullable(item.TestFieldEnum2Nullable);
			if (ignore.ContainsKey("testFieldFloat") == false) sub.SetTestFieldFloat(item.TestFieldFloat);
			if (ignore.ContainsKey("testFieldFloatNullable") == false) sub.SetTestFieldFloatNullable(item.TestFieldFloatNullable);
			if (ignore.ContainsKey("testFieldGuid") == false) sub.SetTestFieldGuid(item.TestFieldGuid);
			if (ignore.ContainsKey("testFieldGuidNullable") == false) sub.SetTestFieldGuidNullable(item.TestFieldGuidNullable);
			if (ignore.ContainsKey("testFieldInt") == false) sub.SetTestFieldInt(item.TestFieldInt);
			if (ignore.ContainsKey("testFieldIntNullable") == false) sub.SetTestFieldIntNullable(item.TestFieldIntNullable);
			if (ignore.ContainsKey("testFieldLineString") == false) sub.SetTestFieldLineString(item.TestFieldLineString);
			if (ignore.ContainsKey("testFieldLong") == false) sub.SetTestFieldLong(item.TestFieldLong);
			if (ignore.ContainsKey("testFieldMultiLineString") == false) sub.SetTestFieldMultiLineString(item.TestFieldMultiLineString);
			if (ignore.ContainsKey("testFieldMultiPoint") == false) sub.SetTestFieldMultiPoint(item.TestFieldMultiPoint);
			if (ignore.ContainsKey("testFieldMultiPolygon") == false) sub.SetTestFieldMultiPolygon(item.TestFieldMultiPolygon);
			if (ignore.ContainsKey("testFieldPoint") == false) sub.SetTestFieldPoint(item.TestFieldPoint);
			if (ignore.ContainsKey("testFieldPolygon") == false) sub.SetTestFieldPolygon(item.TestFieldPolygon);
			if (ignore.ContainsKey("testFieldSByte") == false) sub.SetTestFieldSByte(item.TestFieldSByte);
			if (ignore.ContainsKey("testFieldSByteNullable") == false) sub.SetTestFieldSByteNullable(item.TestFieldSByteNullable);
			if (ignore.ContainsKey("testFieldShort") == false) sub.SetTestFieldShort(item.TestFieldShort);
			if (ignore.ContainsKey("testFieldShortNullable") == false) sub.SetTestFieldShortNullable(item.TestFieldShortNullable);
			if (ignore.ContainsKey("testFieldString") == false) sub.SetTestFieldString(item.TestFieldString);
			if (ignore.ContainsKey("testFieldTimeSpan") == false) sub.SetTestFieldTimeSpan(item.TestFieldTimeSpan);
			if (ignore.ContainsKey("testFieldTimeSpanNullable") == false) sub.SetTestFieldTimeSpanNullable(item.TestFieldTimeSpanNullable);
			if (ignore.ContainsKey("testFieldUInt") == false) sub.SetTestFieldUInt(item.TestFieldUInt);
			if (ignore.ContainsKey("testFieldUIntNullable") == false) sub.SetTestFieldUIntNullable(item.TestFieldUIntNullable);
			if (ignore.ContainsKey("testFieldULong") == false) sub.SetTestFieldULong(item.TestFieldULong);
			if (ignore.ContainsKey("testFieldULongNullable") == false) sub.SetTestFieldULongNullable(item.TestFieldULongNullable);
			if (ignore.ContainsKey("testFieldUShort") == false) sub.SetTestFieldUShort(item.TestFieldUShort);
			if (ignore.ContainsKey("testFieldUShortNullable") == false) sub.SetTestFieldUShortNullable(item.TestFieldUShortNullable);
			if (ignore.ContainsKey("testFielLongNullable") == false) sub.SetTestFielLongNullable(item.TestFielLongNullable);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<Tb_alltypeInfo> _dataSource;
			protected Dictionary<string, Tb_alltypeInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<MySqlParameter> _parameters = new List<MySqlParameter>();
			public SqlUpdateBuild(List<Tb_alltypeInfo> dataSource) {
				_dataSource = dataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"`Id` IN ({0})", _dataSource.Select(a => a.Id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 cd.DAL.Tb_alltype.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
				BLL.Tb_alltype.RemoveCache(_dataSource);
				return affrows;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
				await BLL.Tb_alltype.RemoveCacheAsync(_dataSource);
				return affrows;
			}
			public SqlUpdateBuild Where(string filterFormat, params object[] values) {
				if (!string.IsNullOrEmpty(_where)) _where = string.Concat(_where, " AND ");
				_where = string.Concat(_where, "(", SqlHelper.Addslashes(filterFormat, values), ")");
				return this;
			}
			public SqlUpdateBuild WhereExists<T>(SelectBuild<T> select) {
				return this.Where($"EXISTS({select.ToString("1")})");
			}
			public SqlUpdateBuild WhereNotExists<T>(SelectBuild<T> select) {
				return this.Where($"NOT EXISTS({select.ToString("1")})");
			}

			public SqlUpdateBuild Set(string field, string value, params MySqlParameter[] parms) {
				if (value.IndexOf('\'') != -1) throw new Exception("cd.DAL.Tb_alltype.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetTestFieldBool(bool? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldBool = value;
				return this.Set("`testFieldBool`", $"?testFieldBool_{_parameters.Count}", 
					GetParameter($"?testFieldBool_{_parameters.Count}", MySqlDbType.Bit, 1, value));
			}
			public SqlUpdateBuild SetTestFieldBool1111(bool? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldBool1111 = value;
				return this.Set("`testFieldBool1111`", $"?testFieldBool1111_{_parameters.Count}", 
					GetParameter($"?testFieldBool1111_{_parameters.Count}", MySqlDbType.Bit, 1, value));
			}
			public SqlUpdateBuild SetTestFieldBoolNullable(bool? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldBoolNullable = value;
				return this.Set("`testFieldBoolNullable`", $"?testFieldBoolNullable_{_parameters.Count}", 
					GetParameter($"?testFieldBoolNullable_{_parameters.Count}", MySqlDbType.Bit, 1, value));
			}
			public SqlUpdateBuild SetTestFieldByte(byte? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldByte = value;
				return this.Set("`testFieldByte`", $"?testFieldByte_{_parameters.Count}", 
					GetParameter($"?testFieldByte_{_parameters.Count}", MySqlDbType.UByte, 3, value));
			}
			public SqlUpdateBuild SetTestFieldByteIncrement(byte value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldByte += value;
				return this.Set("`testFieldByte`", $"ifnull(`testFieldByte`, 0) + ?testFieldByte_{_parameters.Count}", 
					GetParameter($"?testFieldByte_{_parameters.Count}", MySqlDbType.Byte, 3, value));
			}
			public SqlUpdateBuild SetTestFieldByteNullable(byte? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldByteNullable = value;
				return this.Set("`testFieldByteNullable`", $"?testFieldByteNullable_{_parameters.Count}", 
					GetParameter($"?testFieldByteNullable_{_parameters.Count}", MySqlDbType.UByte, 3, value));
			}
			public SqlUpdateBuild SetTestFieldByteNullableIncrement(byte value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldByteNullable += value;
				return this.Set("`testFieldByteNullable`", $"ifnull(`testFieldByteNullable`, 0) + ?testFieldByteNullable_{_parameters.Count}", 
					GetParameter($"?testFieldByteNullable_{_parameters.Count}", MySqlDbType.Byte, 3, value));
			}
			public SqlUpdateBuild SetTestFieldBytes(byte[] value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldBytes = value;
				return this.Set("`testFieldBytes`", $"?testFieldBytes_{_parameters.Count}", 
					GetParameter($"?testFieldBytes_{_parameters.Count}", MySqlDbType.VarBinary, 255, value));
			}
			public SqlUpdateBuild SetTestFieldDateTime(DateTime? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldDateTime = value;
				return this.Set("`testFieldDateTime`", $"?testFieldDateTime_{_parameters.Count}", 
					GetParameter($"?testFieldDateTime_{_parameters.Count}", MySqlDbType.DateTime, -1, value));
			}
			public SqlUpdateBuild SetTestFieldDateTimeNullable(DateTime? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldDateTimeNullable = value;
				return this.Set("`testFieldDateTimeNullable`", $"?testFieldDateTimeNullable_{_parameters.Count}", 
					GetParameter($"?testFieldDateTimeNullable_{_parameters.Count}", MySqlDbType.DateTime, -1, value));
			}
			public SqlUpdateBuild SetTestFieldDecimal(decimal? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldDecimal = value;
				return this.Set("`testFieldDecimal`", $"?testFieldDecimal_{_parameters.Count}", 
					GetParameter($"?testFieldDecimal_{_parameters.Count}", MySqlDbType.Decimal, 10, value));
			}
			public SqlUpdateBuild SetTestFieldDecimalIncrement(decimal value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldDecimal += value;
				return this.Set("`testFieldDecimal`", $"ifnull(`testFieldDecimal`, 0) + ?testFieldDecimal_{_parameters.Count}", 
					GetParameter($"?testFieldDecimal_{_parameters.Count}", MySqlDbType.Decimal, 10, value));
			}
			public SqlUpdateBuild SetTestFieldDecimalNullable(decimal? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldDecimalNullable = value;
				return this.Set("`testFieldDecimalNullable`", $"?testFieldDecimalNullable_{_parameters.Count}", 
					GetParameter($"?testFieldDecimalNullable_{_parameters.Count}", MySqlDbType.Decimal, 10, value));
			}
			public SqlUpdateBuild SetTestFieldDecimalNullableIncrement(decimal value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldDecimalNullable += value;
				return this.Set("`testFieldDecimalNullable`", $"ifnull(`testFieldDecimalNullable`, 0) + ?testFieldDecimalNullable_{_parameters.Count}", 
					GetParameter($"?testFieldDecimalNullable_{_parameters.Count}", MySqlDbType.Decimal, 10, value));
			}
			public SqlUpdateBuild SetTestFieldDouble(double? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldDouble = value;
				return this.Set("`testFieldDouble`", $"?testFieldDouble_{_parameters.Count}", 
					GetParameter($"?testFieldDouble_{_parameters.Count}", MySqlDbType.Double, -1, value));
			}
			public SqlUpdateBuild SetTestFieldDoubleIncrement(double value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldDouble += value;
				return this.Set("`testFieldDouble`", $"ifnull(`testFieldDouble`, 0) + ?testFieldDouble_{_parameters.Count}", 
					GetParameter($"?testFieldDouble_{_parameters.Count}", MySqlDbType.Double, -1, value));
			}
			public SqlUpdateBuild SetTestFieldDoubleNullable(double? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldDoubleNullable = value;
				return this.Set("`testFieldDoubleNullable`", $"?testFieldDoubleNullable_{_parameters.Count}", 
					GetParameter($"?testFieldDoubleNullable_{_parameters.Count}", MySqlDbType.Double, -1, value));
			}
			public SqlUpdateBuild SetTestFieldDoubleNullableIncrement(double value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldDoubleNullable += value;
				return this.Set("`testFieldDoubleNullable`", $"ifnull(`testFieldDoubleNullable`, 0) + ?testFieldDoubleNullable_{_parameters.Count}", 
					GetParameter($"?testFieldDoubleNullable_{_parameters.Count}", MySqlDbType.Double, -1, value));
			}
			public SqlUpdateBuild SetTestFieldEnum1(Tb_alltypeTESTFIELDENUM1? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldEnum1 = value;
				return this.Set("`testFieldEnum1`", $"?testFieldEnum1_{_parameters.Count}", 
					GetParameter($"?testFieldEnum1_{_parameters.Count}", MySqlDbType.Enum, -1, value?.ToInt64()));
			}
			public SqlUpdateBuild SetTestFieldEnum1Nullable(Tb_alltypeTESTFIELDENUM1NULLABLE? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldEnum1Nullable = value;
				return this.Set("`testFieldEnum1Nullable`", $"?testFieldEnum1Nullable_{_parameters.Count}", 
					GetParameter($"?testFieldEnum1Nullable_{_parameters.Count}", MySqlDbType.Enum, -1, value?.ToInt64()));
			}
			public SqlUpdateBuild SetTestFieldEnum2(Tb_alltypeTESTFIELDENUM2? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldEnum2 = value;
				return this.Set("`testFieldEnum2`", $"?testFieldEnum2_{_parameters.Count}", 
					GetParameter($"?testFieldEnum2_{_parameters.Count}", MySqlDbType.Set, -1, value?.ToInt64()));
			}
			public SqlUpdateBuild SetTestFieldEnum2Flag(Tb_alltypeTESTFIELDENUM2 value, bool isUnFlag = false) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldEnum2 = isUnFlag ? ((item.TestFieldEnum2 ?? 0) ^ value) : ((item.TestFieldEnum2 ?? 0) | value);
				return this.Set("`testFieldEnum2`", $"ifnull(`testFieldEnum2`+0,0) {(isUnFlag ? '^' : '|')} ?testFieldEnum2_{_parameters.Count}", 
					GetParameter(string.Concat("?testFieldEnum2_", _parameters.Count), MySqlDbType.Set, -1, value.ToInt64()));
			}
			public SqlUpdateBuild SetTestFieldEnum2UnFlag(Tb_alltypeTESTFIELDENUM2 value) {
				return this.SetTestFieldEnum2Flag(value, true);
			}
			public SqlUpdateBuild SetTestFieldEnum2Nullable(Tb_alltypeTESTFIELDENUM2NULLABLE? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldEnum2Nullable = value;
				return this.Set("`testFieldEnum2Nullable`", $"?testFieldEnum2Nullable_{_parameters.Count}", 
					GetParameter($"?testFieldEnum2Nullable_{_parameters.Count}", MySqlDbType.Set, -1, value?.ToInt64()));
			}
			public SqlUpdateBuild SetTestFieldEnum2NullableFlag(Tb_alltypeTESTFIELDENUM2NULLABLE value, bool isUnFlag = false) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldEnum2Nullable = isUnFlag ? ((item.TestFieldEnum2Nullable ?? 0) ^ value) : ((item.TestFieldEnum2Nullable ?? 0) | value);
				return this.Set("`testFieldEnum2Nullable`", $"ifnull(`testFieldEnum2Nullable`+0,0) {(isUnFlag ? '^' : '|')} ?testFieldEnum2Nullable_{_parameters.Count}", 
					GetParameter(string.Concat("?testFieldEnum2Nullable_", _parameters.Count), MySqlDbType.Set, -1, value.ToInt64()));
			}
			public SqlUpdateBuild SetTestFieldEnum2NullableUnFlag(Tb_alltypeTESTFIELDENUM2NULLABLE value) {
				return this.SetTestFieldEnum2NullableFlag(value, true);
			}
			public SqlUpdateBuild SetTestFieldFloat(float? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldFloat = value;
				return this.Set("`testFieldFloat`", $"?testFieldFloat_{_parameters.Count}", 
					GetParameter($"?testFieldFloat_{_parameters.Count}", MySqlDbType.Float, -1, value));
			}
			public SqlUpdateBuild SetTestFieldFloatIncrement(float value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldFloat += value;
				return this.Set("`testFieldFloat`", $"ifnull(`testFieldFloat`, 0) + ?testFieldFloat_{_parameters.Count}", 
					GetParameter($"?testFieldFloat_{_parameters.Count}", MySqlDbType.Float, -1, value));
			}
			public SqlUpdateBuild SetTestFieldFloatNullable(float? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldFloatNullable = value;
				return this.Set("`testFieldFloatNullable`", $"?testFieldFloatNullable_{_parameters.Count}", 
					GetParameter($"?testFieldFloatNullable_{_parameters.Count}", MySqlDbType.Float, -1, value));
			}
			public SqlUpdateBuild SetTestFieldFloatNullableIncrement(float value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldFloatNullable += value;
				return this.Set("`testFieldFloatNullable`", $"ifnull(`testFieldFloatNullable`, 0) + ?testFieldFloatNullable_{_parameters.Count}", 
					GetParameter($"?testFieldFloatNullable_{_parameters.Count}", MySqlDbType.Float, -1, value));
			}
			public SqlUpdateBuild SetTestFieldGuid(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldGuid = value;
				return this.Set("`testFieldGuid`", $"?testFieldGuid_{_parameters.Count}", 
					GetParameter($"?testFieldGuid_{_parameters.Count}", MySqlDbType.String, 36, value));
			}
			public SqlUpdateBuild SetTestFieldGuidNullable(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldGuidNullable = value;
				return this.Set("`testFieldGuidNullable`", $"?testFieldGuidNullable_{_parameters.Count}", 
					GetParameter($"?testFieldGuidNullable_{_parameters.Count}", MySqlDbType.String, 36, value));
			}
			public SqlUpdateBuild SetTestFieldInt(int? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldInt = value;
				return this.Set("`testFieldInt`", $"?testFieldInt_{_parameters.Count}", 
					GetParameter($"?testFieldInt_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
			public SqlUpdateBuild SetTestFieldIntIncrement(int value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldInt += value;
				return this.Set("`testFieldInt`", $"ifnull(`testFieldInt`, 0) + ?testFieldInt_{_parameters.Count}", 
					GetParameter($"?testFieldInt_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
			public SqlUpdateBuild SetTestFieldIntNullable(int? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldIntNullable = value;
				return this.Set("`testFieldIntNullable`", $"?testFieldIntNullable_{_parameters.Count}", 
					GetParameter($"?testFieldIntNullable_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
			public SqlUpdateBuild SetTestFieldIntNullableIncrement(int value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldIntNullable += value;
				return this.Set("`testFieldIntNullable`", $"ifnull(`testFieldIntNullable`, 0) + ?testFieldIntNullable_{_parameters.Count}", 
					GetParameter($"?testFieldIntNullable_{_parameters.Count}", MySqlDbType.Int32, 11, value));
			}
			public SqlUpdateBuild SetTestFieldLineString(MygisLineString value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldLineString = value;
				return this.Set("`testFieldLineString`", $"ST_GeomFromText(?testFieldLineString_{_parameters.Count})", 
					GetParameter($"?testFieldLineString_{_parameters.Count}", MySqlDbType.Text, -1, value.AsText()));
			}
			public SqlUpdateBuild SetTestFieldLong(long? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldLong = value;
				return this.Set("`testFieldLong`", $"?testFieldLong_{_parameters.Count}", 
					GetParameter($"?testFieldLong_{_parameters.Count}", MySqlDbType.Int64, 20, value));
			}
			public SqlUpdateBuild SetTestFieldLongIncrement(long value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldLong += value;
				return this.Set("`testFieldLong`", $"ifnull(`testFieldLong`, 0) + ?testFieldLong_{_parameters.Count}", 
					GetParameter($"?testFieldLong_{_parameters.Count}", MySqlDbType.Int64, 20, value));
			}
			public SqlUpdateBuild SetTestFieldMultiLineString(MygisMultiLineString value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldMultiLineString = value;
				return this.Set("`testFieldMultiLineString`", $"ST_GeomFromText(?testFieldMultiLineString_{_parameters.Count})", 
					GetParameter($"?testFieldMultiLineString_{_parameters.Count}", MySqlDbType.Text, -1, value.AsText()));
			}
			public SqlUpdateBuild SetTestFieldMultiPoint(MygisMultiPoint value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldMultiPoint = value;
				return this.Set("`testFieldMultiPoint`", $"ST_GeomFromText(?testFieldMultiPoint_{_parameters.Count})", 
					GetParameter($"?testFieldMultiPoint_{_parameters.Count}", MySqlDbType.Text, -1, value.AsText()));
			}
			public SqlUpdateBuild SetTestFieldMultiPolygon(MygisMultiPolygon value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldMultiPolygon = value;
				return this.Set("`testFieldMultiPolygon`", $"ST_GeomFromText(?testFieldMultiPolygon_{_parameters.Count})", 
					GetParameter($"?testFieldMultiPolygon_{_parameters.Count}", MySqlDbType.Text, -1, value.AsText()));
			}
			public SqlUpdateBuild SetTestFieldPoint(MygisPoint value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldPoint = value;
				return this.Set("`testFieldPoint`", $"ST_GeomFromText(?testFieldPoint_{_parameters.Count})", 
					GetParameter($"?testFieldPoint_{_parameters.Count}", MySqlDbType.Text, -1, value.AsText()));
			}
			public SqlUpdateBuild SetTestFieldPolygon(MygisPolygon value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldPolygon = value;
				return this.Set("`testFieldPolygon`", $"ST_GeomFromText(?testFieldPolygon_{_parameters.Count})", 
					GetParameter($"?testFieldPolygon_{_parameters.Count}", MySqlDbType.Text, -1, value.AsText()));
			}
			public SqlUpdateBuild SetTestFieldSByte(byte? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldSByte = value;
				return this.Set("`testFieldSByte`", $"?testFieldSByte_{_parameters.Count}", 
					GetParameter($"?testFieldSByte_{_parameters.Count}", MySqlDbType.Byte, 3, value));
			}
			public SqlUpdateBuild SetTestFieldSByteIncrement(byte value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldSByte += value;
				return this.Set("`testFieldSByte`", $"ifnull(`testFieldSByte`, 0) + ?testFieldSByte_{_parameters.Count}", 
					GetParameter($"?testFieldSByte_{_parameters.Count}", MySqlDbType.Byte, 3, value));
			}
			public SqlUpdateBuild SetTestFieldSByteNullable(byte? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldSByteNullable = value;
				return this.Set("`testFieldSByteNullable`", $"?testFieldSByteNullable_{_parameters.Count}", 
					GetParameter($"?testFieldSByteNullable_{_parameters.Count}", MySqlDbType.Byte, 3, value));
			}
			public SqlUpdateBuild SetTestFieldSByteNullableIncrement(byte value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldSByteNullable += value;
				return this.Set("`testFieldSByteNullable`", $"ifnull(`testFieldSByteNullable`, 0) + ?testFieldSByteNullable_{_parameters.Count}", 
					GetParameter($"?testFieldSByteNullable_{_parameters.Count}", MySqlDbType.Byte, 3, value));
			}
			public SqlUpdateBuild SetTestFieldShort(short? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldShort = value;
				return this.Set("`testFieldShort`", $"?testFieldShort_{_parameters.Count}", 
					GetParameter($"?testFieldShort_{_parameters.Count}", MySqlDbType.Int16, 6, value));
			}
			public SqlUpdateBuild SetTestFieldShortIncrement(short value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldShort += value;
				return this.Set("`testFieldShort`", $"ifnull(`testFieldShort`, 0) + ?testFieldShort_{_parameters.Count}", 
					GetParameter($"?testFieldShort_{_parameters.Count}", MySqlDbType.Int16, 6, value));
			}
			public SqlUpdateBuild SetTestFieldShortNullable(short? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldShortNullable = value;
				return this.Set("`testFieldShortNullable`", $"?testFieldShortNullable_{_parameters.Count}", 
					GetParameter($"?testFieldShortNullable_{_parameters.Count}", MySqlDbType.Int16, 6, value));
			}
			public SqlUpdateBuild SetTestFieldShortNullableIncrement(short value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldShortNullable += value;
				return this.Set("`testFieldShortNullable`", $"ifnull(`testFieldShortNullable`, 0) + ?testFieldShortNullable_{_parameters.Count}", 
					GetParameter($"?testFieldShortNullable_{_parameters.Count}", MySqlDbType.Int16, 6, value));
			}
			public SqlUpdateBuild SetTestFieldString(string value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldString = value;
				return this.Set("`testFieldString`", $"?testFieldString_{_parameters.Count}", 
					GetParameter($"?testFieldString_{_parameters.Count}", MySqlDbType.VarChar, 255, value));
			}
			public SqlUpdateBuild SetTestFieldTimeSpan(TimeSpan? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldTimeSpan = value;
				return this.Set("`testFieldTimeSpan`", $"?testFieldTimeSpan_{_parameters.Count}", 
					GetParameter($"?testFieldTimeSpan_{_parameters.Count}", MySqlDbType.Time, -1, value));
			}
			public SqlUpdateBuild SetTestFieldTimeSpanNullable(TimeSpan? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldTimeSpanNullable = value;
				return this.Set("`testFieldTimeSpanNullable`", $"?testFieldTimeSpanNullable_{_parameters.Count}", 
					GetParameter($"?testFieldTimeSpanNullable_{_parameters.Count}", MySqlDbType.Time, -1, value));
			}
			public SqlUpdateBuild SetTestFieldUInt(uint? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldUInt = value;
				return this.Set("`testFieldUInt`", $"?testFieldUInt_{_parameters.Count}", 
					GetParameter($"?testFieldUInt_{_parameters.Count}", MySqlDbType.UInt32, 10, value));
			}
			public SqlUpdateBuild SetTestFieldUIntIncrement(int value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldUInt = (uint?)((int?)item.TestFieldUInt + value);
				return this.Set("`testFieldUInt`", $"ifnull(`testFieldUInt`, 0) + ?testFieldUInt_{_parameters.Count}", 
					GetParameter($"?testFieldUInt_{_parameters.Count}", MySqlDbType.Int32, 10, value));
			}
			public SqlUpdateBuild SetTestFieldUIntNullable(uint? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldUIntNullable = value;
				return this.Set("`testFieldUIntNullable`", $"?testFieldUIntNullable_{_parameters.Count}", 
					GetParameter($"?testFieldUIntNullable_{_parameters.Count}", MySqlDbType.UInt32, 10, value));
			}
			public SqlUpdateBuild SetTestFieldUIntNullableIncrement(int value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldUIntNullable = (uint?)((int?)item.TestFieldUIntNullable + value);
				return this.Set("`testFieldUIntNullable`", $"ifnull(`testFieldUIntNullable`, 0) + ?testFieldUIntNullable_{_parameters.Count}", 
					GetParameter($"?testFieldUIntNullable_{_parameters.Count}", MySqlDbType.Int32, 10, value));
			}
			public SqlUpdateBuild SetTestFieldULong(ulong? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldULong = value;
				return this.Set("`testFieldULong`", $"?testFieldULong_{_parameters.Count}", 
					GetParameter($"?testFieldULong_{_parameters.Count}", MySqlDbType.UInt64, 20, value));
			}
			public SqlUpdateBuild SetTestFieldULongIncrement(long value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldULong = (ulong?)((long?)item.TestFieldULong + value);
				return this.Set("`testFieldULong`", $"ifnull(`testFieldULong`, 0) + ?testFieldULong_{_parameters.Count}", 
					GetParameter($"?testFieldULong_{_parameters.Count}", MySqlDbType.Int64, 20, value));
			}
			public SqlUpdateBuild SetTestFieldULongNullable(ulong? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldULongNullable = value;
				return this.Set("`testFieldULongNullable`", $"?testFieldULongNullable_{_parameters.Count}", 
					GetParameter($"?testFieldULongNullable_{_parameters.Count}", MySqlDbType.UInt64, 20, value));
			}
			public SqlUpdateBuild SetTestFieldULongNullableIncrement(long value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldULongNullable = (ulong?)((long?)item.TestFieldULongNullable + value);
				return this.Set("`testFieldULongNullable`", $"ifnull(`testFieldULongNullable`, 0) + ?testFieldULongNullable_{_parameters.Count}", 
					GetParameter($"?testFieldULongNullable_{_parameters.Count}", MySqlDbType.Int64, 20, value));
			}
			public SqlUpdateBuild SetTestFieldUShort(ushort? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldUShort = value;
				return this.Set("`testFieldUShort`", $"?testFieldUShort_{_parameters.Count}", 
					GetParameter($"?testFieldUShort_{_parameters.Count}", MySqlDbType.UInt16, 5, value));
			}
			public SqlUpdateBuild SetTestFieldUShortIncrement(short value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldUShort = (ushort?)((short?)item.TestFieldUShort + value);
				return this.Set("`testFieldUShort`", $"ifnull(`testFieldUShort`, 0) + ?testFieldUShort_{_parameters.Count}", 
					GetParameter($"?testFieldUShort_{_parameters.Count}", MySqlDbType.Int16, 5, value));
			}
			public SqlUpdateBuild SetTestFieldUShortNullable(ushort? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldUShortNullable = value;
				return this.Set("`testFieldUShortNullable`", $"?testFieldUShortNullable_{_parameters.Count}", 
					GetParameter($"?testFieldUShortNullable_{_parameters.Count}", MySqlDbType.UInt16, 5, value));
			}
			public SqlUpdateBuild SetTestFieldUShortNullableIncrement(short value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFieldUShortNullable = (ushort?)((short?)item.TestFieldUShortNullable + value);
				return this.Set("`testFieldUShortNullable`", $"ifnull(`testFieldUShortNullable`, 0) + ?testFieldUShortNullable_{_parameters.Count}", 
					GetParameter($"?testFieldUShortNullable_{_parameters.Count}", MySqlDbType.Int16, 5, value));
			}
			public SqlUpdateBuild SetTestFielLongNullable(long? value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFielLongNullable = value;
				return this.Set("`testFielLongNullable`", $"?testFielLongNullable_{_parameters.Count}", 
					GetParameter($"?testFielLongNullable_{_parameters.Count}", MySqlDbType.Int64, 20, value));
			}
			public SqlUpdateBuild SetTestFielLongNullableIncrement(long value) {
				if (_dataSource != null) foreach (var item in _dataSource) item.TestFielLongNullable += value;
				return this.Set("`testFielLongNullable`", $"ifnull(`testFielLongNullable`, 0) + ?testFielLongNullable_{_parameters.Count}", 
					GetParameter($"?testFielLongNullable_{_parameters.Count}", MySqlDbType.Int64, 20, value));
			}
		}
		#endregion

		public Tb_alltypeInfo Insert(Tb_alltypeInfo item) {
			if (int.TryParse(string.Concat(SqlHelper.ExecuteScalar(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}

		#region async
		async public Task<Tb_alltypeInfo> GetItemAsync(MySqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as Tb_alltypeInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(MySqlDataReader dr, int dataIndex) {
			Tb_alltypeInfo item = new Tb_alltypeInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = (int?)dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 43; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldBool = (bool?)dr.GetBoolean(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldBool1111 = (bool?)dr.GetBoolean(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldBoolNullable = (bool?)dr.GetBoolean(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldByte = (byte?)dr.GetByte(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldByteNullable = (byte?)dr.GetByte(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldBytes = dr.GetValue(dataIndex) as byte[];
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldDateTime = (DateTime?)dr.GetDateTime(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldDateTimeNullable = (DateTime?)dr.GetDateTime(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldDecimal = (decimal?)dr.GetDecimal(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldDecimalNullable = (decimal?)dr.GetDecimal(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldDouble = (double?)dr.GetDouble(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldDoubleNullable = (double?)dr.GetDouble(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldEnum1 = (Tb_alltypeTESTFIELDENUM1?)dr.GetInt64(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldEnum1Nullable = (Tb_alltypeTESTFIELDENUM1NULLABLE?)dr.GetInt64(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldEnum2 = (Tb_alltypeTESTFIELDENUM2?)dr.GetInt64(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldEnum2Nullable = (Tb_alltypeTESTFIELDENUM2NULLABLE?)dr.GetInt64(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldFloat = (float?)dr.GetFloat(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldFloatNullable = (float?)dr.GetFloat(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldGuid = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldGuidNullable = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldInt = (int?)dr.GetInt32(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldIntNullable = (int?)dr.GetInt32(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldLineString = MygisGeometry.Parse(dr.GetString(dataIndex)) as MygisLineString;
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldLong = (long?)dr.GetInt64(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldMultiLineString = MygisGeometry.Parse(dr.GetString(dataIndex)) as MygisMultiLineString;
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldMultiPoint = MygisGeometry.Parse(dr.GetString(dataIndex)) as MygisMultiPoint;
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldMultiPolygon = MygisGeometry.Parse(dr.GetString(dataIndex)) as MygisMultiPolygon;
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldPoint = MygisGeometry.Parse(dr.GetString(dataIndex)) as MygisPoint;
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldPolygon = MygisGeometry.Parse(dr.GetString(dataIndex)) as MygisPolygon;
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldSByte = (byte?)dr.GetByte(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldSByteNullable = (byte?)dr.GetByte(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldShort = (short?)dr.GetInt16(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldShortNullable = (short?)dr.GetInt16(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldString = dr.GetString(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldTimeSpan = (TimeSpan?)dr.GetValue(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldTimeSpanNullable = (TimeSpan?)dr.GetValue(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldUInt = (uint?)dr.GetInt32(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldUIntNullable = (uint?)dr.GetInt32(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldULong = (ulong?)dr.GetInt64(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldULongNullable = (ulong?)dr.GetInt64(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldUShort = (ushort?)dr.GetInt16(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldUShortNullable = (ushort?)dr.GetInt16(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFielLongNullable = (long?)dr.GetInt64(dataIndex);
			return (item, dataIndex);
		}
		public Task<int> DeleteAsync(int Id) {
			return SqlHelper.ExecuteNonQueryAsync(string.Concat(TSQL.Delete, "`Id` = ?Id"), 
				GetParameter("?Id", MySqlDbType.Int32, 11, Id));
		}
		async public Task<Tb_alltypeInfo> InsertAsync(Tb_alltypeInfo item) {
			if (int.TryParse(string.Concat(await SqlHelper.ExecuteScalarAsync(TSQL.Insert, GetParameters(item))), out var loc1)) item.Id = loc1;
			return item;
		}
		#endregion
	}
}