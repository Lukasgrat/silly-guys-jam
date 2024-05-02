using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMod : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject player;

    public void playGame()
    {
        player.GetComponent<PlayerController>().isLocked = false;
        canvas.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
