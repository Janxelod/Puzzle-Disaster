using UnityEngine;
using System.Collections;

public class HitCollisionScript : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void onTriggerEnter(Collider2D other)
	{
		if (other.gameObject.name == "Teleporter") {

		}else if (other.gameObject.name == "Laser(Clone)") {

		}else if(other.gameObject.name=="InterruptorArea"){

		}else if(other.gameObject.name=="CollisionLava"){

		}
	}
}
