using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{

    public Text textUI { get; private set; }

    void Start()
    {
        Debug.Log("Start del result text!");
        textUI = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.status.Equals(GameStatus.IN_GAME))
        {
            textUI.text = GameManager.Instance.status.Equals(GameStatus.WIN) ? "You Win" : "You Lose";
        }
    }
}
