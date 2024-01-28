using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    private System.Random rand;

    void Start() {
        rand = new System.Random();
    }

    public void SpawnAFish() {
        float randValue = Random.value;
        GameObject fishToSpawn = ChooseRandFish(randValue);

        if (fishToSpawn != null ) { Instantiate(fishToSpawn, transform.position, Quaternion.identity);  }

    }

    private GameObject ChooseRandFish(float randValue) {
        // initialize a list te generate numbers within a range with no repeats
        List<int> numbers = new List<int>();
        for (int i = 0; i < fishPrefabs.Length; i++) { numbers.Add(i); }

        int randomIndex = 0;
        int randNum = 0;
        while (numbers.Count > 0) {
            randomIndex = rand.Next(0, numbers.Count);
            randNum = numbers[randomIndex];

            if (fishPrefabs[randNum].GetComponent<FishEntity>().getSpawnProbability() >= randValue) {
                return fishPrefabs[randomIndex];
            }

            numbers.RemoveAt(randomIndex);
        }

        return null;
    }
}
