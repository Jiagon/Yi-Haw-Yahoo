using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableManager : MonoBehaviour
{
    public List<GameObject> placeables;
    public GameObject arcPrefab;
    GameObject arcReactor;

    // Start is called before the first frame update
    void Start()
    {
        arcReactor = GameObject.FindGameObjectWithTag("Player");
        placeables = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemovePlaceable(GameObject placeable)
    {
        if (placeables.Contains(placeable))
            placeables.Remove(placeable);
    }

    public void UpdatePlaceables()
    {
        placeables.Clear();

        if (arcReactor == null)
        {
            arcReactor = Instantiate(arcPrefab, GameObject.Find("PlayingPlane").transform);
        }

        foreach (Placeable p in FindObjectsOfType<Placeable>())
        {
            placeables.Add(p.gameObject);
        }

        ResetPlaceables();
    }

    void ResetPlaceables()
    {
        foreach (GameObject placeable in placeables)
        {
            placeable.GetComponent<Placeable>().ResetHealth();
        }
    }

}
