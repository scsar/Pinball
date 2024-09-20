using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	[SerializeField] private GameObject scoreText;
	[SerializeField] private GameObject scoreImage;
	[SerializeField] private GameObject TimeText;
	[SerializeField] private GameObject timeImage;
	[SerializeField] private GameObject CoinText;
	[SerializeField] private GameObject coinImage;
	[SerializeField] private GameObject PauseText;
	[SerializeField] private GameObject itemInventory;
	[SerializeField] private GameObject countText;

	[SerializeField] private GameObject resultImage;

	[SerializeField] private GameObject CursorImage;

	[SerializeField] private GameObject restartButton;
	[SerializeField] private GameObject mainButton;
	[SerializeField] private GameObject StageButton;
	private int count = 3;

	private int coin = 0;

	private bool isPaused = false;
	public bool isPausedSet
	{
		get
		{
			return isPaused;
		}
		set
		{
			isPaused = value;
			if (isPaused)
			{
				Time.timeScale = 0f;
				PauseText.SetActive(true);
				restartButton.SetActive(true);
				mainButton.SetActive(true);
				StageButton.SetActive(true);
				CursorImage.SetActive(true);
			}
			else
			{
				Time.timeScale = 1f;
				PauseText.SetActive(false);
				restartButton.SetActive(false);
				mainButton.SetActive(false);
				StageButton.SetActive(false);
				CursorImage.SetActive(false);
			}
		}
	}
	private bool isLoaded = false;
	public bool isLoadedSet
	{
		get
		{
			return isLoaded;
		}
		set
		{
			isLoaded = value;
		}
	}
	private float _time = 60f;
	private int score = 0;
	private int x = 1;
	public int xSet
	{
		set
		{
			x = value;
			if (x > 1)
			{
				StartCoroutine(Timer());
			}
		}
	}
	public int scoreSet
	{
		get
		{
			return score;
		}
		set
		{
			score += value * x;
		}
	}
	private bool isPlayed = true;
	public bool isPlayedSet
	{
		get
		{
			return isPlayed;
		}
		set
		{
			isPlayed = value;
			if (isPlayed)
			{
				_time = 60;
				score = 0;
				Time.timeScale = 1;
				scoreText.SetActive(true);
				scoreImage.SetActive(true);
				
				TimeText.SetActive(true);
				timeImage.SetActive(true);

				itemInventory.SetActive(true);
				for (int i = 0; i < 3; i++)
				{
					itemInventory.transform.GetChild(i).gameObject.SetActive(false);
				}

				countText.SetActive(true);

				CoinText.SetActive(false);
				coinImage.SetActive(false);

				resultImage.SetActive(false);

				CursorImage.SetActive(false);

				StartCoroutine(CountDown());
			}
			else
			{
				scoreText.SetActive(false);
				scoreImage.SetActive(false);

				TimeText.SetActive(false);
				timeImage.SetActive(false);

				itemInventory.SetActive(false);

				countText.SetActive(false);

				resultImage.SetActive(false);

				CursorImage.SetActive(true);

				CoinText.SetActive(true);
				coinImage.SetActive(true);

				coin += score;
				PlayerPrefs.SetInt("Coin", coin);
			}
		}
	}

	public static GameManager Instance = null;
	// Use this for initialization
	void Start () 
	{
		coin = PlayerPrefs.GetInt("Coin", 0);
		isPlayedSet = false;
		Time.timeScale = 1;
		score = 0;
		if (Instance == null)
		{
			Instance = FindObjectOfType<GameManager>();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (SceneManager.GetActiveScene().name == "GameScene")
        {
			if (Input.GetKeyDown(KeyCode.Escape) && !isPlayed)
			{
				MainButton();
			}
            Cursor.lockState = CursorLockMode.Confined;
        }
		else if (SceneManager.GetActiveScene().name == "IntroScene")
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		

		if (isPlayed)
		{
			scoreText.GetComponent<Text>().text = "Score : " + score;

			TimeText.GetComponent<Text>().text = "Time : " + Mathf.Round(_time);

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				isPausedSet = !isPaused;
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				GameObject player = GameObject.FindWithTag("Player");
				player.transform.position = new Vector3(-36.55f, -0.153f, -49.265f);
				score -= 300;
			}
		}
		else
		{
			CoinText.GetComponent<Text>().text = "Coin : " + coin;
		}
	}

	void LateUpdate() 
	{
		if (isPlayed && count == 3)
		{
			_time -= Time.deltaTime;
			if (_time <= 0)
			{
				resultImage.SetActive(true);
				CursorImage.SetActive(true);
				ScoreStar();
				Time.timeScale = 0;
			}
		}	
	}

	IEnumerator Timer()
	{
		yield return new WaitForSeconds(10);
		x = 1;
	}

	IEnumerator CountDown()
	{
		while(count > 0)
		{
		count -= 1;
		countText.GetComponent<Text>().text = "" + count;
		yield return new WaitForSeconds(1); 
		}	
		countText.SetActive(false);
		count = 3;

		Rigidbody ballRig = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
		ballRig.constraints = RigidbodyConstraints.None;
	}

	public void RestartButton()
	{
		ResetPosition();
		isPausedSet = false;
		isPlayedSet = true;
	}

	public void StageSelect()
	{
		ResetPosition();
		isPausedSet = false;
		isPlayedSet = false;
		//Time.timeScale = 1;
	}

	public void StartButton()
	{
		SceneManager.LoadScene("IntroScene");
	}

	public void ExitButton()
	{
		Application.Quit();
	}

	public void MainButton()
	{
		SceneManager.LoadScene("LobbyScene");
	}

	public void TutorialButton()
	{
		SceneManager.LoadScene("Tutorial");
	}

	void ResetPosition()
	{
		GameObject player = GameObject.FindWithTag("Player");
		player.transform.position = new Vector3(-36.55f, -0.153f, -49.265f);
		Rigidbody ballRig = player.GetComponent<Rigidbody>();
		ballRig.constraints = RigidbodyConstraints.FreezePositionZ;
	}

	void ScoreStar()
	{
		resultImage.transform.GetChild(11).GetComponent<Text>().text = "Score : " + score;

		if (score < 3000)
		{

		}
		else if (score < 4500)
		{
			resultImage.transform.GetChild(1).gameObject.SetActive(true);
		}
		else if (score < 6000)
		{
			resultImage.transform.GetChild(1).gameObject.SetActive(true);
			resultImage.transform.GetChild(3).gameObject.SetActive(true);
		}
		else
		{
			resultImage.transform.GetChild(1).gameObject.SetActive(true);
			resultImage.transform.GetChild(3).gameObject.SetActive(true);
			resultImage.transform.GetChild(5).gameObject.SetActive(true);
		}
	}
}
