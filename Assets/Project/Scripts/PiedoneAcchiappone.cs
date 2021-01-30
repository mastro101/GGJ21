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
        Crock ilMostrone = other.GetComponent<Crock>();

        if (oggettoneColpitone != null)
        {
            oggettoneColpitone.CalcioSuperRandom();
        }
        
        if (calzaDiDDDDDDDio != null)
        {
            controlleronePiedoni.SettaCalzonaPresona(calzaDiDDDDDDDio.index, other.transform.parent.gameObject);
        }

        if (ilMostrone != null)
        {
            if (controlleronePiedoni.stiamoCalciando == true)
            {
                ilMostrone.SpingitoneCrockkone();
            }

            else
            {
                FindObjectOfType<GameManager>().FinePartitona();
            }
        }

        if (other.CompareTag("Environment") && controlleronePiedoni.stiamoCalciando == true && controlleronePiedoni.iPiediInchiodatiComeGesu == false)
        {
            controlleronePiedoni.InchiodaPiedoni();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Environment") && controlleronePiedoni.stiamoCalciando == true && controlleronePiedoni.iPiediInchiodatiComeGesu == false)
        {
            controlleronePiedoni.InchiodaPiedoni();
        }
    }
}
