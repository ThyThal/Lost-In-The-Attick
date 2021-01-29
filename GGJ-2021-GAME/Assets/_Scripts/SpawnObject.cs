using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private Transform[] spawnpoints = null;

    private void Start()
    {
        if (prefab != null) Spawn(prefab);
    }

    public void Spawn(GameObject go)
    {
        var length = spawnpoints.Length;
        var randomPoint = length != 0 ? spawnpoints[Random.Range(0, length - 1)] : null;

        if (randomPoint != null) go.transform.position = randomPoint.position;
    }
}
