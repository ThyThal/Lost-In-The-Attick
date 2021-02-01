using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private Animator _animator;

    [SerializeField] private AudioSource PressSound = null;

    [SerializeField] private AudioClip soundIn = null;
    [SerializeField] private AudioClip soundOut = null;

    private void Start()
    {

    }

    void Update()
    {
        
    }

    public void OnClickPlay() 
    {
        PlaySound(soundIn);
        GameManager.Instance.isIngame = true; 
        GameManager.Instance.SetPause(false);
        _levelLoader.LoadScene(GameManager.Instance.gameScene);
        
    }
    public void OnClickCredits() 
    {
        PlaySound(soundIn);

        _animator.SetTrigger("ShowCredits"); 
    }
    public void OnClickBack() 
    {
        PlaySound(soundOut);
        
        _animator.SetTrigger("HideCredits"); 
    }
    public void OnClickHelp() 
    {
;       
    }

    public void OnClickExit() 
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
        Application.Quit();
    }

    private void PlaySound(AudioClip soundToPlay)
    {
        PressSound.clip = soundToPlay;

        PressSound.Play(); 
    }
}
