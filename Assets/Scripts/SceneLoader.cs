using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleRss
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadMainScene()
        {
            SceneManager.LoadScene("Main");
        }
    }
}