using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMana : MonoBehaviour
{
    public string sceneName;
    public float waitTime;
    public Animator musicAnim;

    public IEnumerator ChangeScene(){
        musicAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(waitTime);
    }
}
