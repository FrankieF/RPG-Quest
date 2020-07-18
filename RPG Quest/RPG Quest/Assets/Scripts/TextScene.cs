using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScene : MonoBehaviour
{
    public string message;
    public string button;
    public bool isFinished = false;

    void Start()
    {
        Time.timeScale = 0f;
    }

    void Update ()
    {
        if (isFinished)
        {
            ResetTime();
            Destroy(gameObject);
        }
	}

    private void ResetTime()
    {
        Time.timeScale = 1f;
    }

    private void OnGUI()
    {
        GUI.skin.box.wordWrap = true;
        GUI.Box(new Rect((Screen.width * .5f)-100, (Screen.height * .5f)-100, 200f, 200f), message);

        if (GUI.Button(new Rect(Screen.width * .5f - 50, Screen.height * .5f + 100, 100, 50), button, GUI.skin.GetStyle("Button")))
        {
            isFinished = true;
        }
    }
}
