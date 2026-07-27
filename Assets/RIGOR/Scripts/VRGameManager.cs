using TMPro;
using UnityEngine;

public class VRGameManager : MonoBehaviour
{
    [SerializeField] GameObject StartupEnemy;
    [SerializeField] TextMeshProUGUI StartupText;
    [SerializeField] int playerHealth = 100;
    [SerializeField] int playerLife = 3;
    double playerScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StartupEnemy==null)
        {
            StartupText.enabled = false;
        }
    }

    public void IncrementHealth(int hp) { playerHealth += hp; }
    public void IncrementLife(int lf) { playerLife += lf; }
    public void IncrementScore(double pts) { playerScore += pts; }
    public int GetPlayerHealth() { return playerHealth; }
    public int GetPlayerLife() { return playerLife; }
    public double GetPlayerScore() { return playerScore; }
}
