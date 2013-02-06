using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;

namespace WinWam6
{
	static class WWD
	{
		static private OleDbConnection gCn;
		static private OleDbConnection gSysCn;
		static private Dictionary<string, TableWrapper> TableWrapperCache = new Dictionary<string,TableWrapper>();

		static public void OpenDatabase()
		{
			gCn = new OleDbConnection();
			gCn.ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=C:\\users\\geoff\\My Documents\\Visual Studio 2010\\Projects\\WW6-WPF\\ww6.mdb;Persist Security Info=False";
			gCn.Open();

			gSysCn = new OleDbConnection();
			gSysCn.ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=C:\\users\\geoff\\My Documents\\Visual Studio 2010\\Projects\\WW6-WPF\\wamtbl.dat;Persist Security Info=False";
			gSysCn.Open();
		}

		static public DbDataReader GetSysReader(string sql)
		{
			OleDbCommand cmd = new OleDbCommand(sql, gSysCn);
			OleDbDataReader rdr = cmd.ExecuteReader();
			return rdr;
		}

		static public DbDataReader GetReader(string sql, bool KeyInfo = false)
		{
			OleDbCommand cmd = new OleDbCommand(sql, WWD.gCn);
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
			OleDbCommand cmd = new OleDbCommand(sql, WWD.gCn);
			return cmd;
		}

		static public DataTable GetTableSchema(string TableName)
		{
			return GetReader("select * from " + TableName + " where 1=0", true).GetSchemaTable();    //Get the table schema
		}

		static public DataAdapter GetDataAdapter(string TableName)
		{
			OleDbDataAdapter da = new OleDbDataAdapter("select * from " + TableName, WWD.gCn);
			OleDbCommandBuilder cb = new OleDbCommandBuilder(da);

			return da;
		}

		static public List<string> TableList()
		{
			List<string> TableList = new List<string>();
			DataTable dt;

			dt = gCn.GetSchema("Tables");
			foreach (DataRow r in dt.Rows)
			{
				TableList.Add(r["TABLE_NAME"].ToString());
			}
			return TableList;
		}

		/// <summary>
		/// These extension functions are wrappers for the Data Reader 'Get' functions that handle Nulls automatically (returning '0' for ints & floats)
		/// </summary>
		/// <param name="rdr"></param>
		/// <param name="OrdinalIndex"></param>
		/// <returns></returns>
		public static string GetStringNoNull(this DbDataReader rdr, int OrdinalIndex)
		{
			string s;
			try
			{
				s = rdr[OrdinalIndex].ToString();
				return s;
			}
			catch (InvalidCastException)
			{
				return "";
			}
		}

		public static int GetInt32NoNull(this DbDataReader rdr, int OrdinalIndex)
		{
			int s;
			try
			{
				if (rdr[OrdinalIndex] != null)
				{
					s = rdr.GetInt32(OrdinalIndex);
					return s;
				}
				else
				{
					return 0;
				}
			}
			catch (InvalidCastException)
			{
				return 0;
			}
		}

		public static int GetInt16NoNull(this DbDataReader rdr, int OrdinalIndex)
		{
			int s;
			try
			{
				if (rdr[OrdinalIndex] != null)
				{
					s = rdr.GetInt16(OrdinalIndex);
					return s;
				}
				else
				{
					return 0;
				}
			}
			catch (InvalidCastException)
			{
				return 0;
			}
		}

		public static bool GetBooleanNoNull(this DbDataReader rdr, int OrdinalIndex)
		{
			bool s;
			try
			{
				s = rdr.GetBoolean(OrdinalIndex);
				return s;
			}
			catch (InvalidCastException)
			{
				return false;
			}
		}

		public static double GetDoubleNoNull(this DbDataReader rdr, int OrdinalIndex)
		{
			double s;
			try
			{
				s = rdr.GetDouble(OrdinalIndex);
				return s;
			}
			catch (InvalidCastException)
			{
				return 0;
			}
		}

