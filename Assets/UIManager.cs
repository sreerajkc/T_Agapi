using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Dependencies")]
    [SerializeField] private GameManager gameManager; 

    [Header("Card UI Elements")]
    [SerializeField] private Sprite cardFlippedSprite;
    [SerializeField] private Sprite cardUnflippedSprite;

    [Header("Game UI Elements")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboText;
    [Space]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI winPanelScoreText;
    [SerializeField] private Button winPanelNextLevelButton;
 
    public Sprite CardFlippedSprite => cardFlippedSprite;
    public Sprite CardUnflippedSprite => cardUnflippedSprite;

    private void Awake()
    {
        Instance = this;
        winPanelNextLevelButton.onClick.AddListener(OnClickWinPanelNextButton);
    }

    private void OnEnable()
    {
        GameManager.OnLevelInitialized += UpdateLeveText;
        GameManager.OnPairMatched += UpdateGameUI;
        GameManager.OnPairUnmatched += UpdateGameUI;
        GameManager.OnWin += DisplayWin;
    }

    private void OnDisable()
    {
        GameManager.OnLevelInitialized -= UpdateLeveText;
        GameManager.OnPairMatched -= UpdateGameUI;
        GameManager.OnPairUnmatched -= UpdateGameUI;
        GameManager.OnWin -= DisplayWin;

    }

    private void UpdateLeveText()
    {
        levelText.text = gameManager.CurrentLevel.ToString();
    }

    private void UpdateGameUI(Card card1, Card card2)
    {
        comboText.text = "<sup>x</sup>" + gameManager.Combo.ToString();
        scoreText.text = gameManager.Score.ToString();
    }



    private void DisplayWin()
    {
        winPanel.SetActive(true);
        winPanelScoreText.text = gameManager.Score.ToString();  
    }

    private void OnClickWinPanelNextButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
