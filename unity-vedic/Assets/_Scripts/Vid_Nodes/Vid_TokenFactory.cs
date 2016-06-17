using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class Vid_TokenFactory  {
    private static Vid_TokenFactory instance;
    private long id = 0;
    private string token = "$Vid_Token::";
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
