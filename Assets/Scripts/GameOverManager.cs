using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static GameOverManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de GameOverManager dans la scéne");
            return;
        }
        instance = this;
    }
    
    public void OnPlayerDeath()
    {
        if (CurrentSceneManager.instance.isPlayerPresentByDefault) // si le joueur est present dans la scene
        {
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad(); // recupere les objets et desactive dontdestroyonload
        }

        gameOverUI.SetActive(true);
    }
    public void RetryButton()
    {
        Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinsPickUp);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerHealth.instance.Respawn();
        gameOverUI.SetActive(false);
    }
    public void MainMenuButton()
    {
        // Retour au menu
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
