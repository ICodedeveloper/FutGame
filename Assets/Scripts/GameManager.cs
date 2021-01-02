using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    //bola
    [SerializeField]
    private GameObject[] bola;
    public int bolasNum =2;
    private bool bolaMorreu = false;
    public int bolasEmCena = 0;
    public Transform pos;
    public bool win;
    public int  tiro = 0;
    
    public bool jogarComecou;


    private bool adsUmaVez = false;
   

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Load;

        pos = GameObject.Find("posInicial").GetComponent<Transform>();
        StarGame();
    }

    void Load(Scene cena, LoadSceneMode modo)
    {

        if (OndeEstou.instance.fase !=0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2) {

            pos = GameObject.Find("posInicial").GetComponent<Transform>();
            StarGame();
        }
        
    }


    void Start()
    {

      //PlayerPrefs.DeleteAll();
            ScoreManager.instance.GameStartScroreM();

    }

 
    void Update()
    {
        ScoreManager.instance.UpdateScore();
        UiManager.instance.UpdateUI();
        NascBolas();


        if (bolasNum <= 0) {

            GameOver();
        }

        if (win == true) {

            WinGame();
        }
    }





    void NascBolas()
    {
        if (OndeEstou.instance.fase >= 2)
        {

            if (bolasNum > 0 && bolasEmCena == 0 && Camera.main.transform.position.x <= 0.5f)
            {

                Instantiate(bola[OndeEstou.instance.bolaEmUso], new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
                bolasEmCena += 1;
                tiro = 0;
            }
        }
        else
        {
            if (bolasNum > 0 && bolasEmCena == 0)
            {

                Instantiate(bola[OndeEstou.instance.bolaEmUso], new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
                bolasEmCena += 1;
                tiro = 0;
            }

        }

     }
       
    

    void GameOver() {

        UiManager.instance.GameOverUI();
        jogarComecou = false;
        

        if (adsUmaVez == false)
        {
            AdsUnity.instance.ShowAds();
            adsUmaVez = true;
        }
    }


    void WinGame() {
        UiManager.instance.WinGameUI();
        jogarComecou = false;
    }

    void StarGame() {

        jogarComecou = true;
        bolasNum = 2;
        bolasEmCena = 0;
        win = false;
        UiManager.instance.StartUI();
        adsUmaVez = false;
    }

}
