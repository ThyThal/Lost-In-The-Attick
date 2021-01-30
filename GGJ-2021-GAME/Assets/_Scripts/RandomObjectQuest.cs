using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectQuest : MonoBehaviour
{
    // Que seleccione un Objeto Random de una lista
    // Que lo instancie usando el SpawnObject
    // Que le diga al GameManager que Objeto es Condición de Victoria

    [SerializeField] private GameObject[] gameObjects = null;

    private void Start()
    {
        var length = gameObjects.Length;
        var randomObject = length != 0 ? gameObjects[Random.Range(0, length - 1)] : null;
    }
}
