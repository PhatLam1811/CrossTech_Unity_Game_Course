using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollidable
{
    void onCollided(GameObject collidedObj);
}
