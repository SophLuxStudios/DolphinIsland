using System;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    //used scripts
    //ObjectPooler objectPooler;

    //used prefabs
    [SerializeField] private GameObject terrainTile;

    //used properties
    private Vector3 nextSpawnPoint;

    private void Start()
    {
        //objectPooler = ObjectPooler.Instance;

        for(int i = 0; i < 3; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        GameObject temp = Instantiate(terrainTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;
    }
}