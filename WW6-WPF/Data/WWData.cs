using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;
using System.Linq;

namespace WinWam6
{
	static class WWD
	{

        static private readonly DateTime nullDate = new DateTime(1800, 1, 1);
        static public DateTime NullDate
		{
			get { return nullDate; }
		}

		static private OleDbConnection gCn;
		static private OleDbConnection gSysCn;

	    static public void OpenDatabase()
		{

			gCn = new OleDbConnection
			{
			    ConnectionString =
			        "Provider=Microsoft.Jet.OleDb.4.0;Data Source=C:\\users\\geoff\\My Documents\\Visual Studio 2010\\Projects\\WW6-WPF\\ww6.mdb;Persist Security Info=False"
			};
	        gCn.Open();

			gSysCn = new OleDbConnection
			{
			    ConnectionString =
			        "Provider=Microsoft.Jet.OleDb.4.0;Data Source=C:\\users\\geoff\\My Documents\\Visual Studio 2010\\Projects\\WW6-WPF\\wamtbl.dat;Persist Security Info=False"
			};
	        gSysCn.Open();
		}

		static public bool DatabaseIsOpen()
		{
			if (gCn == null)
			{ return false; }

		    return (gCn.State == ConnectionState.Open);

		}

		static public DbDataReader GetSysReader(string sql)
		{
			var cmd = new OleDbCommand(sql, gSysCn);
			OleDbDataReader rdr = cmd.ExecuteReader();
			return rdr;
		}

		static public DbDataReader GetReader(string sql, bool KeyInfo = false)
		{
			var cmd = new OleDbCommand(sql, gCn);
			if (KeyInfo)
			{
				OleDbDataReader rdr = cmd.ExecuteReader(CommandBehavior.KeyInfo);
				return rdr;
			}
			else
			{
				OleDbDataReader rdr = cmd.ExecuteReader();
				return rdr;
			}
		}

		static public DbCommand GetCommand(string sql)
		{
			var cmd = new OleDbCommand(sql, gCn);
			return cmd;
		}

        static public DbDataAdapter GetAdapter(string sql)
        {
            var da = new OleDbDataAdapter(sql,gCn);
            return da;
        }

		static public DataTable GetTableSchema(string TableName)
		{
			return GetReader("select * from " + TableName + " where 1=0", true).GetSchemaTable();    //Get the table schema
		}

		static public DataAdapter GetDataAdapter(string TableName)
		{
			var da = new OleDbDataAdapter("select * from " + TableName, gCn);

			return da;
		}

		static public List<string> TableList()
		{
		    DataTable dt = gCn.GetSchema("Tables");
		    return (from DataRow r in dt.Rows
		        select r["TABLE_NAME"].ToString()).ToList();
		}

		/// <summary>
		/// These extension functions are wrappers for the Data Reader 'Get' functions that handle Nulls automatically (returning '0' for ints & floats)
		/// </summary>
		/// <param name="rdr"></param>
		/// <param name="OrdinalIndex"></param>
		/// <returns></returns>
		public static string GetStringNoNull(this DbDataReader rdr, int OrdinalIndex)
		{
		    try
			{
			    string s = rdr[OrdinalIndex].ToString();
			    return s;
			}
			catch (InvalidCastException)
			{
				return "";
			}
		}

		public static int GetInt32NoNull(this DbDataReader rdr, int OrdinalIndex)
		{
		    try
		    {
		        if (rdr[OrdinalIndex] != null)
				{
				    int s = rdr.GetInt32(OrdinalIndex);
				    return s;
				}
		        return 0;
		    }
		    catch (InvalidCastException)
			{
				return 0;
			}
		}

		public static int GetInt16NoNull(this DbDataReader rdr, int OrdinalIndex)
		{
		    try
		    {
		        if (rdr[OrdinalIndex] != null)
				{
				    int s = rdr.GetInt16(OrdinalIndex);
				    return s;
				}
		        return 0;
		    }
		    catch (InvalidCastException)
			{
				return 0;
			}
		}

		public static bool GetBooleanNoNull(this DbDataReader rdr, int OrdinalIndex)
		{
		    try
			{
			    bool s = rdr.GetBoolean(OrdinalIndex);
			    return s;
			}
			catch (InvalidCastException)
			{
				return false;
			}
		}

		public static double GetDoubleNoNull(this DbDataReader rdr, int OrdinalIndex)
		{
		    try
			{
			    double s = rdr.GetDouble(OrdinalIndex);
			    return s;
			}
			catch (InvalidCastException)
			{
				return 0;
			}
		}

		public static decimal GetDecimalNoNull(this DbDataReader rdr, int OrdinalIndex)
		{
		    try
			{
			    decimal s = rdr.GetDecimal(OrdinalIndex);
			    return s;
			}
			catch (InvalidCastException)
			{
				return 0;
			}
		}

