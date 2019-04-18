using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    void Update() {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)){
        Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit raycastHit;
        if (Physics.Raycast(raycast, out raycastHit)) {
            if (raycastHit.collider.tag == "Placeable") {
                if(raycastHit.collider.gameObject.GetComponent<TestTurret>()){  //Check if we tap on turret.
                    raycastHit.collider.gameObject.GetComponent<TestTurret>().ToggleActive();
                }
            }
        }
    }
    }
}
