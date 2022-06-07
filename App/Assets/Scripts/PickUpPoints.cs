using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoints : MonoBehaviour
{
    public int pointsToGive; // amount of points to add

    private ScoreManager theScoreManager;// reference to the type scoreManager 
   // private Obstacle theObstacle;
    private float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>(); // finding our score manager script
 //       theObstacle = FindObjectOfType<Obstacle>();
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D other) // build in functio in unity - when sth with collider bump into this object
    {
        if(other.gameObject.name == "Player") // if sth what bumped into object has a tag "Player"
        {
            theScoreManager.addPoints(pointsToGive); // using the function from scoremanager to add points
            gameObject.SetActive(false); // so the coin will disappear
        }
    }
}