		public static DateTime GetDateTimeNoNull(this DbDataReader rdr, int OrdinalIndex)
		{
			DateTime s;
			try
			{
				s = rdr.GetDateTime(OrdinalIndex);
				return s;
			}
			catch (InvalidCastException)
			{
				s = new DateTime(1899, 12, 1);
				return s;
			}
		}

		public static DateTime ParseNoNull(this DateTime datetime, string s)
		{
			DateTime d;
			if (DateTime.TryParse(s, out d))
			{
				return d;
			}
		    return new DateTime(1900, 1, 1);
		}



		//Factory that returns a TableWrapper object. Keeps track of objects that have been created already
		// and will return the old object if it exists
		//NOTE: This may not be threadsafe, if more than one thread tries to access the same Wrapper

		// REALLY IMPORTANT NOTE: This function needs to return a COPY of the cached Wrapper - Need to change.

/*		public static TableWrapper GetTableWrapper(string TableName)
		{
			TableWrapper returnWrapper;

			if (TableWrapperCache.TryGetValue(TableName, out returnWrapper))
			{
				return returnWrapper;
			}
			else
			{
				returnWrapper = new TableWrapper(TableName);
				TableWrapperCache.Add(TableName, returnWrapper);        //Save it for later
				return returnWrapper;
			}   
		}
		*/

		//WrapField - This function puts the delimiters around the field value
		//  based on the type of database we are connected to. Also escapes any values
		//  for safety

		public static string WrapField(Field field)
		{
			string s;
			
			//TODO - Add differences based on type of database

			if (null == field.Value)
			{
				return "null";
			}

			switch (field.DataType)
			{
				case "System.String":
					s = "'" + WWH.QuoteString(field.Value.ToString()) + "'";
					break;

				case "System.DateTime":
					s = "#" + field.Value.ToString() + "#";
					break;

				default:
					s = field.Value.ToString();
					break;
			}

			return s;
		}
	}

	#region Data Update Insert

	//This section contains the classes that manages the Table Schemas and Update/Insert wrappers
	
	//This class encapsulates FIELD information
    /// <summary>
    /// Encapsulates information about a database field.
    /// </summary>
    /// <param name="Name">Field Name in database</param>
    /// <param name="DataType">String field representing datatype. Based on return types from a Datatable</param>
    /// <param name="Length">Length field for string datatypes</param>
    /// <param name="Changed">Boolean. Represents whether the Field has ever changed since first read from the database.
    /// Note that when a value is saved it checks to see if it matches the existing value. If it does, it does not save, and does not set CHANGED.</param>
    /// <param name="IsKey">Boolean. Whether this field is part of the Primary Key. Used to fetch new rows and create the Update query</param>
    /// <param name="RecentChange">Boolean. Whether the last time the field saved it changed. This can be used, for example, to only raise NotifyPropertyChanged if the value actually changes.</param>
    /// <param name="Value">The actual stored value. Note that this is of type OBJECT and needs to be cast into the proper datatype.
    /// When set, automatically trims to proper length, and validates against data type. Also sets Changed and RecentChange properties</param>
    /// 
	public class Field
	{
		private readonly string m_name;
		private readonly string m_datatype;
		private readonly int m_length;
		private readonly bool m_iskey;
		private object m_value;

		public string Name { get { return m_name; } }
		public string DataType { get { return m_datatype; } }
		public int Length { get { return m_length; } }
		public bool Changed { get; set; }
		public bool IsKey { get { return m_iskey; } }
        public bool RecentChange { get; set; }
        private string valString = "";
        private Int32 valInt32 = 0;
        private Int16 valInt16 = 0;
        private decimal valDecimal = 0;
        private double valDouble = 0;
        private Single valSingle = 0;
        private DateTime? valDate;


