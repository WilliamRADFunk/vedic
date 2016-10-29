using UnityEngine;
using System.Collections;
using System.Text;

public class Vid_ColFormater : Vid_Object {

    public bool notNull = false;
    public bool defaultValue = false;
    public bool isUNIQUE = false;
    public bool doAutoIncrement = false;

    public Vid_ColFormater() {
        output_dataType = VidData_Type.DATABASE_COL;
    }

    public override void Awake() {
        inputs = new Vid_ObjectInputs(2);
        acceptableInputs = new VidData_Type[2];
            acceptableInputs[0] = VidData_Type.DATABASE_COL;
            acceptableInputs[1] = VidData_Type.NUM;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        Vid_DB_Col col = (Vid_DB_Col)inputs.getInput_atIndex(0);
        if(col != null) {
            switch (col.type) {
                case MySql_colTypes.MYSQL_INT:
                    sb.Append(TabTool.TabCount() + col.ToString() + " int ");
                    if (notNull) {
                        sb.Append("NOT NULL ");
                    }
                    if (defaultValue) {
                        sb.Append("DEFAULT ");
                        if(inputs.getInput_atIndex(1) != null) {
                            sb.Append(inputs.getInput_atIndex(1).ToString());
                        }
                    }
                    else if (doAutoIncrement) {
                        sb.Append("AUTO_INCREMENT ");
                    }
                    break;
                case MySql_colTypes.MYSQL_FLOAT:
                    sb.Append(TabTool.TabCount() + col.ToString() + " float ");
                    if (notNull) {
                        sb.Append("NOT NULL ");
                    }
                    if (defaultValue) {
                        sb.Append("DEFAULT ");
                        if (inputs.getInput_atIndex(1) != null) {
                            sb.Append(inputs.getInput_atIndex(1).ToString());
                        }
                    }
                    else if (doAutoIncrement) {
                        sb.Append("AUTO_INCREMENT ");
                    }
                    break;
                case MySql_colTypes.MYSQL_DOUBLE:
                    sb.Append(TabTool.TabCount() + col.ToString() + " double ");
                    if (notNull) {
                        sb.Append("NOT NULL ");
                    }
                    if (defaultValue) {
                        sb.Append("DEFAULT ");
                        if (inputs.getInput_atIndex(1) != null) {
                            sb.Append(inputs.getInput_atIndex(1).ToString());
                        }
                    }
                    else if (doAutoIncrement) {
                        sb.Append("AUTO_INCREMENT ");
                    }
                    break;
                case MySql_colTypes.MYSQL_VARCHAR:
                    sb.Append(TabTool.TabCount() + col.ToString() + " varchar(255) ");
                    if (notNull) {
                        sb.Append("NOT NULL ");
                    }
                    if (defaultValue) {
                        sb.Append("DEFAULT ");
                    }
                    break;
                case MySql_colTypes.MYSQL_BLOB:
                    sb.Append(TabTool.TabCount() + col.ToString() + " BLOB ");
                    break;
                case MySql_colTypes.MYSQL_TIMESTAMP:
                    sb.Append(TabTool.TabCount() + col.ToString() + " timestamp ");
                    break;
            }
        }
        return sb.ToString();
    }


    public override bool addInput(Vid_Object obj) {
        if (obj.output_dataType == VidData_Type.DATABASE_COL) {
            return base.addInput(obj, 0);
        }
        if (obj.output_dataType == VidData_Type.NUM) {
            return base.addInput(obj, 1);
        }
        return false;
    }
    public override bool addInput(Vid_Object obj, int argumentIndex) {
        if (obj.output_dataType == VidData_Type.DATABASE_COL) {
            return base.addInput(obj, 0);
        }
        if (obj.output_dataType == VidData_Type.NUM) {
            return base.addInput(obj, 1);
        }
        return false;
    }

    public override int AcceptedInputIndex(VidData_Type t) {
        switch (t) {
            case VidData_Type.DATABASE_COL:
                return 0;
            case VidData_Type.NUM:
                return 1;
        }
        return -1;
    }

}
