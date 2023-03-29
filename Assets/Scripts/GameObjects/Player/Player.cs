using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseGameObj
{
    float cooldown;

    [SerializeField] GameObject ammoType;
    [SerializeField] GameObject gunBarrel;

    // Start is called before the first frame update
    void Start()
    {
        speed = 3.0f;
        cooldown = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.deltaTime;

        ProcessInput();

        Move(elapsedTime);
        Shoot(elapsedTime);
    }

    public void ProcessInput()
    {
        // ======================================
        // Move
        // ======================================
        // get normalized moving input directions
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        movingVector = new Vector3(x: hor, y: ver);
    }

    public void Shoot(float elapsedTime)
    {
        cooldown -= elapsedTime;

        if (cooldown <= 0.0f)
        {
            Debug.Log(GetType().Name + " shooting");

            Vector3 barrelPos = gunBarrel.transform.position;

            // shoot a bullet every 1s
            Instantiate(ammoType, barrelPos, Quaternion.identity);

            // reset cooldown
            cooldown = 1f;
        }
    }
}
