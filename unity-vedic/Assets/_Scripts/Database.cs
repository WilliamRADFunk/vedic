﻿using UnityEngine;
using System.Collections.Generic;

namespace Database
{
    public static class VedicDatabase
    {
        public static DatabaseBuilder.Database db;
    }
    public class DatabaseBuilder
    {
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
        }
        public struct Column
        {
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
            public string GetColor()
            {
                return color;
            }
            public void SetColor(string c)
            {
                color = c;
            }
        }
        public static Database ConstructDB(string name, string data)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>(); // Color matching
            Database db = new Database();
            db.SetName(name);
            db.tables = new List<Table>();
            data = data.Substring(10);

            // Loops through all the tables for this database.
            while (true)
            {
                Table tb = new Table();
                tb.SetName( data.Substring(0, data.IndexOf(":")) );
                tb.columns = new List<Column>();
                data = data.Substring(data.IndexOf("{") + 1);

                // Loops through all the columns for this table.
                while (true)
                {
                    Column col = new Column();
                    col.SetName( data.Substring(0, data.IndexOf(":")) );
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
            // Print out in the debugger for the colors to each column
            for (int i = 0; i < db.tables.Count; i++)
            {
                for (int j = 0; j < db.tables[i].columns.Count; j++)
                {
                    //Column oldCol = db.tables[i].columns[j];
                    //oldCol.color = getRandomColor();
                    //db.tables[i].columns[j] = oldCol;
                    Debug.Log(db.tables[i].GetName() + "--->" + db.tables[i].columns[j].GetName() + "--->" + db.tables[i].columns[j].GetColor() + "\n");
                }
            }

            return db;
        }
        public static Table ConstructTable(string name)
        {
            Table tab = new Table();
            tab.SetName(name);
            return tab;
        }
        public static Column ConstructColumn(string name)
        {
            Column col = new Column();
            col.SetName(name);
            return col;
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
