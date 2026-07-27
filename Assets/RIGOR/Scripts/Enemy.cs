using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] double hitValue;
    [SerializeField] double killValue;
    int health;
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI healthText;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            collision.gameObject.GetComponent<Bullet>().ScoreFeedback(hitValue);
            health -= collision.gameObject.GetComponent<Bullet>().GetDamage();
            Debug.Log($"Enemy takes {collision.gameObject.GetComponent<Bullet>().GetDamage()} damage!");
            if (health <= 0) { Debug.Log("Enemy Destroyed!"); collision.gameObject.GetComponent<Bullet>().ScoreFeedback(killValue); Destroy(gameObject); }
        }
    }
    void Start() { health = hp; }

    void Update()
    {
        healthText.text = $"{health}/{hp}";
        healthBar.value = health;
        healthBar.minValue = 0;
        healthBar.maxValue = hp;
    }
}
