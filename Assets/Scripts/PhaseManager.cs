using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PhaseState
{
    Placement = 0,
    Attack = 1,
    GameOver = 2
}

public class PhaseManager : MonoBehaviour
{

    private PhaseState currentState;

    void Start(){
        SetGameState(PhaseState.Placement);
    }

    public PhaseState GetCurrentState() {
        return currentState;
    }
    public void EnterAttack(){
        SetGameState(PhaseState.Attack);
    }
    public void EnterGameOver() {
        SetGameState(PhaseState.GameOver);
    }
    public void EnterPlacable() {
        SetGameState(PhaseState.Placement);
    }
    public void SetGameState(PhaseState phase) {
        currentState = phase;   //Set our new current game phase
        switch(phase) {
            case PhaseState.Placement:
                //Enable all target image objects
                ToggleGameObjects("Placeable",true);
                ToggleGameObjects("placementUI",true);
                ToggleGameObjects("gameoverUI",false);
                ToggleGameObjects("attackUI",false);
                DestroyGameObjectsWithTag("placedObject");
                for(int i = 0; i < GameObject.FindObjectsOfType<CardZone>().Length; i++) {
                    GameObject.FindObjectsOfType<CardZone>()[i].hasActiveObject = false;
                }
                //Toggle Stop button to play mode
                break;
            case PhaseState.Attack:
                Debug.Log("In Attack");
                ToggleGameObjects("Placeable",false);
                ToggleGameObjects("placementUI",false);
                ToggleGameObjects("attackUI",true);
                //Toggle play button to Stop Mode
                //Start the spawning of enemeies
                break;
            case PhaseState.GameOver:
                ToggleGameObjects("gameoverUI",true);
                ToggleGameObjects("attackUI",false);
                break;
            default:
                break;
        }
    }

    void ToggleGameObjects(string tag, bool isActive) {
        GameObject[] objs = Resources.FindObjectsOfTypeAll<GameObject>();
        List<GameObject> tagObjs = new List<GameObject>();
        for(int i = 0; i < objs.Length; i++) {
            if(objs[i].tag == tag) {
                tagObjs.Add(objs[i]);
            }
        }
        for(int i = 0; i < tagObjs.Count; i++) {
            tagObjs[i].SetActive(isActive);
        }
    }
    void DestroyGameObjectsWithTag(string tag) {
        GameObject[] objs = Resources.FindObjectsOfTypeAll<GameObject>();
        List<GameObject> tagObjs = new List<GameObject>();
        for(int i = 0; i < objs.Length; i++) {
            if(objs[i].tag == tag) tagObjs.Add(objs[i]);
        }
        for(int i = 0; i < tagObjs.Count; i++) {
            GameObject.Destroy(tagObjs[i]);
        }
    }
}