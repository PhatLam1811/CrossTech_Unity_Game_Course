using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoving
{
    float speed { get; set; }

    void Move(Vector3 movingVector, float elapsedTime);
}
