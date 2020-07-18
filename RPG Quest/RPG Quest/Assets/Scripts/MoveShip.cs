using UnityEngine;
using System.Collections;

public class MoveShip : MonoBehaviour {

	public GameObject ship, rpgPos, shipPos, player1, player2, rpg, game;
	public GameObject[] rpgTargets;
	public GameObject[] shipTargets;

	public bool m,n;

	public void Update() {
		if (m) {
			m = false;
			StartCoroutine (RPG ());
		}
		if (n) {
			n = false;
			StartCoroutine (Ship ());
		}
	}

	public void Move(int position) {
		StartCoroutine (
			position == 1 ? RPG() : Ship()
		);
	}

	public IEnumerator RPG() {
		float t = 0;
		GameObject cam = GameObject.Find ("Main Camera");
		CameraController cc = cam.GetComponent<CameraController> ();
		while (true) {
			t += Time.deltaTime;
			Vector3.Lerp (ship.transform.position, rpgPos.transform.position, t / 5f);
			if (t > 5) {
				for (int i = 0; i < cc.targets.Length; i++)
					cc.targets [i] = rpgTargets [i];
				game.SetActive (false);
				rpg.SetActive (true);
				break;
			}
			yield return null;
		}
	}

	public IEnumerator Ship() {
		float t = 0;
		while (true) {
			t += Time.deltaTime;
			Vector3.Lerp (ship.transform.position, shipPos.transform.position, t / 3f);
			if (t > 5) {
				rpg.SetActive (false);
				game.SetActive (true);
				break;
			}
			yield return null;
		}
	}
}
