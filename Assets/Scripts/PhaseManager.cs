﻿using System.Collections;
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
    private EnemyManager eManager;
    private PlaceableManager pManager;
     GameObject screen_1;
     GameObject screen_2;
    private Image screen;
     GameObject button_next;
     GameObject button_last;

    void Start(){
        SetGameState(PhaseState.Placement);
        eManager = GetComponent<EnemyManager>();
        pManager = GetComponent<PlaceableManager>();
    }

    public PhaseState GetCurrentState() {
        return currentState;
    }
    public void EnterAttack(){
        pManager.UpdatePlaceables();
        SetGameState(PhaseState.Attack);
    }
    public void EnterGameOver() {
        SetGameState(PhaseState.GameOver);
    }
    public void EnterPlacable() {
        eManager.ResetEnemies(15);
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
                GameObject[] zones = GameObject.FindGameObjectsWithTag("CardZone");
                for(int i = 0; i < zones.Length; i++) {
                    zones[i].GetComponent<CardZone>().RemoveActiveObject();
                }
                //Toggle Stop button to play mode
                //Toggle Stop button to play mode
                break;
            case PhaseState.Attack:
                ToggleGameObjects("Placeable",false);
                ToggleGameObjects("placementUI",false);
                ToggleGameObjects("attackUI",true);
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