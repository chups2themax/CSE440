﻿using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {
    private Transform followTarget;
    private float moveSpeed;
    private float turnSpeed;
    private bool isZombie;
    private Vector3 targetPosition;


    public void GrantCatTheSweetReleaseOfDeath()
    {
        DestroyObject(transform.parent.gameObject);
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    public void JoinConga(Transform followTarget, float moveSpeed, float turnSpeed)
    {
        this.followTarget = followTarget;
        this.moveSpeed = moveSpeed *2f;
        this.turnSpeed = turnSpeed;

        isZombie = true;

        targetPosition = followTarget.position;

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetBool("InConga", true);


    }
    public void ExitConga()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        targetPosition = new Vector3(cameraPos.x + Random.Range(-1.5f, 1.5f), cameraPos.y + Random.Range(-1.5f, 1.5f), followTarget.position.z);

        Transform cat = transform.GetChild(0);
        cat.GetComponent<Animator>().SetBool("InConga", false);
    }

    // Use this for initialization
    /*void Start () {
	
	}*/
	
	//Update is called once per frame
	void Update () {

        if(!isZombie)
        {
            Vector3 currentPosition = transform.position;
            Vector3 moveDirection = targetPosition - currentPosition;

            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), turnSpeed * Time.deltaTime);

            float distanceToTarget = moveDirection.magnitude;
            if(distanceToTarget > 0)
            {
                if (distanceToTarget > moveSpeed)
                {
                    distanceToTarget = moveSpeed;

                    moveDirection.Normalize();
                    Vector3 target = moveDirection * distanceToTarget + currentPosition;
                    transform.position = Vector3.Lerp(currentPosition, target, moveSpeed * Time.deltaTime);

                }
            }
        }
	
	}

   public void UpdateTargetPosition()
    {
        targetPosition = followTarget.position;
    }
}
