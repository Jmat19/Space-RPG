using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteSelf : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;
    Text text;
    void Awake()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
    }
    void Update()
    {
        if (text.index == 1)
        {
            Destroy (image1);
        }
        
        if (text.index == 2)
        {
            SceneManager.LoadScene("ExplorationTest");
        }
    }
}
