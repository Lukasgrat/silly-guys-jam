using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    bool active = false;
    [SerializeField]
    GameObject canvas;

    private void Update()
    {
        // should change over to pause menu, but isn't
        // debug log shows that this runs but canvas won't change
        if (Input.GetKeyDown("escape") && !active)
        {
            gameObject.GetComponent<PlayerController>().isLocked = true;
            Debug.Log("esc");
            canvas.SetActive(true);
            active = true;
        }

        if (Input.GetKeyDown("escape") && canvas.activeInHierarchy)
        {
            canvas.SetActive(false);
            active = false;
            gameObject.GetComponent<PlayerController>().isLocked = false;
        }
    }
}
