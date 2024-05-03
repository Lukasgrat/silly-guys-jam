using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMod : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject player;

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            playGame();
        }
    }
    public void playGame()
    {
        Time.timeScale = 1;
        player.GetComponent<PlayerController>().isLocked = false;
        canvas.SetActive(false);
    }

    public void quitGame()
    {
        SceneManager.LoadScene(0);
    }
}