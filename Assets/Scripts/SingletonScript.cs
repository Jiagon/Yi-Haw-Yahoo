using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingletonScript : MonoBehaviour
{
    public int upgrades = 0;
    public GameObject upgradeDisplay;

    public static SingletonScript instance;
    public static SingletonScript Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<SingletonScript>();
                if(instance == null)
                {
                    GameObject singleton = new GameObject("Singleton Manager");
                    instance = singleton.AddComponent<SingletonScript>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ResetComponents()
    {
        upgradeDisplay = GameObject.Find("upgradeNum");
        upgradeDisplay.GetComponent<Text>().text = "0" + upgrades;
    }

    public void ChangeUpgradeNum(int numUpgrades)
    {
        upgrades = numUpgrades;
        upgradeDisplay.GetComponent<Text>().text = "0" + upgrades;
    }
}
