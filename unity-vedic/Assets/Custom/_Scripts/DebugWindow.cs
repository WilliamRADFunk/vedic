using UnityEngine;

public class DebugWindow : MonoBehaviour
{
    [SerializeField]
    private TextMesh textMesh1;
    private TextMesh textMesh2;
    private TextMesh textMesh3;
    private TextMesh textMesh4;

    // Use this for initialization
    void Start()
    {
        textMesh1 = gameObject.GetComponentInChildren<TextMesh>();
        textMesh2 = gameObject.GetComponentInChildren<TextMesh>();
        textMesh3 = gameObject.GetComponentInChildren<TextMesh>();
        textMesh4 = gameObject.GetComponentInChildren<TextMesh>();
    }

    void OnEnable()
    {
        Application.logMessageReceived += LogMessage;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogMessage;
    }

    public void LogMessage(string message, string stackTrace, LogType type)
    {
        if (textMesh1.text.Length > 300)
        {
            textMesh1.text = message + "\n";
        }
        else
        {
            textMesh1.text = message + "\n" + textMesh1.text + "\n";
        }
        textMesh2.text = textMesh1.text;
        textMesh3.text = textMesh1.text;
        textMesh4.text = textMesh1.text;
    }
}
