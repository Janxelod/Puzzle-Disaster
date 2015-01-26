using UnityEngine;
using System.Collections;

public class RescueFriends : RecieverScript {

	public int currentFriends=2;
	public bool isRescated=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void startAction()
	{
		GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().playRescueFriends();
		GameObject.Find("Player").GetComponent<PlayerScript>().totalFriends++;
		currentFriends++;
		isRescated=true;
		Debug.Log("Rescue Friends");

	}
	public override void makeAction()
	{
		if(!isRescated)
		{
			Invoke("startAction",1f);
		}

	}
}
