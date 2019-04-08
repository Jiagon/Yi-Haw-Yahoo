using UnityEngine;
using Vuforia;

public class CardTracker : MonoBehaviour, ITrackableEventHandler
{
    GameObject manager;
	LevelManager lvlManager;
	void Start(){
		lvlManager = manager.GetComponent<LevelManager>();
	}
	public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus){
    }

	void OnTrackingFound() {
		if(lvlManager.isIdEnabled(GetComponent<Placeable>().id)) {
			GetComponent<MeshRenderer>().enabled = false;
			GetComponent<Collider>().enabled = false;
		} else {
			manager.GetComponent<LevelManager>().UpdateIdList(GetComponent<Placeable>().id);
		}
        
	}

	void OnTrackingLost() {
		manager.GetComponent<LevelManager>().RemoveIdFromList(GetComponent<Placeable>().id);
	}
}
