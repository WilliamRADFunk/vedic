using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    private bool pristine = true;
    private bool is3dCursors = false;
    private bool isRTSon = false;
    private bool isKeyboard = false;
    private bool isNodeSpawner = false;
    private bool isLaserOn = false;
    private bool isSpeech = true;
    private bool isSpeechPossible = true;
    private GameObject tableHarManager;
    private GameObject DbImporter;
    private GameObject DbExporter;
    private GameObject MenuSound;
    private GameObject MenuInput;
    private GameObject MenuTeleport;
    private GameObject MenuMain;
    private GameObject threeDCursors;
    private GameObject BigTable;
    private GameObject Keyboard;
    private GameObject MenuPods;
    public GameObject teleporter;
    private GameObject NodeSpawner;
    [SerializeField]
    private GameObject SpeechRecognizer;
    public RaycastHandler[] Lasers;

    void Update()
    {
        if(pristine)
        {
            DbExporter = GameObject.FindGameObjectWithTag("DbExporter");
            DbImporter = GameObject.FindGameObjectWithTag("DbImporter");
            MenuTeleport = GameObject.FindGameObjectWithTag("MenuTeleport");
            MenuMain = GameObject.FindGameObjectWithTag("MenuMain");
            MenuInput = GameObject.FindGameObjectWithTag("MenuInput");
            threeDCursors = GameObject.FindGameObjectWithTag("3dCursors");
            MenuSound = GameObject.FindGameObjectWithTag("MenuSound");
            BigTable = GameObject.FindGameObjectWithTag("Pedestal");
            Keyboard = GameObject.FindGameObjectWithTag("Keyboard");
            MenuPods = GameObject.FindGameObjectWithTag("MenuPods");
            NodeSpawner = GameObject.FindGameObjectWithTag("NodeSpawner");


            if (DbExporter != null)
            {
                DbExporter.SetActive(false);
            }
            if (MenuTeleport != null)
            {
                MenuTeleport.SetActive(false);
            }
            if (MenuInput != null)
            {
                MenuInput.SetActive(false);
            }
            if (threeDCursors != null)
            {
                threeDCursors.SetActive(false);
            }
            if(MenuSound != null)
            {
                MenuSound.SetActive(false);
            }
            if (Keyboard != null)
            {
                Keyboard.SetActive(false);
            }
            if (MenuPods != null)
            {
                MenuPods.SetActive(false);
            }
            if (NodeSpawner != null)
            {
                NodeSpawner.SetActive(false);
            }

            pristine = false;
        }
    }
	public void ShowDbImporter()
    {
        DbImporter.SetActive(true);
        DbExporter.SetActive(false);
    }

    public void ShowDbExporter()
    {
        DbExporter.SetActive(true);
        DbImporter.SetActive(false);
    }

    public void ShowMenuMain()
    {
        MenuMain.SetActive(true);
        MenuSound.SetActive(false);
        MenuTeleport.SetActive(false);
        MenuInput.SetActive(false);
        MenuPods.SetActive(false);
    }

    public void ShowMenuTeleport()
    {
        MenuTeleport.SetActive(true);
        MenuSound.SetActive(false);
        MenuInput.SetActive(false);
        MenuMain.SetActive(false);
        MenuPods.SetActive(false);
    }

    public void ShowMenuInput()
    {
        MenuInput.SetActive(true);
        MenuSound.SetActive(false);
        MenuTeleport.SetActive(false);
        MenuMain.SetActive(false);
        MenuPods.SetActive(false);
    }

    public void ShowMenuPods()
    {
        MenuPods.SetActive(true);
        MenuInput.SetActive(false);
        MenuSound.SetActive(false);
        MenuTeleport.SetActive(false);
        MenuMain.SetActive(false);
    }

    public void ShowMenuSound()
    {
        MenuSound.SetActive(true);
        MenuInput.SetActive(false);
        MenuTeleport.SetActive(false);
        MenuMain.SetActive(false);
        MenuPods.SetActive(false);
    }
    /*************************** 3DCURSORS TOGGLING START **********************************/
    public void Toggle3dCursors()
    {
        if (is3dCursors)
        {
            threeDCursors.SetActive(false);
            is3dCursors = false;

            if (MenuInput.activeSelf)
            {
                ToggleOff("3dCursorToggle");
            }
            else
            {
                MenuInput.SetActive(true);
                ToggleOff("3dCursorToggle");
                MenuInput.SetActive(false);
            }
        }
        else
        {
            threeDCursors.SetActive(true);
            is3dCursors = true;

            if (MenuInput.activeSelf)
            {
                ToggleOn("3dCursorToggle");
            }
            else
            {
                MenuInput.SetActive(true);
                ToggleOn("3dCursorToggle");
                MenuInput.SetActive(false);
            }
        }
    }
    /*************************** 3DCURSORS TOGGLING END ************************************/
    /*************************** RTS TOGGLING START ****************************************/
    public void ToggleRts()
    {
        if (isRTSon)
        {
            BigTable.GetComponent<RtsHandler>().InteractOff();
            isRTSon = false;
        }
        else
        {
            if(BigTable.GetComponent<RtsHandler>().InteractOn())
            {
                isRTSon = true;
            }
            else
            {
                ResetRtsToggle();
            }
        }
    }

    public void ResetRtsToggle()
    {
        isRTSon = false;
        if (MenuInput.activeSelf)
        {
            ToggleOff("RtsToggle");
        }
        else
        {
            MenuInput.SetActive(true);
            ToggleOff("RtsToggle");
            MenuInput.SetActive(false);
        }
    }
    /*************************** RTS TOGGLING END ******************************************/
    /*************************** LASER TOGGLING START **************************************/
    public void ToggleLaser()
    {
        if (isLaserOn)
        {
            Lasers[0].GetComponent<RaycastHandler>().ToggleLineRenderer();
            Lasers[1].GetComponent<RaycastHandler>().ToggleLineRenderer();
            isLaserOn = false;

            if (MenuInput.activeSelf)
            {
                ToggleOff("LaserToggle");
            }
            else
            {
                MenuInput.SetActive(true);
                ToggleOff("LaserToggle");
                MenuInput.SetActive(false);
            }
        }
        else
        {
            if (Lasers[0].GetComponent<RaycastHandler>().ToggleLineRenderer() && Lasers[1].GetComponent<RaycastHandler>().ToggleLineRenderer())
            {
                isLaserOn = true;

                if (MenuInput.activeSelf)
                {
                    ToggleOn("LaserToggle");
                }
                else
                {
                    MenuInput.SetActive(true);
                    ToggleOn("LaserToggle");
                    MenuInput.SetActive(false);
                }
            }
        }
    }
    /*************************** LASER TOGGLING END ****************************************/
    /*************************** KEYBOARD TOGGLING START ***********************************/
    public void ToggleKeyboard()
    {
        if (isKeyboard)
        {
            Keyboard.SetActive(false);
            isKeyboard = false;

            if (MenuInput.activeSelf)
            {
                ToggleOff("KeyToggle");
            }
            else
            {
                MenuInput.SetActive(true);
                ToggleOff("KeyToggle");
                MenuInput.SetActive(false);
            }
        }
        else
        {
            Keyboard.SetActive(true);
            isKeyboard = true;

            if (MenuInput.activeSelf)
            {
                ToggleOn("KeyToggle");
            }
            else
            {
                MenuInput.SetActive(true);
                ToggleOn("KeyToggle");
                MenuInput.SetActive(false);
            }
        }
    }
    public void ShowKeyboard(InputField inp)
    {
        if (!isKeyboard)
        {
            Keyboard.SetActive(true);
            Keyboard.GetComponent<KeyboardController>().SetInputField(inp);
            ToggleKeyboard();
        }
    }
    /*************************** KEYBOARD TOGGLING END *************************************/
    /*************************** SPEECH TOGGLING START *************************************/
    public void DeactivateSpeechToggle()
    {
        isSpeech = false;
        isSpeechPossible = false;

        if (MenuInput.activeSelf)
        {
            GameObject toggle = GameObject.FindGameObjectWithTag("SpeechToggle");
            Toggle tog = toggle.GetComponent<Toggle>();
            tog.isOn = false;
            tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
            tog.interactable = false;
        }
        else
        {
            MenuInput.SetActive(true);
            GameObject toggle = GameObject.FindGameObjectWithTag("SpeechToggle");
            Toggle tog = toggle.GetComponent<Toggle>();
            tog.isOn = false;
            tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
            tog.interactable = false;
            MenuInput.SetActive(false);
        }
    }

    public void ToggleSpeech()
    {
        if (isSpeech)
        {
            SpeechRecognizer.GetComponent<SpeechController>().ToggleSpeech();
            isSpeech = false;

            if (MenuInput.activeSelf)
            {
                ToggleOff("SpeechToggle");
            }
            else
            {
                MenuInput.SetActive(true);
                ToggleOff("SpeechToggle");
                MenuInput.SetActive(false);
            }
        }
        else if(isSpeechPossible)
        {
            SpeechRecognizer.GetComponent<SpeechController>().ToggleSpeech();
            isSpeech = true;

            if (MenuInput.activeSelf)
            {
                ToggleOn("SpeechToggle");
            }
            else
            {
                MenuInput.SetActive(true);
                ToggleOn("SpeechToggle");
                MenuInput.SetActive(false);
            }
        }
    }
    /*************************** SPEECH TOGGLING END ***************************************/
    /*************************** NODES TOGGLING START **************************************/
    public void ToggleNodeSpawner()
    {
        if (isNodeSpawner)
        {
            NodeSpawner.SetActive(false);
            isNodeSpawner = false;

            if (MenuInput.activeSelf)
            {
                ToggleOff("NodeToggle");
            }
            else
            {
                MenuInput.SetActive(true);
                ToggleOff("NodeToggle");
                MenuInput.SetActive(false);
            }
        }
        else
        {
            NodeSpawner.SetActive(true);
            isNodeSpawner = true;

            if (MenuInput.activeSelf)
            {
                ToggleOn("NodeToggle");
            }
            else
            {
                MenuInput.SetActive(true);
                ToggleOn("NodeToggle");
                MenuInput.SetActive(false);
            }
        }
    }
    /*************************** NODES TOGGLING END ****************************************/
    /*************************** POD TOGGLING START ****************************************/
    public void TogglePod(string pod)
    {
        GameObject[] toggles = GameObject.FindGameObjectsWithTag("PodToggle");
        for (int i = 0; i < toggles.Length; i++)
        {
            if ((toggles[i].name).Contains(pod))
            {
                Toggle tog = toggles[i].GetComponent<Toggle>();
                if (!tog.isOn)
                {
                    tog.isOn = false;
                    tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
                    BigTable.GetComponent<PodManager>().DeactivatePod( System.Int32.Parse(pod) );
                }
                else
                {
                    tog.isOn = true;
                    tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
                    BigTable.GetComponent<PodManager>().ActivatePod(System.Int32.Parse(pod));
                }
            }
        }
    }
    /*************************** POD TOGGLING END ******************************************/
    private void ToggleOn(string tagName)
    {
        GameObject toggle = GameObject.FindGameObjectWithTag(tagName);
        Toggle tog = toggle.GetComponent<Toggle>();
        tog.isOn = true;
        tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
    }

    private void ToggleOff(string tagName)
    {
        GameObject toggle = GameObject.FindGameObjectWithTag(tagName);
        Toggle tog = toggle.GetComponent<Toggle>();
        tog.isOn = false;
        tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
    }

    public void SendTableHarnessManager(GameObject tblHarManager)
    {
        tableHarManager = tblHarManager;
    }

    public void ClearAllTables()
    {
        tableHarManager.GetComponent<TableHarness>().Deconstruct();
    }

    public void OnTeleportHover(int loc)
    {
        teleporter.GetComponent<Teleporter>().OnHoverTele(loc);
    }

    public void OnTeleportExit(int loc)
    {
        teleporter.GetComponent<Teleporter>().OffHoverTele(loc);
    }

    public void OnTeleportClick(int loc)
    {
        teleporter.GetComponent<Teleporter>().OffHoverTele(loc);
    }
}
