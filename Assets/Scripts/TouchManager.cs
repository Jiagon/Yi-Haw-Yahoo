using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    GameObject tapObject;
    void Update()
    {
        //If we get a simple tap.
        if(Input.touchCount ==  1 && Input.GetTouch(0).phase == TouchPhase.Began) {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                tapObject = hit.collider.gameObject;
                //If we tap on a placeable
                if(hit.collider.gameObject.GetComponent<Placeable>() && hit.collider.tag != "Player" && GetComponent<PhaseManager>().GetCurrentState() == PhaseState.Placement){  //We are tapping on a placable. (That's not the arc)
                    hit.collider.gameObject.GetComponent<Placeable>().ToggleInteractionUI();
                }
            }
            if(GetRaycastHitFromTag("Enemy") && GetComponent<PhaseManager>().GetCurrentState() == PhaseState.Attack) {
                GameObject[] placables = GameObject.FindGameObjectsWithTag("Placeable");
                foreach(GameObject t in placables) {
                    if(t.GetComponent<TestTurret>() != null) {  //Only select the turrets
                        if (Vector3.Magnitude(t.transform.position - hit.collider.transform.position) < t.GetComponent<TestTurret>().radius) {
                            //We are within radius
                            hit.collider.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
        //If we are dragging
        if(Input.touchCount ==  1 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            if(tapObject != null && tapObject.tag == "Placeable") {
                float rotateSpeed = 100.0f;
                if(Input.GetTouch(0).deltaPosition.x < 0) {
                    tapObject.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.Self);
                } else if(Input.GetTouch(0).deltaPosition.x > 0) {
                    tapObject.transform.Rotate(-Vector3.up * Time.deltaTime * rotateSpeed, Space.Self);
                }
            }
        }
    }
    bool GetRaycastHitFromTag(string tag){
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) {
            if(hit.collider.gameObject.tag == tag){
                return true;
            }
        }
        return false;
    }
}
