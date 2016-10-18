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

    public void ToggleLaser()
    {
        if (isLaserOn)
        {
            Lasers[0].GetComponent<RaycastHandler>().ToggleLineRenderer();
            Lasers[1].GetComponent<RaycastHandler>().ToggleLineRenderer();
            isLaserOn = false;
        }
        else
        {
            if (Lasers[0].GetComponent<RaycastHandler>().ToggleLineRenderer() && Lasers[1].GetComponent<RaycastHandler>().ToggleLineRenderer())
            {
                isLaserOn = true;
            }
        }
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


    public void ToggleNodeSpawner()
    {
        if (isNodeSpawner)
        {
            Debug.Log("In the anti-toggler");
            NodeSpawner.SetActive(false);

            if (MenuInput.activeSelf)
            {
                GameObject toggle = GameObject.FindGameObjectWithTag("NodeToggle");
                Toggle tog = toggle.GetComponent<Toggle>();
                tog.isOn = false;
                isNodeSpawner = false;
                tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
            }
            else
            {
                MenuInput.SetActive(true);
                GameObject toggle = GameObject.FindGameObjectWithTag("NodeToggle");
                Toggle tog = toggle.GetComponent<Toggle>();
                tog.isOn = false;
                isNodeSpawner = false;
                tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
                MenuInput.SetActive(false);
            }
        }
        else
        {
            Debug.Log("In the toggler");
            NodeSpawner.SetActive(true);

            if (MenuInput.activeSelf)
            {
                GameObject toggle = GameObject.FindGameObjectWithTag("NodeToggle");
                Toggle tog = toggle.GetComponent<Toggle>();
                tog.isOn = true;
                isNodeSpawner = true;
                tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
            }
            else
            {
                MenuInput.SetActive(true);
                GameObject toggle = GameObject.FindGameObjectWithTag("NodeToggle");
                Toggle tog = toggle.GetComponent<Toggle>();
                tog.isOn = true;
                isNodeSpawner = true;
                tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
                MenuInput.SetActive(false);
            }
        }
    }

    public void HideNodeSpawner()
    {
        if (isNodeSpawner)
        {
            NodeSpawner.SetActive(false);
            isNodeSpawner = false;

            if (MenuInput.activeSelf)
            {
                GameObject toggle = GameObject.FindGameObjectWithTag("NodeToggle");
                Toggle tog = toggle.GetComponent<Toggle>();
                tog.isOn = false;
                tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
            }
            else
            {
                MenuInput.SetActive(true);
                GameObject toggle = GameObject.FindGameObjectWithTag("NodeToggle");
                Toggle tog = toggle.GetComponent<Toggle>();
                tog.isOn = false;
                tog.GetComponent<Leap.Unity.InputModule.ToggleToggler>().SetToggle(tog);
                MenuInput.SetActive(false);
            }
        }
    }

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
