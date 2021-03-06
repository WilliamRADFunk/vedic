﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using DatabaseUtilities;

public static class ViewAssembler {

    static string prefabDirectory = "Prefab/";

    static GameObject columnPrefab;
    static GameObject tablePrefab;
    static GameObject harnessPrefab;
    static GameObject diskHarnessPrefab;
    static GameObject diskPrefab;

	// Use this for initialization
    static ViewAssembler()
    {
        columnPrefab = Resources.Load<GameObject>(prefabDirectory + "column");
        tablePrefab = Resources.Load<GameObject>(prefabDirectory + "table");
        harnessPrefab = Resources.Load<GameObject>(prefabDirectory + "harness");
        diskHarnessPrefab = Resources.Load<GameObject>(prefabDirectory + "DiskHarness");
        diskPrefab = Resources.Load<GameObject>(prefabDirectory + "Disk");
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

            case (int)View_Type.DiskHarness:
                return GameObject.Instantiate(diskHarnessPrefab);

            case (int)View_Type.Disk:
                return GameObject.Instantiate(diskPrefab);

            default:
                Debug.Log("ERROR: Input for ViewAssemlber failed to trigger object generation.");
                return null;
        }
    }

    public static GameObject GenerateViewObject(Database database, bool pod, bool analytic, int analyticType)
    {
        GameObject currentHarness = Generate((int)View_Type.Harness);
        Transform harnessTransform =  currentHarness.transform;

        List<DatabaseUtilities.Table> tableInfo = database.tables;
        int tableCount = tableInfo.Count;

        GameObject[] tables = new GameObject[tableCount];

        for(int i = 0; i < tableCount; i++)
        {
            tables[i] = GenerateTableObj(tableInfo[i], harnessTransform, analytic);
        }
        
        if(!analytic)
        {
            currentHarness.GetComponent<TableHarness>().Initialize(tables, pod);
        }
        else
        {
            currentHarness.GetComponent<TableHarness>().Initialize(tables, analytic, analyticType);
        }

        currentHarness.transform.localPosition = new Vector3(0, 0, 0);
        return currentHarness;
    }

    public static GameObject GenerateAnalyticObject(Database database, int analyticType)
    {
        //Make sure to generate the correct type of harness (disk harness)
        GameObject currentHarness = Generate((int)View_Type.DiskHarness);
        Transform harnessTransform = currentHarness.transform;

        List<DatabaseUtilities.Table> tableInfo = database.tables;
        int tableCount = tableInfo.Count;

        GameObject[] anaObjects = new GameObject[tableCount];

        if(analyticType == 1)
        {
            DatabaseUtilities.Table infoObject = tableInfo[0];
            List<DatabaseUtilities.Column> dataTypes = infoObject.columns;
            int dataTypeSize = dataTypes.Count;

            GameObject[] disks = new GameObject[dataTypeSize];

            for(int i = 0; i < dataTypeSize; i++)
            {
                disks[i] = GenerateDiskObj(dataTypes[i], harnessTransform);
            }

            currentHarness.GetComponent<DiskHarness>().Initialize(disks, dataTypes);
        }

        return currentHarness;
    }

    private static GameObject GenerateTableObj(DatabaseUtilities.Table table, Transform harness, bool aType)
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

        currentTable.GetComponent<Table>().initialization(table.GetName(), table.GetId(), cols, harness, aType);
        return currentTable;
    }

    private static GameObject GenerateColumnObj(DatabaseUtilities.Column col, Transform parent, int key)
    {
        GameObject curCol =  Generate((int)View_Type.Column);
        curCol.GetComponent<Column>().Initialize(key, parent, col.GetName(), col.GetId(), col.GetColor());

        return curCol;
    }

    private static GameObject GenerateDiskObj(DatabaseUtilities.Column cylinderInfo, Transform harness)
    {
        //Generate Disk object from factory.
        GameObject disk = Generate((int)View_Type.Disk);

        float scaleSize = float.Parse(cylinderInfo.fields[0]);
        string dataTypeName = cylinderInfo.GetName();
        string hexColor = cylinderInfo.GetColor();

        //Initialize the disk object...
        disk.GetComponent<Disk>().Initialize(harness, dataTypeName, hexColor, scaleSize);

        return disk;

    }
}
