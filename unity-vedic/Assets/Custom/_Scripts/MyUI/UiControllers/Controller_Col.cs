using UnityEngine;
using UnityEngine.UI;
using System;

public class Controller_Col : MonoBehaviour {

    public Transform prefab;
    public Vid_DB_Col node;
    
    /*Setters*/
    public void setColName(InputField t) {
        node.colName = t.text;
    }
    public void setCellName(InputField t) {
        node.cellName = t.text;
    }
    public void setCharvar_Number(InputField t) {
        try {
            node.charvar_Number = int.Parse(t.text);
        }
        catch (FormatException e) { Debug.Log(e.ToString()); }
    }
    public void changeColMod(Dropdown d) {
        switch (d.value) {
            case 0:
                node.colMode = Vid_DB_Col.ColState.NAME;
                break;
            case 1:
                node.colMode = Vid_DB_Col.ColState.EXPRESSION;
                break;
            case 2:
                node.colMode = Vid_DB_Col.ColState.DATA;
                break;
        }
    }
    public void changeDataType(Dropdown d) {
        switch (d.value) {
            case 0:
                node.type = MySql_colTypes.MYSQL_INT;
                break;
            case 1:
                node.type = MySql_colTypes.MYSQL_FLOAT;
                break;
            case 2:
                node.type = MySql_colTypes.MYSQL_DOUBLE;
                break;
            case 3:
                node.type = MySql_colTypes.MYSQL_VARCHAR;
                break;
            case 4:
                node.type = MySql_colTypes.MYSQL_BLOB;
                break;
        }
    }
    public void setAsflag(Toggle t) {
        node.asFlag = t.isOn;
    }
    public void setNotNull(Toggle t) {
        node.notNull = t.isOn;
    }
    public void dublicateNode() {
        Instantiate(prefab, new Vector3(prefab.position.x, prefab.position.y+.2f, prefab.position.z), prefab.rotation);
    }
    /*Resetters*/
    public void resetTableName(Text t) {
        t.text = node.colName;
    }
    public void resetCellName(Text t) {
        t.text = node.cellName;
    }
}
