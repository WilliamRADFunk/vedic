using UnityEngine;
using UnityEngine.UI;
using DatabaseUtilities;
using System.Collections.Generic;


public class Con_Table : Con_Con {

    public Vid_DB_Table vidObj;
    public Text dataText;
    public InputField inField_ColName;
    Database db = null;
    List<string> tableNames;
    int tableIndex = 0;


    // Use this for initialization
    void Start () {
        base.Start();
        db = VedicDatabase.db;
        if (vidObj != null && inField_ColName != null
            && dataText != null) {
            inField_ColName.text = vidObj.tableName;
            dataText.text = vidObj.tableName;
        }
        if (db == null) {
            DatabaseUtilities.Table[] tables = db.tables.ToArray();
            foreach (DatabaseUtilities.Table t in tables) {
                tableNames.Add(t.GetName());
            }
        }
    }

    public void ToogleRight_TableName() {
        if (tableIndex + 1 < tableNames.Count) {
            tableIndex++;
        }
        else {
            tableIndex = 0;
        }
        inField_ColName.text = tableNames[tableIndex];
        dataText.text = inField_ColName.text;
        vidObj.tableName = inField_ColName.text;
    }
    public void ToogleLeft_TableName() {
        if (tableIndex - 1 >= 0) {
            tableIndex--;
        }
        else {
            tableIndex = tableNames.Count - 1;
        }
        inField_ColName.text = tableNames[tableIndex];
        dataText.text = inField_ColName.text;
        vidObj.tableName = inField_ColName.text;
    }

    public void SetValue(InputField inField) {
        vidObj.tableName = inField.text;
        dataText.text = inField.text;
    }
}
