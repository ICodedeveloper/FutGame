using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AdsUnity : MonoBehaviour
{

    public static AdsUnity instance;
    private string gameId = "3383883";
    private Button BtnAds;
    public bool AdsBtnAcionado = false;


    private void Awake()
   


    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);

        }

        SceneManager.sceneLoaded += PegaBtn;
    }


    private void Start()
    {
        Advertisement.Initialize(gameId);
    }



    void PegaBtn(Scene cena,LoadSceneMode mode) {

        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2) {

            BtnAds = GameObject.Find("AdsBtn").GetComponent<Button>();
            BtnAds.onClick.AddListener(AdsBtn);

        }
    }


    void AdsBtn() {

        if (Advertisement.IsReady("rewardedVideo"))
        {

            Advertisement.Show("rewardedVideo", new ShowOptions() {resultCallback = AdsAnalise });
            AdsBtnAcionado = true;
        }

    }

    void AdsAnalise(ShowResult result) {

        if (result == ShowResult.Finished) {

            ScoreManager.instance.ColetaMoedas(200);
        }

    }


    public void ShowAds()
    {

        if (PlayerPrefs.HasKey("AdsUnity"))
        {

            if (PlayerPrefs.GetInt("AdsUnity") == 3)
            {

                if (Advertisement.IsReady("rewardedVideo"))
                {

                    Advertisement.Show("rewardedVideo");

                }
                PlayerPrefs.SetInt("AdsUnity", 1);
            }
            else
            {

                PlayerPrefs.SetInt("AdsUnity", PlayerPrefs.GetInt("AdsUnity") + 1);
            }

        }
        else
        {
            PlayerPrefs.SetInt("AdsUnity",1);
        }


    }
   

}
