using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonMainMenu;
    [SerializeField] private Button buttonHelp;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private Button buttonGoBack;

    [Header("Menu")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject helpMenu;

    //Extras
    private bool pauseMenuActive;


    void Start()
    {
        buttonResume.onClick.AddListener(OnClickResume);
        buttonHelp.onClick.AddListener(OnClickHelp);
        buttonQuit.onClick.AddListener(OnClickQuit);
        buttonGoBack.onClick.AddListener(OnClickGoBack);
        buttonMainMenu.onClick.AddListener(OnClickMenu);
        pauseMenu.SetActive(true);
        pauseMenuActive = true;
        helpMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuActive)
            {
                OnClickResume();
            } else
            {
                OnClickGoBack();
            }
        }
    }

    public void OnClickResume() 
    { 
        SceneManager.LoadScene("GameScene"); 
    }

    public void OnClickHelp() 
    {
        if (pauseMenuActive)
        {
            pauseMenuActive = false;
            pauseMenu.SetActive(false);
            helpMenu.SetActive(true);
        }
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickGoBack() 
    {
        if (!pauseMenuActive)
        {
            pauseMenuActive = true;
            pauseMenu.SetActive(true);
            helpMenu.SetActive(false);
        }
    }

    public void OnClickQuit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
        Application.Quit();
    }
}