		public object Value
		{
			get
			{
			    switch (DataType)
			    {
			        case "System.String":
			            return valString;
                    case "System.Double":
			            return valDouble;
                    case "System.Int16":
			            return valInt16;
                    case "System.Int32":
			            return valInt32;
                    case "System.Decimal":
			            return valDecimal;
                    case "System.Single":
			            return valSingle;
                    case "System.DateTime":
			            return valDate;

                    default:
			            return m_value;
			    }
			}
			set
			{
			    //TODO - Add more datatype checking
			    bool valChanged = false;

                switch (DataType)
                {
                    case "System.String":
                    {
                        string checkString;

                        checkString = (value as string);
                        if (string.IsNullOrEmpty(checkString))
                            checkString = string.Empty;
                        if (checkString.Length > Length)
                        {
                            //Truncate to length
                            valString = valString.Substring(0, Length);
                        }
                        if (valString != checkString)
                        {
                            valString = checkString;
                            valChanged = true;
                        }
                        break;
                    }

                    case "System.Double":
                        {
                            double checkVal;
                            if (!(double.TryParse(value.ToString(), out checkVal))) checkVal = 0;
                            if (checkVal != valDouble)
                            {
                                valDouble = checkVal;
                                valChanged = true;
                            }
                            break;
                        }

                    case "System.Int16":
                        {
                            Int16 checkVal;
                            if (!(Int16.TryParse(value.ToString(), out checkVal))) checkVal = 0;
                            if (checkVal != valInt16)
                            {
                                valInt16 = checkVal;
                                valChanged = true;
                            }
                            break;
                        }

                    case "System.Int32":
                        {
                            Int32 checkVal;
                            if (!(Int32.TryParse(value.ToString(), out checkVal))) checkVal = 0;
                            if (checkVal != valInt32)
                            {
                                valInt32 = checkVal;
                                valChanged = true;
                            }
                            break;
                        }

                    case "System.Decimal":
                        {
                            decimal checkVal;
                            if (!(Decimal.TryParse(value.ToString(), out checkVal))) checkVal = 0;
                            if (checkVal != valDecimal)
                            {
                                valDecimal = checkVal;
                                valChanged = true;
                            }
                            break;
                        }

                    case "System.Single":
                        {
                            Single checkVal;
                            if (!(Single.TryParse(value.ToString(), out checkVal))) checkVal = 0;
                            if (checkVal != valSingle)
                            {
                                valSingle = checkVal;
                                valChanged = true;
                            }
                            break;
                        }

                    case "System.DateTime":
                        {
                            DateTime tmpVal;
                            DateTime? checkVal;
                            if (!(DateTime.TryParse(value.ToString(), out tmpVal)))
                            {
                                checkVal = null;
                            }
                            else
                            {
                                checkVal = tmpVal;
                            }

                            if (checkVal.HasValue && valDate.HasValue)
                            {
                                if (checkVal.Value != valDate.Value)
                                {
                                    valDate = checkVal;
                                    valChanged = true;
                                }
                            }
                            else
                            {
                                // one or both is null, so update only if one or the other is null
                                if (!checkVal.HasValue && !valDate.HasValue)
                                {
                                    valDate = checkVal;
                                    valChanged = true;
                                }
                            }
                            break;
                        }

                    default:
                    {
                        m_value = value;
                        valChanged = true;
                        break;
                    }

                }


				// If the value has changed from the current value mark us as dirty
			    if (valChanged) Changed = true;
			    
			    RecentChange = valChanged;
			}
		}


		public Field(string Name, string DataType, int Length, bool IsKey)
		{
			m_name = Name;
			m_datatype = DataType;
			m_length = Length;
			m_iskey = IsKey;
			Changed = false;
		}
	}

	public class TableWrapper
	{
		//Private backing fields
		private readonly string m_name;
		private readonly Dictionary<string, Field> m_fields;

		//Properties
		public string Name { get { return m_name; } }
		public Dictionary<string, Field> Fields { get { return m_fields; } }

		//Constructor
		public TableWrapper(string Name)
		{
		    m_name = Name;
			m_fields = new Dictionary<string,Field>();

			//Find the fields
			DataTable lTable = WWD.GetTableSchema(Name);

			foreach (DataRow d in lTable.Rows)
			{
				int val;
				string fName;
				string fDataType;
				int fLength;
				bool fIsKey;

				fName = d["ColumnName"].ToString();
				fDataType = d["DataType"].ToString();
				if (Int32.TryParse(d["ColumnSize"].ToString(), out val))
				{
					fLength = val;
				}
				else
				{
					fLength = 0;
				}
				fIsKey = (bool)d["IsKey"];
				
				Field f = new Field(fName, fDataType, fLength, fIsKey) { Value = "", Changed = false };

			    Fields.Add(f.Name, f);     //Add it to the collection
			}
			lTable.Dispose();

		}

		//Access through a field name (to make life simpler)
		public object this [string FieldName]
		{
			get
			{ return Fields[FieldName].Value; }
			set
			{
				Fields[FieldName].Value = value;
			}
		}

		//Methods

		// Update - Creates an UPDATE SQL string
		public string Update()
		{
		    bool FirstItem = true;
			string lval;
			string where = this.Where();   //where clause

			string s = "update " + Name + " set ";
			
			foreach (var kv in Fields)
			{
				if (kv.Value.Value.ToString().Length > 0)
				{
					if (!kv.Value.IsKey)     //Do not update KEY fields
					{
						if (kv.Value.Changed)       //Only store changed values
						{
							if (FirstItem)
							{
								FirstItem = false;          //Add commas if this is not the first item
							}
							else
							{
								s += ", ";
							}

							lval = WWD.WrapField(kv.Value);

							s += kv.Value.Name + "=" + lval;

						}
					}
				}
			}

			if (where.Length == 0 || FirstItem)
			{
				//NOT GOOD - No key or nothing was changed. Need to raise an exception
				//For now, just kill the Update string
				return "";
			}
		    s = s + " where " + @where;
		    return s;
		}

