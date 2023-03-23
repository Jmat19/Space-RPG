using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleStart : MonoBehaviour
{
    //private AudioSource finishSound;
    private void Start()
    {
        //finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //finishSound.Play();
            Destroy(gameObject);
            SceneManager.LoadScene("TestTransScene", LoadSceneMode.Additive);
        }
    }
}
