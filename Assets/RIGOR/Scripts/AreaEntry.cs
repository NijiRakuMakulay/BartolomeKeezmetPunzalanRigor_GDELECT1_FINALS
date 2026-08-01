using UnityEngine;

public class AreaEntry : MonoBehaviour
{
    [SerializeField] LevelArea lvArea;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { Debug.Log("Target spotted!"); lvArea.SpawnEnemies(); }
    }
}
