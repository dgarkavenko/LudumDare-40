using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerPrefs.GetInt("BledNavalny") == 0)
            {
                SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene("Main", LoadSceneMode.Single);
                SceneManager.LoadScene("Level", LoadSceneMode.Additive);
            }
        }
    }
}