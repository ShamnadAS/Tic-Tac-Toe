using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float restartTime = 3f;

    [HideInInspector]
    public int inputNumber = 0;
    public GameObject gameOverObject;
    public GridGenerator gridGenerator;
    private Button[,] gridArray;
    private Text gameOverText;

    private void Start()
    {
        gameOverText = gameOverObject.GetComponentInChildren<Text>();
    }

    // Start is called before the first frame update

    // Update is called once per frame
    public void GameOver(int p)
    {
        StartCoroutine(RestartGame(p));
    }



    IEnumerator RestartGame(int p)
    {

        yield return new WaitForSeconds(0.5f);
        string playerName = p == 0 ? "O" : "X";

        if (p == 1 || p == 0)
        {
            gameOverText.color = p == 1 ? new Color32(200, 218, 211, 255) : new Color32(255, 255, 255, 255);
            gameOverText.text = playerName + " HAS WON!";
        }
        else
        {
            gameOverText.color = new Color32(234, 234, 234, 255);
            gameOverText.text = "DRAW";
        }

        gameOverObject.SetActive(true);

        yield return new WaitForSeconds(restartTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
