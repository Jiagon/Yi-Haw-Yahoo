using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable {
    private string id;
    public const int MAX_HEALTH = 100;
    public int currentHealth = 100;


    public string getId() {
        return id;
    }
    public void setId(string newId) {
        id = newId;
    }

    public bool IsAlive() {
        return currentHealth <= 0;
    }
}
