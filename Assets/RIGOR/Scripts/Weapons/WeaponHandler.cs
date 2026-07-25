using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WeaponHandler : MonoBehaviour
{
    PlayerInput VRInput;
    [SerializeField] Transform PAttach;
    [SerializeField] GameObject Bullet;
    Transform ActiveNozzle;
    [SerializeField] GameObject[] WeaponModels;
    [SerializeField] Transform[] FirePosition;
    [SerializeField] Collider[] PrimaryCollider;
    [SerializeField] Collider[] SecondaryCollider;
    [SerializeField] TextMeshProUGUI PlayerUI;
    string WeaponName;
    int bullets;
    int stockAmmo;
    int health = 100;
    int life = 3;
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
        WeaponStatus();
        if (transform.position.y < -10.0f)
        {
            transform.position = new Vector3(transform.position.x, 3.0f, transform.position.z);
        }
        PlayerUI.text =
            $"Current Weapon: {WeaponName}\n" +
            $"Bullets: {bullets}/{stockAmmo}" +
            $"HP: {health} | Life: {life}";
    }

    public void WeaponStatus()
    {
        WeaponMode = WeaponMode % 3;
        switch (WeaponMode)
        {
            case 0: WeaponModels[0].SetActive(true); WeaponModels[1].SetActive(false); WeaponModels[2].SetActive(false); WeaponName = "Blaster Gun"; break;
            case 1: WeaponModels[0].SetActive(false); WeaponModels[1].SetActive(true); WeaponModels[2].SetActive(false); WeaponName = "Laser Gun"; break;
            case 2: WeaponModels[0].SetActive(false); WeaponModels[1].SetActive(false); WeaponModels[2].SetActive(true); WeaponName = "Cannon Shotgun"; break;
            default: WeaponModels[0].SetActive(true); WeaponModels[1].SetActive(false); WeaponModels[2].SetActive(false); WeaponName = "Blaster Gun"; break;
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
        if (isFired) { Debug.Log("FIRE!"); }
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
