using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButOff : MonoBehaviour
{
    private Button button;

    private GameManager gameManager;

    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(GoOn);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void GoOn()
    {
        gameManager.SoundsOn();
    }

}
