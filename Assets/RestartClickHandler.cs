using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartClickHandler : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
