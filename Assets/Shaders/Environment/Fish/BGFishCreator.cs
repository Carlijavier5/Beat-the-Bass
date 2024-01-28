using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class BGFishCreator : MonoBehaviour {
    [SerializeField] private float height;
    [SerializeField] private float width;
    [SerializeField] private float destroyDelay;
    [SerializeField] private float spawnDelay;

    [SerializeField] private List<BackgroundFish> fishPool;
    
    private IEnumerator activeSpawn = null;

    private void Update() {
        if (activeSpawn == null) {
            activeSpawn = SpawnFishAction();
            StartCoroutine(activeSpawn);
        }
    }

    private IEnumerator SpawnFishAction() {
        Vector3 localPos = transform.position;
        Vector3 spawnPos = localPos + new Vector3(Random.Range(-width + 1, width), 0f, Random.Range(-height + 1, height));
        Debug.Log(spawnPos.ToString());
        int index = Random.Range(0, fishPool.Count);
        GameObject fish = Instantiate(fishPool[index].gameObject, spawnPos, Quaternion.identity);
        Destroy(fish, destroyDelay);
        yield return new WaitForSeconds(spawnDelay);
        activeSpawn = null;
        yield return null;
    }
}
