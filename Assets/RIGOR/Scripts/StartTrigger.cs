using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Start is Attacked!");
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.Log("Here we go!");
            Destroy(gameObject);
        }
    }
}
