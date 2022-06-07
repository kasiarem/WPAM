using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // -----------------------------------spawn variables----------------
    public float timeBtwSpawn;  //time we are counting down from
    public float startTimeBtwSpawn =10;  // time from which we want to count down from and then spawn

    public float timeBtwSpawnRed;  //time we are counting down from
    public float startTimeBtwSpawnRed = 32;

    public ObjectPooler[] theDronePools; // array 
    public bool keepSpawning = true; // do we want to spawn objects

    private SpawnPoint theNewSpawn;
    private AudioManager theAudio;

    public Transform pointToSpawn;

    private void Start()
    {
        theNewSpawn = FindObjectOfType<SpawnPoint>();
        theAudio = FindObjectOfType<AudioManager>();
        startTimeBtwSpawnRed = 32;
        startTimeBtwSpawn = 10;
    }
    void Update()
    {
        if (keepSpawning)
        {
            if (timeBtwSpawn <= 0) // time for new bomb
            {
                timeBtwSpawn = startTimeBtwSpawn; // reset timer to next spawn
                SpawnPlease(0);
                theAudio.PlayDisturbance();
            }

            else    // take the time from timebtwspawn
            {
                timeBtwSpawn -= Time.deltaTime;
            }
            if (timeBtwSpawnRed <= 0) // time for new bomb
            {
                timeBtwSpawnRed = startTimeBtwSpawnRed; // reset timer to next spawn
                SpawnPlease(1);
                theAudio.PlayDisturbance();

            }

            else    // take the time from timebtwspawn
            {
                timeBtwSpawnRed -= Time.deltaTime;
            }

        }

    }
    void SpawnPlease(int s)
    {

        GameObject newObstacle = theDronePools[s].GetPooledObject(); // get an object of the type/ number pointed by array position 

        if (newObstacle != null) // if it is not null
        {
            theNewSpawn.GetNewLocation();
            newObstacle.transform.position = pointToSpawn.transform.position  ;// give it a position
            newObstacle.transform.rotation = Quaternion.Euler(0, Random.Range(-120, 180), 0);
            newObstacle.SetActive(true); //set it active
        }

    }
    
}
