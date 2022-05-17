using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Spawner spawner;
    Mino activeMino;

    [SerializeField]
    private float dropInterval = 0.25f;
    float nextdropTimer;

    Stage stage;

    float nextKeyDownTimer, nextKeyLeftRightTimer, nextKeyRotateTimer;

    [SerializeField]
    private float nextKeyDownInterval = 0.02f;

    [SerializeField]
    private float nextKeyLeftRightInterval, nextKeyRotateInterval = 0.25f;

    [SerializeField]
    private GameObject gameOverPanel;

    bool gameOver;


    // Start is called before the first frame update
    private void Start()
    {
        spawner = GameObject.FindObjectOfType<Spawner>();
        stage = GameObject.FindObjectOfType<Stage>();

        spawner.transform.position = Rounding.Round(spawner.transform.position);

        // 
        nextKeyDownTimer = Time.time + nextKeyDownInterval;
        nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
        nextKeyRotateTimer = Time.time + nextKeyRotateInterval;

        if (!activeMino)
        {
            activeMino = spawner.SpawnMino();
        }

        if (gameOverPanel.activeInHierarchy)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (gameOver)
        {
            return;
        }

        PlayerInput();
    }

    void PlayerInput()
    {
        if(((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) &&
            Time.time > nextKeyLeftRightTimer) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            activeMino.MoveRight();

            nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;

            if (!stage.CheckPosition(activeMino))
            {
                activeMino.MoveLeft();
            }
        }
        else if(((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) &&
            Time.time > nextKeyLeftRightTimer) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            activeMino.MoveLeft();

            nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;

            if (!stage.CheckPosition(activeMino))
            {
                activeMino.MoveRight();
            }
        }
        else if(Input.GetKey(KeyCode.E) && (Time.time > nextKeyRotateTimer))
        {
            activeMino.RotateRight();
            nextKeyRotateTimer = Time.time + nextKeyRotateInterval;

            if (!stage.CheckPosition(activeMino))
            {
                activeMino.RotateLeft();
            }
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && (Time.time > nextKeyDownTimer) || (Time.time > nextdropTimer))
        {
            activeMino.MoveDown();
            nextdropTimer = Time.time + dropInterval;

            if (!stage.CheckPosition(activeMino))
            {
                if (stage.OverLimit(activeMino))
                {
                    GameOver();
                }
                else
                {
                    BottomStage();
                }
            }
        }
    }

    void BottomStage()
    {
        activeMino.MoveUP();
        stage.SaveMinoInGrid(activeMino);

        activeMino = spawner.SpawnMino();

        nextKeyDownTimer = Time.time;
        nextKeyLeftRightTimer = Time.time;
        nextKeyRotateTimer = Time.time;

        stage.ClearAllRows();
    }

    void GameOver()
    {
        activeMino.MoveUP();

        if (!gameOverPanel.activeInHierarchy)
        {
            gameOverPanel.SetActive(true);
        }

        gameOver = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
