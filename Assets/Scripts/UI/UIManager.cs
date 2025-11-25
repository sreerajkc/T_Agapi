using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Properties
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
    [SerializeField] private Button nextLevelButton;
 
    public static Sprite CardFlippedSprite;
    public static Sprite CardUnflippedSprite;

    private void Awake()
    {
        CardFlippedSprite = cardFlippedSprite;
        CardUnflippedSprite = cardUnflippedSprite;

        nextLevelButton.onClick.AddListener(OnClickWinPanelNextButton);
    }

    private void OnEnable()
    {
        GameManager.OnLevelInitialized += UpdateLevelText;
        GameManager.OnPairMatched += UpdateGameUI;
        GameManager.OnPairUnmatched += UpdateGameUI;
        GameManager.OnWin += DisplayWin;
    }

    private void OnDisable()
    {
        GameManager.OnLevelInitialized -= UpdateLevelText;
        GameManager.OnPairMatched -= UpdateGameUI;
        GameManager.OnPairUnmatched -= UpdateGameUI;
        GameManager.OnWin -= DisplayWin;

    }

    private void UpdateLevelText()
    {
        levelText.text = "LVL : " + gameManager.CurrentLevel.ToString();
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
