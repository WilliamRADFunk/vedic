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
    HandModel hand_model;
    Hand leap_hand;

    public LineRenderer lineTester;
    Ray rr = new Ray();
    RaycastHit hit;

    private bool active = false;

    void Start()
    {
        hand_model = GetComponent<HandModel>();
        leap_hand = hand_model.GetLeapHand();
        if (leap_hand == null) Debug.LogError("No leap_hand founded");
        active = false;
        resetLineRender();
    }

    void resetLineRender()
    {
        lineTester.enabled = false;
    }

    void Update()
    {

        if(active)
        {
            lineTester.enabled = true;
            
            lineTester.SetPosition(0, hand_model.GetPalmPosition());
            lineTester.SetPosition(1, (hand_model.GetPalmDirection() * 20f));//+ 2 * hand_model.GetPalmPosition());// * finger.GetTipPosition());

            rr.origin = hand_model.GetPalmPosition();
            rr.direction = hand_model.GetPalmDirection();

            int bitLayer = 1 << 15;

            if (Physics.Raycast(hand_model.GetPalmPosition(), hand_model.GetPalmDirection(), out hit, Mathf.Infinity, bitLayer))
            {
                Debug.Log("RAYCAST HIT OBJECT!");
                Debug.Log("Object name :: " + hit.collider.gameObject.name);

                if (hit.collider.CompareTag("ViewTable"))
                {
                    hit.collider.gameObject.GetComponent<Table>().AltActivation();
                }
            }
        }
        else
        {
            lineTester.enabled = false;
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

        return active;
    }
}