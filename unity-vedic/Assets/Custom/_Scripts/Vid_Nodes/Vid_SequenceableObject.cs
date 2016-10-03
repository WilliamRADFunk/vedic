using UnityEngine;
using System.Collections;
using System.Text;

public abstract class Vid_SequenceableObject : Vid_Object, Stringifyable {
    public Vid_SequenceableObject[] sequence;

    public abstract void stringify(StringBuilder targetSting);

    public Vid_SequenceableObject getSequence(int index) { return sequence[index]; }
    public virtual void setSequence(Vid_SequenceableObject s, int index)
    {
        sequence[index] = s;
    }
}
