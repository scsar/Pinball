using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUIController : MonoBehaviour,InterAction {
	
	[SerializeField] Camera cam;    // 우리가 보고 있는 카메라
	RaycastHit hitInfo; // 레이저를 쏴서 맞춘 오브젝트 정보를 저장

	[SerializeField] private GameObject talkImage, nameImage;
	private Text talkText, nameText;
	private bool isPlayed;
	private bool isTalk;
	private string Scenename = null;
	[SerializeField] private TalkData[] talkDatas;
	private string typeText = "";

	[HideInInspector] public string ename;

	private AudioSource audio;

	
    public AudioClip[] audioClips;


	// 이벤트 관리를위한 핸들러
    public delegate void DialogueEndHandler();
    public event DialogueEndHandler OnDialogueEndEvent;

	// // outlineShader
	// Material outline;
	// Renderer renderers;
	// List<Material> materialList = new List<Material>();

	void Awake()
	{
		talkImage.SetActive(false);
		nameImage.SetActive(false);
		talkText = talkImage.transform.GetChild(0).GetComponent<Text>();
		nameText = nameImage.transform.GetChild(0).GetComponent<Text>();
		audio = GetComponent<AudioSource>();
	}

	void Start()
	{
		isTalk = false;

		// Scene이름을 가져옴
		Scenename = SceneManager.GetActiveScene().name;
		// Scene에 최초진입인경우 false 아닌경우 true
		//isPlayed = PlayerPrefs.GetInt(Scenename) == 1 ? true : false;
		isPlayed = false;
		if (!isPlayed) //해당씬에 처음 진입한경우
		{
			PlayerPrefs.SetInt(Scenename, 1); 
			// 씬에대한 대사 배열 가져옴
			talkDatas = Dialogue.GetObjectDialogue(Scenename);
			StartCoroutine(ShowNextDialogue());
		}
		else if (SceneManager.GetActiveScene().name == "IntroScene")
		{
			SceneManager.LoadScene("GameScene");
		}
		
		// outline = new Material(Shader.Find("Draw/OutlineShader"));
	}


	void Update () 
	{
		if (!GameManager.Instance.isPlayedSet)
		{
			audio.clip = audioClips[1];
			if (Input.GetMouseButtonDown(0) && !isTalk)
			{	
				Inter();
			}

		}
		else
		{
			audio.clip = audioClips[0];
		}
		

	}

	public void Inter()
	{
		Vector3 t_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);  // 마우스의 위치

    	// 마우스 위치에서 레이저를 쏘기
    	if (Physics.Raycast(cam.ScreenPointToRay(t_mousePos), out hitInfo, 10))
    	{	
			talkDatas = hitInfo.transform.GetComponent<Dialogue>().GetObjectDialogue();
			if (talkDatas != null)
			{
				ename = hitInfo.transform.GetComponent<Dialogue>().geteventname;
				StartCoroutine(ShowNextDialogue());
			}
    	}
	}

	IEnumerator ShowNextDialogue()
	{
		isTalk = true;
		talkImage.SetActive(true);
		nameImage.SetActive(true);
		yield return new WaitForSeconds(0.001f);
		for (int i = 0; i < talkDatas.Length; i++)
		{
			nameText.text = talkDatas[i].name;
			foreach (string context in talkDatas[i].contexts)
			{
				audio.Play();
				yield return new WaitForSeconds(0.05f);
				for (int j = 0; j < context.Length; j++)
				{
					typeText += context[j];
					talkText.text = typeText;
					yield return null;
				}
				typeText = "";
				audio.Stop();
				yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
			}
		}
		talkImage.SetActive(false);
		nameImage.SetActive(false);
		isTalk = false;
		OnDialogueEnd();
	}

	void OnDialogueEnd()
	{
		// 이벤트 트리거
		if (OnDialogueEndEvent != null)
		{
			OnDialogueEndEvent();
		}
	}


}
