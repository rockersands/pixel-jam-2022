using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float platformSpeed;
    private Vector3 tempPosition;
    private int currentPointTravel;
    private bool tankDetected,reverse,stopOneWay;
    public List<Transform> LocationPoints = new List<Transform>();
    public enum MovingPlatType {tankDetectRound,tankDetectOneWay,autoRound,autoOneWay}
    public MovingPlatType myMovingType;

    private void Update()
    {
        switch (myMovingType)
        {
            case MovingPlatType.tankDetectRound:
                if(tankDetected)
                {
                    tempPosition = Vector3.MoveTowards(transform.position, LocationPoints[currentPointTravel].position, platformSpeed * Time.deltaTime);
                    transform.position = tempPosition;
                    if (transform.position == LocationPoints[currentPointTravel].position)
                    {
                        if (currentPointTravel == LocationPoints.Count - 1)
                        {
                            reverse = true;
                        }
                        if (!reverse)
                            currentPointTravel++;
                        if (reverse)
                        {
                            if (currentPointTravel >= 0)
                                currentPointTravel--;
                            if (currentPointTravel < 0)
                            {
                                reverse = false;
                                currentPointTravel++;
                            }
                        }
                    }
                }
                break;
            case MovingPlatType.tankDetectOneWay:
                #region tank detect one way
                if (tankDetected)
                {
                    if (tankDetected)
                    {
                        if(!stopOneWay)
                        {
                            tempPosition = Vector3.MoveTowards(transform.position, LocationPoints[currentPointTravel].position, platformSpeed * Time.deltaTime);
                            transform.position = tempPosition;
                            if (transform.position == LocationPoints[currentPointTravel].position)
                            {
                                currentPointTravel++;
                                if (currentPointTravel == LocationPoints.Count)
                                    stopOneWay = true;
                            }
                        }
                    }
                }
                #endregion
                break;
            case MovingPlatType.autoRound:
                #region autoRound Platform
                tempPosition = Vector3.MoveTowards(transform.position, LocationPoints[currentPointTravel].position, platformSpeed * Time.deltaTime);
                transform.position = tempPosition;
                if (transform.position == LocationPoints[currentPointTravel].position)
                {
                    if (currentPointTravel == LocationPoints.Count -1)
                    {
                        reverse = true;
                    }
                    if (!reverse)
                        currentPointTravel++;
                    if (reverse)
                    {
                        if (currentPointTravel >= 0)
                            currentPointTravel--;
                        if (currentPointTravel < 0)
                        {
                            reverse = false;
                            currentPointTravel++;
                        }
                    }
                }
                #endregion
                break;
            case MovingPlatType.autoOneWay:
                if (!stopOneWay)
                {
                    tempPosition = Vector3.MoveTowards(transform.position, LocationPoints[currentPointTravel].position, platformSpeed * Time.deltaTime);
                    transform.position = tempPosition;
                    if (transform.position == LocationPoints[currentPointTravel].position)
                    {
                        currentPointTravel++;
                        if (currentPointTravel == LocationPoints.Count)
                            stopOneWay = true;
                    }
                }
                break;
            default:
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (myMovingType == MovingPlatType.tankDetectOneWay  || myMovingType == MovingPlatType.tankDetectRound))
            tankDetected = true;
    }
}
