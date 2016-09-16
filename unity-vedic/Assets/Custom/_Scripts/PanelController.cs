using UnityEngine;

public class PanelController : MonoBehaviour
{
    private bool pristine = true;
    GameObject DbImporter;
    GameObject DbExporter;

    void Update()
    {
        if(pristine)
        {
            DbImporter = GameObject.FindGameObjectWithTag("DbImporter");
            DbExporter = GameObject.FindGameObjectWithTag("DbExporter");
            DbExporter.SetActive(false);
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
        DbImporter.SetActive(false);
        DbExporter.SetActive(true);
    }
}
