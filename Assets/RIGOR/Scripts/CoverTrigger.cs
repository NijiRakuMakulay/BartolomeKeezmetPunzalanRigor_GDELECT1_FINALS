using UnityEngine;
using UnityEngine.Analytics;

public class CoverTrigger : MonoBehaviour
{
    [SerializeField] WeaponHandler Gunner;
    [SerializeField] VRGameManager GameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera")) { Debug.Log("Reloaded!"); Gunner.ReloadWeapon(); }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera")) { GameManager.ToggleCover(true); }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera")) { GameManager.ToggleCover(false); }
    }
}
