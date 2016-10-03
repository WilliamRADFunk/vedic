using UnityEngine;
using UnityEngine.UI;
using System;

public class Controller_Table : MonoBehaviour {

    public Vid_DB_Table node;

    public void setTableName(InputField t) {
        node.tableName = t.text;
    }
}
