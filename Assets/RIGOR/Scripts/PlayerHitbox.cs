using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] WeaponHandler Gunner;
    [SerializeField] VRGameManager GameManager;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            if (Gunner.AliveCheck())
            {
                Gunner.GetHit(collision);
            }
        }
    }
}
