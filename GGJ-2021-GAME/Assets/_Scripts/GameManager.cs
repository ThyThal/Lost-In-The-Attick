using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] public string menuScene = "Main Menu";
    [SerializeField] public string gameScene = "Main Game";

    [SerializeField] public LevelLoader levelLoader;
    [SerializeField] public PauseMenuController pauseMenu;

    

    public static GameManager Instance;

    [SerializeField] private GameObject[] gameObjects = null;
    private GameObject questObject = null;

    public VictoryTrigger victoryTrigger = null;

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
        victoryTrigger.OnVictory.AddListener(OnVictoryHandler);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            GameOver();
        }
		
		if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            ShowPause();
            isPaused = true;
            Input.ResetInputAxes();
        }
    }
	
	public bool IsPaused()
    {
        return isPaused;
    }

    private void ShowPause()
    {
        pauseMenu.canvasGroup.alpha = 1;
        pauseMenu.canvasGroup.interactable = true;
    }

    public void HidePause()
    {
        pauseMenu.canvasGroup.alpha = 0;
        pauseMenu.canvasGroup.interactable = false;
    }

    public void SetPause(bool value)
    {

        isPaused = value;
    }

    private void GameOver()
    {
        // TODO: poner pantalla de Derrota
        levelLoader.LoadScene(GameManager.Instance.menuScene);
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
}
