using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> spawnedSoldiersList = new List<GameObject>();

    //Used to define an area to spawn the soldiers in
    [Header("Spawn Area")]
    public Vector3 maxSpawnArea;

    public Vector3 minSpawnArea;

    [Space(10)]
    [Header("Object References")]
    //The soldier prefab
    public GameObject soldierPref;

    //Used for organisation
    public GameObject soldierHolder;

    [Space(10)]
    [Header("Other")]
    //count of soldiers
    public int soldierCount;

    //Time between soldier spawns
    public float timeBetweenSpawn;

    //spawned soldier counts
    private int spawnedSoldiers = 0;

    //Time used for spawning
    private float curTime;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        SpawnSoldier(soldierCount);
    }

    //Spawns a soldiers
    private void SpawnSoldier(int count)
    {
        curTime += Time.deltaTime;
        if (spawnedSoldiers < count && curTime > timeBetweenSpawn)
        {
            GameObject soldier = Instantiate(soldierPref, new Vector3(Random.Range(minSpawnArea.x, maxSpawnArea.x), Random.Range(minSpawnArea.y, maxSpawnArea.y), Random.Range(minSpawnArea.z, maxSpawnArea.z)), Quaternion.identity) as GameObject;
            spawnedSoldiersList.Add(soldier);
            soldier.transform.SetParent(soldierHolder.transform);
            spawnedSoldiers++;
            curTime = 0;
        }
    }
}