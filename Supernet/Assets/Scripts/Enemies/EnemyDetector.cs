using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform target;
    [Space(5)]

    [Header("Settings")]
    [SerializeField] float shootDetectionRadius;
    [SerializeField] float contactRadius;
    [SerializeField] float targetSpeed;

    GameObject nearestShotEnemy;
    public GameObject NearestShotEnemy => nearestShotEnemy;

    private void Update()
    {
        SetTargetPosition();

        if (nearestShotEnemy != null && Vector3.Distance(player.position, nearestShotEnemy.transform.position) < contactRadius)
        {
            PlayerManager.Instance.TriggerPushback(nearestShotEnemy);
            PlayerManager.Instance.ChangeHealth(-nearestShotEnemy.GetComponent<EnemyBase>().ContactDamage);
        }
    }

    public bool DetectNearestEnemy(out GameObject enemy, float radius)
    {
        enemy = null;

        Collider[] enemiesInRadius = Physics.OverlapSphere(player.position, radius, enemyLayer);

        if (enemiesInRadius.Length <= 0)
            return false;
        else
        {
            enemy = enemiesInRadius[0].gameObject;

            float currentLowestDistance = Vector3.Distance(player.position, enemy.transform.position);
            float newLowestDistance;
            foreach (Collider c in enemiesInRadius)
            {
                newLowestDistance = Vector3.Distance(player.position, c.gameObject.transform.position);
                if (newLowestDistance < currentLowestDistance)
                {
                    currentLowestDistance = newLowestDistance;
                    enemy = c.gameObject;
                }
            }

            return true;
        }
    }

    void SetTargetPosition()
    {
        if (DetectNearestEnemy(out GameObject enemy, shootDetectionRadius))
        {
            nearestShotEnemy = enemy;

            Vector3 newEnemyPos = new Vector3(enemy.transform.position.x, target.position.y, enemy.transform.position.z);
            if (target.gameObject.activeInHierarchy)
                target.position = Vector3.MoveTowards(target.position, newEnemyPos, targetSpeed * Time.deltaTime);
            else
            {
                target.gameObject.SetActive(true);
                target.position = newEnemyPos;
            }
        }
        else
        {
            target.gameObject.SetActive(false);
            nearestShotEnemy = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(player.position, shootDetectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position, contactRadius);
    }
}
