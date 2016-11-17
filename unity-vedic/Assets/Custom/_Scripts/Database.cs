using UnityEngine;
using System.Collections.Generic;

namespace DatabaseUtilities
{
    public enum View_Type { Harness, Table, Column, DiskHarness, Disk };

    public static class VedicDatabase
    {
        public static Database db;
        public static Database dbAnalytic3;
        public static bool isDatabaseNull = true;

        // Gets the string name of the table attached to the param id
        public static string GetTableName(string id)
        {
            for(int i = 0; i < db.tables.Count; i++)
            {
                if (db.tables[i].GetId() == id) return db.tables[i].GetName();
            }
            return "No table with that ID.";
        }
        // Gets the string names, in list form, of all columns belonging to the table with the param id
        public static List<string> GetTableColumns(string id)
        {
            for (int i = 0; i < db.tables.Count; i++)
            {
                if (db.tables[i].GetId() == id)
                {
                    List<string> cols = new List<string>();
                    for (int j = 0; j < db.tables[i].columns.Count; j++)
                    {
                        cols.Add(db.tables[i].columns[j].GetName());
                    }
                    return cols;
                }
            }
            return null;
        }
        // Get the total number of columns in the database
        public static int GetNumOfColumns()
        {
            int count = 0;
            for (int i = 0; i < db.tables.Count; i++)
            {
                count += db.tables[i].columns.Count;
            }
            return count;
        }
        // Gets the string name of the column attached to the param id
        public static string GetColumnName(string id)
        {
            for (int i = 0; i < db.tables.Count; i++)
            {
                for (int j = 0; j < db.tables[i].columns.Count; j++)
                {
                    if (db.tables[i].columns[j].GetId() == id) return db.tables[i].columns[j].GetName();
                }
            }
            return "No column with that ID.";
        }
        // Sorts the database tables by number of columns in descending order.
        public static Database SortTablesByColumnQuantity()
        {
            Database db = new Database();
            db.SetName("Analytic_1");
            Table[] tabs = VedicDatabase.db.tables.ToArray();
            for(int i = 0; i < tabs.Length; i++)
            {
                int min = tabs[i].columns.Count;
                int mindex = i;
                for(int j = i+1; j < tabs.Length; j++)
                {
                    if(tabs[j].columns.Count < min)
                    {
                        mindex = j;
                        min = tabs[j].columns.Count;
                    }
                }
                Table tempTab = tabs[i];
                tabs[i] = tabs[mindex];
                tabs[mindex] = tempTab;
            }
            List<Table> tables = new List<Table>();
            for(int k = 0; k < tabs.Length; k++)
            {
                tables.Add(tabs[k]);
            }
            db.tables = tables;
            return db;
        }
        // Get Analytic 2 database (datatype proportions)
        public static Database GetDataTypeDB()
        {
            Database db = new Database();
            db.SetName("Analytic_2");
            db.tables = new List<Table>();
            Table tab = new Table();
            tab.SetId("A-2");
            tab.SetName("DataTypes in Proportion");
            tab.columns = new List<Column>();
            

            Dictionary<string, int> dic = new Dictionary<string, int>();

            for(int i = 0; i < VedicDatabase.db.tables.Count; i++)
            {
                for(int j = 0; j < VedicDatabase.db.tables[i].columns.Count; j++)
                {
                    if (dic.ContainsKey(VedicDatabase.db.tables[i].columns[j].GetType()))
                    {
                        dic[VedicDatabase.db.tables[i].columns[j].GetType()]++;
                    }
                    else
                    {
                        dic.Add(VedicDatabase.db.tables[i].columns[j].GetType(), 1);
                    }
                }
            }

            Dictionary<string, int>.KeyCollection keyColl = dic.Keys;

            int total = 0;
            foreach (string s in keyColl)
            {
                total += dic[s];
            }

            foreach (string s in keyColl)
            {
                Column col = new Column();
                col.SetName(s);
                col.SetId(s + "-2");
                col.SetColor(VariableColorTable.GetVariableColor(s));
                col.SetType(s);
                col.fields = new List<string>();
                col.fields.Add( ( (double)dic[s] / (double)total).ToString() );
                tab.columns.Add(col);
            }
            db.tables.Add(tab);
            return db;
        }
    }
    public class DatabaseBuilder
    {
        public static Database ConstructDB(string name, string data)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>(); // Color matching
            Database db = new Database();
            db.SetName(name);
            db.tables = new List<Table>();
            data = data.Substring(10);
            int tableCounter = -1;
            int columnCounter = -1;

