﻿using UnityEngine;

public abstract class Vid_Object : MonoBehaviour, Inputable
{
    [HideInInspector] public Vid_ObjectInputs inputs;
    protected VidData_Type[] acceptableInputs;
    public VidData_Type output_dataType;

    public virtual void Awake(){}

    /*Builder Functions*/
    public virtual void updateData() { }
    public virtual bool addInput(Vid_Object obj, int argumentIndex) {
        bool b = inputs.setInput_atIndex(obj, argumentIndex);
        return b;
    }

    public virtual bool addInput(Vid_Object obj) {
        bool b = false;
        return b;
    }
    public virtual bool removeInput( int argumentIndex) {
        if(inputs != null) {
            return inputs.removeInput_atIndex(argumentIndex);
            
        }
       return false;
    }

    public virtual int AcceptedInputIndex(VidData_Type t) {
        return -1;
    }

    /* Getters */
    public VidData_Type[] getAcceptableInputs() {
        return acceptableInputs;
    }

    public void destroyObj() {
        NodeSystemEssentials.uiMove_Tool.removeObjFromHolding(gameObject);
        GameObject.Destroy(this.gameObject);
    }


    public Vid_ObjectInputs GetInputs() {
        return inputs;
    }
}
