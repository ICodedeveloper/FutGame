using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolasShop : MonoBehaviour
{

    public static BolasShop instance;

    public List<Bolas> bolaslist = new List<Bolas>();
    public List<GameObject> bolaSuporteList = new List<GameObject>();
    public List<GameObject> compraBtnList = new List<GameObject>();

    public GameObject baseBolaItem;
    public Transform conteudo;

    private void Awake()
    {
        if (instance == null) {

            instance = this;

        }

    }

    void Start()
    {
       
        FillList();
    }


    void Update()
    {

    }


    void FillList() {

        foreach (Bolas b in bolaslist) {

            GameObject itensBola = Instantiate(baseBolaItem) as GameObject;
            itensBola.transform.SetParent(conteudo, false);
            BolasSuport item = itensBola.GetComponent<BolasSuport>();

            item.bolaID = b.bolasID;
            item.bolaPreco.text = b.bolasPreco.ToString();
            item.BtnCompra.GetComponent<compraBola>().bolaIDe = b.bolasID;


            //lista CompraBtn

            compraBtnList.Add(item.BtnCompra);



            //lista BolaSupportList

            bolaSuporteList.Add(itensBola);


            if (PlayerPrefs.GetInt("BTN" + item.bolaID) == 1)
            {
                b.bolasComprou = true;

            }

            if (PlayerPrefs.HasKey("BTNS" + item.bolaID) && b.bolasComprou)
            {
                item.BtnCompra.GetComponent<compraBola>().btnText.text = PlayerPrefs.GetString("BTNS"+item.bolaID);
            }

            if (b.bolasComprou == true)
            {

                item.bolaSprite.sprite = Resources.Load<Sprite>("Sprites/" + b.bolasNomeSprite);
                item.bolaPreco.text = "Comprado!";

                if (PlayerPrefs.HasKey("BTNS"+item.bolaID) == false)
                {
                    item.BtnCompra.GetComponent<compraBola>().btnText.text = "Usando";
                }
            }
            else {

                item.bolaSprite.sprite = Resources.Load<Sprite>("Sprites/" + b.bolasNomeSprite + "_cinza");

            }
        }
    }

    public void UpdateSprite(int bola_id) {

        for (int i = 0; i < bolaSuporteList.Count; i++) {

            BolasSuport bolasSuportScript = bolaSuporteList[i].GetComponent<BolasSuport>();


            if (bolasSuportScript.bolaID == bola_id) {

                for (int j = 0; j < bolaslist.Count; j++) {

                    if (bolaslist[j].bolasID == bola_id) {

                        if (bolaslist[j].bolasComprou == true)
                        {

                            bolasSuportScript.bolaSprite.sprite = Resources.Load<Sprite>("Sprites/" + bolaslist[j].bolasNomeSprite);
                            bolasSuportScript.bolaPreco.text = "Comprado!";
                            SalvaBolasLojaInfo(bolasSuportScript.bolaID);
                        }
                        else {

                            bolasSuportScript.bolaSprite.sprite = Resources.Load<Sprite>("Sprites/" + bolaslist[j].bolasNomeSprite + "_cinza");
                        }
                    }
                }

            }
        }


    }

    void SalvaBolasLojaInfo(int idBola)
    {
        for (int i = 0; i < bolaslist.Count; i++)
        {
            BolasSuport bolasSup = bolaSuporteList[i].GetComponent<BolasSuport>();

            if (bolasSup.bolaID == idBola) {
                PlayerPrefs.SetInt("BTN" + bolasSup.bolaID, bolasSup.BtnCompra ? 1 : 0);
            }
        }
    }


    public void SalvaBolasLOjaText(int idBola, string s) {

        for (int i =0; i< bolaslist.Count;i++)
        {
            BolasSuport bolasSup = bolaSuporteList[i].GetComponent<BolasSuport>();

            if (bolasSup.bolaID == idBola)
            {

                PlayerPrefs.SetString("BTNS" + bolasSup.bolaID, s);

            }
        }
    }

}
