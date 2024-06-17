using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject[] birdPrefabs;
    [SerializeField] GameObject character;
    [SerializeField] GameObject panelMenu;
    [SerializeField] GameObject panelPlay;
    [SerializeField] GameObject panelFinish;
    [SerializeField] GameObject[] SpawnPoints;
    [SerializeField] GameObject[] items;
    [SerializeField] TMP_Text txtTime;
    [SerializeField] TMP_Text txtScoreFinish;
    [SerializeField] TMP_Text txtScorePlay;
    [SerializeField] GameObject panelHighScore;

    private int indexBirdSelected;
    GameObject characterSelected;
    private float timer;
    public static bool isPlay;
    private float timeSpawn;
    private GameObject player;
    private int HighScore;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
        }
        indexBirdSelected = 0;
        characterSelected = Instantiate(birdPrefabs[indexBirdSelected], Vector3.zero, Quaternion.identity);
        characterSelected.transform.SetParent(character.transform);
        panelMenu.SetActive(true);
        panelFinish.SetActive(false);
        panelPlay.SetActive(false);
        panelHighScore.SetActive(false);
        isPlay = false;
        timer = 0;
    }

    private void Start()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.HasKey("Score"))
        {
            HighScore = PlayerPrefs.GetInt("Score");
        }

    }

    private void Update()
    {

        if (isPlay)
        {
            txtScorePlay.text = "Score: " + player.GetComponent<PlayerController>().score.ToString();
            if (player.GetComponent<PlayerController>().isFinish)
            {
                panelPlay.SetActive(false);
                txtScoreFinish.text = "Score: " + player.GetComponent<PlayerController>().score.ToString();
                panelFinish.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                float sTimer = Time.time - timer;
                txtTime.text = string.Format("{0:00}:{1:00}", sTimer / 60, sTimer % 60);
                player.GetComponent<PlayerController>().speed = 10 * (1 + (Time.time - timer) / 60);
                if (Time.time - timeSpawn >= 2f)
                {
                    timeSpawn = Time.time;
                    int itemIndex = Random.Range(0, items.Length - 1);
                    int point = Random.Range(0, 100) % 2;
                    GameObject item = Instantiate(items[itemIndex], SpawnPoints[point].transform.position, Quaternion.identity);
                    if (item.gameObject.tag.Equals("Trap"))
                    {
                        TrapController script = item.GetComponent<TrapController>();
                        script.speed = 3 * (1 + (Time.time - timer) / 60);
                    }
                    else if (item.gameObject.tag.Equals("Fruit"))
                    {
                        FruitController script = item.GetComponent<FruitController>();
                        script.speed = 3 * (1 + (Time.time - timer) / 60);
                    }
                }
            }
            
        }   
    }
    public void btnNextCharacter()
    {
        indexBirdSelected++;
        if(indexBirdSelected >= birdPrefabs.Length)
        {
            indexBirdSelected = 0;
        }
        Destroy(characterSelected);
        characterSelected = Instantiate(birdPrefabs[indexBirdSelected], Vector3.zero, Quaternion.identity);
        characterSelected.transform.SetParent(character.transform);
    }

    public void btnBackCharacter()
    {
        indexBirdSelected--;
        if (indexBirdSelected < 0)
        {
            indexBirdSelected = birdPrefabs.Length - 1;
        }
        Destroy(characterSelected);
        characterSelected = Instantiate(birdPrefabs[indexBirdSelected], Vector3.zero, Quaternion.identity);
        characterSelected.transform.SetParent(character.transform);
    }

    public void btnStartGame()
    {
        Destroy(characterSelected);
        isPlay = true;
        timer = Time.time;
        timeSpawn = Time.time;
        panelMenu.SetActive(false);
        panelPlay.SetActive(true);
        player = Instantiate(birdPrefabs[indexBirdSelected], new Vector3(0, 0, 0), Quaternion.identity);
        int itemIndex = Random.Range(0, items.Length - 1);
        int point = Random.Range(0, 1);
        GameObject item = Instantiate(items[itemIndex], SpawnPoints[point].transform.position, Quaternion.identity);
        if (item.gameObject.tag.Equals("Trap"))
        {
            TrapController script = item.GetComponent<TrapController>();
            script.speed = 3 * (1 + (Time.time - timer) / 60);
        }
        else if (item.gameObject.tag.Equals("Fruit"))
        {
            FruitController script = item.GetComponent<FruitController>();
            script.speed = 3 * (1 + (Time.time - timer) / 60);
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MenuGame");
    }

    public void OnShowScore()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            panelHighScore.SetActive(true);
            TMP_Text txtHighScore = panelHighScore.GetComponentInChildren<TMP_Text>();
            txtHighScore.text = PlayerPrefs.GetInt("Score").ToString();
        }
    }

    public void TurnOffHighScore()
    {
        panelHighScore.SetActive(false);
    }
}
