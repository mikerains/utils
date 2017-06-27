using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Humanizer;

namespace mysqlgen
{
    public partial class Form1 : Form
    {
        private List<string> tables = new List<string>() {
            "content",
            "content_version_scale_values",
            "content_version_variants",
            "content_versions",
            "instrument_content",
            "instrument_scale_values",
            "instrument_versions",
            "instruments",
            "members",
            "project_instrument_versions",
            "respondent_statuses",
            "respondent_variables",
            "respondents",
            "scale_types",
            "scale_value_versions",
            "scale_value_version_variants",
            "scale_values",
            "scales",
            "team_instances",
            "team_instance_members",
            "team_member_types",
            "teams",
            "text_variable_types",
            "text_variable_value_variants",
            "text_variable_values",
            "text_variables",
            "text_variant_variables",
            "text_variants"
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            //ArrayList cols = getColNames("respondents", "respondent_id");
            //foreach (GenColumn col in cols)
            //{
            //    Console.WriteLine(col.colname + " " + ConvertSchemaDatatypeToCSharp(col.datatype));
            //}
            GenAllTables();
        }

        private void GenAllTables()
        {
            string cnstring = ConfigurationManager.AppSettings["cnstring"].ToString();
            using (MySqlConnection cn = new MySqlConnection(cnstring))
            {
                string tablessql = "select * from INFORMATION_ScHEMA.TABLES";
                var tablesDs = GetDataset(tablessql, cn);
                if (DBUtil.hasRows(tablesDs))
                {
                    List<string> dbsets = new List<string>();
                    List<string> modelbuilders = new List<string>();
                    foreach (DataRow row in tablesDs.Tables[0].Rows)
                    {
                        string tablename = row["TABLE_NAME"].ToString();
                        if ((int)tablename[0] > 97 && tables.Contains(tablename))
                        {
                            //WriteEntityFrameworkEntities(cn, tablename, dbsets, modelbuilders);
                            WriteGallupEntities(cn, tablename);
                        }
                    }

                    System.IO.StreamWriter swm = GetStreamWriter(Path.Combine(SelectedFolder.Text, "_ModelBuilderStatements" + ".cs"));
                    foreach (string stmt in modelbuilders)
                    {
                        writeline(swm, 3, stmt);
                    }
                    swm.Flush();
                    swm.Close();

                    System.IO.StreamWriter swdbsets = GetStreamWriter(Path.Combine(SelectedFolder.Text, "_DbSetStatements" + ".cs"));
                    foreach (string stmt in dbsets)
                    {
                        writeline(swdbsets, 3, stmt);
                        writeline(swdbsets, 0, "");
                    }
                    swdbsets.Flush();
                    swdbsets.Close();
                }
            }
        }

        private void WriteEntityFrameworkEntities(MySqlConnection cn, string tablename, List<string> dbsets, List<string> modelbuilders)
        {
            string pksql = "select * from information_schema.COLUMNS where  TABLE_NAME='" + tablename + "' and COLUMN_KEY='PRI' ";
            var pkDs = GetDataset(pksql, cn);
            string pkcolname = getPKColName(pkDs);
            var cols = getColNames(cn, tablename, pkcolname);
            System.IO.StreamWriter sw = GetStreamWriter(Path.Combine(SelectedFolder.Text, tablename + ".cs"));

            writeline(sw, 0, "// <copyright file=\"content.cs\" company=\"Gallup\">");
            writeline(sw, 0, "// Copyright (c) Gallup. All rights reserved.");
            writeline(sw, 0, "// </copyright>");
            writeline(sw, 0, "");
            writeline(sw, 0, "namespace GSS.RespondentReporting.Core.Repositories.Context.Model");
            writeline(sw, 0, "{");
            writeline(sw, 1, "using System;");
            writeline(sw, 1, "using System.Diagnostics.CodeAnalysis;");
            writeline(sw, 1, "/// <summary>");
            writeline(sw, 1, $"/// entity for table name \"{ tablename}\"");
            writeline(sw, 1, "/// </summary>");
            writeline(sw, 1, "[SuppressMessage(\"Microsoft.StyleCop.CSharp.DocumentationRules\", \"SA1300\", Justification = \"Class name is lowercase to match table name\")]");
            writeline(sw, 1, "public class " + tablename);
            writeline(sw, 1, "{");
            foreach (GenColumn col in cols)
            {
                string dtype = ConvertSchemaDatatypeToCSharp(col.datatype);
                write(sw, 2, "public " + dtype);
                if (col.isNullable && dtype != "string")
                {
                    write(sw, 0, "?");
                }
                writeline(sw, 0, " " + col.colname + "{ get; set;}");
                if (col.isPK)
                {
                    modelbuilders.Add($"modelBuilder.Entity<{tablename}>().ToTable($\"{tablename}\").HasKey(t => t.{col.colname});");
                    dbsets.Add($"public DbSet<{tablename}> {tablename} {{ get; set; }}");
                }
            }
            writeline(sw, 1, "} //class");
            writeline(sw, 0, "} //namespace");
            sw.Flush();
            sw.Close();
        }

