using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;

    private void Update()
    {
        // should change over to pause menu, but isn't
        // debug log shows that this runs but canvas won't change
        if (Input.GetKeyDown("escape") && !PauseMenu.activeInHierarchy)
        {
            Time.timeScale = 0;
            gameObject.GetComponent<PlayerController>().isLocked = true;
            PauseMenu.SetActive(true);
        }

        //if (Input.GetKeyDown("escape") && canvas.activeInHierarchy)
        //{
        //    Time.timeScale = 0;
        //    canvas.SetActive(true);
        //    gameObject.GetComponent<PlayerController>().isLocked = false;
        //}
    }
}
