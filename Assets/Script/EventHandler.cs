using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour {


	[SerializeField] private Camera mainCamera;
	[SerializeField] private Camera loadCamera;
	string scenename;
	// Use this for initialization
	[SerializeField] private GameObject player;
	private PlayerUIController playeruicontroller;
	private Rigidbody p_rigidbody;


	void Start () 
	{
		playeruicontroller = player.GetComponent<PlayerUIController>();
		p_rigidbody = player.GetComponent<Rigidbody>();

		mainCamera.enabled = true;
		loadCamera.enabled = false;

		if (playeruicontroller != null)
		{
			playeruicontroller.OnDialogueEndEvent += Handler;
		}

	}
	
	// Update is called once per frame
	void OnDestroy () 
	{
		if (playeruicontroller != null)
		{
			playeruicontroller.OnDialogueEndEvent -= Handler;
		}
	}

	void Handler()
	{
		scenename = SceneManager.GetActiveScene().name;
		if (scenename == "IntroScene")
			StartCoroutine(IntroTrigger());	
		if (playeruicontroller.ename == "Play")
		{
			StartCoroutine(PlayLoading());
		}
		
	}

	IEnumerator IntroTrigger()
	{
		p_rigidbody.freezeRotation = false;
		player.transform.Rotate(new Vector3(0f, 0f, 2f));
		yield return new WaitForSeconds(2);
		GameManager.Instance.isLoadedSet = true;
		mainCamera.enabled = !mainCamera.enabled;
		loadCamera.enabled = !loadCamera.enabled;
		yield return new WaitForSeconds(9);
		SceneManager.LoadScene("GameScene");
	}

	IEnumerator PlayLoading()
	{
		yield return new WaitForSeconds(2);
		GameManager.Instance.isLoadedSet = true;
		mainCamera.enabled = !mainCamera.enabled;
		loadCamera.enabled = !loadCamera.enabled;
		yield return new WaitForSeconds(9);
		GameManager.Instance.isPlayedSet = true;
		mainCamera.enabled = !mainCamera.enabled;
		loadCamera.enabled = !loadCamera.enabled;
	}
}
