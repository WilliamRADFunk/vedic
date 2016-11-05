using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DynamicButton : MonoBehaviour
{

    [SerializeField]
    private Button button;
    private Table instance;

    public void SetInstance(Table tempInstance)
    {
        instance = tempInstance;
        button.onClick.AddListener(delegate { switchTableState();});
    }

    public Button GetButtonInstance()
    {
        return button;
    }

   private void switchTableState()
    {
        instance.ForceOut();
    }

    private bool pullTableState()
    {
        return instance.GetGuiState();
    }
}