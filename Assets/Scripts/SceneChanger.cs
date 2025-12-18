
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void Load(int index = 0)
    {
        SceneManager.LoadScene(index);
    }
}
