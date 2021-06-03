using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private KeyCode _PauseMenuKey;
    [SerializeField]
    private Text _PauseMenuText;
    [SerializeField]
    private Button _ResumeButton;
    [SerializeField]
    private Button _SaveButton;
    [SerializeField]
    private Button _LoadButton;
    [SerializeField]
    private Button _QuitButton;
    [SerializeField]
    private GameObject[] _ItemsToPause;


    private void Start()
    {
        _PauseMenuText.gameObject.SetActive(false);
        _ResumeButton.gameObject.SetActive(false);
        _SaveButton.gameObject.SetActive(false);
        _LoadButton.gameObject.SetActive(false);
        _QuitButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(_PauseMenuKey))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (_PauseMenuText.gameObject.activeSelf == false)
        {
            _PauseMenuText.gameObject.SetActive(true);
            _ResumeButton.gameObject.SetActive(true);
            _SaveButton.gameObject.SetActive(true);
            _LoadButton.gameObject.SetActive(true);
            _QuitButton.gameObject.SetActive(true);
            foreach (GameObject item in _ItemsToPause)
            {
                if (item.GetComponent<PlayerObj>() != null)
                {
                    item.GetComponent<PlayerObj>().Paused(true);
                }
                else
                {
                    item.SetActive(false);
                }
            }
        }
        else
        {
            _PauseMenuText.gameObject.SetActive(false);
            _ResumeButton.gameObject.SetActive(false);
            _SaveButton.gameObject.SetActive(false);
            _LoadButton.gameObject.SetActive(false);
            _QuitButton.gameObject.SetActive(false);
            foreach (GameObject item in _ItemsToPause)
            {
                if (item.GetComponent<PlayerObj>() != null)
                {
                    item.GetComponent<PlayerObj>().Paused(false);
                }
                else
                {
                    item.SetActive(true);
                }
            }
        }
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
