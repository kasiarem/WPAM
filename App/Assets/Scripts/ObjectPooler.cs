using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject pooledObject; // reference to  an gameobject we want to pool

    public int pooledAmount; // how many we are going to use

    List<GameObject> pooledObjects; // lista dla różnych prefabów które beda wywoływane

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>(); // to make sure that we get clear list

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject); // casting whaat is on right to makke sure it is a Game object type
            obj.SetActive(false);
            pooledObjects.Add(obj); //adding obj to the list
        }


    }
    // new function public so we can call it everywhere in game
    public GameObject GetPooledObject() //looking and getting a pooled object
    {
        for (int i = 0; i < pooledObjects.Count; i++) // i< than amount of object in our list pooledObjects
        {
            if (!pooledObjects[i].activeInHierarchy) //checking if the object is no active
            {
                return pooledObjects[i]; // getting the first nonactive object 
            }

        }
        // it is possible that we can't find an nonactive object so we need to create one
        GameObject obje = (GameObject)Instantiate(pooledObject); // casting whaat is on right to makke sure it is a Game object type
        obje.SetActive(false);
        pooledObjects.Add(obje); //adding obj to the list
        return obje; //returning created object
    }

}
