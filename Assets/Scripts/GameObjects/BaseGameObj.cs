using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Move(Vector3 movingVector, float elapsedTime, float speed)
    {
        transform.position += movingVector * elapsedTime * speed;
    }
}
