using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject bullets, gameOverPanel;
    [SerializeField] Transform playerCameraTransform;
    [SerializeField] KeyCode shoot, quit;
    [SerializeField] TMP_Text scoreText, ammoText;
    [SerializeField] float firePower;
    [SerializeField] AudioSource shootingSound;
    [SerializeField] Sprite youWinImage, tryAgainImage;

    [SerializeField] MouseLooker mouseLooker;

    bool isGameOver;
    short outOfGameBullet, currentBullet, maxBullet;
    GameObject currentBulletGameObject;

    public bool isGameStart, hasTarget;
    public short score;
    public GameObject targetPlaces;

    void Awake()
    {
        //initialize
        maxBullet = 30;
        hasTarget = false;
        isGameOver = false;
        isGameStart = false;
        score = 0;
        outOfGameBullet = 0;
        currentBullet = 0;
        scoreText.text = "Score: " + score;
        targetPlaces = null;
    }

    void FixedUpdate()
    {
        //just in case if the score is higher than 20 for some unknown reason
        if (score >= 20)
        {
            //player win
            //tell the Game Manager that the game is over
            isGameOver = true;
            isGameStart = false;
            //unlock the camera
            mouseLooker.LockCursor(false);
            mouseLooker.enabled = false;
            //show gameOverPanel
            gameOverPanel.SetActive(true);
        }

        //if all ammo drainned and player have not win and see if the last bullet is landed
        else if ((currentBullet >= maxBullet) && score < 20 && (bullets.transform.GetChild(currentBullet - 1).gameObject.tag == "Untagged" || bullets.transform.GetChild(currentBullet - 1).gameObject.activeSelf == false))
        {
            //player lose
            //tell the Game Manager that the game is over
            isGameOver = true;
            isGameStart = false;
            //unlock the camera
            mouseLooker.LockCursor(false);
            mouseLooker.enabled = false;
            //change Game Over Image to try again
            gameOverPanel.transform.GetChild(0).GetComponent<Image>().sprite = tryAgainImage;
            //show gameOverPanel
            gameOverPanel.SetActive(true);
        }
    }

    void Update()
    {
        //Game Loop
        if(isGameStart == true)
        {
            //show game UI
            ammoText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(true);

            //spawn target
            spawnTarget();
        }
        else
        {
            //hide game UI
            ammoText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
        }

        //if game have not start yet
        if(Input.GetKeyDown(shoot) && isGameStart == false)
        {
            //if player lose and tries to fire
            if ((currentBullet >= maxBullet) && score < 20)
            {
                //ammo is 0
                Debug.Log("No ammo");
            }
            else
            {
                //reset outOfGameBullet if it is larger than 29
                if (outOfGameBullet >= maxBullet)
                {
                    outOfGameBullet = 0;
                }
                //set the bullet that needs to be fired
                currentBulletGameObject = bullets.transform.GetChild(outOfGameBullet).gameObject;
                //shoot
                shooting();
                outOfGameBullet++;
            }
        }

        //if game started and player have not lose yet
        if (Input.GetKeyDown(shoot) && isGameOver == false && isGameStart == true)
        {
            if(currentBullet < maxBullet)
            {
                //set the bullet that needs to be fired
                currentBulletGameObject = bullets.transform.GetChild(currentBullet).gameObject;
                //shoot
                shooting();
                //drain ammo
                currentBullet++;
                ammoText.text = maxBullet - currentBullet + " / " + maxBullet;
            }
            else
            {
                //Error
                Debug.Log("Error");
            }
        }

        //during game over
        if (isGameOver == true)
        {
            //reset after geting any mouse click or key input
            if (Input.anyKeyDown)
            {
                reset();
            }
        }

        //Quit game
        if (Input.GetKeyDown(quit))
        {
            Application.Quit();
        }
    }

    void shooting()
    {
        //allow scoring with that bullet
        currentBulletGameObject.tag = "Bullet";
        //show bullet GameObject
        currentBulletGameObject.SetActive(true);
        //set velocity to 0
        currentBulletGameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //reset bullet position
        currentBulletGameObject.transform.position = playerCameraTransform.position + playerCameraTransform.forward;
        //add force to the bullet
        currentBulletGameObject.GetComponent<Rigidbody>().AddForce(playerCameraTransform.forward * firePower, ForceMode.VelocityChange);
        //play shooting sound
        shootingSound.Play();
    }

    void spawnTarget()
    {
        if (!hasTarget && score < 20)
        {
            //set target active
            targetPlaces.transform.GetChild(Random.Range(0, 10)).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            //tell the Game Manager that target exists
            hasTarget = true;
        }
    }

    void reset()
    {
        //re-initialization
        hasTarget = false;
        isGameOver = false;
        isGameStart = false;
        score = 0;
        currentBullet = 0;
        scoreText.text = "Score: " + score;

        //set all NPC active
        for(int i = 0; i < targetPlaces.transform.parent.parent.childCount; i++)
        {
            targetPlaces.transform.parent.parent.GetChild(i).GetChild(5).gameObject.SetActive(true);
        }

        //hide all target just in case the player lose
        for (int i = 0; i < targetPlaces.transform.childCount; i++)
        {
            targetPlaces.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
        }

        //lock the mouse again
        mouseLooker.LockCursor(true);
        mouseLooker.enabled = true;

        //hide the gameOverPanel
        gameOverPanel.SetActive(false);
    }
}
