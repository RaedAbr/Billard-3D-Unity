using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public GameObject[] balls;
    public bool allBallsStopped;
    public Text TurnText;
    public bool isFirstBallInHole;
    public GameObject GameOver;
    public Text Winner;

    private bool isTurnFinish { get; set; }
    private Player currentPlayer;
    private Player otherPlayer;
    private Player player1;
    private Player player2;
    private bool switchPlayer;

    private void SetCurrentPlayer(Player player)
    {
        currentPlayer = player;
        TurnText.text = player.Name.ToString();
    }

    void InitGame()
    {
        player1 = new Player(PlayerName.Player1);
        player2 = new Player(PlayerName.Player2);
        isFirstBallInHole = true;
        isTurnFinish = true;
        SetCurrentPlayer(player2);
        switchPlayer = true;
        GameOver.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        InitGame();
    }

    void NextTurn()
    {
        if (switchPlayer)
        {
            if (currentPlayer.Name == PlayerName.Player1)
            {
                SetCurrentPlayer(player2);
                otherPlayer = player1;
            }
            else
            {
                SetCurrentPlayer(player1);
                otherPlayer = player2;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        allBallsStopped = true;
		foreach (GameObject g in balls)
        {
            if (g != null)
            {
                if (!g.GetComponent<Rigidbody>().IsSleeping())
                {
                    allBallsStopped = false;
                    isTurnFinish = false;
                    break;
                }
            }
        }
        WhiteBallController whiteBallController = balls[0].GetComponent<WhiteBallController>();
        if (allBallsStopped && !isTurnFinish)
        {
            whiteBallController.ShowFirstPersonView();
            whiteBallController.TurnOnLineRenderers();
            isTurnFinish = true;
            NextTurn();
        }
	}

    public void BallInHole(GameObject ball)
    {
        BallProperties ballProperties = ball.GetComponent<BallProperties>();
        int ballNumber = ballProperties.ballNumber;
        Debug.Log(ballNumber);

        if (ballNumber == 0) // white ball
        {
            GameObject whiteBall = balls[0];
            whiteBall.transform.position = whiteBall.GetComponent<WhiteBallController>().LastPosition;
            whiteBall.GetComponent<Rigidbody>().Sleep();
        }
        else
        {
            balls[ballNumber] = null;
            if (isFirstBallInHole)
            {
                if (ballNumber < 8)
                {
                    if (currentPlayer.Name == PlayerName.Player1)
                    {
                        player1.BallsType = BallsType.solids;
                    }
                    else
                    {
                        player2.BallsType = BallsType.stripes;
                    }
                }
                else if (ballNumber > 8)
                {
                    if (currentPlayer.Name == PlayerName.Player1)
                    {
                        player1.BallsType = BallsType.stripes;
                    }
                    else
                    {
                        player2.BallsType = BallsType.solids;
                    }
                }
                else // black ball
                {
                    if (currentPlayer.Name == PlayerName.Player1)
                    {
                        Winner.text = "Winner : Player 2";
                    }
                    else
                    {
                        Winner.text = "Winner : Player 1";
                    }
                    GameOver.SetActive(true);
                }
            }
            else
            {
                if (currentPlayer.BallsType == BallsType.solids)
                {
                    if (ballNumber < 8)
                    {
                        switchPlayer = false;
                    }
                    else if (ballNumber > 8)
                    {
                        switchPlayer = true;
                    }
                }
                else
                {
                    if (ballNumber > 8)
                    {
                        switchPlayer = false;
                    }
                    else if (ballNumber < 8)
                    {
                        switchPlayer = true;
                    }
                }
            }
        }
    }

    public void ChangeScene()
    {
        Application.LoadLevel("MainMenu");
    }
}
