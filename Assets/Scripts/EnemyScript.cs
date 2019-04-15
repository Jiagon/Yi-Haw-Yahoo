using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health = 0;
    public int attack = 5;
    public GameObject moveTarget;
    public GameObject attackTarget;
    public EnemyManager eManager;

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
        {
            eManager.RemoveEnemy(this.gameObject);
            Destroy(this.gameObject);
        }
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
        if(attackTarget != null)
        {
            attackTarget.GetComponent<Placeable>().TakeDamage(attack);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        // TODO: Update canvas
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Placeable" || collision.gameObject == moveTarget)
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
