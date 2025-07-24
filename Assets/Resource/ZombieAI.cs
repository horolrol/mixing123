using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public double maxHealth = 100;
    private double currentHealth;

    public float detectionRange = 100f;
    public float chaseRange = 50f;
    private NavMeshAgent agent;
    private Transform target;

    public ZombieSpawner spawner;

    public event System.Action OnZombieDeath; // 웨이브 관리를 위한 이벤트
    [HideInInspector] public double damage = 10;
    [HideInInspector] public float moveSpeed = 3.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHealth = (double)maxHealth;
        agent.speed = moveSpeed;
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= detectionRange)
        {
            agent.SetDestination(target.position);
        }

        else if (distance <= chaseRange)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.ResetPath();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"좀비 데미지 받음! 현재 체력: {currentHealth}");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"Zombie died");
        OnZombieDeath?.Invoke(); // 좀비가 죽을 때 웨이브 컨트롤러에게 알림
        OnZombieDeath = null;
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage((int)damage); // 좀비가 플레이어를 때림
            }
        }
    }
}
