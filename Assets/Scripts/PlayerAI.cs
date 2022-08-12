using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAI : MonoBehaviour
{
    public GridGenerator gridGenerator;
    public InputController inputController;
    public InitialSetUp initialSetUp;

    private Button[,] gridArray;
    private int rows = 3;
    private int AI;

    private void Start()
    {
        AI = Mathf.Abs(initialSetUp.player - 1);
        gridArray = gridGenerator.gridArray;
    }

    int Minimax(int depth, bool isMax)
    {
        int score = inputController.Evaluate();

        if (score == 10)
            return score - depth;

        if (score == -10)
            return score + depth;

        if (inputController.IsMovesLeft() == false)
            return 0;

        // If this maximizer's move
        if (isMax)
        {
            int best = -1000;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (gridArray[i, j].GetComponent<GridStats>().input == -1)
                    {
                        gridArray[i, j].GetComponent<GridStats>().input = 1;

                        best = Mathf.Max(best, Minimax(depth + 1, !isMax));

                        gridArray[i, j].GetComponent<GridStats>().input = -1;
                    }
                }
            }
            return best;
        }

        // If this minimizer's move
        else
        {
            int best = 1000;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (gridArray[i, j].GetComponent<GridStats>().input == -1)
                    {
                        gridArray[i, j].GetComponent<GridStats>().input = 0;

                        best = Mathf.Min(best, Minimax(depth + 1, !isMax));

                        // Undo the move
                        gridArray[i, j].GetComponent<GridStats>().input = -1;
                    }
                }
            }
            return best;
        }
    }

    public Button  FindBestMove()
    {
        int bestVal = 1000;

        Button bestMove = null;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (gridArray[i, j].GetComponent<GridStats>().input == -1)
                {
                    //Make the move
                    gridArray[i, j].GetComponent<GridStats>().input = AI;

                    //Compute the evaluation function of the move
                    int moveVal = Minimax(0, true);

                    gridArray[i, j].GetComponent<GridStats>().input = -1;

                    if (moveVal < bestVal)
                    {
                        bestMove = gridArray[i, j];
                        bestVal = moveVal;
                    }
                }
            }
        }

        return bestMove;
    }
}
