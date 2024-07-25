using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] List<WeaponSO> weapons;

    PlayerController playerController;
    EnemyDetector enemyDetector;
    WeaponSO currentWeapon;
    float currentMaxInterval, currentInterval;
    int currentWeaponIndex;

    private void Start()
    {
        enemyDetector = GetComponent<EnemyDetector>();
        playerController = GetComponent<PlayerController>();
        currentWeapon = weapons[0];
        currentWeaponIndex = 0;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            SwapWeapon();
        }

        if (playerController.isMoving)
        {
            currentInterval = 0;
            return;
        }

        if(enemyDetector.NearestEnemy != null)
        {
            currentInterval += Time.deltaTime;
            if (currentInterval >= currentWeapon.shotInterval)
            {
                Bullet.Shot(enemyDetector.NearestEnemy.transform, playerController.playerWeaponTip, currentWeapon, BulletType.PlayerBullet);
                currentInterval = 0;
            }
        }
    }


    void SwapWeapon()
    {
        currentWeaponIndex++;
        if (currentWeaponIndex + 1 > weapons.Count)
            currentWeaponIndex = 0;

        currentWeapon = weapons[currentWeaponIndex];
        currentInterval = 0;
    }

}
