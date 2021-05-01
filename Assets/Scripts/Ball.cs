using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Ball : MonoBehaviour
{
   // private Interactable interactable;
    private void OnHandHoverBegin(Hand hand)
    {
      //  interactable = GetComponent<Interactable>();
    }


    //-------------------------------------------------
    private void OnHandHoverEnd(Hand hand)
    {
        
    }


    //-------------------------------------------------
    private void OnAttachedUpdate(Hand hand)
    {
        /* GrabTypes grabType = hand.GetGrabStarting();
         bool isGrabEnding = hand.IsGrabEnding(gameObject);

         if (interactable.attachedToHand == null && grabType != GrabTypes.None)
         {
             hand.AttachObject(gameObject, grabType);
             hand.HoverLock(interactable);
         }
         else if(isGrabEnding)
         {
             hand.DetachObject(gameObject);
             hand.HoverUnlock(interactable);
         }*/
        AIMgr.inst.GetAttention(); 
    }
    

}
