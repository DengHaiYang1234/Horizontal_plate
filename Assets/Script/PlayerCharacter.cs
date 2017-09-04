using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public Transform grabObject;
    public Transform grabSocket;

    public int layer = 1;
	void Start ()
    {
        animator.SetLayerWeight(layer, 1f);
	}

    protected override void Update()
    {
        animator.SetBool("Grab", grabObject ? true : false);
        base.Update();
    }

    public void GrabCheck()
    {
        if(grabObject != null && roateComplete)
        {
            grabObject.transform.SetParent(null);
            grabObject.GetComponent<Rigidbody>().isKinematic = false;
            grabObject = null;
        }
        else
        {
            var dist = allCharacter.radius;
            RaycastHit hit;
            if(Physics.Raycast(transform.position,transform.forward,out hit,dist + 1f))
            {
                if(hit.collider.CompareTag("GrabBox"))
                {
                    grabObject = hit.transform;
                    grabObject.SetParent(grabSocket);
                    grabObject.localPosition = Vector3.zero;
                    grabObject.localRotation = Quaternion.identity;
                    grabObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }
}
