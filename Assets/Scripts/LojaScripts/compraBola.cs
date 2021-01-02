using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compraBola : MonoBehaviour
{
    public int bolaIDe;
    public Text btnText;
    private Animator falido;

    private GameObject txtMoedas;


    //controle de comprabotao
    public void ComprarBolaBtn() {

        for (int i =0; i< BolasShop.instance.bolaslist.Count; i++) {


            if (BolasShop.instance.bolaslist[i].bolasID == bolaIDe && !BolasShop.instance.bolaslist[i].bolasComprou && PlayerPrefs.GetInt("moedasSave") >= BolasShop.instance.bolaslist[i].bolasPreco)
            {

                BolasShop.instance.bolaslist[i].bolasComprou = true;
                UpdateComprarBtn();
                ScoreManager.instance.PerdeMoedas(BolasShop.instance.bolaslist[i].bolasPreco);
                GameObject.Find("MoedaLoja").GetComponent<Text>().text = PlayerPrefs.GetInt("moedasSave").ToString();
            }

            else if (BolasShop.instance.bolaslist[i].bolasID == bolaIDe && !BolasShop.instance.bolaslist[i].bolasComprou && PlayerPrefs.GetInt("moedasSave") <= BolasShop.instance.bolaslist[i].bolasPreco)
            {
                falido = GameObject.FindGameObjectWithTag("falido").GetComponent<Animator>();
                falido.Play("FalidoANIM");
            }
            else if (BolasShop.instance.bolaslist[i].bolasID == bolaIDe && BolasShop.instance.bolaslist[i].bolasComprou) {

                UpdateComprarBtn();
            }
        }

        BolasShop.instance.UpdateSprite(bolaIDe);
    }

    void UpdateComprarBtn() {


        btnText.text = "Usando";

        for (int i = 0; i < BolasShop.instance.compraBtnList.Count; i++)
        {

            compraBola compraBolaScript = BolasShop.instance.compraBtnList[i].GetComponent<compraBola>();

            for (int j = 0; j < BolasShop.instance.bolaslist.Count; j++)
            {
                if (BolasShop.instance.bolaslist[j].bolasID == compraBolaScript.bolaIDe) {

                    BolasShop.instance.SalvaBolasLOjaText(compraBolaScript.bolaIDe, "Usando");

                    if (BolasShop.instance.bolaslist[j].bolasID == compraBolaScript.bolaIDe && BolasShop.instance.bolaslist[j].bolasComprou && BolasShop.instance.bolaslist[j].bolasID == bolaIDe) {

                        OndeEstou.instance.bolaEmUso = compraBolaScript.bolaIDe;
                        PlayerPrefs.SetInt("BolaUse",compraBolaScript.bolaIDe);
                    }
                }


                if (BolasShop.instance.bolaslist[j].bolasID == compraBolaScript.bolaIDe && BolasShop.instance.bolaslist[j].bolasComprou && BolasShop.instance.bolaslist[j].bolasID != bolaIDe) 
                {
                    compraBolaScript.btnText.text = "Use";

                    BolasShop.instance.SalvaBolasLOjaText(compraBolaScript.bolaIDe, "Use");

                 }

            }
        }

        


    }
        public void FalidoInvers()
        {

            falido = GameObject.FindGameObjectWithTag("falido").GetComponent<Animator>();
            falido.Play("FalidoSAIR");


        }
}
