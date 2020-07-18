using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	public GameObject spwanParticle, particlePosition, boss, bossPosition;
	public mob mob;
	Tomb tomb;

	public void Start () {
		StartCoroutine (Battle ());
		mob = GetComponent<mob> ();
	}

	public IEnumerator Battle () {
		while (true) {
			if (mob.health < 1)
				tomb.OpenGate ();
			yield return null;
		}
	} 
}
