using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private string cardZoneTagName = "cardZone";
    private CardZone[] cardZones; //Array of all card placement zones in the level.
    public List<string> activeIds = new List<string>();
    public int numOfCardZones;  //How many zones we have in this level (For UI purposes)
    public int activeCardZones = 0; //Number of card zones active with an object placed in them.
    void Start() {
        cardZones = GameObject.FindObjectsOfType<CardZone>();
        numOfCardZones = cardZones.Length;
    }

    public void UpdateIdList(string id) {
        for(int i = 0; i < activeIds.Count; i++) {
            if(activeIds[i] == id) {
                return;
            }
        }
        activeIds.Add(id);
    }

    public void RemoveIdFromList(string id){
        for(int i = 0; i < activeIds.Count; i++) {
            if(activeIds[i] == id) {
                activeIds.Remove(id);
            }
        }
    }

    public bool isIdEnabled(string id) {
        for(int i = 0; i < activeIds.Count; i++) {
            if(activeIds[i] == id) {
                return true;
            }
        }
        return false;
    }
}