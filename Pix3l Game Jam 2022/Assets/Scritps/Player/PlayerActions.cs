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
    [Header("Raycasts Walls")]
    [SerializeField] private LayerMask InteractableLayers;
    [SerializeField] private BoxCollider2D myTriggerCollider;
    private GameObject touchingObjectRight, touchingObjectLeft;
    private IInteractable tempIInteractable;
    public float leftWallRayExtra;
    public float rightWallRayExtra;
    public float wallRaysHeights;
    public float rayWallsLenghts;
    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        playerControls = new ActionsPlayer();
        playerControls.PlayerActions.Interaction.performed += _ => Interaction();
    }
    private void Update()
    {
        SidesVerifier();
    }
    private void Interaction()
    {
        if (tempIInteractable != null)
            tempIInteractable.interact();
    }
    private void SidesVerifier()
    {
        if(TouchingLeftWall() && !turningRight)
        {
             tempIInteractable = touchingObjectLeft.GetComponent<IInteractable>();
        }
        if(TouchingRightWall() && turningRight)
        {
            tempIInteractable = touchingObjectRight.GetComponent<IInteractable>();
        }
        if(!TouchingRightWall() && !TouchingLeftWall())
        {
            tempIInteractable = null;
        }
    }
    #region SidesCheckRayCast
    #region LeftWallDetect
    private bool TouchingLeftWall()
    {
        Color myRayColor;
        #region point creation, definition and ray creation
        Vector2 leftPoint = transform.position;
        leftPoint.x -= (myTriggerCollider.bounds.extents.x - leftWallRayExtra);
        leftPoint.y -= (myTriggerCollider.bounds.extents.y - wallRaysHeights);
        RaycastHit2D leftWallRay = Physics2D.Raycast(leftPoint, Vector2.left, myTriggerCollider.bounds.extents.x + rayWallsLenghts, InteractableLayers);
        #endregion
        #region rayColoring and drawing
        if (leftWallRay)
        {
            myRayColor = Color.red;
            if(touchingObjectLeft != null)
                touchingObjectLeft = leftWallRay.collider.gameObject;
        }
        else
        {
            myRayColor = Color.white;
            touchingObjectLeft = null;
        }
        Debug.DrawRay(leftPoint, Vector2.left * (myTriggerCollider.bounds.extents.x + rayWallsLenghts), myRayColor);
        #endregion
        #region returning values
        if (leftWallRay)
            return true;
        else return false;
        #endregion
    }
    #endregion
    #region RighttWallDetect
    private bool TouchingRightWall()
    {
        Color myRayColor;
        Vector2 rightPoint = transform.position;
        rightPoint.x += (myTriggerCollider.bounds.extents.x + rightWallRayExtra);
        rightPoint.y -= (myTriggerCollider.bounds.extents.y - wallRaysHeights);
        RaycastHit2D rightWallRay = Physics2D.Raycast(rightPoint, Vector2.right, myTriggerCollider.bounds.extents.x + rayWallsLenghts, InteractableLayers);

        if (rightWallRay)
        {
            myRayColor = Color.red;
            if(!touchingObjectRight)
                touchingObjectRight = rightWallRay.collider.gameObject;
        }
        else
        {
            myRayColor = Color.white;
            touchingObjectRight = null;
        }
        Debug.DrawRay(rightPoint, Vector2.right * (myTriggerCollider.bounds.extents.x + rayWallsLenghts), myRayColor);
        if (rightWallRay)
            return true;
        else return false;
    }
    #endregion
    #endregion
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
