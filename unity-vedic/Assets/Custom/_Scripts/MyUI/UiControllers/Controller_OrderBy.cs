using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Controller_OrderBy : Controller_MultiInput {

    public RectTransform toggle;
    public VerticalLayoutGroup oderNodelayout;
    public List<RectTransform> toggles;


    public override void addButton_toLayout() {
        RectTransform rt = (RectTransform)Instantiate(toggle, new Vector3(0, 0, 0), Quaternion.identity);
        Controller_DescToggle dCon = rt.GetComponentInChildren<Controller_DescToggle>();
        rt.SetParent(oderNodelayout.transform, false);
        toggles.Add(rt);
        dCon.index = toggles.Count;
        base.addButton_toLayout();
    }

    public override void removeButton_fromLayout() {
        GameObject.Destroy(toggles[toggles.Count - 1].gameObject);
        toggles.RemoveAt(toggles.Count - 1);
        base.removeButton_fromLayout();
    }

}
