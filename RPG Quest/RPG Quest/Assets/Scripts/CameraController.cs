using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	private static CameraController c = null;
	public static CameraController getInstance {
		get {
			return c == null ? c = GameObject.Find("Main Camera").GetComponent<CameraController>() : c;
		}
	}

	public GameObject[] targets;


	public Transform cameraPivot;
	public float moveDamping = 3f;
	public float yDistance;
	public Bounds bounds;
	public bool stop = false;
	public GameObject game;

	void Start()
	{
		DontDestroyOnLoad (game);
		StartCoroutine(FollowPlayer());
	}

	public void StopFollow () {
		stop = true;
	}

	public void Reset() {
		stop = false;
		StartCoroutine(FollowPlayer());

	}

	IEnumerator FollowPlayer()
	{
		while (true)
		{
			if (stop)
				break;
			bounds = GetBounds();
			var targetPosition = bounds.center/*Vector3.ClampMagnitude(bounds.center, maxDistance)*/;
			//targetPosition.z = 0;
			targetPosition.y += yDistance;
			cameraPivot.position = Vector3.Lerp(cameraPivot.position, targetPosition, moveDamping * Time.deltaTime);
			yield return null;
		}
	}

	void OnDrawGizmos()
	{
		var bounds = GetBounds();

		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(bounds.center, bounds.size);
	}

	Bounds GetBounds()
	{
		var bounds = new Bounds ();
		//if(CameraTarget.transforms[0].position != null)
		bounds = new Bounds(targets[0].transform.position, new Vector3(0, 2, 0));
		for (int i = 1; i < targets.Length; i++) {
			bounds.Encapsulate (targets[i].transform.position);
		}
		return bounds;
	}
}
