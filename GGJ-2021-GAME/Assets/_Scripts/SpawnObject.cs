using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private QuestItem player = null;
    [SerializeField] private GameObject[] prefabs = null;
    [SerializeField] private Transform[] spawnpoints = null;
    [SerializeField] private FlavorFullOldMan oldManDialogue;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        var lengthSpawnpoints = spawnpoints.Length;
        var randomSpawnpoint = lengthSpawnpoints != 0 ? spawnpoints[Random.Range(0, lengthSpawnpoints - 1)] : null;

        var lengthPrefabs = prefabs.Length;
        var randomPrefab = lengthPrefabs != 0 ? prefabs[Random.Range(0, lengthPrefabs - 1)] : null;

        oldManDialogue.CheckQuestItemNumberAndSprite(randomPrefab.GetComponent<ObjectQuestPickedUp>().ItemID);

        //print($"Prefab: {randomPrefab} - Spawnpoint: {randomSpawnpoint}");

        //if (randomPoint != null) go.transform.position = randomPoint.position;
        if (randomSpawnpoint != null && randomPrefab != null)
        {
            var clone = Instantiate(randomPrefab, randomSpawnpoint.position, Quaternion.identity);
            player.AssignQuestObject(clone);
        }
    }
}
