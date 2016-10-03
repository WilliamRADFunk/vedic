using UnityEngine;
using System.Collections;

public class FunctionTool  {
    private static FunctionTool instance;
    Vid_Function currentFunction;
    
    private FunctionTool() {}

    public static FunctionTool getInstance() {
        if(instance == null) {
            instance = new FunctionTool();
        }
        return instance;
    }

    public bool checkReturnType(Vid_Return r) {
        if(currentFunction == null) { return false; }
        else {
            return (currentFunction.prefix_returnVal == r.returnType);
        }
    }

    public Vid_Function getCurrentFunction() {
        return currentFunction;
    }
    public void setCurrentFunction(Vid_Function f) {
        this.currentFunction = f;
    }
}
