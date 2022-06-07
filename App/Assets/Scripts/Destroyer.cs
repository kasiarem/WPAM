using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameObject destructionPoint; // place when the platforms will be deactivate
    // Start is called before the first frame update
    void Start()
    {
        destructionPoint = GameObject.Find("DegenerationPoint"); // finding destructionPoint at the start of the game
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < destructionPoint.transform.position.x)
        {
            gameObject.SetActive(false); //deaktywacja game object
        }
    }
}
