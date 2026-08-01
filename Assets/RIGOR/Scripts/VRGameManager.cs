using TMPro;
using UnityEngine;

public class VRGameManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject StartupEnemy;
    [SerializeField] GameObject FirstSection;
    [SerializeField] TextMeshProUGUI StartupText;
    [SerializeField] int playerHealth = 100;
    bool isCovered;
    const int maxHealth = 100;
    [SerializeField] int playerLife = 3;
    double playerScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (StartupEnemy == null)
        {
            StartupText.text = "Here we go! Use the thumbstick to teleport to the next area!";
            FirstSection.GetComponent<LevelArea>().ToggleTeleportation(true);
        }
    }

    public void ToggleCover(bool covered) { isCovered = covered; if (!isCovered) { Debug.LogWarning("Action!"); } else { Debug.Log("Standing by..."); } }
    public bool CoverCheck() { return isCovered; }
    public void IncrementHealth(int hp) { playerHealth += hp; }
    public void IncrementLife(int lf) { playerLife += lf; }
    public void IncrementScore(double pts) { playerScore += pts; }
    public int GetPlayerHealth() { return playerHealth; }
    public void ResetPlayerHealth() { playerHealth = maxHealth; }
    public int GetPlayerLife() { return playerLife; }
    public double GetPlayerScore() { return playerScore; }
}
