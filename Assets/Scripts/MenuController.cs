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

    private int indexBirdSelected;
    GameObject characterSelected;
    private float timer;
    private bool isPlay;
    private float timeSpawn;
    private GameObject player;

    private void Awake()
    {
        indexBirdSelected = 0;
        characterSelected = Instantiate(birdPrefabs[indexBirdSelected], Vector3.zero, Quaternion.identity);
        characterSelected.transform.SetParent(character.transform);
        panelMenu.SetActive(true);
        panelFinish.SetActive(false);
        panelPlay.SetActive(false);
        isPlay = false;
        timer = 0;
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
     
        if(isPlay)
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
        isPlay = true;
        timer = Time.time;
        timeSpawn = Time.time;
        panelMenu.SetActive(false);
        panelPlay.SetActive(true);
        player = Instantiate(birdPrefabs[indexBirdSelected], new Vector3(0, -0.5f, 0), Quaternion.identity);
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

    
}
