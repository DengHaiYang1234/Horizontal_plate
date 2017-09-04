using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiControl : MonoBehaviour
{
    Character character;
    float lastCheckStateTime = 0;

    float simulateInputX;

    bool simulateJump;

	void Start ()
    {
        character = GetComponent<Character>();
	}
	
	void Update ()
    {
		if(Time.time > lastCheckStateTime + 2)
        {
            lastCheckStateTime = Time.time;
            simulateInputX = Random.Range(-1f, 1f);
            simulateJump = Random.Range(0, 2) == 1 ? true : false;
        }
        MoveControl(simulateInputX);
        Jump(simulateJump);
	}

    void MoveControl(float inputX)
    {
        character.Move(inputX);
        if(inputX != 0)
        {
            var dir = Vector3.right * inputX;
            character.Rotate(dir, 10f);
        }
    }

    void Jump(bool jump)
    {
        if(jump)
        {
            character.Jump();
            simulateJump = false;
        }
    }
}
