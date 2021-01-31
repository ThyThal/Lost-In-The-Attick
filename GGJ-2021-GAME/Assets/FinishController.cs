using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FinishController : MonoBehaviour
{
    [SerializeField] private Image _winImage;
    [SerializeField] private Image _loseImage;
    [SerializeField] private LevelLoader _levelLoader;

    private void Start()
    {
        if (GameManager.Instance.hasWon) { _winImage.enabled = true; }
        else { _loseImage.enabled = true; }
    }

    public void OnClickMenu()
    {
        _levelLoader.LoadScene(GameManager.Instance.menuScene);
    }

    public void OnClickRestart()
    {
        _levelLoader.LoadScene(GameManager.Instance.gameScene);
    }

    public void OnClickExit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
        Application.Quit();
    }
}
