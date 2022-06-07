using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider; // reference to slider - health bar
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
    public void SetNumberOfLives(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}
