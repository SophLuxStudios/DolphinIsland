using UnityEngine;

public class DecorationSpawner : MonoBehaviour
{
    //used classes
    private TerrainTile terrainTile;

    private void Awake()
    {
        terrainTile = GameObject.FindObjectOfType<TerrainTile>();

        SpawnDecoration();
    }

    private void SpawnDecoration()
    {
        //choose a random point to spawn decoration
        int decorationSpawnIndex = Random.Range(0,5);
        Transform decorationSpawnPoint = transform.GetChild(decorationSpawnIndex).transform;

        int isRight;

        if(decorationSpawnIndex < 3)
        {
            isRight = 1;
        }
        else
        {
            isRight = -1;
        }

        //spawn the decoration at chosen position
        terrainTile.SpawnDecoration(decorationSpawnPoint, isRight);
    }
}