using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input : MonoBehaviour
{
    public float turnDelay;

    public GridGenerator gridGenerator;
    public InitialSetUp initialSetUp;
    public InputController inputController;
    public PlayerAI playerAI;


    private GameController gameController;

    private int AI;
    private int player;

    void Start()
    {
        player = initialSetUp.player;
        AI = Mathf.Abs(player - 1);


        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    //Player input
    public void Response(Button button)
    {
        var stats = button.GetComponent<GridStats>();

        stats.input = player;
        var text = button.GetComponentInChildren<Text>();

        text.color = new Color32(217, 228, 221, 255);
        text.text = player == 1 ? "X" : "O";
        button.enabled = false;

        if (inputController.Evaluate() == 10 || !inputController.IsMovesLeft())
        {
            gameController.GameOver(inputController.IsMovesLeft() == true ? player : -1);
        }
        else
        {
            AITurn();
        }

    }

    void AITurn()
    {
        Button button = playerAI.FindBestMove();

        var stats = button.GetComponent<GridStats>();
        stats.input = AI;
        var text = button.GetComponentInChildren<Text>();

        text.color = new Color32(217, 228, 221, 255);
        text.text = AI == 1 ? "X" : "O";
        button.enabled = false;

        if (inputController.Evaluate() == -10 || !inputController.IsMovesLeft())
        {
            gameController.GameOver(inputController.IsMovesLeft() == true ? AI : -1);
        }
    }
}
