using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Interactable : MonoBehaviour
{
    public enum interactionTypes 
    { 
        TEXT,
        OBTAIN
    }
    public interactionTypes myInteractionType;
    public void Interaction()
    {
        switch (myInteractionType)
        {
            case interactionTypes.TEXT:
                break;
            case interactionTypes.OBTAIN:
                break;
            default:
                break;
        }
    }
}
