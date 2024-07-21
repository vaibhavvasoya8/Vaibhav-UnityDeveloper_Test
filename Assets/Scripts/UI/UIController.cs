using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class UIController : MonoBehaviour
{
    public static UIController inst;

    [SerializeField] Canvas gamePlay;
    [SerializeField] Canvas gameOver;
    [SerializeField] Canvas win;

    [SerializeField] Text txtCollected;
    [SerializeField] Text txtCollectedGameover;

    [SerializeField] int TotalCubes;
    int cubeCount = 0;
    private void Awake()
    {
        inst = this;
    }
    private void Start()
    {
        txtCollected.text = cubeCount + "/" + TotalCubes;
        gamePlay.enabled = true;
        gameOver.enabled = false;
        win.enabled = false;
    }
    public void CubeCollected(int val)
    {
        cubeCount++;
        txtCollected.text = cubeCount + "/" + TotalCubes;
        // check all cube are collected.
        if(cubeCount == TotalCubes)
        {
            //show the win screen
            gamePlay.enabled = false;
            win.enabled = true;
        }
    }

    public void ShowGameOver()
    {
        txtCollectedGameover.text = cubeCount + "/" + TotalCubes;
        gamePlay.enabled = false;
        gameOver.enabled = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
