using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlay() { SceneManager.LoadScene("Game"); }
    public void OnClickCredits() { _animator.SetTrigger("ShowCredits"); }
    public void OnClickHelp() { }
    public void OnClickBack() { _animator.SetTrigger("HideCredits"); }
    public void OnClickExit() {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
        Application.Quit();
    }
}
