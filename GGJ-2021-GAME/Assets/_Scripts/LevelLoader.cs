using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _transitionTime = 1f;

    private void Start()
    {
        GameManager.Instance.levelLoader = this;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }


    private IEnumerator LoadLevel(string sceneName)
    {
        // Play Animation
        _animator.SetTrigger("Start");
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
