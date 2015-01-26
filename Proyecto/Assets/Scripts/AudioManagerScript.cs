using UnityEngine;
using System.Collections;

public class AudioManagerScript : MonoBehaviour {

	public AudioSource[] factory;
	// Use this for initialization
	void Start () {
		factory = gameObject.GetComponents<AudioSource>();
	}
	private void muteWorld()
	{
		foreach (AudioSource item in factory) {
			item.volume=0;
		}
	}
	public void playWrong()
	{
		AudioSource wrongSound = factory[1];
		if(!wrongSound.isPlaying)
		{
			wrongSound.Play();
		}
	}
	public void playSwitch()
	{
		AudioSource switchSound = factory[2];
		if(!switchSound.isPlaying)
		{
			switchSound.Play();
		}
	}
	public void playWallSwitch()
	{
		AudioSource wallSound = factory[3];
		if(!wallSound.isPlaying)
		{
			wallSound.Play();
		}
	}
	public void playTeleport()
	{
		AudioSource teleport = factory[4];
		if(!teleport.isPlaying)
		{
			teleport.Play();
		}
	}
	public void playRescueFriends()
	{
		AudioSource rescueSound = factory[5];
		if(!rescueSound.isPlaying)
		{
			rescueSound.Play();
		}
	}
	public void playHurtMan()
	{
		AudioSource hurtSound = factory[7];
		if(!hurtSound.isPlaying)
		{
			hurtSound.Play();
		}
	}
	public void playHurtWoman()
	{
		AudioSource hurtSound = factory[6];
		if(!hurtSound.isPlaying)
		{
			hurtSound.Play();
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
