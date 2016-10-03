using System;
using System.Text;

public class Vid_DB_Col : Vid_Object {

    public enum ColState {
        NAME,
        EXPRESSION,
        DATA,
    }
    public ColState colMode = ColState.NAME;
    public MySql_colTypes type = MySql_colTypes.MYSQL_INT;

    public string colName;
    public string cellName = "defaultNAME";
    public string asName = "defaultNAME" ;

    private bool isSetable = false;
    public bool notNull = false;
    public bool asFlag = false;
    public int charvar_Number = 1;


    public Vid_DB_Col() {
        output_dataType = VidData_Type.DATABASE_COL;
    }

    public override void Awake() 
    {
        inputs = new Vid_ObjectInputs(1);
        acceptableInputs = new VidData_Type[1];
           acceptableInputs[0] = VidData_Type.DATABASE_TABLE;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        Vid_Object obj = inputs.getInput_atIndex(0);
        switch (colMode) {
            case ColState.NAME:
                if (asFlag) {
                    if(obj != null) {
                        return obj.ToString() + "." + colName + " As" + asName;
                    }
                    else {
                        return colName + " As " + asName;
                    }
                }
                else {
                    if (obj != null) {
                        return obj.ToString() + "." + colName;
                    }
                    else {
                        return colName;
                    }
                }
            case ColState.EXPRESSION:
                if (obj != null) {
                    return obj.ToString() + "." + colName + " = " + cellName;
                }
                else {
                    return colName + " = " + cellName;
                }
            case ColState.DATA:
                switch (type) {
                    case MySql_colTypes.MYSQL_INT:
                        sb.Append(colName + " int ");
                        if (notNull) {
                            sb.Append("NOT NULL");
                        }
                        break;
                    case MySql_colTypes.MYSQL_FLOAT:
                        sb.Append(colName + " float ");
                        if (notNull) {
                            sb.Append("NOT NULL");
                        }
                        break;
                    case MySql_colTypes.MYSQL_DOUBLE:
                        sb.Append(colName + " double ");
                        if (notNull) {
                            sb.Append("NOT NULL");
                        }
                        break;
                    case MySql_colTypes.MYSQL_TIMESTAMP:
                        sb.Append(colName + " TIMESTAMP  ");
                        if (notNull) {
                            sb.Append("NOT NULL");
                        }
                        break;
                    case MySql_colTypes.MYSQL_CHAR:
                        sb.Append(colName + " VARCHAR(" + charvar_Number + ")");
                        if (notNull) {
                            sb.Append("NOT NULL");
                        }
                        break;
                    case MySql_colTypes.MYSQL_BLOB:
                        sb.Append(colName + " BLOB ");
                        if (notNull) {
                            sb.Append("NOT NULL");
                        }
                        break;
                    case MySql_colTypes.MYSQL_ENUM:
                        break;
                }
                return sb.ToString();
        }
        return "";
    }

    public static explicit operator Vid_DB_Col(string v) {
        throw new NotImplementedException();
    }

    public override bool addInput(Vid_Object obj, int argumentIndex) {
        if (obj.output_dataType == VidData_Type.DATABASE_TABLE) {
            return base.addInput(obj, argumentIndex);
        }
        return false;
    }

    /*Getters*/
    public bool isNotNull(){ return notNull; }
    public int getCarVarNumber() { return charvar_Number; }
    public bool getSetable() { return isSetable; }

    /*Setters*/
    public void set_NotNull(bool b){this.notNull = b; }
    public void setCarVarNumber(int i) { this.charvar_Number = i; }
    public void setSetable(bool b) {
        this.isSetable = b;
    }
}
