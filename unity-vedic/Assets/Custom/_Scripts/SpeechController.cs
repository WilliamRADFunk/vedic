using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechController : MonoBehaviour
{
    private bool isSpeech = true;

    private List<string> m_Keywords;
    private List<string> wordChain;
    private string[] commandPhrases;

    [SerializeField]
    private PanelController PanelController;

    private KeywordRecognizer m_Recognizer;
    private bool pristine = true;

    void Start()
    {
        m_Keywords = new List<string>();
        wordChain = new List<string>();
        commandPhrases = new string[10];
    }
    void Update()
    {
        if (pristine)
        {
            // Checks to see if machine can use the speech recognition
            if (PhraseRecognitionSystem.isSupported)
            {
                CreateRecognizedCommands();
                m_Recognizer = new KeywordRecognizer(m_Keywords.ToArray());
                m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
                m_Recognizer.Start();
            }
            // If not, make the speech toggle (in panel) non-interactable.
            else
            {
                PanelController.DeactivateSpeechToggle();
                isSpeech = false;
            }
            pristine = false;
        }
    }
    public void ToggleSpeech()
    {
        if(isSpeech)
        {
            DisableSpeech();
            isSpeech = false;
        }
        else
        {
            EnableSpeech();
            isSpeech = true;
        }
    }
    private void EnableSpeech()
    {
        m_Recognizer.Start();
    }
    private void DisableSpeech()
    {
        m_Recognizer.Stop();
    }
    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
        wordChain.Add(args.text);
        Debug.Log(builder.ToString());
        if (args.text.Equals("execute", StringComparison.OrdinalIgnoreCase)) ActivateWordChain();
    }
    private void CreateRecognizedCommands()
    {
        m_Keywords.Add("computer");
        m_Keywords.Add("execute");
        m_Keywords.Add("panel");
        m_Keywords.Add("import");
        m_Keywords.Add("query");
        m_Keywords.Add("send");
        m_Keywords.Add("test");
        m_Keywords.Add("keyboard");
        m_Keywords.Add("laser");
        m_Keywords.Add("node");
        m_Keywords.Add("threedee");
        m_Keywords.Add("cursors");
        m_Keywords.Add("speech");
        m_Keywords.Add("toggle");
        m_Keywords.Add("lobby");
        m_Keywords.Add("browser");
        m_Keywords.Add("query");
        m_Keywords.Add("analytics");
        m_Keywords.Add("teleport");

        commandPhrases[0] = "keyboard toggle";
        commandPhrases[1] = "laser toggle";
        commandPhrases[2] = "node toggle";
        commandPhrases[3] = "threedee cursors toggle";
        commandPhrases[4] = "speech toggle";
        commandPhrases[5] = "panel import";
        commandPhrases[6] = "teleport lobby";
        commandPhrases[7] = "teleport browser";
        commandPhrases[8] = "teleport query";
        commandPhrases[9] = "teleport analytics";
    }
    private void ActivateWordChain()
    {
        if ( !wordChain[0].Equals("computer", StringComparison.OrdinalIgnoreCase)) Debug.Log("Improper command start.");
        else
        {
            switch(CheckCommands())
            {
                case 0:
                    Debug.Log(commandPhrases[0]); // keyboard toggle
                    PanelController.ToggleKeyboard();
                    break;
                case 1:
                    Debug.Log(commandPhrases[1]); // laser toggle
                    PanelController.ToggleLaser();
                    break;
                case 2:
                    Debug.Log(commandPhrases[2]); // node toggle
                    PanelController.ToggleNodeSpawner();
                    break;
                case 3:
                    Debug.Log(commandPhrases[3]); // threedee cursors toggle
                    PanelController.Toggle3dCursors();
                    break;
                case 4:
                    Debug.Log(commandPhrases[4]); // speech toggle
                    DisableSpeech();
                    break;
                case 5:
                    Debug.Log(commandPhrases[5]); // panel import
                    PanelController.ShowDbImporter();
                    break;
                case 6:
                    Debug.Log(commandPhrases[6]); // teleport lobby
                    PanelController.teleporter.GetComponent<Teleporter>().jumpSwitch(0);
                    break;
                case 7:
                    Debug.Log(commandPhrases[7]); // teleport browser
                    PanelController.teleporter.GetComponent<Teleporter>().jumpSwitch(1);
                    break;
                case 8:
                    Debug.Log(commandPhrases[8]); // teleport query
                    PanelController.teleporter.GetComponent<Teleporter>().jumpSwitch(2);
                    break;
                case 9:
                    Debug.Log(commandPhrases[9]); // teleport analytics
                    PanelController.teleporter.GetComponent<Teleporter>().jumpSwitch(3);
                    break;
                default:
                    Debug.Log("Invalid Command");
                    break;
            }
        }
        Debug.Log("Flushing chain");
        wordChain = new List<string>(); // Flush the chain
    }
    private int CheckCommands()
    {
        bool commandRecognized;

        // Strip barrier words from chain to avoid misalignment of comparison.
        wordChain.RemoveAt(0);
        wordChain.RemoveAt(wordChain.Count - 1);

        for(int i = 0; i < commandPhrases.Length; i++)
        {
            commandRecognized = true;
            string[] commandPhrase = commandPhrases[i].Split(' ');
            for (int j = 0; j < commandPhrase.Length; j++)
            {
                //  If the two list / arrays of words are of different length, move on.
                //  If any one word doesn not link up, move on.
                if (commandPhrase.Length != wordChain.Count || !commandPhrase[j].Equals(wordChain[j], StringComparison.OrdinalIgnoreCase))
                {
                    commandRecognized = false;
                    break;
                }
            }
            // No discrepencies found between words. It's a match!
            if(commandRecognized)
            {
                Debug.Log("Returning index: " + i);
                return i;
            }
        }
        return -1;
    }
}
