using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitBattle : MonoBehaviour
{
    public void ExitBattlez()
    {
        SceneManager.LoadScene("ExplorationTest");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ExitBattlez();
        }
    }
}