        private void WriteGallupEntities(MySqlConnection cn, string tablename)
        {
            string humanTablename = tablename.ToPascalCase();

            string pksql = "select * from information_schema.COLUMNS where  TABLE_NAME='" + tablename + "' and COLUMN_KEY='PRI' ";
            var pkDs = GetDataset(pksql, cn);
            string pkcolname = getPKColName(pkDs);
            var cols = getColNames(cn, tablename, pkcolname);
            System.IO.StreamWriter sw = GetStreamWriter(Path.Combine(SelectedFolder.Text, humanTablename + ".cs"));

            writeline(sw, 0, "// <copyright file=\"content.cs\" company=\"Gallup\">");
            writeline(sw, 0, "// Copyright (c) Gallup. All rights reserved.");
            writeline(sw, 0, "// </copyright>");
            writeline(sw, 0, "");
            writeline(sw, 0, "namespace GSS.RespondentReporting.Core.Entities");
            writeline(sw, 0, "{");
            writeline(sw, 1, "using System;");
            writeline(sw, 1, "using GSS.RespondentReporting.Core.Repositories;");
            //writeline(sw, 1, "using GSS.RespondentReporting.Core.Enums;");
            writeline(sw, 1, "/// <summary>");
            writeline(sw, 1, $"/// entity for table name \"{ tablename}\"");
            writeline(sw, 1, "/// </summary>");
            //writeline(sw, 1, "[SuppressMessage(\"Microsoft.StyleCop.CSharp.DocumentationRules\", \"SA1300\", Justification = \"Class name is lowercase to match table name\")]");
            writeline(sw, 1, "[DBTable(Table.TBL_RESPONDENTS, InsertPrimaryKey = true)]");
            
            writeline(sw, 1, "public class " + humanTablename + " : BaseEntity");
            writeline(sw, 1, "{");
            for(int loop = 0; loop < 2; loop++) { 
                foreach (GenColumn col in cols)
                {
                    
                    string humanColname = col.colname.ToPascalCase();
                    string dtype = ConvertSchemaDatatypeToCSharp(col.datatype);
                    if (0==loop && col.isPK)
                    {
                        writeline(sw, 0, "");
                        writeline(sw, 2, $"[DBColumn(\"{col.colname}\", IsPrimaryKey =true)]");
                        write(sw, 2, "public " + dtype);
                        if (col.isNullable && dtype != "String")
                        {
                            write(sw, 0, "?");
                        }
                        writeline(sw, 0, " " + humanColname + "{ get; set;}");
                    } else if (1==loop && !col.isPK)
                    {
                        writeline(sw, 0, "");
                        writeline(sw, 2, $"[DBColumn(\"{col.colname}\")]");
                        write(sw, 2, "public " + dtype);
                        if (col.isNullable && dtype != "string")
                        {
                            write(sw, 0, "?");
                        }
                        writeline(sw, 0, " " + humanColname + "{ get; set;}");
                    }


                }
            }
            writeline(sw, 1, "} //class");
            writeline(sw, 0, "} //namespace");
            sw.Flush();
            sw.Close();
        }


        private StreamWriter GetStreamWriter(string filename)
        {
            FileStream fs = File.Open(filename, FileMode.Create);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
            return sw;
        }

        private void writeline(StreamWriter sw, int indents, string text)
        {
            sw.Write(new string('\t', indents));
            sw.WriteLine(text);
        }

        private void write(StreamWriter sw, int indents, string text)
        {
            sw.Write(new string('\t', indents));
            sw.Write(text);
        }

        private string getPKColName(DataSet ds)
        {
            string pkColname = "UNKNOWN";
            if (DBUtil.hasRows(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //if (row["is_identity"].ToString().ToUpper().Equals("TRUE"))
                    //{
                    pkColname = row["COLUMN_NAME"].ToString();
                    //}
                }
            }
            return pkColname;
        }

