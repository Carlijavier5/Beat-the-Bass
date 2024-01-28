using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    public Transform spawnPoint;

    void Start() {
    }

    public void SpawnAFish() {
        float randValue = Random.Range(0, 1);
        GameObject fishToSpawn = ChooseRandFish(randValue);

        if (fishToSpawn != null ) { Instantiate(fishToSpawn, spawnPoint.position, Quaternion.identity);  }

    }

    private GameObject ChooseRandFish(float randValue) {
        // initialize a list te generate numbers within a range with no repeats
        List<int> numbers = new List<int>();
        for (int i = 0; i < fishPrefabs.Length; i++) { numbers.Add(i); }

        int randomIndex = 0;
        int randNum = 0;
        while (numbers.Count > 0) {
            randomIndex = Random.Range(0, numbers.Count);
            randNum = numbers[randomIndex];

            Debug.Log(randValue);
            if (fishPrefabs[0] == null) { Debug.Log("fish is null"); }
            Debug.Log(fishPrefabs[0].GetComponent<FishEntity>());
            if (fishPrefabs[0].GetComponent<FishEntity>().getSpawnProbability() >= 0.1f) {

                return fishPrefabs[randomIndex];
            }

            randValue -= fishPrefabs[randNum].GetComponent<FishEntity>().getSpawnProbability();
            numbers.RemoveAt(randomIndex);
        }

        return null;
    }
}