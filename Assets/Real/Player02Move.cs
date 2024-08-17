using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02Move : MonoBehaviour
{   
    public GameObject player02;
    public GameObject opponent;
    public Vector3 oppPosition;
    private Animator anim;
    private Rigidbody rb;
    private bool FaceingLeft = true;
    private bool FaceingRight = false;
    public bool faceLeft => FaceingLeft;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(FaceLeft());
    }

    void Update()
    {
        oppPosition = opponent.transform.position;
        if (oppPosition.x > player02.transform.position.x && !FaceingRight)
        {
            StartCoroutine(FaceRight());
        }
        else if (oppPosition.x < player02.transform.position.x && !FaceingLeft)
        {
            StartCoroutine(FaceLeft());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "5P_AttackBox" || other.tag == "5K_AttackBox" || other.tag == "5S_AttackBox" || other.tag == "5HS_AttackBox" ||
        other.tag == "2P_AttackBox" || other.tag == "2K_AttackBox" || other.tag == "2S_AttackBox" || other.tag == "2HS_AttackBox" || other.tag == "6P_AttackBox" ||
        other.tag == "6K_AttackBox" || other.tag == "6S_AttackBox" || other.tag == "6HS_AttackBox")
        {
            anim.SetTrigger("Hurt");
        }
    }

    IEnumerator FaceLeft()
    {
        if (!FaceingLeft)
        {
            FaceingLeft = true;
            FaceingRight = false;
            yield return new WaitForSeconds(0.15f);

            // Flip the character by inverting the scale on the X axis
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x) * -1;  // Invert the X scale to face left
            transform.localScale = newScale;

            // Set weights: RightLayer = 0, LeftLayer = 1
            // anim.SetLayerWeight(1, 0);  // RightLayer
            // anim.SetLayerWeight(2, 1);  // LeftLayer
        }
    }

    IEnumerator FaceRight()
    {
        if (!FaceingRight)
        {
            FaceingRight = true;
            FaceingLeft = false;
            yield return new WaitForSeconds(0.15f);

            // Reset the character scale to face right
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x);  // Ensure the X scale is positive
            transform.localScale = newScale;

            // Set weights: RightLayer = 1, LeftLayer = 0
            // anim.SetLayerWeight(1, 1);  // RightLayer
            // anim.SetLayerWeight(2, 0);  // LeftLayer
        }
    }

}
