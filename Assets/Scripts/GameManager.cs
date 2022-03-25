using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public enum Screen
{
    MENU,
    LEVEL1,
    LEVEL2,
    END
}

public enum GameStatus
{
    IN_GAME,
    WIN,
    LOSE
}

public class GameManager : MonoBehaviour
{
    private Screen currentScreen;
    public GameStatus status;

    public Board board;

    public Tilemap level1;
    public Tilemap level2;

    public bool inGame;


    private static GameManager _Instance;
    public static GameManager Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<GameManager>();
                // name it for easy recognition
                _Instance.name = _Instance.GetType().ToString();
                // mark root as DontDestroyOnLoad();
                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        currentScreen = Screen.MENU;
        inGame = false;
        status = GameStatus.IN_GAME;
        //this.board.gameObject.SetActive(false);
    }

    public void ChangeScene(string scene)
    {
        if (scene.Equals("Level1"))
        {
            currentScreen = Screen.LEVEL1;
            AlignStatus();
            if (!currentScreen.Equals("Game")) SceneManager.LoadScene("Game");
            inGame = true;
            //this.board.gameObject.SetActive(true);

        } else if (scene.Equals("Level2"))
        {
            currentScreen = Screen.LEVEL2;
            AlignStatus();
            if(!currentScreen.Equals("Game")) SceneManager.LoadScene("Game");
            inGame = true;
            //this.board.gameObject.SetActive(true);
        } else if(scene.Equals("Win") || scene.Equals("Lose"))
        {
            status = scene.Equals("Win") ? GameStatus.WIN : GameStatus.LOSE;
            currentScreen = Screen.END;
            if (!currentScreen.Equals("End Scene")) SceneManager.LoadScene("End Scene");
            inGame = false;
            Debug.Log("You Lose");
        } else if (scene.Equals("Menu"))
        {
            currentScreen = Screen.MENU;
            if (!currentScreen.Equals("Menu")) SceneManager.LoadScene("Menu");
            inGame = false;
            //this.board.gameObject.SetActive(false);
        }
    }

    private void AlignStatus()
    {
        if(status.Equals(GameStatus.LOSE) || status.Equals(GameStatus.WIN))
        {
            status = GameStatus.IN_GAME;
        }
    }

}
