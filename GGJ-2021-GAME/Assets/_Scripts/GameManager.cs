using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] public string menuScene = "Main Menu";
    [SerializeField] public string gameScene = "Main Game";

    [SerializeField] public LevelLoader levelLoader;

    public static GameManager Instance;

    [SerializeField] private PauseMenuController pauseMenu = null;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            GameOver();
        }
		
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = true;
            pauseMenu.gameObject.SetActive(true);
        }
    }
	
	public bool IsPaused()
    {
        return isPaused;
    }

    public void SetPause(bool value)
    {
        isPaused = value;
    }

    private void GameOver()
    {
        levelLoader.LoadScene(GameManager.Instance.menuScene);
    }
}
