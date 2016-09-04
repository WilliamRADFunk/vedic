using UnityEngine;
using System.Collections.Generic;

namespace Database
{
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
