using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.WSA;
using Random = UnityEngine.Random;

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
    public AudioClip[] suoniniPassini;
    public AudioClip[] suoniniDiDolore;
    public AudioClip[] suoniniSpaccaPiede;
    public GameObject stelloneDolorone;
    [HideInInspector]
    public bool iPiediInchiodatiComeGesu;
    public float durataCalcino = 0.15f;
    [HideInInspector]
    public bool stiamoCalciando;
    public float lerpinoTransformino = 0.1f;
    public float lerponeRotazione = 10f;
    public float duratinaStordimentello = 1.5f;
    public float duratonaImmunitissima = 2f;

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
    private bool immunissimo;
    private bool cambiaLevettoneControllerone;

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
        //renderoneMarxone = piedeMarx.GetComponent<Renderer>();
        //renderinoHitlerino = piedeHitler.GetComponent<Renderer>();
        corponePiedoni = GetComponent<Rigidbody>();
        altezzaBasePiedino = piedeMarx.transform.position.y;
        sorgentinaAudino = GetComponent<AudioSource>();
    }

    void Update()
    {
        /*if (renderoneMarxone.isVisible == false)
        {
            piedeMarx.transform.position = bersaglinoMarxino.position;
        }

        if (renderinoHitlerino.isVisible == false)
        {
            piedeHitler.transform.position = bersaglioneHitlerone.position;
        }*/

        Vector3 inputStickDestro = rotazioneDelCamerone * Time.deltaTime * new Vector3(Input.GetAxis("RightStickY"), Input.GetAxis("RightStickX"), 0f);
        Vector3 inputStickSinistro = rotazioneDelCamerone * Time.deltaTime * new Vector3(Input.GetAxis("LeftStickY"), Input.GetAxis("LeftStickX"), 0f);

        if (inputStickDestro != Vector3.zero || inputStickSinistro != Vector3.zero)
        {
            if (inputStickDestro != Vector3.zero && inputStickSinistro != Vector3.zero)
            {
                cambiaLevettoneControllerone = !cambiaLevettoneControllerone;
                
                if (cambiaLevettoneControllerone == true)
                {
                    luiSiSenteOsservato.transform.Rotate(0, inputStickDestro.y, 0, Space.Self);
                }
                else
                {
                    luiSiSenteOsservato.transform.Rotate(0, inputStickSinistro.y, 0, Space.Self);
                }
            }
            
            else if (inputStickSinistro == Vector3.zero)
            {
                luiSiSenteOsservato.transform.Rotate(0, inputStickDestro.y, 0, Space.Self);
            }
            
            else if (inputStickDestro == Vector3.zero)
            {
                luiSiSenteOsservato.transform.Rotate(0, inputStickSinistro.y, 0, Space.Self);
            }
            
        }

        if (iPiediInchiodatiComeGesu == true)
        {
            return;
        }

        posizioneHitlerone = piedeHitler.transform.position;
        posizioneMarxone = piedeMarx.transform.position;

        ControllinoSeTiSeiMossino();

        if ((Input.GetButtonDown("X") || Input.GetAxis("ArrowsX") < 0) && Input.GetAxis("Triggers") > -0.3 && Input.GetAxis("Triggers") < 0.3 && stiamoCalciando == false)
        {
            IlCalcioèSalutare();
        }

        if (rightTriggerPressed && Input.GetAxis("Triggers") < 0.3)
        {
            FermaHitler();
        }

        if (leftTriggerPressed && Input.GetAxis("Triggers") > -0.3)
        {
            FermaMarx();
        }

        if (Input.GetAxis("Triggers") >= 0.3 && mossoHitler == false && stiamoCalciando == false)
        {
            rightTriggerPressed = true;
            MoveFoot(corpoDiHitler, bersaglioneHitlerone);

            if (Vector3.Distance(piedeHitler.transform.position, bersaglioneHitlerone.position) >=
                distanzaMassimaPiedini)
            {
                FermaHitler();
            }
        }

        else if (Input.GetAxis("Triggers") <= -0.3 && mossoMarx == false && stiamoCalciando == false)
        {
            leftTriggerPressed = true;
            MoveFoot(corpoDiMarx, bersaglinoMarxino);

            if (Vector3.Distance(piedeMarx.transform.position, bersaglinoMarxino.position) >= distanzaMassimaPiedini)
            {
                FermaMarx();
            }
        }
    }

    void IlCalcioèSalutare()
    {
        print("calciamo?");
        piedeMarx.transform.position += new Vector3(0, altezzaPassino, 0);
        piedeHitler.transform.position += new Vector3(0, altezzaPassino, 0);
        StartCoroutine("CalciaTutto");
    }

    void FineCalcino()
    {
        stiamoCalciando = false;
        piedeHitler.transform.position = bersaglioneHitlerone.position;
        piedeMarx.transform.position = bersaglinoMarxino.position;
        piedeHitler.transform.rotation = bersaglioneHitlerone.rotation;
        piedeMarx.transform.rotation = bersaglinoMarxino.rotation;
        mossoHitler = false;
        mossoMarx = false;
        leftTriggerPressed = false;
        rightTriggerPressed = false;
    }

    IEnumerator CalciaTutto()
    {
        float durata = durataCalcino;
        stiamoCalciando = true;
        while (true)
        {
            print("calciamo");
            Vector3 noise = new Vector3(Random.Range(-1, 1), altezzaPassino / 2, Random.Range(-1, 1));
            Quaternion rotNoise = new Quaternion(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90),
                Random.Range(-90, 90));
            Quaternion rotNoise1 = new Quaternion(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90),
                Random.Range(-90, 90));
            piedeMarx.transform.position = Vector3.Lerp(piedeMarx.transform.position,
                bersaglinoMarxino.transform.position + noise, lerpinoTransformino);
            noise.y = -noise.y;
            piedeHitler.transform.position = Vector3.Lerp(piedeHitler.transform.position,
                bersaglioneHitlerone.transform.position - noise, lerpinoTransformino);
            piedeMarx.transform.rotation = Quaternion.Slerp(piedeMarx.transform.rotation, rotNoise, lerponeRotazione);
            piedeHitler.transform.rotation =
                Quaternion.Slerp(piedeHitler.transform.rotation, rotNoise1, lerponeRotazione);
            durata -= 0.02f;
            if (durata <= 0 || iPiediInchiodatiComeGesu == true)
            {
                FineCalcino();
                break;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    void MoveFoot(Rigidbody corpinoPiedino, Transform bersaglione)
    {
        corpinoPiedino.velocity = transform.forward * velocitaPiedini;
        float nuovaAltezzazza =
            Mathf.Lerp(corponePiedoni.transform.position.y, altezzaPassino, velocitaSollevamentoPiedone);
        corpinoPiedino.transform.position += new Vector3(0, nuovaAltezzazza, 0);
    }

    void FermaHitler()
    {
        corpoDiHitler.velocity = Vector3.zero;
        if (corpoDiHitler.transform.position.y != altezzaBasePiedino)
        {
            corpoDiHitler.transform.position -=
                new Vector3(0, corpoDiHitler.transform.position.y - altezzaBasePiedino, 0);
        }

        if (!mossoHitler)
        {
            sorgentinaAudino.PlayOneShot(suoniniPassini[Random.Range(0, suoniniPassini.Length)]);
        }
        
        mossoHitler = true; 
    }

    void FermaMarx()
    {
        corpoDiMarx.velocity = Vector3.zero;
        if (corpoDiMarx.transform.position.y != altezzaBasePiedino)
        {
            corpoDiMarx.transform.position -= new Vector3(0, corpoDiMarx.transform.position.y - altezzaBasePiedino, 0);
        }

        if (!mossoMarx)
        {
            sorgentinaAudino.PlayOneShot(suoniniPassini[Random.Range(0, suoniniPassini.Length)]);
        }
        
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
            calza.GetComponent<Sock>().SpriteDammelaSubito(false);
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
            calza.GetComponent<Sock>().SpriteDammelaSubito(true);
            calza.SetActive(false);
        }
    }

    public void InchiodaPiedoni()
    {
        if (!iPiediInchiodatiComeGesu)
        {
            StartCoroutine(RumoriDiOssaRotte());
        }
        iPiediInchiodatiComeGesu = true;
        Invoke("SchiodaLeFetteDellaMorte", duratinaStordimentello);
    }

    IEnumerator RumoriDiOssaRotte()
    {
        GameObject stelloneInstance = Instantiate(stelloneDolorone, transform.position + new Vector3(0,0.5f,0), Quaternion.Euler(-90,0,0));
        sorgentinaAudino.PlayOneShot(suoniniSpaccaPiede[Random.Range(0, suoniniSpaccaPiede.Length)]);

        yield return new WaitForSeconds(.4f);
        
        sorgentinaAudino.PlayOneShot(suoniniDiDolore[Random.Range(0, suoniniDiDolore.Length)]);
        Destroy(stelloneInstance);
    }

    public void SchiodaLeFetteDellaMorte()
    {
        iPiediInchiodatiComeGesu = false;
    }
}