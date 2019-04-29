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
    float radius;
    bool canAttack = false;
    public float attackCoolDownTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        if (currentHealth <= 0)
            currentHealth = 20;

        if (displayHealth != null)
            originalDisplayDimensions = displayHealth.GetComponent<RectTransform>().sizeDelta;
        moveTarget = GameObject.FindGameObjectWithTag("Player");

        moving = true;

        maxVelocity = 0.6f;
        radius = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            eManager.RemoveEnemy(this.gameObject);
            Destroy(this.gameObject);
        }
        if (moving)
        {
            FindClosePlaceable();
            Move();
        }
        if(canAttack) {
            Attack();
        }
    }

    void Move()
    {
        if (moveTarget != null)
        {
            Vector3 move = (moveTarget.transform.position - this.transform.position).normalized;
            transform.position += (move * Time.deltaTime * maxVelocity);
            FaceTarget(moveTarget);
        }
    }

    void Attack()
    {
        if (attackTarget != null)
        {
            attackTarget.GetComponent<Placeable>().TakeDamage(attack);
            GetComponent<AudioSource>().Play();
            if (!attackTarget.GetComponent<Placeable>().IsAlive())
            {
                eManager.RemovePlaceable(attackTarget);
                attackTarget = null;
                moving = true;
            }
        }
        canAttack = false;
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown() {
        yield return new WaitForSeconds(attackCoolDownTime);
        canAttack = true;
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

    void FindClosePlaceable()
    {
        foreach (GameObject p in eManager.GetPlaceables())
        {
            if (Vector3.Magnitude(p.transform.position - transform.position) < radius)
            {
                attackTarget = p;
                moving = false;
            }
        }
    }

    void FaceTarget(GameObject target)
    {
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
    }
}
