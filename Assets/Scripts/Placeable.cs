using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placeable : MonoBehaviour
{
    public int baseDamage = 0;
    public int damage = 0;
    public string id;

    public int baseHealth = 100;
    public int MAX_HEALTH = 100;
    public int currentHealth = 1000;
    public GameObject displayMaxHealth;
    public GameObject displayHealth;
    Vector2 originalDisplayDimensions;
    public bool isArc = false;
    Camera cam;
    
    public GameObject interactionContainer;
    List<float> upgradeList = new List<float> {1.0f,1.1f,1.25f,1.5f,1.75f,1.9f,2.0f,2.5f,3.0f};
    int currentUpgradeLevel = 0;

    public string getId() {
        return id;
    }
    public void setId(string newId) {
        id = newId;
    }

    public bool IsAlive() {
        return currentHealth > 0;
    }

    protected virtual void Start()
    {
        MAX_HEALTH = (int)(baseHealth * upgradeList[currentUpgradeLevel]);
        originalDisplayDimensions = displayHealth.GetComponent<RectTransform>().sizeDelta;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    protected virtual void Update()
    {
        if (!IsAlive())
            Destroy(this.gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0) {
            Kill();
        }
        // TODO: Update canvas
        if(displayHealth != null)
        {
            displayHealth.GetComponent<RectTransform>().sizeDelta = new Vector2(originalDisplayDimensions.x * (float)((float)currentHealth / (float)MAX_HEALTH), originalDisplayDimensions.y);
        }
    }

    void Kill() {
        if(isArc) {
            Destroy(gameObject);    //Destroy the gameobject
            GameObject.Find("GameManager").GetComponent<PhaseManager>().EnterGameOver();
            GameObject.Find("Outcome").GetComponent<Text>().text = "DEFEAT";
        } else {
            Destroy(gameObject);
        }
    }

    public void ResetHealth()
    {
        currentHealth = MAX_HEALTH;
        displayHealth.GetComponent<RectTransform>().sizeDelta = originalDisplayDimensions;
    }

    private void OnGUI()
    {
        if (cam != null)
        {
            Vector3 v = cam.transform.position - transform.position;
            v.x = v.z = 0.0f;

            //displayMaxHealth.transform.LookAt(cam.transform.position - v);
            //displayMaxHealth.transform.Rotate(0, 180, 0);
            //displayHealth.transform.LookAt(cam.transform.position - v);
            //displayHealth.transform.Rotate(0, 180, 0);
        }
    }
    public void ToggleInteractionUI() {
        interactionContainer.SetActive(!interactionContainer.activeSelf);
    }

    public void RemoveObject() {
        GameObject[] zones = GameObject.FindGameObjectsWithTag("CardZone");
        foreach(GameObject zone in zones) {
            if(zone.GetComponent<CardZone>().activeObject== gameObject) {
                zone.GetComponent<CardZone>().RemoveActiveObject();
            }
        }
    }

    public void Upgrade()
    {
        SingletonScript s = FindObjectOfType<SingletonScript>();
        if (currentUpgradeLevel + 1 >= upgradeList.Count || s.upgrades < 1) {
            return;
        }
        s.ChangeUpgradeNum(s.upgrades - 1);
        currentUpgradeLevel++;
        MAX_HEALTH = (int)(baseHealth * upgradeList[currentUpgradeLevel]);
        damage = (int)(baseDamage * upgradeList[currentUpgradeLevel]);
    }
}
