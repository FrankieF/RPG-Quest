using UnityEngine;
using System.Collections;

public class LevelSystem : MonoBehaviour {

	//we need 100 exp to level up 
	public int Level;
	public int exp;
	public int expNeeded;
	public Fighter Player;
	public bool levelup;

	// Use this for initialization
	void Start () 
	{
		expNeeded =(int)(Mathf.Pow(Level,2)+100);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Y))
			exp += 100;
		LevelUp ();
	}

	void LevelUp()
	{
		// we will make an  exponetial leveling system ^3 +100
		if (exp >= expNeeded) 
		{
			expNeeded =(int)(Mathf.Pow(Level,2)+100 + (expNeeded*Level));
			Level = Level+1;
			exp = exp - (int)(Mathf.Pow(Level,2)+100);
			levelup = true;
		}

	}

	void LevelEffect(int stat)
	{
		switch (stat) {
		case 1 : 
			Player.damage += (int)Mathf.Pow (Level, 2) + 20;
			break;
		case 2: 
			Player.maxHealth +=100;
			break;
		case 3: 
			if (Player.specialAttack < Player.attacks.Length) {
				Player.attacks [Player.specialAttack].enabled = true;
				Player.specialAttack++;
			}
			break;
		}
		Player.Health = Player.maxHealth;
		levelup = false;
	}

	public void OnGUI() {
		if (levelup) {
			if (GUI.Button (new Rect (10, 70, 80, 25f), "DAMAGE")) 
				LevelEffect (1);
			if (GUI.Button (new Rect (10, 40, 80f, 25f), "HEALTH"))
				LevelEffect (2);
			if (GUI.Button (new Rect (10, 100, 80f, 25f), "POWER")) 
				LevelEffect (3);
			
		}
	}
}
