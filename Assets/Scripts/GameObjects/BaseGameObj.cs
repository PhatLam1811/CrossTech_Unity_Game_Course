using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameObj : MonoBehaviour
{
    protected float speed;
    protected Vector3 movingVector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Move(float elapsedTime)
    {
        // Debug.Log(GetType().Name + " Move");

        transform.position += movingVector * elapsedTime * speed;
    }
}
