using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private TextMeshProUGUI _youWinText;

    [SerializeField]
    private Button _playAgainButton;

    [SerializeField]
    private Button _quitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _quitButton.onClick.AddListener(QuitGame);
        _quitButton.gameObject.SetActive(false);

        _playAgainButton.onClick.AddListener(ReloadGame);
        _playAgainButton.gameObject.SetActive(false);

        _youWinText.gameObject.SetActive(false);

        _scoreText.text = "Score: 0";

        _playerController.OnCollectPickUp += UpdateScoreText;

        _playerController.OnAllPickUpsCollected += DisplayEndScreen;

        _playerController.OnTouchEnemy += ChangeYouWinText;
        _playerController.OnTouchEnemy += DisplayEndScreen;

        _playerController.OnFalling += ChangeYouWinText;
        _playerController.OnFalling += DisplayEndScreen;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateScoreText(object sender, OnCollectPickUpArgs onCollectPickUpArgs)
    {
        _scoreText.text = $"Score: {onCollectPickUpArgs.collectedPickUps}";
    }

    private void DisplayEndScreen()
    {
        _quitButton.gameObject.SetActive(true);
        _playAgainButton.gameObject.SetActive(true);
        _youWinText.gameObject.SetActive(true);
    }

    private void ChangeYouWinText()
    {
        _youWinText.text = "You lose!";
    }

    private void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void OnDestroy()
    {
        _playAgainButton.onClick.RemoveAllListeners();

        _playerController.OnCollectPickUp -= UpdateScoreText;

        _playerController.OnAllPickUpsCollected -= DisplayEndScreen;

        _playerController.OnTouchEnemy -= ChangeYouWinText;
        _playerController.OnTouchEnemy -= DisplayEndScreen;

        _playerController.OnFalling -= ChangeYouWinText;
        _playerController.OnFalling -= DisplayEndScreen;
    }
}
