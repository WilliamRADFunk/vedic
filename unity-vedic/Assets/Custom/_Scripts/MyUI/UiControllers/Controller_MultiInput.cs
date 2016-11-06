using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Controller_MultiInput : MonoBehaviour, ButtonAdder {
    public Vid_MultiInput node;
    public RectTransform button;
    public GridLayoutGroup layout;
    public List<RectTransform> buttons;

    public virtual void addButton_toLayout() {
        RectTransform rt = (RectTransform)Instantiate(button, new Vector3(0, 0, 0), Quaternion.identity);
        InputButton input = rt.GetComponent<InputButton>();
        input.vidObj = node;
        rt.SetParent(layout.transform, false);
        buttons.Add(rt);
        input.argumentIndex = buttons.Count;
        node.incromentInputs();
        layout.spacing = new Vector2(layout.spacing.x, layout.spacing.y + 10);
    }

    public virtual void removeButton_fromLayout() {
        GameObject.Destroy(buttons[buttons.Count - 1].gameObject);
        buttons.RemoveAt(buttons.Count - 1);
        node.decromentInputs();
        layout.spacing = new Vector2(layout.spacing.x, layout.spacing.y - 10);
    }
}