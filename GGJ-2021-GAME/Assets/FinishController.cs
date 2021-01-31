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

    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip lose;
    private AudioSource audioSrc;

    private void Start()
    {

        audioSrc = GetComponent<AudioSource>();
        if (GameManager.Instance.hasWon) 
        { 
            _winImage.enabled = true;
            audioSrc.clip = win;
        }
        else
        { 
            _loseImage.enabled = true;
            audioSrc.clip = lose;
        }


        audioSrc.Play();
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
