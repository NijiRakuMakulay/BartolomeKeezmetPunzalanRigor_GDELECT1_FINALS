using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject EnemyToSpawn;
    [SerializeField] GameObject Target;
    [SerializeField] VRGameManager GameManager;
    
    public void SpawnEnemy()
    {
        GameObject newEnemy;
        newEnemy = Instantiate(EnemyToSpawn, transform.position, transform.rotation);
        newEnemy.GetComponent<Enemy>().SetEnemyTarget(Target.transform);
        newEnemy.GetComponent<Enemy>().SetGameManager(GameManager);
    }
}
