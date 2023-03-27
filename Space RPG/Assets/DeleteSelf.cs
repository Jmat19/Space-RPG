using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteSelf : MonoBehaviour
{
    SceneMana sceneMana;
    public GameObject image1;
    public GameObject image2;
    public GameObject deleteplease;
    Text text;
    public float waitTime;
    public Animator musicAnim;
    void Awake()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
        sceneMana = GameObject.Find("SceneManager").GetComponent<SceneMana>();
    }
    void Update()
    {
        if (text.index == 1)
        {
            Destroy (image1);
        }
        
        if (text.index == 2)
        {
            Destroy (deleteplease);
            SceneManager.LoadScene("ExplorationTest");
        }
    }
    public IEnumerator ChangeScene(){
        musicAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(waitTime);
    }

}
