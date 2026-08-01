using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class LevelArea : MonoBehaviour
{
    [SerializeField] TeleportationAnchor ActionPoint;
    [SerializeField] LevelArea NextArea;
    [SerializeField] Transform[] EnemySpawnPoints;
    bool isAreaActive = false;
    bool isAreaCompleted = false;
    GameObject[] ActiveEnemies;

    public void ToggleTeleportation(bool on) { ActionPoint.enabled = on; }
    public void SpawnEnemies()
    {
        isAreaActive = true;
        foreach (Transform t in EnemySpawnPoints)
        {
            t.gameObject.GetComponent<EnemySpawner>().SpawnEnemy();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ToggleTeleportation(false);
    }

    void Update()
    {
        if (isAreaActive)
        {
            if (isAreaCompleted)
            {
                if(NextArea != null)
                {
                    NextArea.ToggleTeleportation(true);
                }
                else
                {
                    Debug.Log("Is this the last area?");
                }
            }
            else
            {
                ActiveEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (ActiveEnemies.Length <= 0) { isAreaCompleted = true; }
            }
        }
    }
}
