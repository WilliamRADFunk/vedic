using UnityEngine;
using System.Collections;
using System.Text;

public class Vid_OrderBy : Vid_MultiInput {

    public bool[] isDesc;

    public Vid_OrderBy() {
        output_dataType = VidData_Type.DATABASE_CALUSE;
    }
    public override void Awake() {
        base.Awake();
        inputs = new Vid_ObjectInputs(inputSize);
        acceptableInputs = new VidData_Type[1];
            acceptableInputs[0] = VidData_Type.DATABASE_COL;
        isDesc = new bool[1];
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder("ORDER BY ");
        for (int i = 0; i < inputs.getSize(); i++) {
            Vid_DB_Col obj = (Vid_DB_Col)inputs.getInput_atIndex(i);
            if (obj != null) {
                sb.Append(obj.colName);
                if (isDesc[i]) {
                    sb.Append(" DESC");
                }
                if (i < inputs.getSize() - 1) {
                    sb.Append(", ");
                }
            }
        }
        return sb.ToString();
    }

    public override bool addInput(Vid_Object obj, int index) {
        if (obj.output_dataType == VidData_Type.DATABASE_COL) {
            Vid_DB_Col colnode = (Vid_DB_Col)obj;
            bool b = inputs.setInput_atIndex(obj, index);
            return b;
        }
        return false;
    }

    public override void incromentInputs() {
        inputSize++;
        Vid_ObjectInputs newInputs = new Vid_ObjectInputs(inputSize);
        bool[] newIsDesc = new bool[inputSize];
        for (int i = 0; i < inputSize - 1; i++) {
            newInputs.setInput_atIndex(inputs.getInput_atIndex(i), i);
            newIsDesc[i] = isDesc[i];
        }                  
        inputs = newInputs;
        isDesc = newIsDesc;
    }
    public override void decromentInputs() {
        if (inputSize > 1) {
            inputSize--;
        }
        Vid_ObjectInputs newInputs = new Vid_ObjectInputs(inputSize);
        bool[] newIsDesc = new bool[inputSize];
        for (int i = 0; i < inputSize; i++) {
            newInputs.setInput_atIndex(inputs.getInput_atIndex(i), i);
            newIsDesc[i] = isDesc[i];
        }
        inputs = newInputs;
        isDesc = newIsDesc;
    }
}
