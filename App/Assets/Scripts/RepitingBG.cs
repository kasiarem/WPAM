using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepitingBG : MonoBehaviour
{
    public float speed;

    public float endX;
    public float startX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x <= endX) // jesli nasza platforma wyszła za ekran
        {
            Vector2 pos = new Vector2(startX, transform.position.y); // wektor z pozycja startową = startX
            transform.position = pos;   // przypisuje temu obiektowi wartości wektora pos - zmieniam składową x na startX
        }

    }
}
