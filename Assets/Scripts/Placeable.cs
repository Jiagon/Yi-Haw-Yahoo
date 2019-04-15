using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour {
    public string id;
    public const int MAX_HEALTH = 1000;
    public int currentHealth = 1000;
    public GameObject displayHealth;
    Vector2 originalDisplayDimensions;


    public string getId() {
        return id;
    }
    public void setId(string newId) {
        id = newId;
    }

    public bool IsAlive() {
        return currentHealth <= 0;
    }

    void Start()
    {
        originalDisplayDimensions = displayHealth.GetComponent<RectTransform>().sizeDelta;
    }

    void Update()
    {
        TakeDamage(1);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Taking damage of " + damage + ", current Health: " + currentHealth);
        Debug.Log(currentHealth + ", " + MAX_HEALTH + ", " + (float)((float)currentHealth / (float)MAX_HEALTH));
        currentHealth -= damage;
        // TODO: Update canvas
        if(displayHealth != null)
        {
            displayHealth.GetComponent<RectTransform>().sizeDelta = new Vector2(originalDisplayDimensions.x * (float)((float)currentHealth / (float)MAX_HEALTH), originalDisplayDimensions.y);
        }
    }
}
