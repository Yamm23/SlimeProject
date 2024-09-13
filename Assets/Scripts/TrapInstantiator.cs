using UnityEngine;
using System.Collections.Generic;

public class Instantiator : MonoBehaviour
{
    public GameObject[] trapPrefabs;   // Array to store different trap prefabs
    public Transform[] spawnPoints;    // Array of spawn points where traps will be instantiated

    // Start is called before the first frame update
    void Start()
    {
        InstantiateTraps();
    }

    void InstantiateTraps()
    {
        // Make sure we have enough spawn points for all traps
        if (spawnPoints.Length < trapPrefabs.Length)
        {
            Debug.LogError("Not enough spawn points for all traps!");
            return;
        }

        // Create lists to hold available traps and spawn points
        List<GameObject> remainingTraps = new List<GameObject>(trapPrefabs);
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        // Loop through and ensure every trap prefab gets instantiated
        while (remainingTraps.Count > 0)
        {
            // Pick a random trap and a random spawn point
            int trapIndex = Random.Range(0, remainingTraps.Count);
            int spawnIndex = Random.Range(0, availableSpawnPoints.Count);

            GameObject selectedTrap = remainingTraps[trapIndex];
            Transform spawnPoint = availableSpawnPoints[spawnIndex];

            // Instantiate the trap at the spawn point
            Instantiate(selectedTrap, spawnPoint.position, spawnPoint.rotation);

            // Remove the trap and spawn point from their respective lists
            remainingTraps.RemoveAt(trapIndex);
            availableSpawnPoints.RemoveAt(spawnIndex);
        }
    }
}
