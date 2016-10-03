using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Text;

public class Vid_AlterQuery : Vid_Query
{
    enum AlterState
    {
        INSERT,
        DELETE
    }
    AlterState state = AlterState.INSERT;
    Vid_DB_Col col;

    //getters

    //setters
    public void setState_Insert()
    {
        state = AlterState.INSERT;
    }
    public void setState_Delete()
    {
        state = AlterState.INSERT;
    }
    public void setCol(Vid_DB_Col col)
    {
        this.col = col;
    }


}
