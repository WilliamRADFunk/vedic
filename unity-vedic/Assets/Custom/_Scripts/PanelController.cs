using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    private bool pristine = true;
    private bool is3dCursors = true;
    private bool isRTSon = false;
    private bool isKeyboard = false;
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
    public GameObject teleporter;

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
            DbExporter.SetActive(false);
            MenuTeleport.SetActive(false);
            MenuInput.SetActive(false);
            threeDCursors.SetActive(false);
            MenuSound.SetActive(false);
            Keyboard.SetActive(false);
            is3dCursors = false;
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

    public void ToggleKeyboard()
    {
        if (isKeyboard)
        {
            Keyboard.SetActive(false);

            if (MenuInput.activeSelf)
            {
                GameObject toggle = GameObject.FindGameObjectWithTag("KeyToggle");
                Toggle tog = toggle.GetComponent<Toggle>();
                tog.isOn = false;
                isKeyboard = false;
                tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
            }
            else
            {
                MenuInput.SetActive(true);
                GameObject toggle = GameObject.FindGameObjectWithTag("KeyToggle");
                Toggle tog = toggle.GetComponent<Toggle>();
                tog.isOn = false;
                isKeyboard = false;
                tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
                MenuInput.SetActive(false);
            }
        }
        else
        {
            Keyboard.SetActive(true);

            if (MenuInput.activeSelf)
            {
                GameObject toggle = GameObject.FindGameObjectWithTag("KeyToggle");
                Toggle tog = toggle.GetComponent<Toggle>();
                tog.isOn = true;
                isKeyboard = true;
                tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
            }
            else
            {
                MenuInput.SetActive(true);
                GameObject toggle = GameObject.FindGameObjectWithTag("KeyToggle");
                Toggle tog = toggle.GetComponent<Toggle>();
                tog.isOn = true;
                isKeyboard = true;
                tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
                MenuInput.SetActive(false);
            }
        }
    }

    public void ShowKeyboard(InputField inp)
    {
        if (!isKeyboard)
        {
            Keyboard.SetActive(true);
            isKeyboard = true;
            Keyboard.GetComponent<KeyboardController>().SetInputField(inp);

            if (MenuInput.activeSelf)
            {
                GameObject toggle = GameObject.FindGameObjectWithTag("KeyToggle");
                Toggle tog = toggle.GetComponent<Toggle>();
                tog.isOn = true;
                isKeyboard = true;
                tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
            }
            else
            {
                MenuInput.SetActive(true);
                GameObject toggle = GameObject.FindGameObjectWithTag("KeyToggle");
                Toggle tog = toggle.GetComponent<Toggle>();
                tog.isOn = true;
                isKeyboard = true;
                tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
                MenuInput.SetActive(false);
            }
        }
    }

    public void ShowMenuMain()
    {
        MenuMain.SetActive(true);
        MenuSound.SetActive(false);
        MenuTeleport.SetActive(false);
        MenuInput.SetActive(false);
    }

    public void ShowMenuTeleport()
    {
        MenuTeleport.SetActive(true);
        MenuSound.SetActive(false);
        MenuInput.SetActive(false);
        MenuMain.SetActive(false);
    }

    public void ShowMenuInput()
    {
        MenuInput.SetActive(true);
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
    }

    public void Toggle3dCursors()
    {
        if (is3dCursors)
        {
            threeDCursors.SetActive(false);
            is3dCursors = false;
        }
        else
        {
            threeDCursors.SetActive(true);
            is3dCursors = true;
        }
    }

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
        if(MenuInput.activeSelf)
        {
            GameObject toggle = GameObject.FindGameObjectWithTag("RtsToggle");
            Toggle tog = toggle.GetComponent<Toggle>();
            tog.isOn = false;
            isRTSon = false;
            tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
        }
        else
        {
            MenuInput.SetActive(true);
            GameObject toggle = GameObject.FindGameObjectWithTag("RtsToggle");
            Toggle tog = toggle.GetComponent<Toggle>();
            tog.isOn = false;
            isRTSon = false;
            tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
            MenuInput.SetActive(false);
        }
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
