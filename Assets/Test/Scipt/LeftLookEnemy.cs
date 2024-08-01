using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftLookEnemy : MonoBehaviour
{
    public Transform dummy;
    public LeftCharacterMovementKeyBoard movementScript;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (IsGrounded() || movementScript.IsDashing) 
        {
            LookAtDummy();
        }
    }

    private void LookAtDummy()
    {
        Vector3 directionToDummy = dummy.position - transform.position;
        directionToDummy.y = 0;

        Vector3 directionToUse = directionToDummy;
        bool shouldFlip = false;

        if (movementScript.IsDashing)
        {
            float airtime = movementScript.airtime;
            float dashElapsedTime = Time.time - movementScript.dashStartTime;

            // คำนวณทิศทางเมื่อ Dash
            float additionalDistance = 1f;
            directionToUse = directionToDummy.normalized * (directionToDummy.magnitude + additionalDistance);

            // ตรวจสอบว่าตัวละครอยู่กลางอากาศในช่วง airtime
            if (dashElapsedTime < airtime)
            {
                shouldFlip = true;
            }
        }
        else if (IsGrounded())
        {
            shouldFlip = true;
        }

        if (shouldFlip)
        {
            if (directionToUse.x > 0 && !movementScript.facingRight)
            {
                movementScript.facingRight = true;
                Flip();
            }
            else if (directionToUse.x < 0 && movementScript.facingRight)
            {
                movementScript.facingRight = false;
                Flip();
            }
        }
    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y = movementScript.facingRight ? 0 : 180;
        transform.eulerAngles = rotation;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}