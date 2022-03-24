using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class MenuUI : MonoBehaviour
{

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Play"))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
