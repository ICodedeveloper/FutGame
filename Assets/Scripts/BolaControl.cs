using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BolaControl : MonoBehaviour
{

    //Posicao seta
    //Seta
    
    public GameObject setaGO;

    // Ang

    public float zRotate;
    public bool liberaRot = false;
    public bool liberaTiro = false;

    //força

    private Rigidbody2D bola;
    public float force = 0;
    public GameObject seta2Img;

    //paredes
    private Transform paredeLD, paredeLE;
    //mortebolaAnim
    [SerializeField]
    private GameObject MorteBolaAnim;

    //toque

    private Collider2D toqueCol;

    private void Awake()
    {
       
        setaGO = GameObject.Find("Seta");
        seta2Img = setaGO.transform.GetChild(0).gameObject;
        setaGO.GetComponent<Image>().enabled = false;
        seta2Img.GetComponent<Image>().enabled = false;
        paredeLD = GameObject.Find("Parede_LD").GetComponent<Transform>();
        paredeLE = GameObject.Find("Parede_LE").GetComponent<Transform>();
    }


  

    void Start()
    {

       

        //força
        
        bola = GetComponent<Rigidbody2D>();
       
 


    }

  
    void Update()
    {

        RotacaoSeta();
        InputDeRotacao();
        LimitaRotacao();
        PosicionaSeta();
        //força
        ControlaForca();
        AplicaForca();
        //paredes
        Paredes();

    }

    void PosicionaSeta()
    {

        setaGO.GetComponent<Image>().rectTransform.position = transform.position;
    }



    void RotacaoSeta()
    {

        setaGO.GetComponent<Image>().rectTransform.eulerAngles = new Vector3(0, 0, zRotate);

    }


    void InputDeRotacao()
    {

        if (liberaRot == true)
        {

            float moveY = Input.GetAxis("Mouse Y");

            if (zRotate < 90)
            {

                if (moveY > 0)
                {

                    zRotate += 2.5f;
                }
            }
            if (zRotate > 0)
            {
                if (moveY < 0)
                {
                    zRotate -= 2.5f;
                }

            }
        }

    }

    void LimitaRotacao()
    {

        if (zRotate >= 90)
        {
            zRotate = 90;
        }

        if (zRotate < 0)
        {

            zRotate = 0;
        }

    }


    private void OnMouseDown()
    {
        if (GameManager.instance.tiro == 0) {

            liberaRot = true;
           
            setaGO.GetComponent<Image>().enabled = true;
            seta2Img.GetComponent<Image>().enabled = true;

            toqueCol = GameObject.FindGameObjectWithTag("toque").GetComponent<Collider2D>();
        }
        
    }

    private void OnMouseUp()
    {
        liberaRot = false;
        setaGO.GetComponent<Image>().enabled = false;
        seta2Img.GetComponent<Image>().enabled = false;
       
        if (GameManager.instance.tiro == 0 && force > 0) {

            liberaTiro = true;

            seta2Img.GetComponent<Image>().fillAmount = 0;
            AudioManager.instance.SonsFXToca(1);
            GameManager.instance.tiro = 1;
            toqueCol.enabled = false;
        }
       
    }


    //força

    void AplicaForca()
    {

        float x = force * Mathf.Cos(zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(zRotate * Mathf.Deg2Rad);

        if (liberaTiro == true)
        {

            bola.AddForce(new Vector2(x, y));
            liberaTiro = false;
        }

    }

    void ControlaForca()
    {

        if (liberaRot == true)
        {

            if (liberaRot == true)
            {

                float moveX = Input.GetAxis("Mouse X");

                if (moveX < 0)
                {
                    seta2Img.GetComponent <Image>().fillAmount += 0.8f * Time.deltaTime;
                    force = seta2Img.GetComponent<Image>().fillAmount * 1000;
                }

                if (moveX > 0)
                {

                    seta2Img.GetComponent<Image>().fillAmount -= 0.8f * Time.deltaTime;
                    force = seta2Img.GetComponent<Image>().fillAmount * 1000;
                }
            }


        }

    }



    void BolaDinamica() {

        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }


    void Paredes() {

        if (this.gameObject.transform.position.x > paredeLD.position.x) {

            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }

        if (this.gameObject.transform.position.x < paredeLE.position.x)
        {

            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }
    }


    private void OnTriggerEnter2D(Collider2D outro)
    {

        if (outro.gameObject.CompareTag("morte")) {
            Instantiate(MorteBolaAnim, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }

        if (outro.gameObject.CompareTag("win")) {

            GameManager.instance.win = true;
            int temp = OndeEstou.instance.fase +1;
            temp++;
            PlayerPrefs.SetInt("Level"+temp,1);

        }
    }

}
