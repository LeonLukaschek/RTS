using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    //Used to define an area to spawn the soldiers in
    [Header("Spawn Area")]
    public Vector3 maxSpawnArea;
    public Vector3 minSpawnArea;

    [Space (10)]
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
    public int timeBetweenSpawn;
    //spawned soldier cont
    private int spawnedSoldiers = 0;
    //Time used for spawning
    private float curTime;

    void Start()
    {
    }

    void FixedUpdate()
    {
        SpawnSoldier(soldierCount);
    }
    //Spawns a soldiers
    void SpawnSoldier(int count)
    {
        curTime += Time.deltaTime;
        if (spawnedSoldiers < count && curTime > timeBetweenSpawn)
        {
            Debug.Log("Spawning a soldier");

            GameObject soldier = Instantiate(soldierPref, new Vector3(Random.Range(minSpawnArea.x, maxSpawnArea.x), Random.Range(minSpawnArea.y, maxSpawnArea.y), Random.Range(minSpawnArea.z, maxSpawnArea.z)), Quaternion.identity) as GameObject;
            //Add soldier to soldierHolder GameObject
            soldier.transform.SetParent(soldierHolder.transform);
            spawnedSoldiers++;
            curTime = 0;
        }
    }
}
