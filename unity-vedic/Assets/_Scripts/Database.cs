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
            public string name;
            public List<Table> tables;

            public void AddTable(Table t)
            {
                tables.Add(t);
            }
        }
        public struct Table
        {
            public string name;
            public List<Column> columns;

            public void AddColumn(Column c)
            {
                columns.Add(c);
            }
        }
        public struct Column
        {
            public string name;
            public List<string> fields;
            public string color;

            public void AddField(string f)
            {
                fields.Add(f);
            }
        }
        public static Database ConstructDB(string name, string data)
        {
            Database db = new Database();
            db.name = name;
            db.tables = new List<Table>();

            data = data.Substring(10);
            while (true)
            {
                Table tb = new Table();
                tb.name = data.Substring(0, data.IndexOf(":"));
                tb.columns = new List<Column>();
                data = data.Substring(data.IndexOf("{") + 1);

                while (true)
                {
                    Column col = new Column();
                    col.name = data.Substring(0, data.IndexOf(":"));
                    col.fields = new List<string>();
                    data = data.Substring(data.IndexOf("[") + 1);

                    do
                    {
                        if (data.IndexOf(",") != -1 && data.IndexOf(",") < data.IndexOf("]"))
                        {
                            col.AddField(data.Substring(0, data.IndexOf(",")));
                            data = data.Substring(data.IndexOf(",") + 1);
                        }
                        else
                        {
                            col.AddField(data.Substring(0, data.IndexOf("]")));
                            data = data.Substring(data.IndexOf("]") + 1);
                            break;
                        }
                    } while (true);
                    tb.AddColumn(col);
                    if(data.Substring(0, 1).Equals(","))
                    {
                        data = data.Substring(1);
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
                    data = data.Substring(1);
                }
                else
                {
                    break;
                }
            }
            
            return db;
        }
        public static Table ConstructTable(string name)
        {
            Table tab = new Table();
            tab.name = name;
            return tab;
        }
        public static Column ConstructColumn(string name)
        {
            Column col = new Column();
            col.name = name;
            return col;
        }
    }
}
