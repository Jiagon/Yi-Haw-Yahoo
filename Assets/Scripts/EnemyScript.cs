using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    int MAX_HEALTH = 20;
    public int currentHealth = 0;
    public int attack = 5;
    public GameObject moveTarget;
    public GameObject attackTarget;
    public EnemyManager eManager;
    public GameObject displayHealth;

    Vector2 originalDisplayDimensions;
    bool moving;
    float maxVelocity;

    // Start is called before the first frame update
    void Start()
    {
        if(currentHealth <= 0)
            currentHealth = 20;

        if (displayHealth != null)
            originalDisplayDimensions = displayHealth.GetComponent<RectTransform>().sizeDelta;
        moveTarget = GameObject.FindGameObjectWithTag("Player");

        moving = true;

        maxVelocity = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0 || eManager.pManager.GetCurrentState() != PhaseState.Attack)
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
        if (moveTarget != null)
        {
            Vector3 move = (moveTarget.transform.position - this.transform.position).normalized;
            transform.position += (move * Time.deltaTime * maxVelocity);
        }
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
        currentHealth -= damage;
        // TODO: Update canvas
        if (displayHealth != null)
        {
            displayHealth.GetComponent<RectTransform>().sizeDelta = new Vector2(originalDisplayDimensions.x * (float)((float)currentHealth / (float)MAX_HEALTH), originalDisplayDimensions.y);
        }
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
