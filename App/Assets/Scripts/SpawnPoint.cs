using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Transform spawnPoint;
    const float maxX = 6;
    const float minY = 0.1f;
    const float maxY = 1.3f;
    const float minZ = 3;
    const float maxZ = 8;

    public void GetNewLocation()
    {
        Vector3 spawnPointT = new Vector3(Random.Range(-maxX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        spawnPoint.transform.position = spawnPointT;
    }
}
