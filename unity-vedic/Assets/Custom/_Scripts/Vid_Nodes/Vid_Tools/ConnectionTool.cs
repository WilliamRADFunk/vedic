using UnityEngine;
using System.Collections;

public class ConnectionTool  {

    private static ConnectionTool instance;
    private InputButton inputButton;
    private OutputButton outputButton;

    private ConnectionTool()
    {
    }

    public static ConnectionTool getInstance()
    {
        if (instance == null)
        {
            instance = new ConnectionTool();
        }
        return instance;
    }

    public bool currentSeleted()
    {
        return outputButton != null;
    }

    public InputButton getInputButton()
    {
        return inputButton;
    }
    public OutputButton getOutputButton()
    {
        return outputButton;
    }

    public void setOutputButton(OutputButton n)
    {
        outputButton = n;
    }
    public void setInputButton(InputButton c)
    {
        inputButton = c;
    }

    public void resetTool()
    {
        inputButton = null;
        outputButton = null;
    }
}
