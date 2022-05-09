using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour,IInteractable
{
    Vector2 posThisFrame, posLastFrame;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    [SerializeField] private GameObject capyAnimatorGO;
    [SerializeField] private GameObject ramonaAnimatorGO;
    [SerializeField] private GameObject nathanielAnimatorGO;
    private Rigidbody2D myRb;
    private DialogosNpc myDialogNpc;
    public Transform firstPosToGo;
    private bool alreadyChasing;
    public bool chasing,goingFirstPos,aligning;
    public Transform chasingObject;
    public float originalSpeed, rotationSpeed,bulletSpeedAcceleration,aligningTime,firstPosSpeed,speedLimit, distanceBetween;
    private float speed;
    private bool turningRight, turningUp, turningDown, turningLeft,waitingDelay;
    [SerializeField] Collider2D myCollider;
    #region update
    private void Update()
    {
        if(chasingObject != null)
            goingThisWay(chasingObject);
        posLastFrame = posThisFrame;

        posThisFrame = transform.position;

        CheckMoveDirection();
    }
    #endregion
    #region checkMoveDirection
    private void CheckMoveDirection()
    {
        if(!waitingDelay)
            StartCoroutine(UpdateStateDelay());
    }
    IEnumerator UpdateStateDelay()
    {
        waitingDelay = true;
        yield return new WaitForSeconds(.3f);
        float yDiff = posLastFrame.y - posThisFrame.y;
        float xDiff = posLastFrame.x - posThisFrame.x;
        yDiff = Mathf.Abs(yDiff);
        xDiff = Mathf.Abs(xDiff);
        if (posThisFrame.x > posLastFrame.x &&( xDiff > yDiff || xDiff == yDiff))
        {
            mySpriteRenderer.flipX = true;
            if (!turningUp && !turningDown )
                myAnimator.SetBool("PWalkSides", true);
            turningRight = true;
        }
        else
        {
            turningRight = false;
            if (!turningLeft)
                myAnimator.SetBool("PWalkSides", false);
        }

        if (posThisFrame.x < posLastFrame.x &&( xDiff > yDiff || xDiff == yDiff))
        {
            mySpriteRenderer.flipX = false;
            if (!turningUp && !turningDown )
                myAnimator.SetBool("PWalkSides", true); ;
            turningLeft = true;
        }
        else
        {
            turningLeft = false;
            if (!turningRight)
                myAnimator.SetBool("PWalkSides", false);
        }
        if (posThisFrame.y < posLastFrame.y && (xDiff < yDiff || xDiff == yDiff))
        {
            Debug.Log("walking down");
            if (!turningRight && !turningLeft )
                myAnimator.SetBool("PWalkDown", true);
            turningDown = true;
        }
        else
        {
            myAnimator.SetBool("PWalkDown", false);
            turningDown = false;
        }
        if (posThisFrame.y > posLastFrame.y && (xDiff < yDiff || xDiff == yDiff))
        {
            Debug.Log("walking Up");
            if (!turningRight && !turningLeft )
                myAnimator.SetBool("PWalkUp", true);
            turningUp = true;
        }
        else
        {
            turningUp = false;
            myAnimator.SetBool("PWalkUp", false);
        }
        if (!turningUp && !turningDown && !turningLeft && !turningRight)
        {
            myAnimator.SetBool("PWalkSides", false);
            myAnimator.SetBool("PWalkDown", false);
            myAnimator.SetBool("PWalkUp", false);
        }

        waitingDelay = false;
    }
    #endregion
    #region onEnable
    private void OnEnable()
    {
        myRb = GetComponent<Rigidbody2D>();
        speed = originalSpeed;
        myDialogNpc = GetComponent<DialogosNpc>();
        switch (myDialogNpc.myNpc)
        {
            case DialogosNpc.Npc.Capy:
                mySpriteRenderer = capyAnimatorGO.GetComponent<SpriteRenderer>();
                myAnimator = capyAnimatorGO.GetComponent<Animator>();
                ramonaAnimatorGO.SetActive(false);
                nathanielAnimatorGO.SetActive(false);
                break;
            case DialogosNpc.Npc.Ramona:
                mySpriteRenderer = ramonaAnimatorGO.GetComponent<SpriteRenderer>();
                myAnimator = ramonaAnimatorGO.GetComponent<Animator>();
                capyAnimatorGO.SetActive(false);
                nathanielAnimatorGO.SetActive(false);
                break;
            case DialogosNpc.Npc.Nathaniel:
                mySpriteRenderer = nathanielAnimatorGO.GetComponent<SpriteRenderer>();
                myAnimator = nathanielAnimatorGO.GetComponent<Animator>();
                ramonaAnimatorGO.SetActive(false);
                capyAnimatorGO.SetActive(false);
                break;
            default:
                break;
        }
    }
    #endregion
    #region goingAndRotatingThisWay
    public void goingThisWay(Transform objective)
    {
        Debug.Log($"{myRb.velocity}");
        if (chasing && PlayerActions.moving)
        {
            if(Vector2.Distance(transform.position,chasingObject.position) > distanceBetween)
            {
                speed += Time.deltaTime * bulletSpeedAcceleration;
                transform.position = Vector3.MoveTowards(transform.position, chasingObject.position, Mathf.Clamp(speed, 0, speedLimit) * Time.deltaTime);
            }
        }
        else if(!PlayerActions.moving)
        {
            myRb.velocity = Vector3.zero;
            myRb.angularVelocity = 0;
        }
    }
    #endregion
    #region interact
    public void interact()
    {
        if (alreadyChasing) { return; }
        myDialogNpc.MostrarDialogo();
        if (PlayerActions.followingAnimalsTran.Count == 0)
        {
            PlayerActions.followingAnimalsTran.Add(transform);
            chasingObject = PlayerActions.player;
           // myCollider.isTrigger = true;
            alreadyChasing = true;
        }
        else if (PlayerActions.followingAnimalsTran.Count > 0)
        {
            Debug.Log("following next");
            chasingObject = PlayerActions.followingAnimalsTran[PlayerActions.followingAnimalsTran.Count -1];
            PlayerActions.followingAnimalsTran.Add(transform);
            //myCollider.isTrigger = true;
            alreadyChasing = true;
        }
    }
    #endregion

}