		public static decimal GetDecimalNoNull(this DbDataReader rdr, int OrdinalIndex)
		{
			decimal s;
			try
			{
				s = rdr.GetDecimal(OrdinalIndex);
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

		//Factory that returns a TableWrapper object. Keeps track of objects that have been created already
		// and will return the old object if it exists
		//NOTE: This may not be threadsafe, if more than one thread tries to access the same Wrapper

		public static TableWrapper GetTableWrapper(string TableName)
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

		//WrapField - This function puts the delimiters around the field value
		//  based on the type of database we are connected to. Also escapes any values
		//  for safety

		public static string WrapField(Field field)
		{
			string s;
			
			//TODO - Add differences based on type of database
		   
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
	public class Field
	{
		private string m_name;
		private string m_datatype;
		private int m_length;
		private bool m_iskey;
		private object m_value;

		public string Name { get { return m_name; } }
		public string DataType { get { return m_datatype; } }
		public int Length { get { return m_length; } }
		public bool Changed { get; set; }
		public bool IsKey { get { return m_iskey; } }
		public object Value
		{
			get { return m_value; }
			set
			{
				object lval;
				lval = value;
				//TODO - Add more datatype checking
				if (this.DataType == "System.String")
				{
					if (value == null) value = "";
					if (value.ToString().Length > this.Length)
					{
						//Truncate to length
						value = value.ToString().Substring(0, this.Length);
					}
				}

				if ((this.DataType == "System.Int32") || (this.DataType == "System.Int16") || (this.DataType == "System.Decimal") || (this.DataType == "System.Double") || (this.DataType == "System.Single"))
				{
					double CheckVal;
					if (!(double.TryParse(value.ToString(), out CheckVal))) value = 0;
				}
				// If the value has changed from the current value, then save it and mark us as dirty
				if (m_value != value)
				{
					m_value = value;
					this.Changed = true;
				}
			}
		}


		public Field(string Name, string DataType, int Length, bool IsKey)
		{
			m_name = Name;
			m_datatype = DataType;
			m_length = Length;
			m_iskey = IsKey;
			this.Changed = false;
		}
	}

	public class TableWrapper
	{
		//Private backing fields
		private string m_name;
		private Dictionary<string, Field> m_fields;

		//Properties
		public string Name { get { return m_name; } }
		public Dictionary<string, Field> Fields { get { return m_fields; } }

		//Constructor
		public TableWrapper(string Name)
		{
			Field f;

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
				
				f = new Field(fName, fDataType, fLength, fIsKey);
				f.Value = "";
				f.Changed = false;

				this.Fields.Add(f.Name, f);     //Add it to the collection
			}
			lTable.Dispose();

		}

		//Access through a field name (to make life simpler)
		public object this [string FieldName]
		{
			get
			{ return this.Fields[FieldName].Value; }
			set
			{
				this.Fields[FieldName].Value = value;
			}
		}

		//Methods

		// Update - Creates an UPDATE SQL string
		public string Update()
		{
			string s; 
			
			bool FirstItem = true;
			string lval;
			string where = this.Where();   //where clause

			s = "update " + this.Name + " set ";
			
			foreach (System.Collections.Generic.KeyValuePair<string, Field> kv in this.Fields)
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
			else
			{
				s = s + " where " + where;
				return s;
			}
		}

		//Insert - Creates an INSERT SQL string
		public string Insert()
		{
			string s;   //Main SQL Query
			string sv;  //Values
			bool FirstItem = true;
			string lval;

			s = "insert into " + this.Name + " (";
			sv = "values (";

			foreach (System.Collections.Generic.KeyValuePair<string, Field> kv in this.Fields)
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
			foreach (System.Collections.Generic.KeyValuePair<string, Field> kv in this.Fields)
			{
				kv.Value.Changed = false;
			}
		}

		// LOAD - Loads a single row based on the KEY fields
		public bool Load()
		{
			bool result = false;

			//Create where clause
			string where = this.Where();

			if (where.Length == 0)
			{
				// No Key fields, so can't use wrapper. Just bail
				return false;
			}

			//Get the correct row

			DbDataReader dr = WWD.GetReader("Select * from " + this.Name + " where " + where);

			while (dr.Read())
			{
				//Should only be one row
				foreach (System.Collections.Generic.KeyValuePair<string, Field> kv in this.Fields)
				{
					kv.Value.Value = dr[kv.Value.Name];     //Load the field value
				}
				this.ClearChanged();        //Clear all the change flags to show we are unchanged.
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
			//First check to see if the row exists
			string where = this.Where();
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

			if (found)
			{
				sql = this.Update();
			}
			else
			{
				sql = this.Insert();
			}
			DbCommand cmd = WWD.GetCommand(sql);
			cmd.ExecuteNonQuery();

			return true;
		}

		//IsDirty - Whether any field has been changed or not
		public bool IsDirty
		{
			get
			{
				bool rtn = false;
				foreach (System.Collections.Generic.KeyValuePair<string, Field> kv in this.Fields)
				{
					if (kv.Value.Changed)
					{ rtn = true; break; }
				}

				return rtn;
			}
		}

		public string Where()
		{
			string where = "";

			foreach (System.Collections.Generic.KeyValuePair<string, Field> kv in this.Fields)
			{
				if (kv.Value.Value.ToString().Length > 0)
				{
					if (kv.Value.IsKey)     //Add to the where clause if it is a key
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
			string sType = "";  //Datatype string
 
			foreach (System.Collections.Generic.KeyValuePair<string, Field> kv in this.Fields)
			{
				sType = kv.Value.DataType.Substring(7); //Strip System. from start of string
				if (sType == "Boolean") sType = "bool";  //correct datatype for Boolean

				output += "public " + sType + " " + kv.Value.Name + "\n\r{";

				switch (sType)
				{
					case "String":
						{
							output += "\tget { return lObj[\"" + kv.Value.Name + "\"].ToString(); }";
							break;
						}
					default:
						{
							output += "\tget { return " + sType + ".Parse(lObj[\"" + kv.Value.Name + "\"].ToString()); }";
							break;
						}
				}
				output += "\tset { lObj[\"" + kv.Value.Name + "\"] = value; NotifyPropertyChanged(\"" + kv.Value.Name + "\"); } \n\r}";
			}

			return output;  
		}
	}

	
	

	


	#endregion

}
