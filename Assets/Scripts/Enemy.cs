using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 pos)
    {
        Transform enemyP = GameAssets.Instance.enemyP;
        Transform enemyTransform = Instantiate(enemyP, pos, Quaternion.identity);

        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    private Rigidbody2D myBody;
    private Transform targetTransform;
    private HealthSystem healthSystem;
    private float moveSpeed = 6f;
    private int damage = 10;
    private float targetRadius = 10f;
    private float lookForTargetDelay = .2f;

    private void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        if(BuildingManager.Instance.Get_HQBuilding()!=null)
            targetTransform = BuildingManager.Instance.Get_HQBuilding().transform;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnDied += HealthSystem_OnDied;
        InvokeRepeating("LookForTargets", Random.Range(.1f, .3f), lookForTargetDelay); // Handle Movement Function
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
        Instantiate(Resources.Load<Transform>("enemyDieParticleP"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Update()
    {
        HandleMovement();        
    }

    private void HandleMovement()
    {
        if (targetTransform != null)
        {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized;
            myBody.velocity = moveDir * moveSpeed;
        }
        else
        {
            myBody.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();
        if (building != null)
        {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(damage);
            this.healthSystem.Damage(999);
        }
    }

    private void LookForTargets()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, targetRadius);
        foreach (Collider2D collider2D in collider2Ds)
        {
            Building building = collider2D.GetComponent<Building>();
            if (building != null)
            {
                if (targetTransform == null)
                {
                    targetTransform = building.transform;
                }
                else
                {
                    if (Vector3.Distance(transform.position, building.transform.position) <
                        Vector3.Distance(transform.position, targetTransform.position))
                    {
                        targetTransform = building.transform;
                    }
                }
            }
        }

        if (targetTransform == null)
        {
            if(BuildingManager.Instance.Get_HQBuilding()!=null)
                targetTransform = BuildingManager.Instance.Get_HQBuilding().transform;
        }
    }
}