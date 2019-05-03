using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private CardZone[] cardZones; //Array of all card placement zones in the level.
    public List<string> activeIds = new List<string>();
    public int numOfCardZones;  //How many zones we have in this level (For UI purposes)
    public int activeCardZones = 0; //Number of card zones active with an object placed in them.
    //public Text cardsLeftText;
    void Start() {
        cardZones = GameObject.FindObjectsOfType<CardZone>();
        numOfCardZones = cardZones.Length;
        //cardsLeftText.text = activeCardZones.ToString() + "/" + numOfCardZones.ToString();
        FindObjectOfType<SingletonScript>().ResetComponents();
    }

    public void UpdateIdList(string id) {
        for(int i = 0; i < activeIds.Count; i++) {
            if(activeIds[i] == id) {
                return;
            }
        }
        activeIds.Add(id);
        //cardsLeftText.text = activeCardZones.ToString() + "/" + numOfCardZones.ToString();
    }

    public void RemoveIdFromList(string id){
        for(int i = 0; i < activeIds.Count; i++) {
            if(activeIds[i] == id) {
                activeIds.Remove(id);
            }
        }
        //cardsLeftText.text = activeCardZones.ToString() + "/" + numOfCardZones.ToString();
    }

    public bool isIdEnabled(string id) {
        for(int i = 0; i < activeIds.Count; i++) {
            if(activeIds[i] == id) {
                return true;
            }
        }
        return false;
    }

    //public void LoadLevel(string levelName) {
    //    SceneManager.LoadScene(levelName);
    //}
}