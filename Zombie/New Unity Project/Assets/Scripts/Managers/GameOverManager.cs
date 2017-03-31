using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
    public PlayerHealth playerHealth;
    public Transform GameOverPanel;
    public float restartDelay = 5f;

    Animator anim;
    float restartTimer;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
            restartTimer += Time.deltaTime;
            if(restartTimer >= restartDelay)
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //SceneManager.LoadScene(0);
                GameOver();
            }
        }
	}

    void GameOver()
    {
        //if (GameOverPanel.gameObject.activeInHierarchy == false)
        //{
            GameOverPanel.gameObject.SetActive(true);
            //Time.timeScale = 0;
        //}
    }
}
