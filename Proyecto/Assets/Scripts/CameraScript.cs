using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	void FixedUpdate(){
		Vector3 position = player.transform.position;
		if (position.y > -311 || position.y<-930) {
			if(position.y>-311)
			{
				transform.position = new Vector3 (transform.position.x, -311, transform.position.z);
			}
			if(position.y<-930)
			{
				transform.position = new Vector3 (transform.position.x, -930, transform.position.z);
			}
		} else {
			transform.position = new Vector3 (transform.position.x, position.y, transform.position.z);
		}
		if (position.x<330+48||position.x>1105+48) {
			if(position.x<330+48)
			{
				transform.position = new Vector3 (330+48, transform.position.y, transform.position.z);
			}
			if(position.x>1105+48)
			{
				transform.position = new Vector3 (1105+48, transform.position.y, transform.position.z);
			}
		} else {
			transform.position = new Vector3 (position.x, transform.position.y, transform.position.z);
		}
	}
	// Update is called once per frame
	void Update () {

	}
}
