using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    #region variables
    public float speed;
    [SerializeField] private SpriteRenderer playerImage;
    private Rigidbody2D myRigidBody;
    public ActionsPlayer playerControls ;
    public bool haveSomethinToInteractWith, turningRight, alreadyTurned, alreadyUnlockedMovement;
    #endregion
    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        playerControls = new ActionsPlayer();
    }
    #region Fixed Update
    private void FixedUpdate()
    {
        Move();
    }
    #endregion
    #region Move
    private void Move()
    {
        Vector2 move = playerControls.PlayerActions.Movement.ReadValue<Vector2>();
        if (move.magnitude > .5)
        {
            //myAnimator.SetBool("Running", true);
            float x, y;
            x = move.x;
            y = move.y;
            myRigidBody.velocity = new Vector2((x * speed), (y * speed));
            if (x > 0)
            {
                TurnRight();
            }
            if (x < 0)
            {
                TurnLeft();
            }
        }
        if (move.magnitude < .2)
        {
            if (alreadyUnlockedMovement)
            {
                myRigidBody.velocity = Vector2.zero;
                StoppedWalking();
                alreadyUnlockedMovement = false;
            }
        }
        else
        {
            if (!alreadyUnlockedMovement)
            {
                //StartedWalking();
                Debug.Log("unlocked");
                alreadyUnlockedMovement = true;
            }
        }
    }
    #endregion
    #region sound walking
    public void StartedWalking()
    {
        //AudioController.PlayContinuosSound(AudioController.ContinuosSound.PlayerRunning);
    }
    public void StoppedWalking()
    {
       // AudioController.StopContinuosSound(AudioController.ContinuosSound.PlayerRunning);
    }
    #endregion
    #region sideChange
    #region turnLeft
    private void TurnLeft()
    {

        if (turningRight)
        {
            turningRight = false;
            alreadyTurned = false;
        }
        if (!turningRight && !alreadyTurned)
        {
            playerImage.flipX = true;
            alreadyTurned = true;
        }
    }
    #endregion
    #region turnRight
    private void TurnRight()
    {
        if (!turningRight)
        {
            turningRight = true;
            alreadyTurned = false;
        }
        if (turningRight && !alreadyTurned)
        {
            playerImage.flipX = false;
            alreadyTurned = true;
        }
    }
    #endregion
    #endregion
    #region onEnableonDisable
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    #endregion
}
