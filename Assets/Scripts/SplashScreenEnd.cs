using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitForAnim());
    }

    IEnumerator waitForAnim()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(1);
    }

}
