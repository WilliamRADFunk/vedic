using UnityEngine;
using UnityEngine.UI;

public class TableController : MonoBehaviour {

    public Vid_DB_Table node;

    public void setTableName(Text t) {
        Debug.Log("Thisasasdds");
        node.tableName = t.text;
        Debug.Log(t.text+" : "+ node.tableName);
    }

    public void resetTableName(Text t) {
        t.text = node.tableName;
    }

}
