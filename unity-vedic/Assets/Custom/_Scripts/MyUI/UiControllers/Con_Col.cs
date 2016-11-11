using UnityEngine;
using UnityEngine.UI;
using DatabaseUtilities;
using System.Collections.Generic;

public class Con_Col : MonoBehaviour {

    public Vid_DB_Col vidObj;
    public Text dataText;
    public InputField inField_ColName;
    public Text colDataTypeText;
    Database db = null;
    List<string> colNames;
    int colIndex = 0;
    

    // Use this for initialization
    void Start () {
        db = VedicDatabase.db;
        if (vidObj != null && inField_ColName != null
            && dataText != null) {
            inField_ColName.text = vidObj.colName;
            dataText.text = vidObj.colName;
        }
        if (vidObj != null && colDataTypeText != null) {
            MySql_colTypes type = vidObj.type;
            switch (type) {
                case MySql_colTypes.MYSQL_INT:
                    colDataTypeText.text = "int";
                    break;
                case MySql_colTypes.MYSQL_FLOAT:
                    colDataTypeText.text = "float";
                    break;
                case MySql_colTypes.MYSQL_DOUBLE:
                    colDataTypeText.text = "double";
                    break;
                case MySql_colTypes.MYSQL_TIMESTAMP:
                    colDataTypeText.text = "timestamp";
                    break;
                case MySql_colTypes.MYSQL_VARCHAR:
                    colDataTypeText.text = "varchar";
                    break;
                case MySql_colTypes.MYSQL_BLOB:
                    colDataTypeText.text = "blob";
                    break;
                case MySql_colTypes.MYSQL_ENUM:
                    colDataTypeText.text = "enum";
                    break;
            }
        }
        if (db == null) {
            DatabaseUtilities.Table[] tables = db.tables.ToArray();
            foreach(DatabaseUtilities.Table t in tables) {
                if (vidObj.getTableName().Equals(t.GetName())) {
                    DatabaseUtilities.Column[] cols = t.columns.ToArray();
                    foreach (DatabaseUtilities.Column c in cols) {
                        colNames.Add(c.GetName());
                    }
                }
            }
        }
	}

    public void ToogleRight_colTypes() {
        MySql_colTypes type = vidObj.type;
        switch (type) {
            case MySql_colTypes.MYSQL_INT:
                vidObj.type = MySql_colTypes.MYSQL_FLOAT;
                colDataTypeText.text = "float";
                break;
            case MySql_colTypes.MYSQL_FLOAT:
                vidObj.type = MySql_colTypes.MYSQL_DOUBLE;
                colDataTypeText.text = "double";
                break;
            case MySql_colTypes.MYSQL_DOUBLE:
                vidObj.type = MySql_colTypes.MYSQL_TIMESTAMP;
                colDataTypeText.text = "timestamp";
                break;
            case MySql_colTypes.MYSQL_TIMESTAMP:
                vidObj.type = MySql_colTypes.MYSQL_VARCHAR;
                colDataTypeText.text = "varchar";
                break;
            case MySql_colTypes.MYSQL_VARCHAR:
                vidObj.type = MySql_colTypes.MYSQL_BLOB;
                colDataTypeText.text = "blob";
                break;
            case MySql_colTypes.MYSQL_BLOB:
                vidObj.type = MySql_colTypes.MYSQL_ENUM;
                colDataTypeText.text = "enum";
                break;
            case MySql_colTypes.MYSQL_ENUM:
                vidObj.type = MySql_colTypes.MYSQL_INT;
                colDataTypeText.text = "int";
                break;
        }
    }
    public void TooglLeft_colTypes() {
        MySql_colTypes type = vidObj.type;
        switch (type) {
            case MySql_colTypes.MYSQL_INT:
                vidObj.type = MySql_colTypes.MYSQL_ENUM;
                colDataTypeText.text = "enum";
                break;
            case MySql_colTypes.MYSQL_FLOAT:
                vidObj.type = MySql_colTypes.MYSQL_INT;
                colDataTypeText.text = "int";
                break;
            case MySql_colTypes.MYSQL_DOUBLE:
                vidObj.type = MySql_colTypes.MYSQL_FLOAT;
                colDataTypeText.text = "float";
                break;
            case MySql_colTypes.MYSQL_TIMESTAMP:
                vidObj.type = MySql_colTypes.MYSQL_DOUBLE;
                colDataTypeText.text = "double";
                break;
            case MySql_colTypes.MYSQL_VARCHAR:
                vidObj.type = MySql_colTypes.MYSQL_TIMESTAMP;
                colDataTypeText.text = "timestamp";
                break;
            case MySql_colTypes.MYSQL_BLOB:
                vidObj.type = MySql_colTypes.MYSQL_VARCHAR;
                colDataTypeText.text = "varchar";
                break;
            case MySql_colTypes.MYSQL_ENUM:
                vidObj.type = MySql_colTypes.MYSQL_BLOB;
                colDataTypeText.text = "blob";
                break;
        }
    }

    public void ToogleRight_ColNamex() {
        if (colIndex+1 < colNames.Count) {
            colIndex++;
        }
        else {
            colIndex = 0;
        }
        inField_ColName.text = colNames[colIndex];
        dataText.text = inField_ColName.text;
        vidObj.colName = inField_ColName.text;
    }
    public void ToogleLeft_ColName() {
        if (colIndex - 1 >= 0) {
            colIndex--;
        }
        else {
            colIndex = colNames.Count - 1;
        }
        inField_ColName.text = colNames[colIndex];
        dataText.text = inField_ColName.text;
        vidObj.colName = inField_ColName.text;
    }

    public void SetValue(InputField inField) {
        vidObj.colName = inField.text;
        dataText.text = inField.text;
    }
}
