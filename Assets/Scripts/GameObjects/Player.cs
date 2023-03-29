using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseGameObj, IMoving
{
    private const float PLAYER_SPEED = 3.0f;

    public float speed { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        speed = PLAYER_SPEED;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.deltaTime;

        ProcessInput(out Vector3 movingVector);

        Move(movingVector, elapsedTime);
    }

    public void ProcessInput(out Vector3 movingVector)
    {
        // Move
        float horIn = Input.GetAxis("Horizontal");
        float verIn = Input.GetAxis("Vertical");
        
        movingVector = new Vector3(x: horIn, y: verIn);
    }

    public void Move(Vector3 movingVector, float elapsedTime)
    {
        transform.position += movingVector * speed * elapsedTime;
    }
}
