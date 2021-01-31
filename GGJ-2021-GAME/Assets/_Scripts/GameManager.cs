using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] public string menuScene = "Main Menu";
    [SerializeField] public string gameScene = "Main Game";
    [SerializeField] public string conditionScene = "Condition";
   

    [SerializeField] public LevelLoader levelLoader;
    [SerializeField] public PauseMenuController pauseMenu;

    private AudioSource audioSrc;
    

    public static GameManager Instance;

    [SerializeField] private GameObject[] gameObjects = null;
    private GameObject questObject = null;

    public VictoryTrigger victoryTrigger = null;

    public bool hasWon = false;
    public bool isIngame = false;
    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AssignQuestObject();
        audioSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    
    }
	
    public void PauseGame ()
    {
        if (!isPaused)
        {
            ShowPause();
            isPaused = true;
            Input.ResetInputAxes();
            Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
            isPaused = false;
            HidePause();
        }

    }
    public bool IsPaused()
    {
        return isPaused;
    }

    private void ShowPause()
    {
        pauseMenu.canvasGroup.alpha = 1;
        pauseMenu.background.SetActive(true);
        pauseMenu.pauseButtons.SetActive(true);
        pauseMenu.canvasGroup.interactable = true;
    }

    public void HidePause()
    {
        pauseMenu.canvasGroup.alpha = 0;
        pauseMenu.background.SetActive(false);
        pauseMenu.pauseButtons.SetActive(false);
        pauseMenu.canvasGroup.interactable = false;
    }

    public void SetPause(bool value)
    {

        isPaused = value;
    }

    public void YouWin()
    {
        hasWon = true;
        levelLoader.LoadScene(GameManager.Instance.conditionScene);
    }

    public void GameOver()
    {
        // TODO: poner pantalla de Derrota
        hasWon = false;
        levelLoader.LoadScene(GameManager.Instance.conditionScene);
    }

    public void LoadMenu()
    {
        levelLoader.LoadScene(menuScene);
    }
    public void AssignQuestObject()
    {
        var length = gameObjects.Length;
        questObject = length != 0 ? gameObjects[Random.Range(0, length - 1)] : null;
    }

    public GameObject GetQuestObject()
    {
        return questObject;
    }

    private void OnVictoryHandler()
    {
        print("Ganaste");
        // TODO: poner pantalla de Victoria
    }


    public void PlaySFX (AudioClip clipToPlay, float volume)
    {
        audioSrc.clip = clipToPlay;
        audioSrc.volume = volume;
        audioSrc.Play();
    }

}
