/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/
// Original script: "MagneticPinch.cs" modified by unitycoder.com to just get the finger position & directions

using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;
using System;

public class RaycastHandler : MonoBehaviour
{
    public LaserController lControl;
    public LineRenderer line;

    HandModel hand_model;
    Hand leap_hand;

    Ray rr = new Ray();
    RaycastHit hit;

    private bool active;

    void Start()
    {
        active = false;
        hand_model = GetComponent<HandModel>();
        leap_hand = hand_model.GetLeapHand();
        if (leap_hand == null) Debug.LogError("No leap_hand founded");
    }

    void Update()
    {

        if(active)
        {            
            line.SetPosition(0, hand_model.GetPalmPosition());
            line.SetPosition(1, (hand_model.GetPalmDirection() * 20f));//+ 2 * hand_model.GetPalmPosition());// * finger.GetTipPosition());

            rr.origin = hand_model.GetPalmPosition();
            rr.direction = hand_model.GetPalmDirection();

            int bitLayer = 1 << 15;

            if (Physics.Raycast(hand_model.GetPalmPosition(), hand_model.GetPalmDirection(), out hit, Mathf.Infinity, bitLayer))
            {
                if (hit.collider.CompareTag("ViewTable"))
                {
                    hit.collider.gameObject.GetComponent<Table>().AltActivation();
                }
            }
        }

        if(gameObject.activeInHierarchy == false) {
            lControl.UpdateLineState(active);
        }
    }

    public bool ToggleLineRenderer()
    {
        if(active)
        {
            active = false;
        }
        else
        {
            active = true;
        }

        lControl.UpdateLineState(active);
        return active;
    }

    
}