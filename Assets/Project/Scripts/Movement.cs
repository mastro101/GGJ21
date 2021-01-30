﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.WSA;

public class Movement : MonoBehaviour
{
    public GameManager managererere;
    public TransformData dovecazzoStoPosizionato;
    public GameObject luiSiSenteOsservato;
    public GameObject piedeMarx;
    public GameObject piedeHitler;
    public Transform bersaglioneHitlerone;
    public Transform bersaglinoMarxino;
    public float velocitaPiedini = 7.5f;
    public float rotazioneDelCamerone = 200;
    public float distanzaMassimaPiedini = 1.5f;
    public float altezzaPassino = 0.75f;
    public float velocitaSollevamentoPiedone = 1;
    public AudioClip suoninoPassini;

    private bool mossoMarx;
    private bool mossoHitler;
    private Vector3 posizioneHitlerone;
    private Vector3 posizioneMarxone;
    private Rigidbody corpoDiMarx;
    private Rigidbody corpoDiHitler;
    private bool rightTriggerPressed;
    private bool leftTriggerPressed;
    private Renderer renderinoHitlerino;
    private Renderer renderoneMarxone;
    private Rigidbody corponePiedoni;
    private float altezzaBasePiedino;
    private int indiceCalzinaSelezionatina;
    private GameObject chePuzzaQuestaCalza;
    private AudioSource sorgentinaAudino;

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
        renderoneMarxone = piedeMarx.GetComponent<Renderer>();
        renderinoHitlerino = piedeHitler.GetComponent<Renderer>();
        corponePiedoni = GetComponent<Rigidbody>();
        altezzaBasePiedino = piedeMarx.transform.position.y;
        sorgentinaAudino = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (renderoneMarxone.isVisible == false)
        {
            piedeMarx.transform.position = bersaglinoMarxino.position;
        }

        if (renderinoHitlerino.isVisible == false)
        {
            piedeHitler.transform.position = bersaglioneHitlerone.position;
        }
        
        Vector3 cameraInputAxis = rotazioneDelCamerone * Time.deltaTime * new Vector3(Input.GetAxis("RightStickY"), Input.GetAxis("RightStickX"), 0f);

        if (cameraInputAxis != Vector3.zero)
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
        corpinoPiedino.velocity = transform.forward * velocitaPiedini;
        float nuovaAltezzazza = Mathf.Lerp(corponePiedoni.transform.position.y, altezzaPassino, velocitaSollevamentoPiedone);
        corpinoPiedino.transform.position += new Vector3(0, nuovaAltezzazza, 0);
    }

    void FermaHitler()
    {
        corpoDiHitler.velocity = Vector3.zero;
        if (corpoDiHitler.transform.position.y != altezzaBasePiedino)
        {
            corpoDiHitler.transform.position -= new Vector3(0, corpoDiHitler.transform.position.y - altezzaBasePiedino, 0);
        }
        sorgentinaAudino.PlayOneShot(suoninoPassini);
        mossoHitler = true;
    }

    void FermaMarx()
    {
        corpoDiMarx.velocity = Vector3.zero;
        if (corpoDiMarx.transform.position.y != altezzaBasePiedino)
        {
            corpoDiMarx.transform.position -= new Vector3(0, corpoDiMarx.transform.position.y - altezzaBasePiedino, 0);
        }
        sorgentinaAudino.PlayOneShot(suoninoPassini);
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
        Vector3 posizioneGenitoriale = posizioneHitlerone - (posizioneHitlerone - posizioneMarxone) * 0.5f;
        posizioneGenitoriale.y = 0;
        corponePiedoni.MovePosition(posizioneGenitoriale); 
        piedeHitler.transform.position = posizioneHitlerone;
        piedeMarx.transform.position = posizioneMarxone;
        mossoMarx = false;
        mossoHitler = false;
        leftTriggerPressed = false;
        rightTriggerPressed = false;
    }

    public void SettaCalzonaPresona(int indiceeeee, GameObject calza)
    {
        if (indiceeeee == indiceCalzinaSelezionatina)
        {
            managererere.PuntonePresone();
            calza.SetActive(false);
            chePuzzaQuestaCalza = null;
        }

        else
        {
            if (chePuzzaQuestaCalza != null)
            {
                chePuzzaQuestaCalza.SetActive(true);
            }
            indiceCalzinaSelezionatina = indiceeeee;
            chePuzzaQuestaCalza = calza;
            calza.SetActive(false);
        }
    }
}
