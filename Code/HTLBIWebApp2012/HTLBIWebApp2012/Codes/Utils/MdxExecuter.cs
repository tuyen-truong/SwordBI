using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.AnalysisServices.AdomdClient;

namespace HTLBIWebApp2012
{
	public class MdxExecuter
	{
		private String _connectionString = String.Empty;

		protected MdxExecuter() { }
		public MdxExecuter(String _connectionString)
		{
			this._connectionString = _connectionString;
		}

		public CellSet ExecuteQuery(String query)
		{
			String sessionId = String.Empty;
			return ExecuteQuery(query, ref sessionId);
		}

		public CellSet ExecuteQuery(String query, ref String sessionId)
		{
			if (!String.IsNullOrWhiteSpace(_connectionString))
			{
				using (AdomdConnection _connect = new AdomdConnection(this._connectionString))
				{
					try
					{
						_connect.Open();
						sessionId = _connect.SessionID;
						using (AdomdCommand cmd = new AdomdCommand(query, _connect))
						{
							CellSet cs = cmd.ExecuteCellSet();
							return cs;
						}
					}
					catch (Exception ex)
					{
						_connect.Close();
						throw ex;
					}
				}
			}
			return null;
		}

		public DataTable ExecuteReader(String query)
		{
			String sessionId = String.Empty;
			return ExecuteReader(query, ref sessionId);
		}

		public DataTable ExecuteReader(String query, ref String sessionId)
		{
			if (!String.IsNullOrWhiteSpace(_connectionString))
			{
				using (AdomdConnection _connect = new AdomdConnection(this._connectionString))
				{
					try
					{
						_connect.Open();
						sessionId = _connect.SessionID;
						using (AdomdCommand cmd = new AdomdCommand(query, _connect))
						{
							using (var reader = cmd.ExecuteReader())
							{
								if (reader != null)
								{
									DataTable table = new DataTable();
									var metadata_table = reader.GetSchemaTable();
									if (metadata_table != null)
									{
										foreach (DataRow row in metadata_table.Rows)
										{
											String columnName = row[0].ToString();
											columnName = Enumerable.LastOrDefault<string>((IEnumerable<string>)columnName.Replace(".[MEMBER_CAPTION]", "").Replace("[", "").Replace("]", "").Split(new char[1] { '.' }));
											DataColumn dataCol = new DataColumn(columnName);
											if (row[0].ToString().Contains("[Measures]"))
											{
												dataCol.DataType = typeof(double);
												dataCol.DefaultValue = (object)0;
											}
											table.Columns.Add(dataCol);
										}
									}

									if (table.Columns.Count >= reader.FieldCount)
									{
										while (reader.Read())
										{
											var values = new object[reader.FieldCount];
											for (int i = 0; i < reader.FieldCount; i++)
											{
												values[i] = reader[i];
											}
											table.Rows.Add(values);
										}
									}
									return table;
								}
							}
						}
					}
					catch (Exception ex)
					{
						_connect.Close();
						throw ex;
					}
				}
			}
			return null;
		}

		public int ExecuteNonQuery(String query)
		{
			return 0;
		}

		public DataSet Execute(String query)
		{
			String sessionId = String.Empty;
			return Execute(query, ref sessionId);
		}

		public DataSet Execute(String query, ref String sessionId)
		{
			if (!String.IsNullOrWhiteSpace(_connectionString))
			{
				using (AdomdConnection _connect = new AdomdConnection(_connectionString))
				{
					try
					{
						using (AdomdDataAdapter adapter = new AdomdDataAdapter(query, _connect))
						{
							DataSet ds = new DataSet();
							adapter.Fill(ds);
							foreach (DataTable tbl in ds.Tables)
							{
								foreach (DataColumn dc in tbl.Columns)
								{
									dc.ColumnName = Helpers.GetDimFieldShortName(dc.ColumnName);
								}
							}

							return ds;
						}
					}
					catch (Exception ex)
					{
						if (_connect.State == ConnectionState.Open)
						{
							_connect.Close();
						}
						throw ex;
					}
				}
			}
			return null;
		}
	}
}