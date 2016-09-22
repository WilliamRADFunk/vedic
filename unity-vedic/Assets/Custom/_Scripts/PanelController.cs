using UnityEngine;

public class PanelController : MonoBehaviour
{
    private bool pristine = true;
    private bool is3dCursors = true;
    private bool isRTSon = false;
    public GameObject DbImporter;
    public GameObject DbExporter;
    public GameObject MenuSound;
    public GameObject MenuInput;
    public GameObject MenuTeleport;
    public GameObject MenuMain;
    public GameObject teleporter;
    public GameObject threeDCursors;
    public GameObject BigTable;

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
            DbExporter.SetActive(false);
            MenuTeleport.SetActive(false);
            MenuInput.SetActive(false);
            threeDCursors.SetActive(false);
            MenuSound.SetActive(false);
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
            BigTable.GetComponent<RtsHandler>().InteractOn();
            isRTSon = true;
        }
    }

    public void ResetRtsToggle()
    {
        //GameObject.FindGameObjectWithTag("RtsToggle").GetComponent<>
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
