using UnityEngine;
using System.Collections;

public class Vid_DataFactory {

    public static Vid_Data CreateData(VidData_Type type)
    {
        switch (type)
        {
            case VidData_Type.BOOL:
                return new Vid_Data(type);
            case VidData_Type.CLASS:
                return new Vid_Data(type);
            case VidData_Type.IMPORT:
                return new Vid_Data(type);
            case VidData_Type.NUM:
                return new Vid_Data(type);
            case VidData_Type.DATABASE_COL:
                return new Vid_Data(type);
            case VidData_Type.DATABASE_TABLE:
                return new Vid_Data(type);
            default:
                return null;
        }
    }

    public static void setDatabase_ColData(Vid_Data data, string name)
    {
        if(data.getVidData_type() != VidData_Type.DATABASE_COL) { return; }
        data.setData(name);
    }

}
