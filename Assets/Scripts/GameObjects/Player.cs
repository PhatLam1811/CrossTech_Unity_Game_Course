using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseGameObj
{
    private float speed = 3.0f;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.deltaTime;

        ProcessInput(out Vector3 movingVector);

        Move(movingVector, elapsedTime, speed);
    }

    public void ProcessInput(out Vector3 movingVector)
    {
        // Move
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        
        movingVector = new Vector3(x: hor, y: ver);
    }
}
