using UnityEngine;
using System.Collections;

public class Tomb : MonoBehaviour {

	public GameObject light;
	public int count = 0, total;
	CutScene1 scene;
	public Tomb tomb;
	public GameObject enemies;
	public string function = "PlayScene";
	private bool isActive = true;

	public void Start () {
		scene = GetComponent<CutScene1> ();
	}

	// Update is called once per frame
	void Update () {
		if (count >= total && isActive) {
			isActive = false;
			Invoke (function, 0f);
		}
	}

	public void PlayScene () {
		scene.StartCoroutine (scene.TombScene ());
		tomb.count++;
		if (enemies != null)
			enemies.SetActive (true);
		this.enabled = false;
	}

	public void GateScene() {
		scene.StartCoroutine (scene.Scene ());
		if (enemies != null)
			enemies.SetActive (true);
		this.enabled = false;
	}

	private void Spawn() {
		scene.StartCoroutine (scene.BossScene ());
		this.enabled = false;
	}

	public void SpawnBoss () {
		Invoke ("Spawn", 6f);
	}

	public void OpenGate () {
		scene.StartCoroutine (scene.Scene ());
		this.enabled = false;
	}
}
