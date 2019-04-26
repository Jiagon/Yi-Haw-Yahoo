using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount ==  1 && Input.GetTouch(0).phase == TouchPhase.Began) {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                if(hit.collider.gameObject.GetComponent<Placeable>() && hit.collider.tag != "Player" && GetComponent<PhaseManager>().GetCurrentState() == 0){  //We are tapping on a placable. (That's not the arc)
                    hit.collider.gameObject.GetComponent<Placeable>().ToggleInteractionUI();
                }
            }
        }
    }
}
