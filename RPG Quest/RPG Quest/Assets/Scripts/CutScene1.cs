using UnityEngine;
using System.Collections;

public class CutScene1 : MonoBehaviour {
	public Transform target;
	public GameObject right, left;
	public bool test;
	public GameObject particlePos, particle, light, boss;
	public bool opened;
	public string function;
	public void Update () {
		if (test) {
			StartCoroutine (function); test = false;
		}
	}

	public void OnTriggerEnter (Collider col) 
	{
		if (col.gameObject.tag == "Player") {
			StartCoroutine (function);
		}
	}

	public IEnumerator Scene ()
	{
		mob.canMove = false;
		GameObject cam = GameObject.Find("Main Camera");
		GameObject player = GameObject.Find ("Player");
		//cam.GetComponent<CameraController> ().enabled = false;
		CameraController.getInstance.StopFollow();
		ClickToMove move = player.GetComponentInChildren<ClickToMove>();
		move.enabled = false;
		Vector3 pos = cam.transform.position;
		Quaternion rot = cam.transform.rotation;
		cam.transform.position = target.transform.position;
		cam.transform.rotation = target.transform.rotation;
		bool deleted = false;
		float t = 0f;
		Invoke ("PlayParticle", .5f);
		while (true) {
			t += Time.deltaTime;
			if (!deleted && t > 1f) {
				Gates ();
				deleted = true;
			}


			if (t > 4) {
				cam.transform.position = pos;
				cam.transform.rotation = rot;
				cam.GetComponent<CameraController> ().enabled = true;
				move.enabled = true;
				CameraController.getInstance.Reset ();
				if(GetComponent<BoxCollider>())
					GetComponent<BoxCollider> ().enabled = false;
				mob.canMove = true;
				break;
			}
			yield return null;
		}
	}

	public IEnumerator TombScene ()
	{
		mob.canMove = false;
		GameObject cam = GameObject.Find("Main Camera");
		GameObject player = GameObject.Find ("Player");
		CameraController.getInstance.StopFollow();
		ClickToMove move = player.GetComponentInChildren<ClickToMove>();
		move.enabled = false;
		Vector3 pos = cam.transform.position;
		Quaternion rot = cam.transform.rotation;
		cam.transform.position = target.transform.position;
		cam.transform.rotation = target.transform.rotation;
		bool played = false;
		float t = 0f;
		Invoke ("PlayParticle", .5f);
		while (true) {
			t += Time.deltaTime;
			if (!played && t > 1f) {
				if (light != null)
					Lights ();
				played = true;
			}
			if (t > 4) {
				cam.transform.position = pos;
				cam.transform.rotation = rot;
				cam.GetComponent<CameraController> ().enabled = true;
				move.enabled = true;
				CameraController.getInstance.Reset ();
				mob.canMove = true;
				break;
			}
			yield return null;
		}
	}

	public IEnumerator BossScene ()
	{
		mob.canMove = false;
		GameObject cam = GameObject.Find("Main Camera");
		GameObject player = GameObject.Find ("Player");
		CameraController.getInstance.StopFollow();
		ClickToMove move = player.GetComponentInChildren<ClickToMove>();
		move.enabled = false;
		Vector3 pos = cam.transform.position;
		Quaternion rot = cam.transform.rotation;
		cam.transform.position = target.transform.position;
		cam.transform.rotation = target.transform.rotation;
		float t = 0f;
		Invoke ("PlayParticle", .2f);
		Invoke ("SpawnBoss", .7f);
		while (true) {
			t += Time.deltaTime;
			if (t > 4) {
				cam.transform.position = pos;
				cam.transform.rotation = rot;
				cam.GetComponent<CameraController> ().enabled = true;
				move.enabled = true;
				CameraController.getInstance.Reset ();
				mob.canMove = true;
				break;
			}
			yield return null;
		}
	}

	public IEnumerator CameraScene ()
	{
		mob.canMove = false;
		GameObject cam = GameObject.Find("Main Camera");
		GameObject player = GameObject.Find ("Player");
		CameraController.getInstance.StopFollow();
		ClickToMove move = player.GetComponentInChildren<ClickToMove> ();
		move.cameraPosition = target;
		move.cutSceneMove = true;
		Vector3 pos = cam.transform.position;
		Quaternion rot = cam.transform.rotation;
		cam.transform.position = target.transform.position;
		cam.transform.rotation = target.transform.rotation;
		float t = 0f;
		while (true) {
			t += Time.deltaTime;

			if (t > 4) {
				cam.transform.position = pos;
				cam.transform.rotation = rot;
				cam.GetComponent<CameraController> ().enabled = true;
				move.enabled = true;
				CameraController.getInstance.Reset ();
				if(GetComponent<BoxCollider>())
					GetComponent<BoxCollider> ().enabled = false;
				move.cutSceneMove = false;
				mob.canMove = true;
				break;
			}
			yield return null;
		}
	}

	public void Lights () {
		light.SetActive (
			opened ? false : true
		);
	}

	public void Gates() {
		right.SetActive (
			opened ? true : false
		);
		left.SetActive (
			opened ? true : false
		);
		opened = !opened;
	}

	public void PlayParticle() {
		GameObject obj = (GameObject)Instantiate (particle, particlePos.transform.position, Quaternion.identity);
		Destroy (obj, 2f);
	}
	public void SpawnBoss() {
		boss.SetActive (true);
		boss.transform.position = particlePos.transform.position;
		boss.transform.rotation = particlePos.transform.rotation;
	}
}
