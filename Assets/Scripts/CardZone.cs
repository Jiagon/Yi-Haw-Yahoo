using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZone : MonoBehaviour
{
    public bool hasActiveObject;
    public LevelManager manager;
    GameObject zoneObject;  //This is the object that we hit (save for restoration).
    public GameObject activeObject;
    string placedObjectTag = "placedObject";
    public Material taken;
    public Material free;
    public void SetActiveObject(GameObject obj) {
        zoneObject = obj;   //Store this so we can remember it later.
        hasActiveObject = true; //Set so we haveActiveObject (Track to use with UI).
        //Set the new object to our current activeObject.
        activeObject = Instantiate(obj,transform.position,transform.rotation);
        activeObject.transform.localScale *= 3f;
        activeObject.tag = placedObjectTag;
        obj.SetActive(false);
        //Set the parent of the new object to this card zone
        activeObject.transform.SetParent(this.gameObject.transform);

        //Change the material
        GetComponent<MeshRenderer>().material = taken;
    }

    public void RemoveActiveObject() {
        if(zoneObject != null && activeObject != null){
            zoneObject.SetActive(true);
            zoneObject.transform.position = zoneObject.transform.parent.transform.position;
            GameObject.Destroy(activeObject);   //Destroy the object
            activeObject = null;    //Set our active object to null.
            hasActiveObject = false;
            //Change the material
            GetComponent<MeshRenderer>().material = free;
        }
    }
    void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Placeable")
        {
            Debug.Log(col.gameObject.GetComponent<Placeable>().id);
            Debug.Log("COLLIDED");
            if(!hasActiveObject) SetActiveObject(col.gameObject);
        }
    }
}
