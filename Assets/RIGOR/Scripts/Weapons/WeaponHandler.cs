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
    [SerializeField] GameObject Bullet;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stockAmmo = 100;
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
        WeaponStatus();
        if (transform.position.y < -10.0f)
        {
            transform.position = new Vector3(transform.position.x, 3.0f, transform.position.z);
        }
        PlayerUI.text =
            $"Current Weapon: {WeaponName}\n" +
            $"Bullets: {bullets}/{stockAmmo}" +
            $"HP: {health} | Life: {life}";
        PlayerScoreUI.text = string.Format("Score: {0:000,000,000}", score);
    }

    public void WeaponStatus()
    {
        WeaponMode = WeaponMode % 3;
        switch (WeaponMode)
        {
            case 0: WeaponModels[0].SetActive(true); WeaponModels[1].SetActive(false); WeaponModels[2].SetActive(false); WeaponName = "Blaster Gun"; bulletSpeed = 5.0f; break;
            case 1: WeaponModels[0].SetActive(false); WeaponModels[1].SetActive(true); WeaponModels[2].SetActive(false); WeaponName = "Laser Gun"; bulletSpeed = 10.0f; break;
            case 2: WeaponModels[0].SetActive(false); WeaponModels[1].SetActive(false); WeaponModels[2].SetActive(true); WeaponName = "Cannon Shotgun"; bulletSpeed = 7.0f; break;
            default: WeaponModels[0].SetActive(true); WeaponModels[1].SetActive(false); WeaponModels[2].SetActive(false); WeaponName = "Blaster Gun"; bulletSpeed = 5.0f; break;
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
        isGrabbed = true;
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
        GameObject newBullet = Instantiate(Bullet, ActiveNozzle.position, ActiveNozzle.rotation);
        newBullet.GetComponent<Bullet>().SetResponsibleShooter(GameManager);
        switch (WeaponMode)
        {
            case 0: newBullet.GetComponent<Bullet>().SetDamage(1); newBullet.GetComponent<Bullet>().Fire(0, ActiveNozzle.forward, bulletSpeed); break;
            case 1: newBullet.GetComponent<Bullet>().SetDamage(3); newBullet.GetComponent<Bullet>().Fire(1, ActiveNozzle.forward, bulletSpeed); break;
            case 2: newBullet.GetComponent<Bullet>().SetDamage(5); newBullet.GetComponent<Bullet>().Fire(2, ActiveNozzle.forward, bulletSpeed); break;
            default: newBullet.GetComponent<Bullet>().SetDamage(1); newBullet.GetComponent<Bullet>().Fire(0, ActiveNozzle.forward, bulletSpeed); break;
        }
    }

    public void FinishedFiring() { isFired = false; Debug.Log("Fire done!"); }

    void OnFireWeapon()
    {
        isFired = true;
    }

    void OnChangeWeapon()
    {
        if (isGrabbed)
        {
            WeaponMode++;
        }
    }
}
