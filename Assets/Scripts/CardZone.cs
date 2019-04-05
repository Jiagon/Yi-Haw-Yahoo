using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZone : MonoBehaviour
{
    public bool hasActiveObject;
    GameObject activeObject;
    public LevelManager manager;
    public void SetActiveObject(GameObject obj) {
        hasActiveObject = true; //Set so we haveActiveObject (Track to use with UI).
        //Set the new object to our current activeObject.
        activeObject = Instantiate(obj,transform.position,transform.rotation);
        obj.SetActive(false);   //Disable the image target object.
        //Set the parent of the new object to this card zone
        activeObject.transform.SetParent(this.gameObject.transform);
    }

    public void RemoveActiveObject() { 
        GameObject.Destroy(activeObject);   //Destroy the object
        activeObject = null;    //Set our active object to null.
        hasActiveObject = false;
    }
    void OnCollisionEnter(Collision col) {
        Debug.Log("Collided");
        if(col.gameObject.tag == "Placeable"){
            if(!hasActiveObject) SetActiveObject(col.gameObject);
        }
    }
}
