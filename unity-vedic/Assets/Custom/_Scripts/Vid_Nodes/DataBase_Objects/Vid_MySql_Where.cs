using System.Text;
using UnityEngine;

public class Vid_MySql_Where : Vid_Object
{
    public Con_Where controller;
    public bool isEXISTS = false;
    bool inFlag = false;


    public Vid_MySql_Where() {
        output_dataType = VidData_Type.DATABASE_CLAUSE;
    }

    public override void Awake() {
        base.Awake();
        inputs = new Vid_ObjectInputs(2);
        acceptableInputs = new VidData_Type[4];
            acceptableInputs[0] = VidData_Type.WHERE_STATMENT;
            acceptableInputs[1] = VidData_Type.DATABASE_CLAUSE;
            acceptableInputs[2] = VidData_Type.STRING;
            acceptableInputs[3] = VidData_Type.Q_SELECT;
    }

    public override string ToString() 
    {
        bool flag = false;
        StringBuilder sb = new StringBuilder();
        sb.Append("WHERE ");
        if (inputs.getInput_atIndex(0) != null) {
            if(inputs.getInput_atIndex(0).output_dataType == VidData_Type.DATABASE_COL) {
                sb.Append(inputs.getInput_atIndex(0).ToString());
                if (inFlag) {
                    sb.Append(" IN \n");
                    flag = true;
                }
            }
            else {
                sb.AppendLine("(");
                TabTool.incromentCount();
                sb.Append(TabTool.TabCount() + inputs.getInput_atIndex(0).ToString());
                TabTool.deccromentCount();
                sb.AppendLine(TabTool.TabCount() + ")");
            }
        }
        else {
            if (isEXISTS) {
                sb.AppendLine("EXISTS");
            }
            else {
                sb.AppendLine("NOT EXISTS");
            }
        }
        if(inputs.getInput_atIndex(1) != null ) {
            sb.AppendLine(TabTool.TabCount() + "(");
            TabTool.incromentCount();
            sb.AppendLine(TabTool.TabCount() + inputs.getInput_atIndex(1).ToString());
            TabTool.deccromentCount();
            sb.AppendLine(TabTool.TabCount() + ")");
        }
        return sb.ToString();
    }
    public override bool removeInput(int argumentIndex) {
        if(argumentIndex == 0) {
            inFlag = false;
            base.removeInput(1);
            if(controller != null) {
                controller.ChangeText();
            }
        }
        return base.removeInput(argumentIndex);
    }

    public override bool addInput(Vid_Object obj) {
        bool b;
        Debug.Log("Here:2");
        switch (obj.output_dataType) {
            case VidData_Type.WHERE_STATMENT:
                b = base.addInput(obj, 0);
                return b;
            case VidData_Type.DATABASE_COL:
                if (obj.output_dataType == VidData_Type.DATABASE_COL) {
                        inFlag = true;
                }
                b = base.addInput(obj, 0);
                return b;
            case VidData_Type.LIST:
                inFlag = true;
                b = base.addInput(obj, 1);
                return b;
            case VidData_Type.Q_SELECT:
                inFlag = true;
                b = base.addInput(obj, 1);
                return b;
            case VidData_Type.DATABASE_CLAUSE:
                inFlag = false;
                b = base.addInput(obj, 1);
                return b;
        }
        return false;
    }

    public override bool addInput(Vid_Object obj, int argumentIndex) {
        Debug.Log("Here:2");
        switch (argumentIndex) {
            case 0:
                if (obj.output_dataType == VidData_Type.WHERE_STATMENT
                    || obj.output_dataType == VidData_Type.DATABASE_COL) {
                    if (obj.output_dataType == VidData_Type.DATABASE_COL) {
                        inFlag = true;
                    }
                    else {
                        inFlag = false;
                    } 
                    
                    bool b = base.addInput(obj, 0);
                    Debug.Log("Here");
                    if (controller != null) {
                        controller.ChangeText();
                    }
                    return b;
                }
                else {
                    return false;
                }
            case 1:
                if (obj.output_dataType == VidData_Type.LIST) {
                    inFlag = true;
                    bool b = base.addInput(obj, 1);
                    if (controller != null) {
                        controller.ChangeText();
                    }
                    return b;
                }
                else if (obj.output_dataType == VidData_Type.Q_SELECT) {
                    inFlag = true;
                    bool b = base.addInput(obj, 1);
                    if (controller != null) {
                        controller.ChangeText();
                    }
                    return b;
                }
                else if (obj.output_dataType == VidData_Type.DATABASE_CLAUSE) {
                    inFlag = false;
                    bool b = base.addInput(obj, 1);
                    if (controller != null) {
                        controller.ChangeText();
                    }
                    return b;
                }
                else {
                    if (controller != null) {
                        controller.ChangeText();
                    }
                    return false;
                }
        }
        return false;
    }

    /*Helper Functions*/
    public override int AcceptedInputIndex(VidData_Type t) {
        switch (t) {
            case VidData_Type.BOOL:
            case VidData_Type.DATABASE_COL:
            case VidData_Type.WHERE_STATMENT:
                return 0;
            case VidData_Type.LIST:
            case VidData_Type.Q_SELECT:
            case VidData_Type.DATABASE_CLAUSE:
            case VidData_Type.STRING:
                return 1;
        }
        return -1;
    }

    public bool IsInFlag() {
        return inFlag;
    }
    public void setInFlag(bool b) {
        inFlag = b;
    }
}
