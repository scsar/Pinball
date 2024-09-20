using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBgm : MonoBehaviour {

	private AudioSource audio;
	public AudioClip clip, clip2;

	// Use this for initialization
	void Start () 
	{
		audio = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GameManager.Instance.isPlayedSet && audio.clip != clip)
		{
			audio.clip = clip;
			audio.Play();
		}
		else if (!GameManager.Instance.isPlayedSet && audio.clip != clip2)
		{
			audio.clip = clip2;
			audio.Play();
		}
		
	}
}
