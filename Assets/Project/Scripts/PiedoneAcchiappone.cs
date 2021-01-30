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
        Props oggettoneColpitone = other.GetComponentInParent<Props>();
        Sock calzaDiDDDDDDDio = other.GetComponentInParent<Sock>();

        if (oggettoneColpitone != null)
        {
            oggettoneColpitone.CalcioSuperRandom();
        }
        
        if (calzaDiDDDDDDDio != null)
        {
            controlleronePiedoni.SettaCalzonaPresona(calzaDiDDDDDDDio.index, other.transform.parent.gameObject);
        }

        if (other.CompareTag("Environment"))
        {
            
        }
    }
}
