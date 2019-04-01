using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private string cardZoneTagName = "cardZone";
    private CardZone[] cardZones; //Array of all card placement zones in the level.
    private List<int> activeIds = new List<int>();
    public int numOfCardZones;  //How many zones we have in this level (For UI purposes)
    public int activeCardZones = 0; //Number of card zones active with an object placed in them.
    void Start() {
        cardZones = GameObject.FindObjectsOfType<CardZone>();
        numOfCardZones = cardZones.Length;
    }

    //This function is called when we place an object.
    //Params:
    //obj: The object we want to place in the zone
    //zone: The cardzone we want to update
    public void PlaceObejct(GameObject obj, GameObject zone) {
        //Check to make sure we havn't already placed an object with this id.
        for(int i = 0; i < activeIds.Count; i++) {
            // if(id == activeIds[i]) {
            //     //Some sort of on-screen indication that you can't place this
            //     return;
            // }
        }
        //If we get here, we can place this object.
        activeCardZones++;
        zone.GetComponent<CardZone>().SetActiveObject(obj);
        //activeIds.Add(obj.GetComponent<ObjectStats>().id);
    }

    //This function removes an active object from an active cardZone.
    //Params:
    //obj: (the object to currently active in the zone)
    //zone: the cardzone gameobject that we want to update
    //Should be called from the function that detects collisions in the object.
    public void RemoveObject(GameObject obj, GameObject zone) {
        activeCardZones--;
        for(int i = 0; i < activeIds.Count; i++) {
            // if(obj.GetComponent<ObjectStats>().id == activeIds[i]) {
            //     activeIds.Remove(activeIds[i]);
            //     zone.GetComponent<CardZone>().RemoveActiveObject();
            // }
        }
    }
}