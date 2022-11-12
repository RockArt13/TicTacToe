using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// making serialized = to be visible in the Insperctor Window

[System.Serializable] public class Player
{
    public Image panel;
    public Text text;
    public Button button;
}

[System.Serializable] public class PlayerColour
{
    public Color panelColour;
    public Color textColour;
}

public class GameController : MonoBehaviour
{

    public Text[] buttonList;

    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;

    public GameObject startInfo;

    private string playerSide;
    private int moveCount;

    public Player playerX;
    public Player playerO;
    public PlayerColour activePlayerColour;
    public PlayerColour inactivePlayerColour;


    void Awake()
    {
        SetGameControllerReferenceOnButtons();
        gameOverPanel.SetActive(false);
        moveCount = 0;
        restartButton.SetActive(false);
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        { buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this); } // this -> GameController
    }

    
    public string GetPlayerSide()
    {
        return playerSide;
    }

    //winning conditions
    public void EndTurn()
    {
        moveCount++;

        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (moveCount >= 9)
        {
            GameOver("draw");
        }

        else ChangeSides();

    }

    //swaping
    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";

        if (playerSide == "X")
        {
            SetPlayerColours(playerX, playerO);
        }
        else
        {
            SetPlayerColours(playerO, playerX);
        }
    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);
        if (winningPlayer == "draw")
        {
            SetGameOverText("It's a Draw!");
            SetPlayerColoursInactive();
        }
        else
        {
            SetGameOverText(winningPlayer + " Wins!");
        }

        restartButton.SetActive(true);
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;

    }

    public void RestartGame()
    {
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }

        SetPlayersButtons(true);
        SetPlayerColoursInactive();
        startInfo.SetActive(true);

    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    // Change the Side and change the colour
    void SetPlayerColours(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColour.panelColour;
        newPlayer.text.color = activePlayerColour.textColour;
        oldPlayer.panel.color = inactivePlayerColour.panelColour;
        oldPlayer.text.color = inactivePlayerColour.textColour;
    }

    // X or O?
    public void SetStartingSide(string StartingSide)
    {
        playerSide = StartingSide;
        if (playerSide == "X")
        {
            SetPlayerColours(playerX, playerO);
        }
        else
        {
            SetPlayerColours(playerO, playerX);
        }
        StartGame();
    }

    /*
     Starter
     */
    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayersButtons(false);
        startInfo.SetActive(false);
    }
    /*
    Starter
    */

    

    void SetPlayersButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }
                                                // |the players' buttons change during the gameplay|
    void SetPlayerColoursInactive()
    {
        playerX.panel.color = inactivePlayerColour.panelColour;
        playerX.text.color = inactivePlayerColour.textColour;
        playerO.panel.color = inactivePlayerColour.panelColour;
        playerO.text.color = inactivePlayerColour.textColour;
    }
}