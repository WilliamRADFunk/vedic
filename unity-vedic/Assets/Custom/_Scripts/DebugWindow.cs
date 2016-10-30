using UnityEngine;
using UnityEngine.UI;

public class DebugWindow : MonoBehaviour
{
    [SerializeField]
    private Text textMesh1;
    [SerializeField]
    private Text textMesh2;
    [SerializeField]
    private Text textMesh3;
    [SerializeField]
    private Text textMesh4;

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
