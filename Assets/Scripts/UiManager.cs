using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UiManager : MonoBehaviour
{

    public static UiManager instance;
    private Text pontosUI, bolasUI;
    [SerializeField]
    private GameObject losePainel,winPainel,pausePainel;
    [SerializeField]
    private Button pauseBtn,pauseBtn_Return;
    [SerializeField]
    private Button btnNovamenteLose, btnLevelLose; // btn lose
    private Button btnLevelWin,btnNovamenteWin,btnAvancaWin,btnNovamenteFase; // btn win
    
    //moedas
    public int moedasNumAntes, moedasNumDepois, resultado;

    private void Awake()
    {
        if (instance == null) {

            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(gameObject);

        }

        SceneManager.sceneLoaded += Load;
        LigaDesligaPainel();


        PegaDados();


    }

    void Load(Scene cena, LoadSceneMode modo)
    {
        PegaDados();
    }
    void PegaDados() {


            if ( OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2)
            {

                // Elementos da UI pontos e bolas
                pontosUI = GameObject.Find("pontosUi").GetComponent<Text>();
                bolasUI = GameObject.Find("bolasUI").GetComponent<Text>();

                //paineis 
                losePainel = GameObject.Find("LosePainel");
                winPainel = GameObject.Find("winPainel");
                pausePainel = GameObject.Find("pausePainel");

                //Botoões Pause
                pauseBtn = GameObject.Find("pauseBtn").GetComponent<Button>();
                pauseBtn_Return = GameObject.Find("PAUSE").GetComponent<Button>();

                //botoes Lose
                btnNovamenteLose = GameObject.Find("novamentelose").GetComponent<Button>();
                btnLevelLose = GameObject.Find("listafases").GetComponent<Button>();

                //botoes win
                btnLevelWin = GameObject.Find("menufaseswin").GetComponent<Button>();
                btnNovamenteWin = GameObject.Find("novamentewin").GetComponent<Button>();
                btnAvancaWin = GameObject.Find("avancarwin").GetComponent<Button>();
            // Botoes fase

            btnNovamenteFase = GameObject.Find("BtnFase").GetComponent<Button>();

                //Eventos

                //pause
                pauseBtn.onClick.AddListener(Pause);
                pauseBtn_Return.onClick.AddListener(PauseRetun);

                //  you lose
                btnNovamenteLose.onClick.AddListener(JogarNovamente);
                btnLevelLose.onClick.AddListener(Levels);

                // you win

                btnLevelWin.onClick.AddListener(Levels);
                btnNovamenteWin.onClick.AddListener(JogarNovamente);
                btnAvancaWin.onClick.AddListener(ProximaFase);


            // btn novamentefase

            btnNovamenteFase.onClick.AddListener(JogarNovamente);

            //

                moedasNumAntes = PlayerPrefs.GetInt("moedasSave");

            }
         }



    public void StartUI()
    {
        LigaDesligaPainel();
    }


    public void UpdateUI() {

        pontosUI.text = ScoreManager.instance.moedas.ToString();
        bolasUI.text = GameManager.instance.bolasNum.ToString();
        moedasNumDepois = ScoreManager.instance.moedas;

    }


    public void GameOverUI() {

        losePainel.SetActive(true);
        winPainel.SetActive(false);

    }

    public void WinGameUI()
    {

        winPainel.SetActive(true);
        losePainel.SetActive(false);


    }

    void LigaDesligaPainel() {

        StartCoroutine(tempo());
    }


    void Pause() {
        pausePainel.SetActive(true);
        pausePainel.GetComponent<Animator>().Play("pause_anim");
        Time.timeScale = 0;
       
        

    }

    void PauseRetun() {

        pausePainel.GetComponent<Animator>().Play("pause_return");
        Time.timeScale = 1;
        StartCoroutine(EsperaPause());

    }


    IEnumerator EsperaPause() {

        yield return new WaitForSeconds(0.8f);
        pausePainel.SetActive(false);
        
    }




    IEnumerator tempo() {

        yield return new WaitForSeconds(0.001f);
        losePainel.SetActive(false);
        winPainel.SetActive(false);
        pausePainel.SetActive(false);
    }


    //you lose

    void JogarNovamente() {

        if (GameManager.instance.win == false && AdsUnity.instance.AdsBtnAcionado == true) {


            SceneManager.LoadScene(OndeEstou.instance.fase);
            AdsUnity.instance.AdsBtnAcionado = false;
        }
         else if (GameManager.instance.win == false && AdsUnity.instance.AdsBtnAcionado == false) {

            SceneManager.LoadScene(OndeEstou.instance.fase);
            resultado = moedasNumDepois - moedasNumAntes;
            ScoreManager.instance.PerdeMoedas(resultado);
            resultado = 0;

        }
        else {

            SceneManager.LoadScene(OndeEstou.instance.fase);
            resultado = 0;
        }

        
       

    }

    void Levels() {
        if (GameManager.instance.win == false  && AdsUnity.instance.AdsBtnAcionado == true) {

            SceneManager.LoadScene(1);
            AdsUnity.instance.AdsBtnAcionado = false;
        }


        else if (GameManager.instance.win == false && AdsUnity.instance.AdsBtnAcionado == false) { 
            resultado = moedasNumDepois - moedasNumAntes;
            ScoreManager.instance.PerdeMoedas(resultado);
            resultado = 0;
            SceneManager.LoadScene(1);
        }
        else {
            resultado = 0;
            SceneManager.LoadScene(1);
        }

        }

    void ProximaFase()
    {
        if (GameManager.instance.win == true) {

            int temp = OndeEstou.instance.fase + 1;
            SceneManager.LoadScene(temp);
        }


    }
}
