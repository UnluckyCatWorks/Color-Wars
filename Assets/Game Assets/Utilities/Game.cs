using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	public AudioSource menu;
	public AudioSource game;

	public GameObject main;
	public GameObject pause;
	public GameObject end;

	public Player[] pjs;

	public Text[] txts;
	public GameObject[] imgs;

	public void Play ()
	{
		main.SetActive ( false );
		pjs[0].gameObject.SetActive ( true ) ;
		pjs[1].gameObject.SetActive ( true );
		menu.Stop ();
		game.Play ();

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		Time.timeScale = 1;
	}

	public void Exit () { Application.Quit (); }

	public void Win ( string win, string lose ) 
	{
		Time.timeScale = 0;

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		end.SetActive ( true );
		GameObject.Find ( "sad" + lose ).SetActive ( true );
		GameObject.Find ( "happy" + lose ).SetActive ( false );
		GameObject.Find ( "happy" + win ).SetActive ( true );
		GameObject.Find ( "sad" + win ).SetActive ( false );

		int count=0;
		for ( int i=0; i!=2; i++ )
		{
			var n = (int) Mathf.Clamp ( (10f - pjs[i].hp) * 10, 0, 100 );
			if (n==0) count++;
			if ( count==2 )
			{
				GameObject.Find ( "eru%" ).GetComponent<Text> ().text = "1 %";
			}
			else txts[i].text = n.ToString () + " %";
		}
	}

	bool paused;
	public void Pause ()
	{
		Time.timeScale = paused ? 1 : 0;
		pause.SetActive ( paused ? false : true );
	}

	public void Restart () { UnityEngine.SceneManagement.SceneManager.LoadScene ( 0 ); }
}
