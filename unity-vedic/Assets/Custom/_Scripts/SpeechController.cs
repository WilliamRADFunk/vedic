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
    private string[] commandsExplained;

    [SerializeField]
    private PanelController PanelController;
    [SerializeField]
    private RtsHandler RtsHandler;
    private InputField textField;
    [SerializeField]
    private WindowTextController WindowTextController;
    [SerializeField]
    private DataCache DataCache;

    private KeywordRecognizer m_Recognizer;
    private List<string> dataList;
    private int dataPointer = 0;
    private int dataInc = 15;
    private bool isTyping = false;
    private bool isScrolling = false;
    private bool isCapitalized = false;
    private bool pristine = true;

    void Start()
    {
        m_Keywords = new List<string>();
        dataList = new List<string>();
        wordChain = new List<string>();
        commandPhrases = new string[31];
        commandsExplained = new string[31];

        word2letter.Add("zero", "0");
        word2letter.Add("one", "1");
        word2letter.Add("two", "2");
        word2letter.Add("three", "3");
        word2letter.Add("four", "4");
        word2letter.Add("five", "5");
        word2letter.Add("six", "6");
        word2letter.Add("seven", "7");
        word2letter.Add("eight", "8");
        word2letter.Add("nine", "9");
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
        word2letter.Add("plus", "+");
        word2letter.Add("minus", "-");
        word2letter.Add("dash", "-");
        word2letter.Add("underscore", "_");
        word2letter.Add("comma", ",");
        word2letter.Add("at", "@");
        word2letter.Add("pound", "#");
        word2letter.Add("dollar", "$");
        word2letter.Add("precent", "%");
        word2letter.Add("caret", "^");
        word2letter.Add("ampersand", "&");
        word2letter.Add("exclaim", "!");
        word2letter.Add("tilda", "~");
        word2letter.Add("tildey", "~");
        word2letter.Add("open-parenthesis", "(");
        word2letter.Add("close-parenthesis", ")");
        word2letter.Add("less-than", "<");
        word2letter.Add("greater-than", ">");
        word2letter.Add("back-slash", "\\");
        word2letter.Add("forward-slash", "/");
        word2letter.Add("question-mark", "?");
        word2letter.Add("double-quote", "\"");
        word2letter.Add("single-quote", "'");
        word2letter.Add("colon", ":");
        word2letter.Add("semi-colon", ";");
        word2letter.Add("pipe", "|");
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
            isScrolling = false;
            Debug.Log("Flushing chain");
            wordChain = new List<string>(); // Flush the chain
        }
        else if (isScrolling)
        {
            if (args.text.Equals("up", StringComparison.OrdinalIgnoreCase))
            {
                if (dataPointer - (2 * dataInc) <= 0) dataPointer = 0;
                else dataPointer -= (2 * dataInc);
                Debug.Log(dataPointer);

                string message = "";
                int counter = 0;
                for (int i = dataPointer; counter < dataInc && i < dataList.Count; i++)
                {
                    if (TranslateLetters(dataList[i]) != dataList[i])
                    {
                        message += dataList[i] + " --> " + TranslateLetters(dataList[i]) + "\n";
                    }
                    else
                    {
                        message += dataList[i] + "\n";
                    }
                    dataPointer++;
                    counter++;
                }
                WindowTextController.UpdateInfo(message, true);
            }
            else if (args.text.Equals("down", StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log(dataPointer);
                string message = "";
                int counter = 0;
                for (int i = dataPointer; counter < dataInc && i < dataList.Count; i++)
                {
                    if (TranslateLetters(dataList[i]) != dataList[i])
                    {
                        message += dataList[i] + " --> " + TranslateLetters(dataList[i]) + "\n";
                    }
                    else
                    {
                        message += dataList[i] + "\n";
                    }
                    if (dataPointer < dataList.Count-1) dataPointer++;
                    counter++;
                }
                WindowTextController.UpdateInfo(message, true);
            }
        }
        else if (isTyping)
        {
            if (args.text.Equals("capitalize", StringComparison.OrdinalIgnoreCase))
            {
                isCapitalized = true;
            }
            else if (args.text.Equals("delete", StringComparison.OrdinalIgnoreCase) && textField.text.Length > 0)
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
            else if (args.text.Equals("grab", StringComparison.OrdinalIgnoreCase))
            {
                textField.text += DataCache.ReadCacheMessage();
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
        else if (args.text.Equals("scroll", StringComparison.OrdinalIgnoreCase))
        {
            isScrolling = true;
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
        m_Keywords.Add("zero");
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
        m_Keywords.Add("plus");
        m_Keywords.Add("minus");
        m_Keywords.Add("dash");
        m_Keywords.Add("underscore");
        m_Keywords.Add("comma");
        m_Keywords.Add("at");
        m_Keywords.Add("pound");
        m_Keywords.Add("dollar");
        m_Keywords.Add("precent");
        m_Keywords.Add("caret");
        m_Keywords.Add("ampersand");
        m_Keywords.Add("exclaim");
        m_Keywords.Add("tilda");
        m_Keywords.Add("tildey");
        m_Keywords.Add("close-parenthesis");
        m_Keywords.Add("open-parenthesis");
        m_Keywords.Add("less-than");
        m_Keywords.Add("greater-than");
        m_Keywords.Add("back-slash");
        m_Keywords.Add("forward-slash");
        m_Keywords.Add("question-mark");
        m_Keywords.Add("double-quote");
        m_Keywords.Add("single-quote");
        m_Keywords.Add("colon");
        m_Keywords.Add("semi-colon");
        m_Keywords.Add("pipe");
        m_Keywords.Add("save");
        m_Keywords.Add("flush");
        m_Keywords.Add("teletype");
        m_Keywords.Add("exit");
        m_Keywords.Add("back");
        m_Keywords.Add("delete");
        m_Keywords.Add("clear");
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
        m_Keywords.Add("drop");
        m_Keywords.Add("table");
        m_Keywords.Add("column");
        m_Keywords.Add("table");
        m_Keywords.Add("alter");
        m_Keywords.Add("table");
        m_Keywords.Add("capitalize");
        m_Keywords.Add("show");
        m_Keywords.Add("vocabulary");
        m_Keywords.Add("commands");
        m_Keywords.Add("explain");
        m_Keywords.Add("scroll");
        m_Keywords.Add("up");
        m_Keywords.Add("down");
        m_Keywords.Add("grab");

        commandPhrases[0] = "keyboard toggle";
        commandsExplained[0] = "Shows/Hides virtual keyboard";
        commandPhrases[1] = "laser toggle";
        commandsExplained[1] = "Activates/Deactivates laser collider from hand";
        commandPhrases[2] = "node toggle";
        commandsExplained[2] = "Shows/Hides node spawner and its spawn";
        commandPhrases[3] = "threedee cursors toggle";
        commandsExplained[3] = "Activates/Deactivates 3D Cursor colliders";
        commandPhrases[4] = "speech toggle";
        commandsExplained[4] = "Turns off speech recognition";
        commandPhrases[5] = "panel import";
        commandsExplained[5] = "Shows database import panel on left menu";
        commandPhrases[6] = "panel export";
        commandsExplained[6] = "Shows database export panel on left menu";
        commandPhrases[7] = "teleport lobby";
        commandsExplained[7] = "Teleports user to lobby station";
        commandPhrases[8] = "teleport browser";
        commandsExplained[8] = "Teleports user to browser station";
        commandPhrases[9] = "teleport query";
        commandsExplained[9] = "Teleports user to query station";
        commandPhrases[10] = "teleport analytics";
        commandsExplained[10] = "Teleports user to analytics station";
        commandPhrases[11] = "select database one";
        commandsExplained[11] = "Selects first \"saved database\" toggle";
        commandPhrases[12] = "select database two";
        commandsExplained[12] = "Selects second \"saved database\" toggle";
        commandPhrases[13] = "select database three";
        commandsExplained[13] = "Selects third \"saved database\" toggle";
        commandPhrases[14] = "select database four";
        commandsExplained[14] = "Selects fourth \"saved database\" toggle";
        commandPhrases[15] = "select database five";
        commandsExplained[15] = "Selects fifth \"saved database\" toggle";
        commandPhrases[16] = "select database six";
        commandsExplained[16] = "Selects sixth \"saved database\" toggle";
        commandPhrases[17] = "select database seven";
        commandsExplained[17] = "Selects seventh \"saved database\" toggle";
        commandPhrases[18] = "select database eight";
        commandsExplained[18] = "Selects eighth \"saved database\" toggle";
        commandPhrases[19] = "select database nine";
        commandsExplained[19] = "Selects ninth \"saved database\" toggle";
        commandPhrases[20] = "database import";
        commandsExplained[20] = "Imports database based off shown connection info";
        commandPhrases[21] = "database save";
        commandsExplained[21] = "Saves database connection info in selected db toggle slot";
        commandPhrases[22] = "query input";
        commandsExplained[22] = "Activates inputfield for query creation";
        commandPhrases[23] = "query submit";
        commandsExplained[23] = "Sends query to database";
        commandPhrases[24] = "query clear";
        commandsExplained[24] = "Clears results output from previous query";
        commandPhrases[25] = "database name";
        commandsExplained[25] = "Activates inputfield for database name";
        commandPhrases[26] = "host name";
        commandsExplained[26] = "Activates inputfield for host name";
        commandPhrases[27] = "user name";
        commandsExplained[27] = "Activates inputfield for user name";
        commandPhrases[28] = "password";
        commandsExplained[28] = "Activates inputfield for password";
        commandPhrases[29] = "show vocabulary";
        commandsExplained[29] = "Displays speech recognition vocabulary";
        commandPhrases[30] = "show commands";
        commandsExplained[30] = "Displays speech recognition commands";
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
                    Debug.Log(commandPhrases[24]); // query clear
                    Text text = GameObject.FindGameObjectWithTag("QueryResult").GetComponentInChildren<Text>();
                    text.text = "";
                    break;
                case 25:
                    Debug.Log(commandPhrases[25]); // database name
                    textField = GameObject.FindGameObjectWithTag("DbName").GetComponent<InputField>();
                    break;
                case 26:
                    Debug.Log(commandPhrases[26]); // host name
                    textField = GameObject.FindGameObjectWithTag("HostName").GetComponent<InputField>();
                    break;
                case 27:
                    Debug.Log(commandPhrases[27]); // user name
                    textField = GameObject.FindGameObjectWithTag("UserName").GetComponent<InputField>();
                    break;
                case 28:
                    Debug.Log(commandPhrases[28]); // password
                    textField = GameObject.FindGameObjectWithTag("Password").GetComponent<InputField>();
                    break;
                case 29:
                    Debug.Log(commandPhrases[29]); // show vocabulary
                    string msgVocab = "";
                    dataList = new List<string>();
                    dataPointer = 0;
                    dataInc = 15;
                    for (int i = 0; i < m_Keywords.Count; i++)
                    {
                        dataList.Add(m_Keywords[i]);
                    }
                    for (int i = 0; i < dataInc && i < dataList.Count; i++)
                    {
                        if (TranslateLetters(dataList[i]) != dataList[i])
                        {
                            msgVocab += dataList[i] + " --> " + TranslateLetters(dataList[i]) + "\n";
                        }
                        else
                        {
                            msgVocab += dataList[i] + "\n";
                        }
                        dataPointer++;
                    }
                    WindowTextController.UpdateInfo(msgVocab, true);
                    break;
                case 30:
                    Debug.Log(commandPhrases[30]); // show commands
                    string msgCom = "";
                    dataList = new List<string>();
                    dataPointer = 0;
                    dataInc = 5;
                    for (int i = 0; i < commandPhrases.Length; i++)
                    {
                        dataList.Add(commandPhrases[i] + " --> " + commandsExplained[i]);
                    }
                    for (int i = 0; i < dataInc && i < dataList.Count; i++)
                    {
                        msgCom += dataList[i] + "\n";
                        dataPointer++;
                    }
                    WindowTextController.UpdateInfo(msgCom, true);
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
        if (word2letter.ContainsKey(word) && isCapitalized)
        {
            isCapitalized = false;
            return word2letter[word].ToUpper();
        }
        else if (word2letter.ContainsKey(word))
        {
            return word2letter[word];
        }
        else return word;
    }
}
