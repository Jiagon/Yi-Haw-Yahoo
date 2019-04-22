using UnityEngine;
using Vuforia;

public class CardTracker : MonoBehaviour, ITrackableEventHandler
{
	public LevelManager lvlManager;
	void Start(){
	}
	public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus){
    }

	void OnTrackingFound() {
		if(lvlManager.isIdEnabled(GetComponent<Placeable>().id)) {
			GetComponent<MeshRenderer>().enabled = false;
			GetComponent<Collider>().enabled = false;
		} else {
			Debug.Log("We're adding an ID");
			lvlManager.UpdateIdList(GetComponent<Placeable>().id);
		}
        
	}

	void OnTrackingLost() {
		Debug.Log("Removing an id");
		lvlManager.RemoveIdFromList(GetComponent<Placeable>().id);
	}
}
