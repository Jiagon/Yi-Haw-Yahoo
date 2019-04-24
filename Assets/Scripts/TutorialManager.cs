using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] screen1Objects;
    public GameObject[] screen2Objects;
    public void ShowTutorial(int screen) {
        if(screen == 0) {
            foreach(GameObject g in screen1Objects) {
                g.GetComponent<Image>().enabled = true;
            }
            foreach(GameObject g in screen2Objects) {
                g.GetComponent<Image>().enabled = false;
            }
        } else {
            Debug.Log("In here");
            foreach(GameObject g in screen1Objects) {
                g.GetComponent<Image>().enabled = false;
            }
            foreach(GameObject g in screen2Objects) {
                g.GetComponent<Image>().enabled = true;
            }
        }
    }
    public void Close() {
        foreach(GameObject g in screen1Objects) {
                g.GetComponent<Image>().enabled = false;
        }
        foreach(GameObject g in screen2Objects) {
                g.GetComponent<Image>().enabled = false;
        }
    }
}
