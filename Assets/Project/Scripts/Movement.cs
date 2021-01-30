using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.WSA;

public class Movement : MonoBehaviour
{
    public TransformData dovecazzoStoPosizionato;
    public GameObject luiSiSenteOsservato;
    public GameObject piedeMarx;
    public GameObject piedeHitler;
    public Transform bersaglioneHitlerone;
    public Transform bersaglinoMarxino;
    public float velocitaPiedini = 1f;
    public float rotazioneDelCamerone;
    public float distanzaMassimaPiedini = 1.5f;
    public Transform bersagliogenitore;

    private bool mossoMarx;
    private bool mossoHitler;
    private Vector3 posizioneHitlerone;
    private Vector3 posizioneMarxone;
    private Rigidbody corpoDiMarx;
    private Rigidbody corpoDiHitler;
    private bool rightTriggerPressed;
    private bool leftTriggerPressed;


    private void OnEnable()
    {
        dovecazzoStoPosizionato.value = transform;
    }

    private void OnDisable()
    {
        dovecazzoStoPosizionato.value = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        corpoDiHitler = piedeHitler.GetComponent<Rigidbody>();
        corpoDiMarx = piedeMarx.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        Vector3 cameraInputAxis = rotazioneDelCamerone * Time.deltaTime * new Vector3(Input.GetAxis("RightStickY"), Input.GetAxis("RightStickX"), 0f);

        if (cameraInputAxis != Vector3.zero && Input.GetAxis("Triggers") < 0.3 && Input.GetAxis("Triggers") > -0.3)
        {
            luiSiSenteOsservato.transform.Rotate(0, cameraInputAxis.y, 0, Space.Self);
        }
        
        posizioneHitlerone = piedeHitler.transform.position;
        posizioneMarxone = piedeMarx.transform.position;
        
        ControllinoSeTiSeiMossino();

        if (rightTriggerPressed && Input.GetAxis("Triggers") < 0.3)
        {
            FermaHitler();
        }

        if (leftTriggerPressed && Input.GetAxis("Triggers") > -0.3)
        {
            FermaMarx();
        }
        
        if (Input.GetAxis("Triggers") >= 0.3 && mossoHitler == false)
        {
            rightTriggerPressed = true;
            MoveFoot(corpoDiHitler, bersaglioneHitlerone);
            
            if (Vector3.Distance(piedeHitler.transform.position, bersaglioneHitlerone.position) >= distanzaMassimaPiedini)
            {
                FermaHitler();
            }
        }

        else if (Input.GetAxis("Triggers") <= -0.3 && mossoMarx == false)
        {
            leftTriggerPressed = true;
            MoveFoot(corpoDiMarx, bersaglinoMarxino);
            
            if (Vector3.Distance(piedeMarx.transform.position, bersaglinoMarxino.position) >= distanzaMassimaPiedini)
            {
                FermaMarx();
            }
        }
        
    }

    void MoveFoot(Rigidbody corpinoPiedino, Transform bersaglione)
    {
        corpinoPiedino.velocity = corpinoPiedino.transform.forward * velocitaPiedini;
    }

    void FermaHitler()
    {
        corpoDiHitler.velocity = Vector3.zero;
        mossoHitler = true;
    }

    void FermaMarx()
    {
        corpoDiMarx.velocity = Vector3.zero;
        mossoMarx = true;
    }

    void ControllinoSeTiSeiMossino()
    {
        if (mossoHitler == true && mossoMarx == true)
        {
            MuoviGenitore();
        }
    }

    void MuoviGenitore()
    {
        print("Muovi genitore");
        Vector3 posizioneGenitoriale = bersagliogenitore.position;
        posizioneGenitoriale.y = 0;
        luiSiSenteOsservato.transform.position = posizioneGenitoriale; 
        piedeHitler.transform.position = posizioneHitlerone;
        piedeMarx.transform.position = posizioneMarxone;
        mossoMarx = false;
        mossoHitler = false;
        leftTriggerPressed = false;
        rightTriggerPressed = false;
    }
}
