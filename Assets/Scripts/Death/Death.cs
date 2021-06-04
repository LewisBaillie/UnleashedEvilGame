using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{

    //Do stuff when you die
    public void playerDie()
    {
        SceneManager.LoadScene(4);
        Cursor.lockState = CursorLockMode.None;
    }
}
