using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float aliveTime;
    int damage;
    [SerializeField] GameObject[] BulletShape;
    VRGameManager VRGM;
    public void Fire(int bulletID, Vector3 fwdTransform, float speed)
    {
        for (int x = 0; x < BulletShape.Length; x++)
        {
            if (x == bulletID) { BulletShape[bulletID].SetActive(true); }
        }
        GetComponent<Rigidbody>().AddForce(fwdTransform * speed, ForceMode.Impulse);
    }

    public void SetDamage(int dmg) { damage = dmg; }
    public void SetResponsibleShooter(VRGameManager human) { VRGM = human; }
    public void DamagePlayer(int dmg) { VRGM.IncrementHealth(-dmg); }
    public int GetDamage() { return damage; }

    private void OnCollisionEnter(Collision collision) { Destroy(gameObject); }

    void Update() { aliveTime += Time.deltaTime; if (aliveTime > 5.0f) { Destroy(gameObject); } }
}
