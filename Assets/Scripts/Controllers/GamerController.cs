using System;
using System.Collections;
using UnityEngine;

public class GamerController : MonoBehaviour
{
    //Variaveis controle de tempo do jogo e spawn de inimigos do tipo chaser
    public double timeGaming;
    [SerializeField] private double timeCurrent;
    public double coldowSpawnEnemys;
    [SerializeField] private float startLastSpawn;
    [SerializeField] public float startLastSpawn1;

    //Variaveis para pegar o player, portoes 
    public GameObject[] ports;
    public GameObject shipChaser;
    public GameObject[] currentShips;
    public GameObject player;
    public GameObject playerBase;

    public bool start;
    public bool gameOver;
    public bool win;

    private UIController _uiController;


    void Start()
    {
        _uiController = FindObjectOfType(typeof(UIController)) as UIController;
        timeGaming = 60;
        coldowSpawnEnemys = 10;

    }

    // Update is called once per frame
    void Update()
    {

        if (start == true)
        {
            //Spawn de inimigos
            if (currentShips[0] == null && ports[0] != null)
            {
                Spawn();
            }
            else if (currentShips[1] == null && ports[1] != null)
            {
                SpawnP1();
            }
            // Verifica se o tempo acabou ou nao
            if (timeCurrent < timeGaming)
            {
                timeCurrent += Time.deltaTime;
                _uiController.textTime.text = TimeSpan.FromMinutes(timeCurrent).ToString(@"hh\:mm\:ss");
            }
            else if (timeCurrent >= timeGaming)
            {
                GameOver();
            }
        }

        else if (gameOver == true || win == true)
        {
            _uiController.textTimefinishedGO.text = "Melhor Tempo: " + TimeSpan.FromMinutes(timeCurrent).ToString(@"hh\:mm\:ss"); ;
            _uiController.textTimefinishedW.text = "Melhor Tempo: " + TimeSpan.FromMinutes(timeCurrent).ToString(@"hh\:mm\:ss"); ;
            StartCoroutine(LoserPlayer(1f));
        }
    }

    //Metodos para spawnar inimigos do tipo chaser
    public void Spawn()
    {
        if (Time.time > coldowSpawnEnemys + startLastSpawn)
        {
            startLastSpawn = Time.time;
            currentShips[0] = Instantiate(shipChaser, ports[0].transform.position, ports[0].transform.rotation);

        }
    }
    public void SpawnP1()
    {
        if (Time.time > coldowSpawnEnemys + startLastSpawn1)
        {
            startLastSpawn1 = Time.time;
            currentShips[1] = Instantiate(shipChaser, ports[1].transform.position, ports[1].transform.rotation);

        }
    }

    //Metodos para controlar a partida
    public void Game()
    {
        Time.timeScale = 1f;
        start = true;
    }

    public void GameOver()
    {
        start = false;
        gameOver = true;
    }

    public void Win()
    {
        win = true;
        start = false;
    }

    IEnumerator LoserPlayer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Time.timeScale = 0f;
    }

}
