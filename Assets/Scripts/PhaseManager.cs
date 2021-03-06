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
        eManager = GetComponent<EnemyManager>();
        pManager = GetComponent<PlaceableManager>();
        SetGameState(PhaseState.Placement);
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
        eManager.ResetEnemies((uint)eManager.totalEnemies);
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
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = GetComponent<AudioManager>().placementMusic;
                GetComponent<AudioSource>().Play();
                break;
            case PhaseState.Attack:
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = GetComponent<AudioManager>().defendMusic;
                GetComponent<AudioSource>().Play();
                ToggleGameObjects("Placeable",false);
                ToggleGameObjects("interactionUI", false);
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