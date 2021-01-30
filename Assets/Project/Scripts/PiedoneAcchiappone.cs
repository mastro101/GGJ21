using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using UnityEngine;

public class PiedoneAcchiappone : MonoBehaviour
{
    public Movement controlleronePiedoni;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Sock>() != null)
        {
            controlleronePiedoni.SettaCalzonaPresona(other.GetComponentInParent<Sock>().index, other.transform.parent.gameObject);
        }
    }
}
