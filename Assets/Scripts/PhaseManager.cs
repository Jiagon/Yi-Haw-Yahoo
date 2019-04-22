using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    public Sprite screen_1;
    public Sprite screen_2;
    private Image screen;
    public GameObject button_next;
    public GameObject button_last;

    void Start(){
        SetGameState(PhaseState.Placement);
        screen = GameObject.Find("screen").GetComponent<Image>();
        button_next = GameObject.Find("tutorial_next");
        button_last = GameObject.Find("tutorial_last");
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
    public void showTutorial() {
        ToggleGameObjects("tutorialUI", true);
        button_next.SetActive(true);
        button_last.SetActive(false);
    }
    public void nextTutPage() {
        print("nextTutCalled");
        //change sprite to next tut page
        screen.sprite = screen_2;
        //switch buttons
        button_next.SetActive(false);
        button_last.SetActive(true);
    }
    public void lastTutPage() {
        //change sprite to last tut page
        screen.sprite = screen_1;
        //switch buttons
        button_next.SetActive(true);
        button_last.SetActive(false);
    }
    public void closeTutorial() {
        ToggleGameObjects("tutorialUI", false);
        button_next.SetActive(false);
        button_last.SetActive(false);
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
                DestroyGameObjectsWithTag("Enemy");
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