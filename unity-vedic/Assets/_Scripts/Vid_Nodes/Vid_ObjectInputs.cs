using UnityEngine;
using System.Collections;

public class Vid_ObjectInputs : MonoBehaviour{
    public Vid_Data[] left;
    public int count;
    int maxsize;

    public Vid_ObjectInputs(int numOf_Inputs)
    {
        count = 0;
        maxsize = numOf_Inputs;
        left = new Vid_Data[numOf_Inputs];
    }

    public bool isInputFull()
    {
        if(left.Length == maxsize)
        {
            return true;
        }
        return false;
    }

    public Vid_Data getInput_atIndex(int index)
    {
        if(index > -1 && index < maxsize)
        {
            return left[index];
        }
        return null;
    }

    public bool setInput(Vid_Data data)
    {
        if(count < maxsize)
        {
            left[count] = data;
            count++;
            return true;
        }
        return false;
    }

}
