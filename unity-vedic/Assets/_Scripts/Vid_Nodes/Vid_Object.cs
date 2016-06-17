using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class Vid_Object : MonoBehaviour, Stringifyable
{

    protected Vid_ObjectInputs inputs;
    protected Vid_Data output;

    public Vid_Object sequence;
    protected Vid_TokenFactory tokenFactory;

    public Vid_Object()
    {
        tokenFactory = Vid_TokenFactory.getInstance();
    }

    public void Awake()
    {
        tokenFactory = Vid_TokenFactory.getInstance();
    }

    public abstract void stringify();

    public abstract bool addInput(Vid_Data data);
    public Vid_Data getOutput() { return output; }
    public Vid_Object getSequence() { return sequence; }

    public void setSequence(Vid_Object sequence) { this.sequence = sequence; }

    
}
