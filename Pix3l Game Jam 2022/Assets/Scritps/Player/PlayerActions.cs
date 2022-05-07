using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerActions : MonoBehaviour
{
    #region variables
    public static List<Transform> followingAnimalsTran = new List<Transform>();
    public static Transform player;
    public static bool moving;
    public float speed;
    [SerializeField] private Sprite up, down,right;
    [SerializeField] private SpriteRenderer playerImage;
    private Rigidbody2D myRigidBody;
    public ActionsPlayer playerControls ;
    private bool turningRight, turningUp, turningDown, turningLeft;
    public bool haveSomethinToInteractWith, alreadyTurned, alreadyUnlockedMovement;
    #endregion
    [Header("Raycasts Walls")]
    [SerializeField] private LayerMask InteractableLayers;
    [SerializeField] private BoxCollider2D myTriggerCollider;
    private GameObject touchingObjectRight, touchingObjectLeft, touchingObjectUp, touchingObjectDown;
    private IInteractable tempIInteractable;
    public float leftWallRayExtra;
    public float rightWallRayExtra;
    public float wallRaysHeights;
    public float rayWallsLenghts;
    public float upWallRayExtra;
    public float downWallRayExtra;
    public float upRaysHorizontals;
    public float upRayHeight;
    public float downRaysHorizontals;
    public float downRayHeight;
    #region awake
    private void Awake()
    {
        player = transform;
        myRigidBody = GetComponent<Rigidbody2D>();
        playerControls = new ActionsPlayer();
        playerControls.PlayerActions.Interaction.performed += _ => Interaction();
    }
    #endregion
    #region update
    private void Update()
    {
        SidesVerifier();
    }
    #endregion
    #region interaction
    private void Interaction()
    {
        if (tempIInteractable != null)
        {
            Debug.Log($"interact");
            tempIInteractable.interact();
        }
    }
    #endregion
    #region SidesVerifier
    private void SidesVerifier()
    {

        if(TouchingLeftWall() && turningLeft)
        {
             tempIInteractable = touchingObjectLeft.GetComponent<IInteractable>();
        }
        if(TouchingRightWall() && turningRight)
        {
            tempIInteractable = touchingObjectRight.GetComponent<IInteractable>();
        }
        if (TouchingDownWall() && turningDown)
        {
            tempIInteractable = touchingObjectDown.GetComponent<IInteractable>();
            if(tempIInteractable != null)
                Debug.Log("objectDown");
        }
        if (TouchingUpWall() && turningUp)
        {
            tempIInteractable = touchingObjectUp.GetComponent<IInteractable>();
            if (tempIInteractable != null)
                Debug.Log("objectUp");
        }
        if (!TouchingRightWall() && !TouchingLeftWall() && !TouchingDownWall() && !TouchingUpWall())
        {
            tempIInteractable = null;
        }
    }
    #endregion
    #region CheckRayCast
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
            if(!touchingObjectLeft)
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
    #region DownWallDetect
    private bool TouchingDownWall()
    {
        Color myRayColor;
        #region point creation, definition and ray creation
        Vector2 point = transform.position;
        point.x -= (myTriggerCollider.bounds.extents.x - downRaysHorizontals);
        point.y -= (myTriggerCollider.bounds.extents.y - downRayHeight);
        //leftPoint.y -= (myTriggerCollider.bounds.extents.y - );
        RaycastHit2D DownWallRay = Physics2D.Raycast(point, Vector2.down, myTriggerCollider.bounds.extents.y + rayWallsLenghts, InteractableLayers);
        #endregion
        #region rayColoring and drawing
        if (DownWallRay)
        {
            myRayColor = Color.red;
            if (!touchingObjectDown)
            {
                touchingObjectDown = DownWallRay.collider.gameObject;
                Debug.Log("i have object down");
            }
        }
        else
        {
            myRayColor = Color.white;
            touchingObjectDown = null;
        }
        Debug.DrawRay(point, Vector2.down * (myTriggerCollider.bounds.extents.y + rayWallsLenghts), myRayColor);
        #endregion
        #region returning values
        if (DownWallRay)
            return true;
        else return false;
        #endregion
    }
    #endregion
    #region UpWallDetect
    private bool TouchingUpWall()
    {
        Color myRayColor;
        Vector2 rightPoint = transform.position;
        rightPoint.x += (myTriggerCollider.bounds.extents.x + upRaysHorizontals);
        rightPoint.y -= (myTriggerCollider.bounds.extents.y - upRayHeight);
        RaycastHit2D upWallRay = Physics2D.Raycast(rightPoint, Vector2.up, myTriggerCollider.bounds.extents.y + rayWallsLenghts, InteractableLayers);

        if (upWallRay)
        {
            myRayColor = Color.red;
            if (!touchingObjectUp)
                touchingObjectUp = upWallRay.collider.gameObject;
        }
        else
        {
            myRayColor = Color.white;
            touchingObjectUp = null;
        }
        Debug.DrawRay(rightPoint, Vector2.up * (myTriggerCollider.bounds.extents.y + rayWallsLenghts), myRayColor);
        if (upWallRay)
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
            moving = true;
            #region variables
            float x, y;
            x = move.x;
            y = move.y;
            #endregion
            myRigidBody.velocity = new Vector2((x * speed), (y * speed));
            #region lados
            if (x > 0)
            {
                turningRight = true;
                TurnRight();
                playerImage.sprite = right;
            }
            else { turningRight = false; }
            if (x < 0)
            {
                TurnLeft();
                turningLeft = true;
                playerImage.sprite = right;
            }
            else { turningLeft = false; }
            if (y > 0)
            {
                playerImage.sprite = up;
                TurnUp();
                turningUp = true;
            }
            else { turningUp = false; }
            if (y < 0)
            {
                TurnDown();
                turningDown = true; 
                playerImage.sprite = down;
            }
            else { turningDown = false; }
            #endregion
        }
        if (move.magnitude < .2)
        {
            moving = false;
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
    #region turnUp
    private void TurnUp()
    {

    }
    #endregion#region turnUp
    #region turnDown
    private void TurnDown()
    {

    }
    #endregion
    #endregion
    #region turnLeft
    private void TurnLeft()
    {
       playerImage.flipX = true;
    }
    #endregion
    #region turnRight
    private void TurnRight()
    {
       playerImage.flipX = false;
    }
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
