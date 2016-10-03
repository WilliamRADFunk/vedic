using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Text;

public abstract class Vid_Expression : Vid_SequenceableObject {
    public StringBuilder expressionText;

    public abstract void startStringify();

}
