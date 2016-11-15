using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Con_Con : MonoBehaviour {
    public Toggle selectButton;
    UIMove_Tool utool;
    ToggleColorChanger tcc;

    // Use this for initialization
    public virtual void Start() {
        utool = NodeSystemEssentials.uiMove_Tool;
        tcc = selectButton.gameObject.GetComponent<ToggleColorChanger>();

        selectButton.onValueChanged.AddListener(toggleTask);
    }

    private void toggleTask(bool arg0) {
        if (utool == null) { return; }
        utool.setholding(gameObject);
        if (tcc == null) { return; }
        tcc.ChangeColor(selectButton);

    }
}
