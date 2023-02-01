using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public PlayerLogic playerRef;
    public List<Agent> enemiesList;
    public CameraSwitcher cameraLogic;
    public GameObject pauseScreen;
    public GameObject gameOverPanel;
    public TextMeshProUGUI wins;
    public TextMeshProUGUI losses;
    public ProfileProgress profileProgress;

    public TextMeshProUGUI gameOverMessage;

    [SerializeField]
    private GameObject inventoryPanel;

    private static string losingMessage = "Defeat!";
    private static string winningMessage = "Victory!";

    void Start()
    {
        gameOverPanel.SetActive(false);
        inventoryPanel.SetActive(false);

        ProfileProgress.profileGameData = Application.persistentDataPath + "/profileGameData.dat";
        profileProgress = ProfileProgress.LoadLocalProfile();

        SetLosses();
        SetWins();
    }
    public void StartGame()
    {
        playerRef.isActive = true;

        foreach (Agent agent in enemiesList)
        {
            agent.ActivateAgent();
        }

        cameraLogic.Initialize();

        pauseScreen.SetActive(false);
        inventoryPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void GameOver()
    {
        inventoryPanel.SetActive(false);
        playerRef.Deactivate();

        foreach (Agent agent in enemiesList)
        {
            agent.DeactivateAgent();
        }

        cameraLogic.Pause();

        Cursor.lockState = CursorLockMode.None;
        gameOverPanel.SetActive(true);
    }

    public void WinGame()
    {
        gameOverMessage.text = winningMessage;
        GameOver();
        profileProgress.Wins++;
        SetWins();
        profileProgress.SaveLocalProfile();
    }

    public void LoseGame()
    {
        gameOverMessage.text = losingMessage;
        GameOver();
        profileProgress.Losses++;
        SetLosses();
        profileProgress.SaveLocalProfile();
    }

    public void SetLosses()
    {
        losses.text = "L: " + profileProgress.Losses;
    }

    public void SetWins()
    {
        wins.text = "W: " + profileProgress.Wins;
    }


    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }

    void Update()
    {

        if (!playerRef.isActive) return;
        if (!enemiesList.Any(a => a != null))
        {
            WinGame();
        }
    }
}
