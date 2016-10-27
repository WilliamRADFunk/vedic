using System;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechController : MonoBehaviour {

    [SerializeField]
    private string[] m_Keywords;
    [SerializeField]
    private PanelController PanelController;

    private KeywordRecognizer m_Recognizer;
    private bool pristine = false;

    void Start()
    {
        // Checks to see if machine can use the speech recognition
        if (PhraseRecognitionSystem.isSupported)
        {
            m_Recognizer = new KeywordRecognizer(m_Keywords);
            m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
            m_Recognizer.Start();
        }
        // If not, make the speech toggle (in panel) non-interactable.
        else
        {
            pristine = true;
        }
    }
    void Update()
    {
        if(pristine)
        {
            PanelController.DeactivateSpeechToggle();
        }
    }
    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
        Debug.Log(builder.ToString());
    }
}
