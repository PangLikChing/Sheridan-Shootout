using UnityEngine;
using System.Collections;

public class MouseLooker : MonoBehaviour {

	Quaternion characterTargetRotation, cameraTargetRotation;
    [SerializeField] KeyCode cancel, fire;
	[SerializeField] Transform playerTransform;
	[SerializeField] Transform cameraTransform;

	void Awake() {
		// start the game with the cursor locked
		LockCursor (true);

		// get the location rotation of the character and the camera
		characterTargetRotation = playerTransform.localRotation;
		cameraTargetRotation = cameraTransform.localRotation;
	}
	
	void Update() {
		// rotate stuff based on the mouse
		LookRotation ();

		// if ESCAPE key is pressed, then unlock the cursor
		if(Input.GetKeyDown(cancel)){
			LockCursor (false);
		}

		// if the player fires, then relock the cursor
		if(Input.GetKeyDown(fire)){
			LockCursor (true);
		}
	}
	
	public void LockCursor(bool isLocked)
	{
		if (isLocked) 
		{
			// make the mouse pointer invisible
			Cursor.visible = false;

			// lock the mouse pointer within the game area
			Cursor.lockState = CursorLockMode.Locked;
		} else {
			// make the mouse pointer visible
			Cursor.visible = true;

			// unlock the mouse pointer so player can click on other windows
			Cursor.lockState = CursorLockMode.None;
		}
	}

	public void LookRotation()
	{
		//get the y and x rotation based on the Input manager
		float yRot = Input.GetAxis("Mouse X");
		float xRot = Input.GetAxis("Mouse Y");

		// calculate the rotation
		characterTargetRotation *= Quaternion.Euler (0f, yRot, 0f);
		cameraTargetRotation *= Quaternion.Euler (-xRot, 0f, 0f);

        playerTransform.localRotation = characterTargetRotation;
        cameraTransform.localRotation = cameraTargetRotation;
	}
}
