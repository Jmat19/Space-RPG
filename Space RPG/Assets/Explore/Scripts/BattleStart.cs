using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleStart : MonoBehaviour
{

    private bool isDead;
    private void Start()
    {
        kys();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //finishSound.Play();
            Destroy(this.gameObject);
            isDead = true;
            SceneManager.LoadScene("TestTransScene", LoadSceneMode.Additive);
        }
    }

    private void kys()
    {
        if (isDead == true)
        {
            Destroy(this.gameObject);
        }
    }
}
