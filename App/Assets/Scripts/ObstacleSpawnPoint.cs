using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnPoint : MonoBehaviour
{
    //public ObjectPooler theObjectPooler;
    public GameObject obstacle;

    // Start is called before the first frame update
    void Start()
    {
       // theObjectPooler = FindObjectOfType<ObjectPooler>();

       /* transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z); // zmień pozycję o szeokość platformy
        GameObject newObstacle = theObjectPooler.GetPooledObject(); // calling the function from other script  and getting it to newObject gameObject

        newObstacle.transform.position = transform.position; // plaicing the object in the right place
        newObstacle.transform.rotation = transform.rotation;
        newObstacle.SetActive(true); // setting the object active
                                   */
        Instantiate(obstacle,transform.position, Quaternion.identity);
    }

}
