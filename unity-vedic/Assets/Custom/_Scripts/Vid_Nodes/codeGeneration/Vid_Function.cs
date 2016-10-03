using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Text;

public class Vid_Function : Vid_Expression
{
    public string functionName = "defaultName";
    public int numOfPrameters = 0;
    public Vid_Prefix.Vid_Common_Prefix prefix_head;
    public Vid_Prefix.Vid_returnValue prefix_returnVal;
    public Dictionary<string, Vid_Data> parameters = new Dictionary<string, Vid_Data>();

    Dictionary<Vid_Prefix, string> prefix;
    List<String> parameters_names;

    public override void Awake()
    {
        base.Awake();
        inputs = new Vid_ObjectInputs(numOfPrameters);
        parameters_names = new List<String>();
        prefix = new Dictionary<Vid_Prefix, string>();
        expressionText = new StringBuilder();
        FunctionTool.getInstance().setCurrentFunction(this);
    }

    public override void startStringify()
    {
        stringify(expressionText);
    }
    public override void stringify(StringBuilder targetString)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append( prefixCommon_String() + " " +
                             prefixReturn_String() + " " +
                             functionName + "(" + writeParameters() + ")"+ Environment.NewLine);
        sb.Append("{"+ Environment.NewLine);
        sb.Append("}");

        string text_outPut = sb.ToString();
        targetString.Append(sb.ToString());

        //if (base.sequence != null)
        //{
        //    base.sequence.stringify(targetString);
        //}
    }
    public override void setSequence(Vid_SequenceableObject s, int index) {
        base.setSequence(s, index);
    }
    /*Builder function*/
    public bool addParameter(Vid_Data data, String name)
    {
        if (data.getVidData_type() != VidData_Type.FUNCTION
                | data.getVidData_type() != VidData_Type.IMPORT)
        {
            if (!parameters.ContainsKey(name))
            {
                parameters.Add(name, data);
                parameters_names.Add(name);
                numOfPrameters++;
                inputs = new Vid_ObjectInputs(numOfPrameters);
            }
            return true;
        }
        return false;
    }

    /*Comand functions*/
    public void toggleVid_Prefix_TypeCommon()
    {
        switch (prefix_head)
        {
            case Vid_Prefix.Vid_Common_Prefix.PUBLIC:
                prefix_head = Vid_Prefix.Vid_Common_Prefix.PRIVATE;
                break;
            case Vid_Prefix.Vid_Common_Prefix.PRIVATE:
                prefix_head = Vid_Prefix.Vid_Common_Prefix.PROTECTED;
                break;
            case Vid_Prefix.Vid_Common_Prefix.PROTECTED:
                prefix_head = Vid_Prefix.Vid_Common_Prefix.PUBLIC;
                break;
            default:
                prefix_head = Vid_Prefix.Vid_Common_Prefix.PUBLIC;
                break;
        }
    }
    public void toggleVid_Prefix_return()
    {
        switch (prefix_returnVal)
        {
            case Vid_Prefix.Vid_returnValue.VOID:
                prefix_head = Vid_Prefix.Vid_Common_Prefix.PRIVATE;
                break;
            case Vid_Prefix.Vid_returnValue.BOOL:
                prefix_head = Vid_Prefix.Vid_Common_Prefix.PROTECTED;
                break;
            case Vid_Prefix.Vid_returnValue.CLASS:
                prefix_head = Vid_Prefix.Vid_Common_Prefix.PUBLIC;
                break;
            case Vid_Prefix.Vid_returnValue.NUM:
                prefix_head = Vid_Prefix.Vid_Common_Prefix.PUBLIC;
                break;
            default:
                prefix_head = Vid_Prefix.Vid_Common_Prefix.PUBLIC;
                break;
        }
    }

    private string prefixCommon_String()
    {
        switch (prefix_head)
        {
            case Vid_Prefix.Vid_Common_Prefix.PUBLIC:
                return "public";
            case Vid_Prefix.Vid_Common_Prefix.PRIVATE:
                return "private";
            case Vid_Prefix.Vid_Common_Prefix.PROTECTED:
                return "protected";
            default:
                return "public";
        }
    }
    private string prefixReturn_String()
    {
        switch (prefix_returnVal)
        {
            case Vid_Prefix.Vid_returnValue.VOID:
                return "void";
            case Vid_Prefix.Vid_returnValue.BOOL:
                return "bool";
            case Vid_Prefix.Vid_returnValue.CLASS:
                return "class";
            case Vid_Prefix.Vid_returnValue.NUM:
                return "num";
            default:
                return "void";
        }
    }
    private string writeParameters()
    {
        StringBuilder sb = new StringBuilder();
        int count = 0;

        foreach (String name in parameters_names)
        {
            Dictionary<String, String> metaData = parameters[name].getMetaData();
            sb.Append(metaData["number_type"] + " " + name);
            if (count < parameters.Count - 1)
            {
                sb.Append(", ");
                count++;
            }
        }
        return sb.ToString();
    }

    /*Getters*/
    public String getParametersName(int index)
    {
        int size = parameters_names.Count;
        if(index >= 0  && index < size)
        {
            return parameters_names[index];
        }
        return null;
    }
    public Vid_Data getParameter_atIndex(int index)
    {
        if (parameters.Count > index && index >= 0)
        {
            return parameters[parameters_names[index]];
        }
        else
        {
            return null;
        }
    }
    /*Setters*/
    public void setName(String name)
    {
        functionName = name;
    }

}
