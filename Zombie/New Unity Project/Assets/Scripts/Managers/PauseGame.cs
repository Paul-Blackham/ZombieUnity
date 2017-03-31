using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    public Transform canvas;
    public Transform GunBarrelEnd;
    	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            GunBarrelEnd.GetComponent<PlayerShooting>().enabled = false;
        }
        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            GunBarrelEnd.GetComponent<PlayerShooting>().enabled = true;
        }
    }

    public void UnPause()
    {
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        GunBarrelEnd.GetComponent<PlayerShooting>().enabled = true;
    }
}
