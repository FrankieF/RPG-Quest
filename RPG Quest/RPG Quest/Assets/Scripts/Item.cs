using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public TextScene text;
    public int attack;
    public Fighter player;

    public IEnumerator Run()
    {
        while (true)
        {
            player.damage += attack;
            text.enabled = true;
            StopCoroutine("Run");
            yield return null;
        }
    }
}
