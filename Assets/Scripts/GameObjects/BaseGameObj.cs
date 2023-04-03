using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameObj : MonoBehaviour
{
    protected Camera viewport;

    protected float speed;
    protected Vector3 movingVector;

    // Start is called before the first frame update
    protected void Start()
    {
        Init();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float elapsedTime = GetElapsedTime();

        Move(elapsedTime);
    }

    protected float GetElapsedTime() { return Time.deltaTime; }
    public string GetClassName() { return GetType().Name; }
    public void SetMovingVector(Vector3 movingVector) { this.movingVector = movingVector; }
    public void SetSpeed(float speed) { this.speed = speed; }

    protected virtual void Init()
    {
        viewport = Camera.main;

        speed = 0f;
        movingVector = new Vector3(x: 0f, y: 0f);
    }

    public virtual void Move(float elapsedTime)
    {
        transform.position += movingVector * elapsedTime * speed;
    }
}
