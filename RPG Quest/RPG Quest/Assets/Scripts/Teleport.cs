using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public Fighter player;
	public KeyCode key;
	public bool inAction;
	public GameObject ParticlePrefab;
	private GameObject instantiated;
	private Vector3 lastPos;
	private bool usemove = true;
	public float waittime;
	public Vector3 position;
	public CharacterController CharControl;
	public AnimationClip idle;

	// Use this for initialization
	void Start () {
		position = transform.position;
	}

	// Update is called once per frame
	void FixedUpdate () 
	{



		if(Vector3.Distance(lastPos, transform.position) > 0.01f) 
		{ 
			lastPos = transform.position; 

		}	

		if (Input.GetKeyDown (key) && usemove) 
		{
			usemove = false;
			player.resetAttack ();
			player.Special_attack = true;
			inAction = true;

			Locate_Position ();
			TeleportToPosition ();

			lastPos = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);

			instantiated = (GameObject)Instantiate (ParticlePrefab, 
				lastPos, transform.rotation);

			StopCoroutine ("Destroy");    // Interrupt in case it's running
			StartCoroutine ("Destroy");
		}

		if (inAction) 
		{
			
			if (GetComponent<Animation>()[player.attack.name].time > 0.9*GetComponent<Animation>()[player.attack.name].length)
			{
				inAction = false;
				player.Special_attack = false;
			}
		}
	}

	//locate position of the user click
	void Locate_Position ()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit, 1000)) {
			if (hit.collider.tag == "floor")
				position = new Vector3 (hit.point.x, hit.point.y, hit.point.z);
		}
	}

	//turn and move the player
	void TeleportToPosition ()
	{ 
		//when game object is moving 
		if (Vector3.Distance (transform.position, position) > 1) {
			player.transform.position = position;
			//GetComponent<Animation> ().CrossFade (idle.name);
		}
	}

	IEnumerator Destroy()
	{
		yield return new WaitForSeconds(waittime);
		if (instantiated != null)
		{
			Destroy(instantiated); 
			usemove = true;
		}
		player.Special_attack = false;
		inAction = false;
	}

}