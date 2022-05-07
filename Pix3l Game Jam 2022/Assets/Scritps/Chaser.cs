using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour,IInteractable
{
    public Transform firstPosToGo;
    private bool alreadyChasing;
    public bool chasing,goingFirstPos,aligning;
    public Transform chasingObject;
    public float originalSpeed, rotationSpeed,bulletSpeedAcceleration,aligningTime,firstPosSpeed,speedLimit, distanceBetween;
    private float speed;
    [SerializeField] Collider2D myCollider;

    private void Update()
    {
        if(chasingObject != null)
            goingAndRotatingThisWay(chasingObject);
    }
    private void OnEnable()
    {
        speed = originalSpeed;
    }
    private void OnDisable()
    {
        chasing = false;
    }
    #region goingAndRotatingThisWay
    public void goingAndRotatingThisWay(Transform objective)
    {
        if (chasing && PlayerActions.moving)
        {
            if(Vector2.Distance(transform.position,chasingObject.position) > distanceBetween)
            {
                speed += Time.deltaTime * bulletSpeedAcceleration;
                transform.position = Vector3.MoveTowards(transform.position, chasingObject.position, Mathf.Clamp(speed, 0, speedLimit) * Time.deltaTime);
            }
        }
    }
    #endregion
    public void interact()
    {
        if (alreadyChasing) { return; }

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

}
