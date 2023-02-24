using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private double inputTempo;
    private double inputSpawn;

    public GameObject panelSettings;
    public GameObject panelMenu;
    public GameObject panelWin;
    public GameObject panelGamerOver;

    public TMP_Text textTime;
    public TMP_Text textTimefinishedGO;
    public TMP_Text textTimefinishedW;

    private GamerController _gamerController;
    void Start()
    {
        _gamerController = FindObjectOfType(typeof(GamerController)) as GamerController;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gamerController.win == true && panelWin.activeSelf == false)
        {
            PanelActiveDesactive(panelWin);
        }
        if (_gamerController.gameOver == true && panelGamerOver.activeSelf == false)
        {
            PanelActiveDesactive(panelGamerOver);
        }
    }
    //Metodos para pegar  o input do inputfield e referencia as consecutivas variaveis de tempo e de spawn
    public void ReadinputTime(string s)
    {
        inputTempo = double.Parse(s);
    }
    public void ReadinputSpawn(string s)
    {
        inputSpawn = double.Parse(s);
    }

    //Metodo para ativar e desativar paines de menu, gameover, win e etc...
    public void PanelActiveDesactive(GameObject panel)
    {
        if (panel.activeSelf == true)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
    }
    public void SettingsActive(GameObject panel)
    {
        if (panel.activeSelf == true)
        {
            panel.SetActive(false);
            _gamerController.timeGaming = inputTempo;
            _gamerController.coldowSpawnEnemys = inputSpawn;
        }
        else
        {
            panel.SetActive(true);
        }
    }

    //Metodo de restart
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }



}
