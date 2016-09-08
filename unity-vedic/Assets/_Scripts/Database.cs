using UnityEngine;
using System.Collections.Generic;

namespace DatabaseUtilities
{
    public enum View_Type { Harness, Table, Column };

    public static class VedicDatabase
    {
        public static Database db;
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
                    if (dic.ContainsKey(col.GetName()))
                    {
                        col.SetColor(dic[col.GetName()]);
                    }
                    else
                    {
                        string c = getRandomColor();
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
        public static string getRandomColor()
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
    public struct Database
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
    public struct Table
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
    public struct Column
    {
        private string id;
        private string name;
        public List<string> fields;
        private string color;

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
}
