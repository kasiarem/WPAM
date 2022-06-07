using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int damage = 1;
    public float speed;

    public GameObject effect; // reference to the effect that will show after bumping into the obstacle

//    private HealthBar theHealthBar;

    // Start is called before the first frame update
    void Start()
    {
       // theHealthBar = FindObjectOfType<HealthBar>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);


    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) ; //jeśli rzecz z którą się zderza jest otagowana Player
        {
            // we iniciate the effect of destruction
            Instantiate(effect, transform.position, Quaternion.identity);
            // uszkadzamy naszego gracza
            // zmniejszamy liczbę żyć
            other.GetComponent<Player>().health -= damage; // weż element o nazwie health ze skryptu Player
            Debug.Log(other.GetComponent<Player>().health);
            gameObject.SetActive(false); //zniszcz przeszkodę jak już obciąży naszego gracza
               
        }
    }
}
