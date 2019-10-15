using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTranslation : MonoBehaviour
{
    [SerializeField]
    string sceneName = null;
    void Start()
    {
        SceneManager.LoadScene(sceneName);
    }
}
