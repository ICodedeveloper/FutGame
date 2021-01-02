using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BotoesMenu : MonoBehaviour
{


    private Animator ConfAnim;
    private bool sobe;

    public void Jogar() {

        SceneManager.LoadScene(1);

    }



    public void AnimMenu() {

        ConfAnim = GameObject.FindGameObjectWithTag("AnimBack").GetComponent<Animator>();
        if (sobe == false) {

            ConfAnim.Play("SobeMenu");
            sobe = true;
        }
        else
        {
            ConfAnim.Play("DesceMenu");
            sobe = false;
        }
        
    }

}
