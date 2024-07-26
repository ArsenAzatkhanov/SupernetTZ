using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : EnemyBase
{
    [SerializeField] Transform shotTip;
    [SerializeField] WeaponSO enemyWeapon;
    [SerializeField] float enemyRotationSpeed;
    float currentInterval;

    private void Update()
    {
        ShootPlayer();
    }

    void ShootPlayer()
    {
        if (!playerInRange)
        {
            currentInterval = 0;
            return;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(transform.position - EnemyManager.Instance.PlayerControllerTransform.position), enemyRotationSpeed * Time.deltaTime * 100);

        currentInterval += Time.deltaTime;

        if (currentInterval > enemyWeapon.shotInterval) 
        {
            Bullet.Shot(EnemyManager.Instance.PlayerControllerTransform, shotTip, enemyWeapon, BulletType.EnemyBullet);
            currentInterval = 0;
        }
    }
}