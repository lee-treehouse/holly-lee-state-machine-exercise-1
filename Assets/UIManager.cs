using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Text distanceTxt;
    public Text stateTxt;

    bool visible = false;

    static UIManager instance;
    public static UIManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetDistance(float d)
    {
        distanceTxt.text = string.Format("Distance: {0:0.00}", d);
    }

    public void SetState(string s)
    {
        stateTxt.text = "State: "+ s;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("1"))
        {
            visible = !visible;
        }
        distanceTxt.enabled = visible;
        stateTxt.enabled = visible;
	}
}
