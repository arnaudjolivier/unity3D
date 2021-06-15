using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) // si le player rentre en contact
        {
            Destroy(transform.parent.parent.gameObject);
        }
    }
}
