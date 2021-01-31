using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    private GameObject questObject = null;

    public void AssignQuestObject(GameObject go)
    {
        questObject = go;
    }

    public GameObject GetQuestObject()
    {
        return questObject;
    }
}
