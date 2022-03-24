using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class MenuUI : MonoBehaviour
{

    void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Play"))
        {
            Debug.Log("Cambiando de escena!");
            SceneManager.LoadScene("Game");
        }
    }
}
