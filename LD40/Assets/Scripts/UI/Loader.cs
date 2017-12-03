using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
            SceneManager.LoadScene("Level", LoadSceneMode.Additive);
        }
    }
}