            // Loops through all the tables for this database.
            while (true)
            {
                tableCounter++;
                columnCounter = -1;
                Table tb = new Table();
                tb.SetName( data.Substring(0, data.IndexOf(":")) );
                tb.SetId("T" + tableCounter);
                tb.columns = new List<Column>();
                data = data.Substring(data.IndexOf("{") + 1);

                // Loops through all the columns for this table.
                while (true)
                {
                    columnCounter++;
                    Column col = new Column();
                    col.SetName( data.Substring(0, data.IndexOf(":")) );
                    col.SetId("T" + tableCounter + "-C" + columnCounter);
                    // If column exists elsewhere, use same color.
                    if (dic.ContainsKey(tb.GetName()))
                    {
                        col.SetColor(dic[tb.GetName()]);
                    }
                    else
                    {
                        string c = GetRandomColor();
                        dic.Add(tb.GetName(), c);
                        col.SetColor(c);
                    }
                    col.fields = new List<string>();
                    data = data.Substring(data.IndexOf("[") + 1);

                    // Loops through all the fields for this column.
                    do
                    {
                        if (data.IndexOf(",") != -1 && data.IndexOf(",") < data.IndexOf("]"))
                        {
                            col.AddField(data.Substring(0, data.IndexOf(",")));
                            data = data.Substring(data.IndexOf(",") + 1); // There's a 'next' field
                        }
                        else
                        {
                            col.AddField(data.Substring(0, data.IndexOf("]")));
                            data = data.Substring(data.IndexOf("]") + 1);
                            break;
                        }
                    } while (true);
                    tb.AddColumn(col);
                    if (data.Substring(0, 1).Equals(","))
                    {
                        data = data.Substring(1); // There's a 'next' column
                    }
                    else
                    {
                        data = data.Substring(1);
                        break;
                    }
                }
                db.AddTable(tb);
                if (data.Substring(0, 1).Equals(","))
                {
                    data = data.Substring(1); // There's a 'next' table
                }
                else
                {
                    break;
                }
            }
            return db;
        }
        public static string GetRandomColor()
        {
            string[] letters = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
            string color = "#";
            for (int i = 0; i < 6; i++)
            {
                color += letters[Tools.GetRandomNum(16)];
            }
            return color;
        }
    }
    public class SelectTable
    {
        private Table table;

        public SelectTable(string data, string id, string name)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>(); // Color matching
            table = new Table();
            table.SetId(id);
            table.SetName(name);
            table.columns = new List<Column>();
            // Loops through all the columns for this table.
            int columnCounter = 0;
            while (true)
            {
                columnCounter++;
                Column col = new Column();
                col.SetName(data.Substring(0, data.IndexOf(":")));
                col.SetId("T:" + name + "-C:" + columnCounter);
                // If column exists elsewhere, use same color.
                if (dic.ContainsKey(col.GetName()))
                {
                    col.SetColor(dic[col.GetName()]);
                }
                else
                {
                    string c = DatabaseBuilder.GetRandomColor();
                    dic.Add(col.GetName(), c);
                    col.SetColor(c);
                }
                col.fields = new List<string>();
                data = data.Substring(data.IndexOf("[") + 1);

                // Loops through all the fields for this column.
                do
                {
                    if (data.IndexOf(",") != -1 && data.IndexOf(",") < data.IndexOf("]"))
                    {
                        col.AddField(data.Substring(0, data.IndexOf(",")));
                        data = data.Substring(data.IndexOf(",") + 1); // There's a 'next' field
                    }
                    else
                    {
                        col.AddField(data.Substring(0, data.IndexOf("]")));
                        data = data.Substring(data.IndexOf("]") + 1);
                        break;
                    }
                } while (true);
                table.AddColumn(col);
                if (data.Substring(0, 1).Equals(","))
                {
                    data = data.Substring(1); // There's a 'next' column
                }
                else
                {
                    data = data.Substring(1);
                    break;
                }
            }
        }

        public Table GetTable()
        {
            return table;
        }
    }
    public class Database
    {
        private string name;
        public List<Table> tables;

        public void AddTable(Table t)
        {
            tables.Add(t);
        }
        public void SetName(string n)
        {
            name = n;
        }
        public string GetName()
        {
            return name;
        }
    }
    public class Table
    {
        private string id;
        private string name;
        public List<Column> columns;

        public void AddColumn(Column c)
        {
            columns.Add(c);
        }
        public void SetName(string n)
        {
            name = n;
        }
        public string GetName()
        {
            return name;
        }
        public void SetId(string i)
        {
            id = i;
        }
        public string GetId()
        {
            return id;
        }
    }
    public class Column
    {
        private string id;
        private string name;
        public List<string> fields;
        private string color;
        private string type = "varchar";

        public void AddField(string f)
        {
            fields.Add(f);
        }
        public void SetName(string n)
        {
            name = n;
        }
        public string GetName()
        {
            return name;
        }
        public void SetId(string i)
        {
            id = i;
        }
        public string GetId()
        {
            return id;
        }
        public string GetColor()
        {
            return color;
        }
        public void SetColor(string c)
        {
            color = c;
        }
        public string GetType()
        {
            return type;
        }
        public void SetType(string t)
        {
            type = t;
        }
    }
    // Miscellaneous functions used across the game.
    public static class Tools
    {
        private static System.Random rando = new System.Random();
        public static int GetRandomNum(int number)
        {
            return rando.Next(number);
        }
    }
    public static class VariableColorTable
    {
        private static Dictionary<string, string> dic = new Dictionary<string, string>()
        {
            { "varchar", "#000000" },
            { "bigint", "#888800" },
            { "longtext", "#880088" },
            { "datetime", "#EEEE00" },
            { "int", "#EE00EE" },
            { "decimal", "#FF0000" },
            { "double", "#000055" }
        };
        public static string GetVariableColor(string type)
        {
            if (dic.ContainsKey(type))
            {
                return dic[type];
            }
            else return "#FFFFFF";
        }
    }
}
