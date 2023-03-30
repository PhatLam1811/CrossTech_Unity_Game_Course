using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameObj : MonoBehaviour
{
    protected float speed;
    protected Vector3 movingVector;

    protected Camera viewport;

    protected float GetElapsedTime()
    {
        return Time.deltaTime;
    }

    public string GetTypeName()
    {
        return GetType().Name;
    }

    public virtual void Move(float elapsedTime)
    {
        // Debug.Log(GetType().Name + " Move");

        transform.position += movingVector * elapsedTime * speed;
    }
}
