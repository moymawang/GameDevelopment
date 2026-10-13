
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Chances")]
    public int startingChances = 3;
    public int chancesLeft;
    public TMP_Text chancesText;

    [Header("Cans")]
    public int totalCans = 6;
    public TMP_Text statusText;

    private HashSet<GameObject> knockedDownCans =
        new HashSet<GameObject>();

    public bool IsGameOver { get; private set; } = false;

    void Start()
    {
        chancesLeft = startingChances;
        UpdateChancesUI();

        if (statusText != null)
            statusText.text = "";
    }

    public void UseChance()
    {
        if (chancesLeft <= 0 || IsGameOver)
            return;

        chancesLeft--;
        UpdateChancesUI();
    }

    public void RegisterCanKnockedDown(GameObject can)
    {
        if (IsGameOver)
            return;

        knockedDownCans.Add(can);


        if (knockedDownCans.Count >= totalCans)
        {
            EndGame(true);
        }
    }

    public void CheckEndGame()
    {
        if (IsGameOver)
            return;

        if (knockedDownCans.Count >= totalCans)
        {
            EndGame(true);
        }
        else if (chancesLeft <= 0)
        {
            EndGame(false);
        }
    }

    void EndGame(bool won)
    {
        IsGameOver = true;

        if (statusText != null)
        {
            statusText.text = won
                ? "YOU WIN!" 
                : "GAME OVER!";
        }

        Debug.Log(won ? "YOU WIN!" : "GAME OVER!");
    }

    void UpdateChancesUI()
    {
        if (chancesText != null)
            chancesText.text = "Chances: " + chancesLeft;
    }
}
