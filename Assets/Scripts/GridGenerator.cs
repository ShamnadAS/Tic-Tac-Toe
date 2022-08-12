using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour
{
    private int rows = 3;
    public Button gridPrefab;
    public Transform gridsParent;
    public Input input;
    public Button[,] gridArray;

    private int scale = 1;

    void Awake()
    {
        var rectTransform = gridPrefab.GetComponent<RectTransform>();
        scale = (int)rectTransform.rect.width;

        gridArray = new Button[rows, rows];

        if (gridPrefab)
        {
            GenerateGrid();
        }
    }

    void GenerateGrid()
    {
        Vector3 startPos = new Vector3(-scale, 0f, scale);
        float startPosition = (rows - 1) * scale / 2;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Button button = Instantiate(gridPrefab, Vector3.zero, Quaternion.identity, gridsParent);

                var rect = button.GetComponent<RectTransform>();
                rect.localPosition = new Vector3(-startPosition + scale * j, startPosition - scale * i, 0);

                var stats = button.GetComponent<GridStats>();
                stats.x = j;
                stats.y = i;

                button.onClick.AddListener(delegate { input.Response(button); });

                gridArray[j, i] = button;
            }
        }

    }
}
