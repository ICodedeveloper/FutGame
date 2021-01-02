using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoedasControlCoin : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D outro)
    {

        if (outro.gameObject.CompareTag("bola")) {

            ScoreManager.instance.ColetaMoedas(10);
            Destroy(this.gameObject);
            AudioManager.instance.SonsFXToca(0);
        }


    }


}
