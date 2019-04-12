using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZone : MonoBehaviour
{
    public bool hasActiveObject;
    public LevelManager manager;
    GameObject zoneObject;  //This is the object that we hit (save for restoration).
    GameObject activeObject;
    string placedObjectTag = "placedObject";
    public void SetActiveObject(GameObject obj) {
        zoneObject = obj;   //Store this so we can remember it later.
        Debug.Log(zoneObject);
        hasActiveObject = true; //Set so we haveActiveObject (Track to use with UI).
        //Set the new object to our current activeObject.
        activeObject = Instantiate(obj,transform.position,transform.rotation);
        activeObject.tag = placedObjectTag;
        obj.SetActive(false);
        //Set the parent of the new object to this card zone
        activeObject.transform.SetParent(this.gameObject.transform);
    }

    public void RemoveActiveObject() {
        zoneObject.GetComponent<Collider>().enabled = true;
        zoneObject.GetComponent<MeshRenderer>().enabled = true;
        zoneObject.SetActive(true);
        zoneObject.transform.position = zoneObject.transform.parent.transform.position;
        GameObject.Destroy(activeObject);   //Destroy the object
        activeObject = null;    //Set our active object to null.
        hasActiveObject = false;
    }
    void OnCollisionEnter(Collision col) {
        Debug.Log(col.gameObject.GetComponent<Placeable>().id);
        if(col.gameObject.tag == "Placeable"){
            if(!hasActiveObject) SetActiveObject(col.gameObject);
        }
    }
}
