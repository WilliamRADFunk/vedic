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

    void Start()
    {
        hand_model = GetComponent<HandModel>();
        leap_hand = hand_model.GetLeapHand();
        if (leap_hand == null) Debug.LogError("No leap_hand founded");
    }

    void Update()
    {
        
        LineRenderer r = new LineRenderer();
        lineTester.SetPosition(0, hand_model.GetPalmPosition());
        lineTester.SetPosition(1,  hand_model.GetPalmDirection() + 2 * hand_model.GetPalmPosition());// * finger.GetTipPosition());

        //FingerModel finger = hand_model.fingers[1];
        //lineTester.SetPosition(0, finger.GetTipPosition());
        //lineTester.SetPosition(1, finger.GetRay().direction + 50 * finger.GetTipPosition());// * finger.GetTipPosition());
    }
}