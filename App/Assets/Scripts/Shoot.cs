using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject hitEffect;
    private DroneScript theDrone;
    private AudioManager theAudio;

    private void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        theDrone = FindObjectOfType<DroneScript>();
    }

    public void ShootNow()
    {
        Vibrations.Vibrate(100);
        theAudio.PlatShoot();

        // from the center of phone
        RaycastHit hit;
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            GameObject go = hit.collider.gameObject;
            if (hit.transform.name == "DroneWhite(Clone)" || hit.transform.name == "DroneRed(Clone)")
            {
                
                Vibrations.Vibrate();
                StartCoroutine(go.GetComponent<DroneScript>().GotShotDown());
                //switch off 
                //hit.transform.gameObject.SetActive(false);
                //Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