        private DataSet GetDataset(string sql, MySqlConnection cn)
        {
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            cmd.CommandType = CommandType.Text;
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public ArrayList getColNames(MySqlConnection cn, string tablename, string pkColname)
        {
            string cnstring = ConfigurationManager.AppSettings["cnstring"].ToString();
            //using (MySqlConnection cn = new MySqlConnection(cnstring))
            //{
            ArrayList gcol = new ArrayList();
            //string sql = "select * from sys.columns where object_id = (select object_id from sys.tables where name='" + tablename + "')";
            //string sql = "select * from information_schema.columns where TABLE_SCHEMA='" + txtSchemaName + "' and TABLE_NAME = '" + tablename + "' order by ORDINAL_POSITION";
            string sql = "select * from information_schema.columns where  TABLE_NAME = '" + tablename + "' order by ORDINAL_POSITION";
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            cmd.CommandType = CommandType.Text;

            ///SqlDataReader dr = cmd.ExecuteReader();
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (DBUtil.hasRows(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    GenColumn gencol = new GenColumn();
                    gencol.colname = row["COLUMN_NAME"].ToString();
                    if (row["IS_NULLABLE"].ToString() == "YES")
                    {
                        gencol.isNullable = true;
                    }
                    long MaxLength = 0;
                    string MaxLengthChar = row["CHARACTER_MAXIMUM_LENGTH"].ToString();
                    if (!string.IsNullOrEmpty(MaxLengthChar))
                    {
                        MaxLength = long.Parse(MaxLengthChar);
                    }
                    if (MaxLength == -1) MaxLength = (2 ^ 31) - 1;
                    gencol.Length = MaxLength;
                    gencol.datatype = null;
                    //56=int, 60=money, 167=varchar, 34=image, 35=text, 36=uniqueidentifier, 61=datetime, 173=binary, 241=xml???  231=nvarchar
                    //int dtype = int.Parse(row["DATA_TYPE"].ToString());
                    string dataType = (row["DATA_TYPE"].ToString());
                    switch (dataType)
                    {
                        case "longblob":
                            gencol.datatype = "LONGBLOB";
                            break;

                        case "int":
                        case "smallint":
                        case "tinyint":
                            gencol.datatype = "INT";
                            break;

                        case "bigint":
                            gencol.datatype = "BIGINT";
                            break;

                        case "decimal":
                            gencol.datatype = "DECIMAL";
                            break;

                        case "double":
                            gencol.datatype = "DOUBLE";
                            break;
                        //case 60: gencol.datatype = "NUMBER";
                        //    break;
                        case "varchar":
                            gencol.datatype = "VARCHAR";
                            break;
                        //case 231: gencol.datatype = "NVARCHAR";
                        //    break;
                        //case 239: gencol.datatype = "NCHAR";
                        //    break;
                        case "char":
                            gencol.datatype = "CHAR";
                            break;

                        case "blob":
                        case "longtext":
                            gencol.datatype = "CLOB";
                            break;

                        case "datetime":
                        case "timestamp":
                            gencol.datatype = "DATETIME";
                            break;

                        case "date":
                            gencol.datatype = "DATE";
                            break;

                        case "time":
                            gencol.datatype = "TIME";
                            break;

                        default:

                            gencol.datatype = "UNKNOWN_SCHEMATYPE: " + dataType;
                            break;
                    }
                    if (gencol.colname.Equals(pkColname)) gencol.isPK = true;
                    else gencol.isPK = false;
                    //if (HasCodeLookup(tablename, gencol.colname)) gencol.hasCodeLookup = true;
                    //else gencol.hasCodeLookup = false;
                    gcol.Add(gencol);
                }
            }

            return gcol;
            // }
        }

        public string ConvertSchemaDatatypeToCSharp(string dtype)
        {
            if (dtype.ToUpper().IndexOf("VARCHAR") == 0) return "string";
            if (dtype.ToUpper().IndexOf("NVARCHAR") == 0) return "string";
            else if (dtype.ToUpper().IndexOf("NUMBER") == 0) return "Decimal";
            else if (dtype.ToUpper().IndexOf("CLOB") == 0) return "string";
            else if (dtype.ToUpper().IndexOf("DATE") == 0) return "DateTime";
            else if (dtype.ToUpper().IndexOf("INT") == 0) return "int";
            else if (dtype.ToUpper().IndexOf("BIGINT") == 0) return "long";
            else if (dtype.ToUpper().IndexOf("NCHAR") == 0) return "string";
            else if (dtype.ToUpper().IndexOf("CHAR") == 0) return "string";
            else if (dtype.ToUpper().IndexOf("DOUBLE") == 0) return "double";
            return "CSharp_TYPE_UNKNOWN_FOR_" + dtype;
        }

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = @"c:\_mike_repo";
            var selected = fbd.ShowDialog();
            if (selected == DialogResult.OK)
            {
                SelectedFolder.Text = fbd.SelectedPath;
            }
        }
    }

    public static class Mike
    {
        public static string ToPascalCase(this string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null) return the_string;
            if (the_string.Length < 2) return the_string.ToUpper();

            // Split the string into words.
            string[] words = the_string.Split(
                new char[] { '_' },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = "";
            foreach (string word in words)
            {
                result +=
                    word.Substring(0, 1).ToUpper() +
                    word.Substring(1);
            }

            return result;
        }
    }

}