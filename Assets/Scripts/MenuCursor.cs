using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCursor : MonoBehaviour
{
    void Start()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            ShowCursor();
        }
    }

    void Update()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            ShowCursor();
        }
    }

    void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}