using UnityEngine;
using System.Collections;

public class SoldierSpawnButton : MonoBehaviour {

    public Spawner spawner;

    public void SpawnSoldier()
    {
        spawner.soldierCount++;
    }

}
