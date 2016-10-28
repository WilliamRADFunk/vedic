using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Text;


public class Vid_UpdateQuery : Vid_Query
{
    public override void Awake() {
        base.Awake();
        base.output_dataType = VidData_Type.DATABASE_TABLE;
        inputs = new Vid_ObjectInputs(3);
        acceptableInputs = new VidData_Type[3];
            acceptableInputs[0] = VidData_Type.DATABASE_TABLE;
            acceptableInputs[1] = VidData_Type.ASSINMENT;
            acceptableInputs[2] = VidData_Type.DATABASE_CLAUSE;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        if (inputs.getInput_atIndex(0) == null) {
            sb.AppendLine("UPDATE error::NoTable SET ");
        }
        else {
            sb.AppendLine("UPDATE " + inputs.getInput_atIndex(0).ToString());
            if (inputs.getInput_atIndex(1) != null) {
                sb.AppendLine(TabTool.TabCount() + " SET " + inputs.getInput_atIndex(1).ToString() + " ");
            }
            if (inputs.getInput_atIndex(2) != null) {
                sb.AppendLine(TabTool.TabCount() + inputs.getInput_atIndex(2).ToString());
            }
        }
        return sb.ToString();
    }

    public override bool addInput(Vid_Object obj) {
        // Note: don't change, Table=0,COL=1,Where=2 need to be these value.  
        bool b = false;
        switch (obj.output_dataType) {
            case VidData_Type.DATABASE_TABLE:
                b = base.addInput(obj, 0);
                return b;
            case VidData_Type.ASSINMENT:
                b = base.addInput(obj, 1);
                return b;
            case VidData_Type.DATABASE_CLAUSE:
                b = base.addInput(obj, 2);
                return b;
        }
        return false;
    }


    public override bool addInput(Vid_Object obj, int argumentIndex) {
        // Note: don't change, Table=0,COL=1,Where=2 need to be these value.  
        switch (argumentIndex) {
            case 0:
                if (obj.output_dataType == VidData_Type.DATABASE_TABLE) {
                    bool b = base.addInput(obj, 0);
                    return b;
                }
                else {
                    return false;
                }
            case 1:
                if (obj.output_dataType == VidData_Type.ASSINMENT) {
                    bool b = base.addInput(obj, 1);
                    return b;
                }
                else {
                    return false;
                }
            case 2:
                if (obj.output_dataType == VidData_Type.DATABASE_CLAUSE) {
                    bool b = base.addInput(obj, 2);
                    return b;
                }
                else {
                    return false;
                }
        }
        return false;
    }


    /*Helper Functions*/
    public override int AcceptedInputIndex(VidData_Type t) {
        switch (t) {
            case VidData_Type.DATABASE_TABLE:
                return 0;
            case VidData_Type.ASSINMENT:
                return 1;
            case VidData_Type.DATABASE_CLAUSE:
                return 2;
        }
        return -1;
    }
}
