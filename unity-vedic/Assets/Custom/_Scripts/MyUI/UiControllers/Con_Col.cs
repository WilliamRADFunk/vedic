using UnityEngine;
using UnityEngine.UI;
using DatabaseUtilities;
using System.Collections.Generic;

public class Con_Col : MonoBehaviour {

    public Vid_DB_Col vidObj;
    public Text dataText;
    Database? db = null;
    List<string> colNames;

    // Use this for initialization
    void Start () {
	    if(vidObj != null && dataText != null) {
            dataText.text = vidObj.colName;
        }
        if(db == null) {
            DatabaseUtilities.Table[] tables = db.GetValueOrDefault().tables.ToArray();
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

    public void ToogleName() {
        
    }

	
}
