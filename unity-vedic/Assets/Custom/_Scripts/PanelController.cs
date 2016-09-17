using UnityEngine;

public class PanelController : MonoBehaviour
{
    private bool pristine = true;
    GameObject DbImporter;
    GameObject DbExporter;
    GameObject MenuTeleport;
    GameObject MenuMain;

    void Update()
    {
        if(pristine)
        {
            DbExporter = GameObject.FindGameObjectWithTag("DbExporter");
            DbImporter = GameObject.FindGameObjectWithTag("DbImporter");
            MenuTeleport = GameObject.FindGameObjectWithTag("MenuTeleport");
            MenuMain = GameObject.FindGameObjectWithTag("MenuMain");
            DbExporter.SetActive(false);
            MenuTeleport.SetActive(false);
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
        MenuTeleport.SetActive(false);
    }

    public void ShowMenuTeleport()
    {
        MenuTeleport.SetActive(true);
        MenuMain.SetActive(false);
    }
}
