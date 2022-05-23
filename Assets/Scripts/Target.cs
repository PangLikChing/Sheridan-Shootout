using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] AudioSource popSound;

    bool scored;

    void Awake()
    {
        //initialize
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        scoreText = GameObject.Find("Score Text (TMP)").GetComponent<TMP_Text>();
    }

    void OnEnable()
    {
        scored = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        //if bullet hits
        if (collision.gameObject.tag == "Bullet")
        {
            if (scored == false)
            {
                //play pop sound
                popSound.Play();
                //increase score count
                gameManager.score += 1;
                scoreText.text = "Score: " + gameManager.score;
                //prevent multi scoring
                scored = true;
                //hide current target
                gameObject.SetActive(false);
                collision.gameObject.SetActive(false);
                //ask Game Manager to spwan new target
                gameManager.hasTarget = false;
            }
        }
    }
}
