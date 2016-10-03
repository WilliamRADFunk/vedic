using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class Vid_TokenFactory  {
    private static Vid_TokenFactory instance;
    private ulong id = 0;
    private ulong idReturn = 0;
    private string token = "$Vid_Token::";
    private string renturnType_Token;
    private Queue<string> queue;
    private StringBuilder stringbuilder;
    
    private Vid_TokenFactory()
    {
        queue = new Queue<string>();
        stringbuilder = new StringBuilder();
    }

    public string generateToken()
    {
        id++;
        string newToken = token + id;
        queue.Enqueue(newToken);
        return newToken;
    }

    public string generateTokenReturn(Vid_Function fun)
    {
        Vid_Prefix.Vid_returnValue type = fun.prefix_returnVal;
        switch (type)
        {
            case Vid_Prefix.Vid_returnValue.VOID:
                renturnType_Token = "$Vid_TokenReturn::void";
                return renturnType_Token;
            case Vid_Prefix.Vid_returnValue.BOOL:
                renturnType_Token = "$Vid_TokenReturn::bool";
                return renturnType_Token;
            case Vid_Prefix.Vid_returnValue.CLASS:
                renturnType_Token = "$Vid_TokenReturn::class";
                return renturnType_Token;
            case Vid_Prefix.Vid_returnValue.NUM:
                renturnType_Token = "$Vid_TokenReturn::num";
                return renturnType_Token;
            default:
                renturnType_Token = "$Vid_TokenReturn::void";
                return renturnType_Token;
        }
    }

    public string popToken() { return queue.Dequeue(); }

    public void resetID()
    {
        queue.Clear();
        id = 0;
    }

    public static Vid_TokenFactory getInstance()
    {
        if(instance == null)
        {
            instance = new Vid_TokenFactory();
        }
        return instance;
    }

    public StringBuilder getStringBuilder() { return stringbuilder; }
    public string getToken() { return queue.Peek(); }
}
