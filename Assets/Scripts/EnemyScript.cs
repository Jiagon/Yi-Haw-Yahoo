using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health = 0;
    public GameObject moveTarget;
    public GameObject attackTarget;

    bool moving;
    float maxVelocity;

    // Start is called before the first frame update
    void Start()
    {
        if(health == 0)
            health = 20;

        moveTarget = GameObject.FindGameObjectWithTag("Player");

        moving = true;

        maxVelocity = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Destroy(this.gameObject);
        if (moving)
            Move();
        else
            Attack();

    }

    void Move()
    {
        Vector3 move = (moveTarget.transform.position - this.transform.position).normalized;
        transform.position += (move * Time.deltaTime * maxVelocity);
    }

    void Attack()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerObject" || collision.gameObject == moveTarget)
        {
            moving = false;
            attackTarget = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (attackTarget != null)
        {
            moving = true;
            attackTarget = null;
        }
    }
}
