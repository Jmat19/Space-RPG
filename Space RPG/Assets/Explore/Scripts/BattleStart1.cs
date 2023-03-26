using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleStart1 : MonoBehaviour
{
    Values values;
    PlayerMovement playerMovement;
    void Awake()
    {
        values = GameObject.Find("Godsend").GetComponent<Values>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        kys();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            values.enemy2dead = true;
            values.heldposX = playerMovement.rb.position.x;
            values.heldposY = playerMovement.rb.position.y;
            Destroy(this.gameObject);
            SceneManager.LoadScene("TestTransScene");
        }
    }

    private void kys()
    {
        if (values.enemy2dead == true)
        {
            Destroy(this.gameObject);
        }
    }
}
