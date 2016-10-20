using UnityEngine;
using System.Collections.Generic;

public class OneButton : NodeButton {

    LineRenderer lr;
    public OutputButton outButton;
    public Vid_Object vidObj;

    bool drawline = false;
    OutputButton output;
    public LineRenderer lineRender;
    Renderer r;

    public override void buttonPressed() {
        if (!ct.currentSeleted()) {
            outButton.buttonPressed();
        }
        else {
            if (ct.getOutputButton().Equals(outButton)) {
                ct.resetTool();
                return;
            }
            else {

            }
            output = ct.getOutputButton();
            Vid_Object outputObj = output.vid_obj;
            output.setIsUse(false);
            bool b = vidObj.addInput(outputObj);
            //if (b) {
            //    used = true;
            //    drawline = true;
            //}
            ct.resetTool();
        }
    }

    void Update() {
        if (drawline) {
            if (!lineRender.enabled) {
                lineRender.enabled = true;
            }
            Vector3[] points = new Vector3[2];
            points[0] = output.transform.position;
            points[1] = this.transform.position;
            lineRender.SetPositions(points);
        }
    }

    private void transferData() {
      //  output = ct.getOutputButton();
      //  Vid_Object outputObj = output.vid_obj;
      ////  ct.setInputButton(this);
      //  output.setIsUse(false);
      //  bool b = vidObj.addInput(outputObj, argumentIndex);
      //  if (b) {
      //      used = true;
      //      drawline = true;
      //  }
      //  ct.resetTool();
    }
}
