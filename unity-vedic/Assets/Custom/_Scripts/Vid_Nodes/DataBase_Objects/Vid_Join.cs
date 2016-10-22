using UnityEngine;
using System.Collections;
using System.Text;

public class Vid_Join : Vid_Object {

    public enum JoinType {
        INNER,
        OUTER,
        LEFT,
        RIGHT,
        NATURAL
    }
    JoinType jointype = JoinType.INNER;

    public Vid_Join() {
        output_dataType = VidData_Type.DATABASE_CALUSE;
    }

    public override void Awake() {
        base.Awake();
        inputs = new Vid_ObjectInputs(3);
        acceptableInputs = new VidData_Type[3];
        acceptableInputs[0] = VidData_Type.DATABASE_TABLE;
        acceptableInputs[1] = VidData_Type.ASSINMENT;
        acceptableInputs[2] = VidData_Type.DATABASE_CALUSE;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        switch (jointype) {
            case JoinType.INNER:
                sb.Append("INNER JOIN ");
                break;
            case JoinType.OUTER:
                sb.Append("OUTTER JOIN");
                break;
            case JoinType.LEFT:
                sb.Append("LEFT JOIN");
                break;
            case JoinType.RIGHT:
                sb.Append("RIGHT JOIN");
                break;
            case JoinType.NATURAL:
                sb.Append("NATURAL JOIN");
                break;
        }
        if (inputs.getInput_atIndex(0) != null) {
            sb.AppendLine(inputs.getInput_atIndex(0).ToString());
        }

        if(inputs.getInput_atIndex(1) != null) {
            sb.AppendLine("ON " + inputs.getInput_atIndex(1).ToString());
        }

        if (inputs.getInput_atIndex(2) != null) {
            sb.AppendLine(inputs.getInput_atIndex(2).ToString());
        }

        return sb.ToString();
    }
    public override bool removeInput(int argumentIndex) {
        if (argumentIndex == 0) {
            base.removeInput(1);
        }
        return base.removeInput(argumentIndex);
    }

    public override bool addInput(Vid_Object obj) {
        bool b;
        switch (obj.output_dataType) {
            case VidData_Type.DATABASE_TABLE:
                b = base.addInput(obj, 0);
                return b;
            case VidData_Type.ASSINMENT:
                b = base.addInput(obj, 1);
                return b;
            case VidData_Type.DATABASE_CALUSE:
                b = base.addInput(obj, 2);
                return b;
        }
        return false;
    }

    public override bool addInput(Vid_Object obj, int argumentIndex) {
        switch (argumentIndex) {
            case 0:
                if (obj.output_dataType == VidData_Type.BOOL
                    || obj.output_dataType == VidData_Type.DATABASE_COL) {
                    bool b = base.addInput(obj, 0);
                    return b;
                }
                else {
                    return false;
                }
            case 1:
                if (obj.output_dataType == VidData_Type.LIST) {
                    bool b = base.addInput(obj, 1);
                    return b;
                }
                else if (obj.output_dataType == VidData_Type.Q_SELECT) {
                    bool b = base.addInput(obj, 1);
                    return b;
                }
                else if (obj.output_dataType == VidData_Type.DATABASE_CALUSE) {
                    bool b = base.addInput(obj, 1);
                    return b;
                }
                else if (obj.output_dataType == VidData_Type.STRING) {
                    bool b = base.addInput(obj, 1);
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
            case VidData_Type.DATABASE_CALUSE:
                return 2;
        }
        return -1;
    }
}
