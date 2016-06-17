using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System;

public class Test_WrittingFile : MonoBehaviour {
    private Vid_TokenFactory tokenFactory = Vid_TokenFactory.getInstance();

    public string path = @"C:\Users\James\Desktop\Function_Test.txt";

    // Use this for initialization
    void Awake () {

        Vid_Number test = new Vid_Number();
        Vid_BasicCondition condition = new Vid_BasicCondition();
        Vid_Branch branch = new Vid_Branch();
        Vid_NumVar vid_numVar = new Vid_NumVar("x");
        Vid_ReturnBool returnBool = new Vid_ReturnBool();

        test.setData("100");

        Vid_Function vid_function = new Vid_Function(1);
        vid_function.addParameter(vid_numVar.getOutput(), vid_numVar.varName);
        vid_function.setName("testScore");
        vid_function.toggleVid_Prefix_return();
        vid_function.setSequence(condition);

        condition.addInput(vid_function.getParameter_atIndex(0));
        condition.addInput(test.getOutput());
        condition.setSequence(branch);

        branch.addInput(condition.getOutput());
        //branch.setSequence(returnBool);
        //branch.setSequence2(returnBool);

        returnBool.addInput(condition.getOutput());


        vid_function.stringify();
        File.WriteAllText(path, tokenFactory.getStringBuilder().ToString());
    }

}