		//Insert - Creates an INSERT SQL string
		public string Insert()
		{
		    string sv;  //Values
			bool FirstItem = true;
			string lval;

			string s = "insert into " + Name + " (";
			sv = "values (";

			foreach (var kv in Fields)
			{
				if (kv.Value.Value.ToString().Length > 0)
				{
					if (FirstItem)
					{
						FirstItem = false;          //Add commas if this is not the first item
					}
					else
					{
						s += ", ";
						sv += ", ";
					}

					s += kv.Value.Name;

					lval = WWD.WrapField(kv.Value);

					sv += lval;

				}
			}

			s = s + ") " + sv + ")";
			return s;
		}

		public void ClearChanged()
		{
			foreach (var kv in Fields)
			{
				kv.Value.Changed = false;
			}
		}

		// LOAD - Loads a single row based on the KEY fields
		public bool Load()
		{
			bool result = false;

			//Create where clause
			string where = Where();

			if (where.Length == 0)
			{
				// No Key fields, so can't use wrapper. Just bail
				return false;
			}

			//Get the correct row

			DbDataReader dr = WWD.GetReader("Select * from " + Name + " where " + where);

			while (dr.Read())
			{
				//Should only be one row
				foreach (var kv in Fields)
				{
					kv.Value.Value = dr[kv.Value.Name];     //Load the field value
				}
				ClearChanged();        //Clear all the change flags to show we are unchanged.
				result = true;
			}
			dr.Close();
			dr.Dispose();

			return result;   
			
		}

		//SAVE: Saves the row to the table
		//If the row exists, does an UPDATE, otherwise does an INSERT
		public bool Save()
		{
			if (!IsDirty) return true;

			//First check to see if the row exists
			string where = Where();
			if (where.Length == 0)
			{
				return false;  //No key fields - can't save
			}

			string sql = "Select * from " + m_name + " where " + where;
			DbDataReader rdr = WWD.GetReader(sql);
			
			bool found = false;

			while (rdr.Read())
			{
				found = true;
			}

			sql = found ? Update() : Insert();

		    if (sql == string.Empty)
		        return false;

			DbCommand cmd = WWD.GetCommand(sql);
		    try
		    {
                cmd.ExecuteNonQuery();
		    }
		    catch (Exception)
		    {
		        
		        throw new ArgumentOutOfRangeException();
		    }


			return true;
		}

		//IsDirty - Whether any field has been changed or not
		public bool IsDirty
		{
			get
			{
			    return Fields.Any(kv => kv.Value.Changed);
			}
		}

		public string Where()
		{
			string where = "";

			foreach (var kv in Fields)
			{
                if (kv.Value.IsKey)     //Add to the where clause if it is a key
				{
                    if (kv.Value.Value.ToString().Length > 0)
					{
						if (where.Length > 0) { where += " and "; }
						where += kv.Value.Name + "=" + WWD.WrapField(kv.Value);
					}
				}
			}

			return where;
		}
		
		public string GenerateClass()
		{
			// Generates the text to be copied and pasted into a Class Definition
			// Loop through all the fields. For each field generate a get/set pair

			string output = "";
		    string nullable = "";   //set to ? if Type needs to be marked as Nullable

			foreach (var kv in Fields)
			{
				string sType = kv.Value.DataType.Substring(7);  //Datatype string
				if (sType == "Boolean") sType = "bool";  //correct datatype for Boolean

				nullable = (sType == "DateTime") ? "?" : string.Empty;

				output += "public " + sType + nullable + " " + kv.Value.Name + "\n\r{\n\r";

				switch (sType)
				{
					case "String":
						{
							output += "\tget { return lObj[\"" + kv.Value.Name + "\"] as string; }\n\r";
							break;
						}
					case "DateTime":
						{
							output += "\tget { return (lObj[\"" + kv.Value.Name + "\"] as DateTime?); }\n\r";
							break;
						}
					default:
						{
							output += "\tget { return (" + sType + ")lObj[\"" + kv.Value.Name + "\"]; }\n\r";
							break;
						}
				}
				output += "\tset { lObj[\"" + kv.Value.Name + "\"] = value; if (lObj.Fields[\"" + kv.Value.Name + "\"].RecentChange) NotifyPropertyChanged(\"" + kv.Value.Name + "\"); } \n\r\n\r}";
			}

			return output;  
		}
	}

	
	

	


	#endregion

}
