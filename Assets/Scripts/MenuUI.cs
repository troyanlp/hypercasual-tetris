using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MenuUI : MonoBehaviour
{

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Level1"))
        {
            GameManager.Instance.ChangeScene("Level1");
        }
        else if (CrossPlatformInputManager.GetButtonDown("Level2"))
        {
            GameManager.Instance.ChangeScene("Level2");
        } else if (CrossPlatformInputManager.GetButtonDown("Menu"))
        {
            GameManager.Instance.ChangeScene("Menu");
        }
    }
}
