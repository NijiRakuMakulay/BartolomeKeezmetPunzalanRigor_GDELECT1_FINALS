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
    [SerializeField] VRGameManager GameManager;
    [SerializeField] GameObject EnemyBullet;
    [SerializeField] float BulletSpeed;
    [SerializeField] int WeaponMode;
    [SerializeField] Transform ActiveNozzle;
    Transform Target;
    [SerializeField] float fireTime;
    float timeToFire;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            collision.gameObject.GetComponent<PlayerBullet>().ScoreFeedback(hitValue);
            health -= collision.gameObject.GetComponent<PlayerBullet>().GetDamage();
            Debug.Log($"Enemy takes {collision.gameObject.GetComponent<PlayerBullet>().GetDamage()} damage!");
            if (health <= 0) { Debug.Log("Enemy Destroyed!"); collision.gameObject.GetComponent<PlayerBullet>().ScoreFeedback(killValue); Destroy(gameObject); }
        }
    }
    void Start()
    {
        health = hp;
        timeToFire = fireTime;
    }

    void Attack()
    {
        GameObject newBullet = Instantiate(EnemyBullet, ActiveNozzle.position, ActiveNozzle.rotation);
        newBullet.GetComponent<EnemyBullet>().SetResponsibleShooter(GameManager);
        switch (WeaponMode)
        {
            case 0: newBullet.GetComponent<EnemyBullet>().SetDamage(1); newBullet.GetComponent<EnemyBullet>().Fire(0, ActiveNozzle.forward, BulletSpeed); break;
            case 1: newBullet.GetComponent<EnemyBullet>().SetDamage(3); newBullet.GetComponent<EnemyBullet>().Fire(1, ActiveNozzle.forward, BulletSpeed); break;
            case 2: newBullet.GetComponent<EnemyBullet>().SetDamage(5); newBullet.GetComponent<EnemyBullet>().Fire(2, ActiveNozzle.forward, BulletSpeed); break;
            default: newBullet.GetComponent<EnemyBullet>().SetDamage(1); newBullet.GetComponent<EnemyBullet>().Fire(0, ActiveNozzle.forward, BulletSpeed); break;
        }
    }

    public void SetEnemyTarget(Transform player) { Target = player; }
    public void SetGameManager(VRGameManager vrgm) { GameManager = vrgm; }


    void Update()
    {
        if(Target != null)
        {
            Vector3 direction = Target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
        }
        if (EnemyBullet != null)
        {
            if (timeToFire <= 0)
            {
                Attack();
                timeToFire = fireTime;
            }
            else
            {
                timeToFire -= Time.deltaTime;
            }
        }
        healthText.text = $"{health}/{hp}";
        healthBar.value = health;
        healthBar.minValue = 0;
        healthBar.maxValue = hp;
    }
}
