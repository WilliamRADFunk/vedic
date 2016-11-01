using UnityEngine;
using System.Collections;

public class InputButton : NodeButton {

    public int argumentIndex;

    public Vid_Object vidObj;

    bool used = false;
    [HideInInspector] public bool drawline = false;
    public OutputButton output;
    public LineRenderer lineRender;
    Renderer r;

    public override void buttonPressed()
    {
        if (used && ct.getOutputButton() != null) {
            if (ct.getOutputButton().inButton.Equals(this)) {
                ct.getOutputButton().setIsUse(false);
                lineRender.enabled = false;
                drawline = false;

                vidObj.removeInput(argumentIndex);
                
                output = null;
                used = false;
                ct.resetTool();
            }
        }

        else if (used && ct.getOutputButton() == null)
        {
            lineRender.enabled = false;
            drawline = false;

            vidObj.removeInput(argumentIndex);

            output = null;
            used = false;
        }
        else {
            if ((ct.getInputButton() == null) && (ct.getOutputButton() != null))
            {
                transferData();
            }
        }
    }

   void Update()
    {
        if(output == null) {
            lineRender.enabled = false;
            return;
        }
        if (drawline)
        {
            if (!lineRender.enabled)
            {
                lineRender.enabled = true;
            }
            Vector3[] points = new Vector3[2];
            points[0] = output.transform.position;
            points[1] = this.transform.position;
            lineRender.SetPositions(points);
        }
    }

   private void transferData()
   {
        output = ct.getOutputButton();
        Vid_Object outputObj = output.vid_obj;
        ct.setInputButton(this);
        output.setIsUse(false);
        bool b = vidObj.addInput(outputObj, argumentIndex);
        if (b) {
            used = true;
            drawline = true;
        }
        output.inButton = this;
        ct.resetTool();
    }
}
