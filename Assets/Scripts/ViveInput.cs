using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR; 

public class ViveInput : MonoBehaviour
{
   // [SteamVR_DefaultAction("Squeeze")]
    public SteamVR_Action_Single sqeezeAction;
    public SteamVR_Action_Vector2 touchAction;
    public bool whistling = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions._default.Teleport.GetStateDown(SteamVR_Input_Sources.Any))
        {
            print("Teleport down");
            whistling = !whistling;
        }
        /*if (SteamVR_Actions._default.Whistle.GetStateDown(SteamVR_Input_Sources.Any))
        {
            print("Whistle down");
        }*/
        if (SteamVR_Actions._default.GrabPinch.GetStateUp(SteamVR_Input_Sources.Any))
        {
            print("Grab Pinch Up");
        }

        float triggerValue = sqeezeAction.GetAxis(SteamVR_Input_Sources.Any);

        if(triggerValue > 0.0f)
        {
            //print(triggerValue);
        }

        Vector2 touchpadValue = touchAction.GetAxis(SteamVR_Input_Sources.Any);
        if (touchpadValue != Vector2.zero)
        {
            //print(touchpadValue);
        }
    }
}
