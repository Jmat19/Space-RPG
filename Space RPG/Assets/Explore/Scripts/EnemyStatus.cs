using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private bool hasBeenKilled;
    public GameObject enemy;

    private void Start()
    {
        if (hasBeenKilled == true)
        {
            Destroy(enemy);
        }
    }
}
