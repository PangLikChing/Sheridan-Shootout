using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCNancy : MonoBehaviour
{
    GameObject player;
    GameManager gameManager;
    [SerializeField] float distance;
    [SerializeField] GameObject instructionText;

    void Awake()
    {
        //initialize
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        //check distance
        if(Vector3.Distance(gameObject.transform.position, player.transform.position) <= distance)
        {
            //play instruction
            instructionText.SetActive(true);
        }
        else
        {
            //hide instruction
            instructionText.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //start game
        if(collision.gameObject.tag == "Bullet")
        {
            //select the target places that the NPC is reponding to
            gameManager.targetPlaces = transform.parent.GetChild(6).gameObject;
            //tell the Game Manager to start the game
            gameManager.isGameStart = true;
            //set all NPCs inactive to prevent starting multiple games
            for(int i = 0; i < transform.parent.parent.childCount; i++)
            {
                transform.parent.parent.GetChild(i).GetChild(5).gameObject.SetActive(false);
            }
        }
    }
}
