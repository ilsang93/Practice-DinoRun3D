using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState = GameState.Ready;
    public GameObject titlePanel;
    public GameObject clearPanel;
    public GameObject gameOverPanel;
    public GameObject gamePanel;
    public TextMeshProUGUI finalDinoCount;
    public Slider progressBar;
    [SerializeField] private TextMeshProUGUI stageText;

    private float totalDistance;
    private DinoContoller dino;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 0;
        clearPanel.SetActive(false);
        gamePanel.SetActive(false);
        dino = FindObjectOfType<DinoContoller>();
    }

    void Update()
    {
        if (gameState == GameState.Ready)
        {
            progressBar.value = 0f;
        }
        else
        {
            progressBar.value = dino.transform.position.z / totalDistance;
        }
    }

    public void GameStart()
    {
        Time.timeScale = 1;
        gameState = GameState.Playing;
        FindObjectOfType<DinoContoller>().dinoCountBubble.SetActive(true);
        titlePanel.SetActive(false);
        gamePanel.SetActive(true);

        stageText.text = "Stage " + GetStage();
    }

    public void SetClearPanel(int dinoCount)
    {
        Time.timeScale = 0;
        SoundManager.instance.PlaySfxOneshot(SfxEnum.DinoHit);
        clearPanel.SetActive(true);
        finalDinoCount.text = dinoCount.ToString();
    }

    public void SetGameOverPanel()
    {
        Time.timeScale = 0;
        SoundManager.instance.PlaySfxOneshot(SfxEnum.GameOver);
        gameOverPanel.SetActive(true);
    }

    public void SetTotalDistance(float distance)
    {
        totalDistance = distance;
    }

    public int GetStage()
    {
        return PlayerPrefs.HasKey("Stage") ? PlayerPrefs.GetInt("Stage") + 1 : 1;
    }

    public void SetStage()
    {
        PlayerPrefs.SetInt("Stage", PlayerPrefs.GetInt("Stage") + 1);
    }
}

public enum GameState
{
    Ready,
    Playing,
    End
}
