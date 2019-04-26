using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour {
    public string id;
    public const int MAX_HEALTH = 1000;
    public int currentHealth = 1000;
    public GameObject displayMaxHealth;
    public GameObject displayHealth;
    Vector2 originalDisplayDimensions;
    Camera cam;

    public GameObject interactionContainer;

    public string getId() {
        return id;
    }
    public void setId(string newId) {
        id = newId;
    }

    public bool IsAlive() {
        return currentHealth > 0;
    }

    void Start()
    {
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
        // TODO: Update canvas
        if(displayHealth != null)
        {
            displayHealth.GetComponent<RectTransform>().sizeDelta = new Vector2(originalDisplayDimensions.x * (float)((float)currentHealth / (float)MAX_HEALTH), originalDisplayDimensions.y);
        }
    }

    private void OnGUI()
    {
        if(cam != null) {
            Vector3 v = cam.transform.position - transform.position;
            v.x = v.z = 0.0f;

            displayMaxHealth.transform.LookAt(cam.transform.position - v);
            displayMaxHealth.transform.Rotate(0, 180, 0);
            displayHealth.transform.LookAt(cam.transform.position - v);
            displayHealth.transform.Rotate(0, 180, 0);
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
}
