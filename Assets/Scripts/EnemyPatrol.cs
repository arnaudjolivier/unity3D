
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;
    public SpriteRenderer graphics;
    public int DamageOnCollision = 20;

    private Transform target;
    private int destPoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position; // direction
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); //technique de deplacement 1 fois

        //si l ennemi arrive a destination
        if (Vector3.Distance(transform.position, target.position) < 0.3f)// distance entre les 2 objets
        {
            destPoint = ( destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint]; // premet d'allez au waypoints
            graphics.flipX = !graphics.flipX; //permet de flip le snake
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(DamageOnCollision);
        }
    }
}
