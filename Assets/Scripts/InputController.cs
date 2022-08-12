using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public GridGenerator gridGenerator;

    private Button[,] gridArray;
    private int rows = 3;

    void Start()
    {
        gridArray = gridGenerator.gridArray;
    }

    //Checks if any moves left to play
    public bool IsMovesLeft()
    {
        foreach (var u in gridArray)
        {
            var stats = u.GetComponent<GridStats>();
            if (stats.input == -1)
                return true;
        }

        return false;
    }


    //Check if any of the players wins
    public int Evaluate()
    {
        //Check columns for X or O victory
        for (int col = 0; col < rows; col++)
        {
            var statsA = gridArray[col, 0].GetComponent<GridStats>();
            var statsB = gridArray[col, 1].GetComponent<GridStats>();
            var statsC = gridArray[col, 2].GetComponent<GridStats>();

            if (statsA.input == statsB.input && statsB.input == statsC.input)
            {
                if (statsA.input == 1)
                    return 10;
                else if (statsA.input == 0)
                    return -10;
            }
        }

        //Check rows for X or O victory
        for (int row = 0; row < rows; row++)
        {
            var statsA = gridArray[0, row].GetComponent<GridStats>();
            var statsB = gridArray[1, row].GetComponent<GridStats>();
            var statsC = gridArray[2, row].GetComponent<GridStats>();

            if (statsA.input == statsB.input && statsB.input == statsC.input)
            {
                if (statsA.input == 1)
                    return 10;
                else if (statsA.input == 0)
                    return -10;
            }
        }

        //Check diagonals for X or O victory
        var statsD = gridArray[0, 0].GetComponent<GridStats>();
        var statsE = gridArray[1, 1].GetComponent<GridStats>();
        var statsF = gridArray[2, 2].GetComponent<GridStats>();
        var statsG = gridArray[0, 2].GetComponent<GridStats>();
        var statsH = gridArray[2, 0].GetComponent<GridStats>();

        
        if (statsD.input == statsE.input && statsE.input == statsF.input || statsG.input == statsE.input && statsE.input == statsH.input)
        {
            if (statsE.input == 1)
                return 10;
            if (statsE.input == 0)
                return -10;
        }

        //If none of the cases met
        return 0;
    }

}
