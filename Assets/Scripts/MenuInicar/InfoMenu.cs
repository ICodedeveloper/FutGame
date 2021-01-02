using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoMenu : MonoBehaviour
{

    private Animator Info;
    private AudioSource musica;
    public Sprite somLigado, somDesligado;
    private Button btnSom;

    private void Start()
    {

        Info = GameObject.FindGameObjectWithTag("MenuInfo").GetComponent<Animator>() as Animator;
        musica = GameObject.Find("AudioManager").GetComponent<AudioSource>() as AudioSource;
        btnSom = GameObject.Find("sound").GetComponent<Button>() as Button;



    }

    public void AnimaInfo() {

        
        Info.Play("InfoImg");
    }
    


    public void SaiInfo()
    {

        Info.Play("InfoImgSai");

    }


    public void LigaDesligaSom() {


        musica.mute = !musica.mute;


        if (musica.mute == true)
        {

            btnSom.image.sprite = somDesligado;

        }
        else
        {
            btnSom.image.sprite = somLigado;
        }


    }

    public void Facebook() {


        Application.OpenURL("https://www.facebook.com/Junior.yzam");

    }

}
