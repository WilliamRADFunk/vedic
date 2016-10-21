using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;

public class LaserController : MonoBehaviour {

    //Hand Models that appear in scene (Used to track availability)
    public GameObject LeftHand;
    public GameObject RightHand;

    //Line Renderers that visualize RayCast usage
    public LineRenderer leftLine;
    public LineRenderer rightLine;

    //Settings to whether or not we want left or right handed usage.
    public bool leftHanded;
    public bool rightHanded;

    bool active;


	// Use this for initialization
	void Start () {
        active = false;
        InvokeRepeating("ManageLines", 1.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () { 
	}

    public void UpdateLineState(bool state)
    {
        active = state;
        
    }

    //Called by InvokeRepeating to manage whether or not the lines should be rendered at any given time.
    private void ManageLines()
    {
        if(active)
        {
            if(LeftHand.activeInHierarchy && leftHanded)
            {
                leftLine.enabled = true;
            }
            else
            {
                leftLine.enabled = false;
            }

            if(RightHand.activeInHierarchy && rightHanded)
            {
                rightLine.enabled = true;
            }
            else
            {
                rightLine.enabled = false;
            }
        }
        else
        {
            leftLine.enabled = false;
            rightLine.enabled = false;
        }
    }
}
