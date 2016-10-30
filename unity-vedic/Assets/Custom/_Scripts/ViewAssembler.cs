using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using DatabaseUtilities;

public static class ViewAssembler {

    static string prefabDirectory = "Prefab/";

    static GameObject columnPrefab;
    static GameObject tablePrefab;
    static GameObject harnessPrefab;

	// Use this for initialization
    static ViewAssembler()
    {
        columnPrefab = Resources.Load<GameObject>(prefabDirectory + "column");
        tablePrefab = Resources.Load<GameObject>(prefabDirectory + "table");
        harnessPrefab = Resources.Load<GameObject>(prefabDirectory + "harness");
    }
    
    private static GameObject Generate(int type)
    {
        switch (type)
        {
            case (int)View_Type.Column:
                    return GameObject.Instantiate(columnPrefab);
    
            case (int)View_Type.Table:
                return GameObject.Instantiate(tablePrefab);

            case (int)View_Type.Harness:
                return GameObject.Instantiate(harnessPrefab);
            default:
                Debug.Log("ERROR: Input for ViewAssemlber failed to trigger object generation.");
                return null;
        }
    }

    public static GameObject GenerateViewObject(Database database, bool pod)
    {
        GameObject currentHarness = Generate((int)View_Type.Harness);
        Transform harnessTransform =  currentHarness.transform;

        List<DatabaseUtilities.Table> tableInfo = database.tables;
        int tableCount = tableInfo.Count;

        GameObject[] tables = new GameObject[tableCount];

        for(int i = 0; i < tableCount; i++)
        {
            tables[i] = GenerateTableObj(tableInfo[i], harnessTransform);
        }

        currentHarness.GetComponent<TableHarness>().Initialize(tables, pod);

        currentHarness.transform.localPosition = new Vector3(0, 0, 0);
        return currentHarness;
    }

    private static GameObject GenerateTableObj(DatabaseUtilities.Table table, Transform harness)
    {
        GameObject currentTable = Generate((int)View_Type.Table);
        Transform tableTransform = currentTable.transform;

        List<DatabaseUtilities.Column> columnInfo = table.columns;
        int columnCount = columnInfo.Count;

        GameObject[] cols = new GameObject[columnCount];

        for(int i = 0; i < columnCount; i++)
        {
            cols[i] = GenerateColumnObj(columnInfo[i], tableTransform, i);
        }

        currentTable.GetComponent<Table>().initialization(table.GetName(), cols, harness);
        return currentTable;
    }

    private static GameObject GenerateColumnObj(DatabaseUtilities.Column col, Transform parent, int key)
    {
        GameObject curCol =  Generate((int)View_Type.Column);
        curCol.GetComponent<Column>().Initialize(key, parent, col.GetName(), col.GetColor());

        return curCol;
    }
}
