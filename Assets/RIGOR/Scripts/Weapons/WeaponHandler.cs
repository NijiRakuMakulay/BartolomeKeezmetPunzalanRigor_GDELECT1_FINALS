using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WeaponHandler : MonoBehaviour
{
    PlayerInput VRInput;
    [SerializeField] VRGameManager GameManager;
    [SerializeField] Transform PAttach;
    [SerializeField] GameObject PlayerBullet;
    Transform ActiveNozzle;
    [SerializeField] GameObject[] WeaponModels;
    [SerializeField] Transform[] FirePosition;
    [SerializeField] Collider[] PrimaryCollider;
    [SerializeField] Collider[] SecondaryCollider;
    [SerializeField] TextMeshProUGUI PlayerUI;
    [SerializeField] TextMeshProUGUI PlayerScoreUI;
    string WeaponName;
    int bullets;
    float bulletSpeed;
    int stockAmmo;
    int health;
    int life;
    double score;
    XRGrabInteractable XRGI;
    int WeaponMode;
    bool isGrabbed = false;
    bool isFired = false;
    bool isAlive = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stockAmmo = 20;
        bullets = stockAmmo;
        VRInput = GetComponent<PlayerInput>();
        XRGI = GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        health = GameManager.GetPlayerHealth();
        life = GameManager.GetPlayerLife();
        score = GameManager.GetPlayerScore();
        string ammo;
        if (bullets > 0) { ammo = $"Bullets: {bullets}/{stockAmmo} |"; } else { ammo = "[TAKE COVER TO RELOAD!] |"; }
        PlayerUI.text =
            $"Current Weapon: {WeaponName}\n" +
            ammo +
            $"HP: {health} | Life: {life}";
        PlayerScoreUI.text = string.Format("Score: {0:000,000,000}", score);
        if (life>0) { isAlive = true; } else { isAlive = false; }
        if (isAlive)
        {
            PlayerUI.enabled = true;
            WeaponStatus();
            if (transform.position.y < -10.0f)
            {
                transform.position = new Vector3(transform.position.x, 3.0f, transform.position.z);
            }
        }
        else
        {
            PlayerUI.enabled = false;
        }
    }

    public void WeaponStatus()
    {
        WeaponMode = WeaponMode % 3;
        switch (WeaponMode)
        {
            case 0: WeaponModels[0].SetActive(true); WeaponModels[1].SetActive(false); WeaponModels[2].SetActive(false); WeaponName = "Blaster Gun"; bulletSpeed = 50.0f; break;
            case 1: WeaponModels[0].SetActive(false); WeaponModels[1].SetActive(true); WeaponModels[2].SetActive(false); WeaponName = "Laser Gun"; bulletSpeed = 70.0f; break;
            case 2: WeaponModels[0].SetActive(false); WeaponModels[1].SetActive(false); WeaponModels[2].SetActive(true); WeaponName = "Cannon Shotgun"; bulletSpeed = 60.0f; break;
            default: WeaponModels[0].SetActive(true); WeaponModels[1].SetActive(false); WeaponModels[2].SetActive(false); WeaponName = "Blaster Gun"; bulletSpeed = 50.0f; break;
        }
        XRGI.colliders[0] = PrimaryCollider[WeaponMode];
        XRGI.colliders[1] = SecondaryCollider[WeaponMode];
        XRGI.attachTransform = PrimaryCollider[WeaponMode].transform;
        XRGI.secondaryAttachTransform = SecondaryCollider[WeaponMode].transform;
        ActiveNozzle = FirePosition[WeaponMode];
        if (isGrabbed) { transform.position = PAttach.position; }
    }

    public void WeaponGrabbed()
    {
        if (isAlive)
        {
            isGrabbed = true;
        }
    }
    public void WeaponDropped()
    {
            isGrabbed = false;
    }

    public void WeaponFired()
    {
        if (isFired) { ShootBullet(); }
    }

    void ShootBullet()
    {
        if (GameManager.CoverCheck())
        {
            Debug.LogError("You cannot shoot while in cover. Reloading weapon now.");
            ReloadWeapon();
        }
        else
        {
            if (bullets > 0)
            {
                bullets--;
                GameObject newBullet = Instantiate(PlayerBullet, ActiveNozzle.position, ActiveNozzle.rotation);
                newBullet.GetComponent<PlayerBullet>().SetResponsibleShooter(GameManager);
                switch (WeaponMode)
                {
                    case 0: newBullet.GetComponent<PlayerBullet>().SetDamage(1); newBullet.GetComponent<PlayerBullet>().Fire(0, ActiveNozzle.forward, bulletSpeed); break;
                    case 1: newBullet.GetComponent<PlayerBullet>().SetDamage(3); newBullet.GetComponent<PlayerBullet>().Fire(1, ActiveNozzle.forward, bulletSpeed); break;
                    case 2: newBullet.GetComponent<PlayerBullet>().SetDamage(5); newBullet.GetComponent<PlayerBullet>().Fire(2, ActiveNozzle.forward, bulletSpeed); break;
                    default: newBullet.GetComponent<PlayerBullet>().SetDamage(1); newBullet.GetComponent<PlayerBullet>().Fire(0, ActiveNozzle.forward, bulletSpeed); break;
                }
            }
            else
            {
                Debug.LogError("Please Reload!");
                isFired = false;
            }
        }
        
    }

    public void ReloadWeapon()
    {
        Debug.Log("Bullets restocked");
        bullets = stockAmmo;
    }

    public void FinishedFiring()
    {
        if (isAlive)
        {
            isFired = false; Debug.Log("Fire done!");
        }
        else { isFired = false; }
    }

    void OnFireWeapon()
    {
        if (isAlive)
        {
            isFired = true;
        }
    }

    void OnChangeWeapon()
    {
        if (isAlive)
        {
            if (isGrabbed)
            {
                WeaponMode++;
            }
        }
    }

    public bool AliveCheck() { return isAlive; }

    public void GetHit(Collision collision)
    {
        collision.gameObject.GetComponent<EnemyBullet>().DamagePlayer(collision.gameObject.GetComponent<EnemyBullet>().GetDamage());
        health -= collision.gameObject.GetComponent<EnemyBullet>().GetDamage();
        if (health <= 0) { GameManager.IncrementLife(-1); GameManager.ResetPlayerHealth(); }
    }
}
