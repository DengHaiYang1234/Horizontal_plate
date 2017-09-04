using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    PlayerCharacter character;
	
	void Start ()
    {
        character = GetComponent<PlayerCharacter>();
	}
	
	
	void Update ()
    {
        var h = Input.GetAxis("Horizontal");

        character.Move(h);

        if(h !=0)
        {
            var dir = Vector3.right * h;
            character.Rotate(dir,15);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            character.Jump();
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            character.GrabCheck();
        }
	}
}
