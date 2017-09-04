using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected  CharacterController allCharacter;
    protected  Animator animator;
    public float runSpeed;
    public float jumpSpeed;
    public bool roateComplete = true;
    public int damage = 100;
    public int heath = 100;
    public GameObject deathFx;
    Vector3 pengdingVelocity;

	void Awake ()
    {
        allCharacter = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
	}
	
	protected virtual void Update ()
    {
        pengdingVelocity.z = 0f;

        allCharacter.Move(pengdingVelocity * Time.deltaTime);

        animator.SetFloat("Speed", allCharacter.velocity.magnitude);

        animator.SetBool("IsGround", allCharacter.isGrounded);
        
        pengdingVelocity.y += allCharacter.isGrounded ? 0f : Physics.gravity.y * 10f * Time.deltaTime;

        Attack();
    }

    public void Jump()
    {
        if(allCharacter.isGrounded)
        {
            pengdingVelocity.y = jumpSpeed;
        }
    }

    public void Move(float inPutX)
    {
        pengdingVelocity.x = inPutX * runSpeed;
    }

    public void Rotate(Vector3 loodDir,float turnSpeed)
    {
        roateComplete = false;
        var targetPos = transform.position + loodDir;
        var characterPos = transform.position;

        characterPos.y = 0;
        targetPos.y = 0;

        Vector3 faceToDir = targetPos - characterPos;

        //Vector3 faceToDir = loodDir;
        Quaternion faceInQuat = Quaternion.LookRotation(faceToDir);
        //Quaternion faceInQuat = Quaternion.LookRotation(loodDir);
        //Debug.DrawLine(Vector3.zero, transform.forward, Color.blue);
        //Debug.DrawLine(Vector3.zero, transform.right, Color.red);
        //Debug.DrawLine(Vector3.zero, transform.up, Color.green);
        Quaternion slerp = Quaternion.Slerp(transform.rotation, faceInQuat, turnSpeed * Time.deltaTime);

        if(slerp == faceInQuat)
        {
            roateComplete = true;
        }

        transform.rotation = slerp;
    }

    public void Die()
    {
        var fx = Instantiate(deathFx, transform.position, Quaternion.Euler(Vector3.zero));
        Destroy(fx, 2);
        Destroy(gameObject);
    }

    public void TakeDamage(Character otherCharacter,int damage)
    {
        //otherCharacter.Jump();
        heath -= damage;
        if(heath <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        var dist = allCharacter.height / 2;
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down,out hit,dist + 0.05f))
        {
            if(hit.transform.GetComponent<Character>() && hit.transform != transform)
            {
                hit.transform.GetComponent<Character>().TakeDamage(this, damage);
            }
        }
    }




}
