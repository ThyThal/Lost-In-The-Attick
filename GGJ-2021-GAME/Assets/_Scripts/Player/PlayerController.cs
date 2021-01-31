using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Flashlight _flashlight;


    public bool questObjectPickedUp;

    void Start()
    {
        questObjectPickedUp = false;
    }


    public void PickedObject()
    {
        questObjectPickedUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsPaused())
        {
            _flashlight.LightMove();
        }
    }
}
