using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Vector2 targetPosition; // vektor (x,y) w którym będzie pozycja naszego player
    public float IncrementY; // zmienna okeślająca o ile ma się player przesunąć 
    public float speed; // prędkość zmiany położenia

    public float maxHeigh;
    public float minHeigh;

    public GameObject deathScreen; // reference to deathscreen

    public int health; // liczba żyć

    public GameObject effect;

    private ScoreManager theScoreManager; // ref to our score manager script

   private Restart theRestart; //ref to restart script
    private HealthBar theHealthBar; // ref to health bar script

    // Start is called before the first frame update
    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();
        theHealthBar = FindObjectOfType<HealthBar>(); // finding objects
        theHealthBar.SetNumberOfLives(health);
    }

    // Update is called once per frame
    void Update()
    {
        theHealthBar.SetHealth(health);
        if (health <=0) // sprawdzanie czy żyjemy
        {
            theScoreManager.scoreIncrease = false;
            deathScreen.SetActive(true);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //ponowne odpalenie sceny gdy skończyły nam się życia
            //    theRestart.GameRestart();
            // reseting score
            // theScoreManager.scoreCount = 0;
            // theScoreManager.scoreIncrease = true;
            Destroy(gameObject);

        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed*Time.deltaTime); // gładsze przejścia położenia dzieki movetowards
        //Time.deltaTime by player poruszał się z tą samą prędkością na różnych komputerach -> bardzo szybki komputer będzie sprawiał że nasz player bedzie się dużo szybciej poruszal, a stary
        // z bugami będzie powodował mocne spowolnienie Playera

      

        if (Input.GetKeyDown(KeyCode.UpArrow) && (transform.position.y < maxHeigh)) // jeśli klikniemy strzałkę w  góre
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            targetPosition = new Vector2(transform.position.x, transform.position.y + IncrementY); // x zostaje taki sam, zmieniam tylko y
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && (transform.position.y > minHeigh)) // jeśli klkniemy strzałke w dół
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            targetPosition = new Vector2(transform.position.x, transform.position.y - IncrementY);
        }
    }
}
