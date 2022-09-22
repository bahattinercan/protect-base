using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public static ArrowProjectile Create(Vector3 pos,Enemy enemy)
    {
        Transform arrowP = Resources.Load<Transform>("arrowProjectile");
        Transform arrowT = Instantiate(arrowP, pos, Quaternion.identity);

        ArrowProjectile arrow = arrowT.GetComponent<ArrowProjectile>();
        arrow.SetTarget(enemy);
        return arrow;
    }

    private Enemy targetEnemy;
    [SerializeField]private float moveSpeed=20f;
    private Vector3 moveDir;
    private Vector3 lastMoveDir;
    private float timeToDie = 2f;
    private int damageAmount = 10;

    private void Start()
    {
        Invoke("Die", timeToDie);
    }

    private void Update()
    {
     
        if (targetEnemy != null)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));


    }

    public void Die()
    {
        CancelInvoke();
        Destroy(gameObject);
    }

    public void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}
