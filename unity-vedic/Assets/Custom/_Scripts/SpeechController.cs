using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class SpeechController : MonoBehaviour
{
    private bool isSpeech = true;

    private List<string> m_Keywords;
    private Dictionary<string, string> word2letter = new Dictionary<string, string>();
    private List<string> wordChain;
    private string[] commandPhrases;

    [SerializeField]
    private PanelController PanelController;
    [SerializeField]
    private RtsHandler RtsHandler;
    private InputField textField;

    private KeywordRecognizer m_Recognizer;
    private bool isTyping = false;
    private bool pristine = true;

    void Start()
    {
        m_Keywords = new List<string>();
        wordChain = new List<string>();
        commandPhrases = new string[28];

        word2letter.Add("alpha", "a");
        word2letter.Add("bravo", "b");
        word2letter.Add("charlie", "c");
        word2letter.Add("delta", "d");
        word2letter.Add("echo", "e");
        word2letter.Add("foxtrot", "f");
        word2letter.Add("golf", "g");
        word2letter.Add("hotel", "h");
        word2letter.Add("indigo", "i");
        word2letter.Add("juliet", "j");
        word2letter.Add("kilo", "k");
        word2letter.Add("lima", "l");
        word2letter.Add("mike", "m");
        word2letter.Add("nancy", "n");
        word2letter.Add("oscar", "o");
        word2letter.Add("papa", "p");
        word2letter.Add("quebec", "q");
        word2letter.Add("romeo", "r");
        word2letter.Add("sierra", "s");
        word2letter.Add("tango", "t");
        word2letter.Add("uniform", "u");
        word2letter.Add("victor", "v");
        word2letter.Add("whiskey", "w");
        word2letter.Add("x-ray", "x");
        word2letter.Add("yankee", "y");
        word2letter.Add("zulu", "z");
        word2letter.Add("period", ".");
        word2letter.Add("space", " ");
        word2letter.Add("star", "*");
        word2letter.Add("equals", "=");
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
        if (args.text.Equals("exit", StringComparison.OrdinalIgnoreCase))
        {
            isTyping = false;
            Debug.Log("Flushing chain");
            wordChain = new List<string>(); // Flush the chain
        }
        else if (isTyping)
        {
            if (args.text.Equals("delete", StringComparison.OrdinalIgnoreCase) && textField.text.Length > 0)
            {
                textField.text = textField.text.Substring(0, textField.text.Length - 1);
            }
            else if (args.text.Equals("back", StringComparison.OrdinalIgnoreCase) && textField.text.Length > 0)
            {
                textField.text = textField.text.Substring(0, textField.text.Length - 1);
            }
            else if (args.text.Equals("erase", StringComparison.OrdinalIgnoreCase))
            {
                textField.text = "";
            }
            else textField.text += TranslateLetters(args.text);
        }
        else if (args.text.Equals("execute", StringComparison.OrdinalIgnoreCase))
        {
            ActivateWordChain();
        }
        else if (args.text.Equals("flush", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("Flushing chain");
            wordChain = new List<string>(); // Flush the chain
        }
        else if (args.text.Equals("teletype", StringComparison.OrdinalIgnoreCase))
        {
            isTyping = true;
            Debug.Log("Flushing chain");
            wordChain = new List<string>(); // Flush the chain
        }
        else if (args.text.Equals("exit", StringComparison.OrdinalIgnoreCase))
        {
            isTyping = false;
            Debug.Log("Flushing chain");
            wordChain = new List<string>(); // Flush the chain
        }
    }
    private void CreateRecognizedCommands()
    {
        m_Keywords.Add("computer");
        m_Keywords.Add("execute");
        m_Keywords.Add("panel");
        m_Keywords.Add("import");
        m_Keywords.Add("export");
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
        m_Keywords.Add("select");
        m_Keywords.Add("database");
        m_Keywords.Add("one");
        m_Keywords.Add("two");
        m_Keywords.Add("three");
        m_Keywords.Add("four");
        m_Keywords.Add("five");
        m_Keywords.Add("six");
        m_Keywords.Add("seven");
        m_Keywords.Add("eight");
        m_Keywords.Add("nine");
        m_Keywords.Add("alpha");
        m_Keywords.Add("bravo");
        m_Keywords.Add("charlie");
        m_Keywords.Add("delta");
        m_Keywords.Add("echo");
        m_Keywords.Add("foxtrot");
        m_Keywords.Add("golf");
        m_Keywords.Add("hotel");
        m_Keywords.Add("indigo");
        m_Keywords.Add("juliet");
        m_Keywords.Add("kilo");
        m_Keywords.Add("lima");
        m_Keywords.Add("mike");
        m_Keywords.Add("nancy");
        m_Keywords.Add("oscar");
        m_Keywords.Add("papa");
        m_Keywords.Add("quebec");
        m_Keywords.Add("romeo");
        m_Keywords.Add("sierra");
        m_Keywords.Add("tango");
        m_Keywords.Add("uniform");
        m_Keywords.Add("victor");
        m_Keywords.Add("whiskey");
        m_Keywords.Add("x-ray");
        m_Keywords.Add("yankee");
        m_Keywords.Add("zulu");
        m_Keywords.Add("period");
        m_Keywords.Add("space");
        m_Keywords.Add("star");
        m_Keywords.Add("equals");
        m_Keywords.Add("save");
        m_Keywords.Add("flush");
        m_Keywords.Add("teletype");
        m_Keywords.Add("exit");
        m_Keywords.Add("back");
        m_Keywords.Add("delete");
        m_Keywords.Add("erase");
        m_Keywords.Add("input");
        m_Keywords.Add("submit");
        m_Keywords.Add("name");
        m_Keywords.Add("host");
        m_Keywords.Add("user");
        m_Keywords.Add("password");
        m_Keywords.Add("where");
        m_Keywords.Add("from");
        m_Keywords.Add("into");
        m_Keywords.Add("values");
        m_Keywords.Add("like");

        commandPhrases[0] = "keyboard toggle";
        commandPhrases[1] = "laser toggle";
        commandPhrases[2] = "node toggle";
        commandPhrases[3] = "threedee cursors toggle";
        commandPhrases[4] = "speech toggle";
        commandPhrases[5] = "panel import";
        commandPhrases[6] = "panel export";
        commandPhrases[7] = "teleport lobby";
        commandPhrases[8] = "teleport browser";
        commandPhrases[9] = "teleport query";
        commandPhrases[10] = "teleport analytics";
        commandPhrases[11] = "select database one";
        commandPhrases[12] = "select database two";
        commandPhrases[13] = "select database three";
        commandPhrases[14] = "select database four";
        commandPhrases[15] = "select database five";
        commandPhrases[16] = "select database six";
        commandPhrases[17] = "select database seven";
        commandPhrases[18] = "select database eight";
        commandPhrases[19] = "select database nine";
        commandPhrases[20] = "database import";
        commandPhrases[21] = "database save";
        commandPhrases[22] = "query input";
        commandPhrases[23] = "query submit";
        commandPhrases[24] = "database name";
        commandPhrases[25] = "host name";
        commandPhrases[26] = "user name";
        commandPhrases[27] = "password";
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
                    Debug.Log(commandPhrases[6]); // panel export
                    PanelController.ShowDbExporter();
                    break;
                case 7:
                    Debug.Log(commandPhrases[7]); // teleport lobby
                    PanelController.teleporter.GetComponent<Teleporter>().jumpSwitch(0);
                    break;
                case 8:
                    Debug.Log(commandPhrases[8]); // teleport browser
                    PanelController.teleporter.GetComponent<Teleporter>().jumpSwitch(1);
                    break;
                case 9:
                    Debug.Log(commandPhrases[9]); // teleport query
                    PanelController.teleporter.GetComponent<Teleporter>().jumpSwitch(2);
                    break;
                case 10:
                    Debug.Log(commandPhrases[10]); // teleport analytics
                    PanelController.teleporter.GetComponent<Teleporter>().jumpSwitch(3);
                    break;
                case 11:
                    Debug.Log(commandPhrases[11]); // select database one
                    PanelController.ToggleOn("DatabaseToggle", 1);
                    PanelController.DbImporter.GetComponent<ImportDatabase>().LoadDB(0);
                    break;
                case 12:
                    Debug.Log(commandPhrases[12]); // select database two
                    PanelController.ToggleOn("DatabaseToggle", 2);
                    PanelController.DbImporter.GetComponent<ImportDatabase>().LoadDB(1);
                    break;
                case 13:
                    Debug.Log(commandPhrases[13]); // select database three
                    PanelController.ToggleOn("DatabaseToggle", 3);
                    PanelController.DbImporter.GetComponent<ImportDatabase>().LoadDB(2);
                    break;
                case 14:
                    Debug.Log(commandPhrases[14]); // select database four
                    PanelController.ToggleOn("DatabaseToggle", 4);
                    PanelController.DbImporter.GetComponent<ImportDatabase>().LoadDB(3);
                    break;
                case 15:
                    Debug.Log(commandPhrases[15]); // select database five
                    PanelController.ToggleOn("DatabaseToggle", 5);
                    PanelController.DbImporter.GetComponent<ImportDatabase>().LoadDB(4);
                    break;
                case 16:
                    Debug.Log(commandPhrases[16]); // select database six
                    PanelController.ToggleOn("DatabaseToggle", 6);
                    PanelController.DbImporter.GetComponent<ImportDatabase>().LoadDB(5);
                    break;
                case 17:
                    Debug.Log(commandPhrases[17]); // select database seven
                    PanelController.ToggleOn("DatabaseToggle", 7);
                    PanelController.DbImporter.GetComponent<ImportDatabase>().LoadDB(6);
                    break;
                case 18:
                    Debug.Log(commandPhrases[18]); // select database eight
                    PanelController.ToggleOn("DatabaseToggle", 8);
                    PanelController.DbImporter.GetComponent<ImportDatabase>().LoadDB(7);
                    break;
                case 19:
                    Debug.Log(commandPhrases[19]); // select database nine
                    PanelController.ToggleOn("DatabaseToggle", 9);
                    PanelController.DbImporter.GetComponent<ImportDatabase>().LoadDB(8);
                    break;
                case 20:
                    Debug.Log(commandPhrases[20]); // database import
                    PanelController.DbImporter.GetComponent<ImportDatabase>().Send();
                    break;
                case 21:
                    Debug.Log(commandPhrases[21]); // database save
                    PanelController.DbImporter.GetComponent<ImportDatabase>().SaveDB();
                    break;
                case 22:
                    Debug.Log(commandPhrases[22]); // query import
                    textField = GameObject.FindGameObjectWithTag("QueryInput").GetComponent<InputField>();
                    break;
                case 23:
                    Debug.Log(commandPhrases[23]); // query submit
                    PanelController.DbExporter.GetComponentInChildren<SendQuery>().Send(GameObject.FindGameObjectWithTag("QueryInput").GetComponent<InputField>());
                    break;
                case 24:
                    Debug.Log(commandPhrases[24]); // database name
                    textField = GameObject.FindGameObjectWithTag("DbName").GetComponent<InputField>();
                    break;
                case 25:
                    Debug.Log(commandPhrases[25]); // host name
                    textField = GameObject.FindGameObjectWithTag("HostName").GetComponent<InputField>();
                    break;
                case 26:
                    Debug.Log(commandPhrases[26]); // user name
                    textField = GameObject.FindGameObjectWithTag("UserName").GetComponent<InputField>();
                    break;
                case 27:
                    Debug.Log(commandPhrases[27]); // password
                    textField = GameObject.FindGameObjectWithTag("Password").GetComponent<InputField>();
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
    private string TranslateLetters(string word)
    {
        if (word2letter.ContainsKey(word))
        {
            return word2letter[word];
        }
        else return word;
    }
}
