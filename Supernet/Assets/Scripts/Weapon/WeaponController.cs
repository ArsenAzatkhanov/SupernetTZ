using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] List<WeaponSO> weapons;
    [SerializeField] GameObject currentWeaponTextObject;
    [SerializeField] float textFadeSpeed;

    PlayerController playerController;
    EnemyDetector enemyDetector;
    WeaponSO currentWeapon;
    float currentMaxInterval, currentInterval;
    int currentWeaponIndex;

    CanvasGroup weaponTextCanvas;
    TextMeshProUGUI currentWeaponText;

    private void Start()
    {
        weaponTextCanvas = currentWeaponTextObject.GetComponent<CanvasGroup>();
        currentWeaponText = currentWeaponTextObject.GetComponent<TextMeshProUGUI>();

        enemyDetector = GetComponent<EnemyDetector>();
        playerController = GetComponent<PlayerController>();
        currentWeapon = weapons[0];
        currentWeaponIndex = 0;
    }

    private void Update()
    {
        if (weaponTextCanvas.alpha > 0)
            weaponTextCanvas.alpha = Mathf.MoveTowards(weaponTextCanvas.alpha, 0, textFadeSpeed * Time.deltaTime);

        if (playerController.isMoving)
        {
            currentInterval = 0;
            return;
        }

        if(enemyDetector.NearestShotEnemy != null)
        {
            currentInterval += Time.deltaTime;
            if (currentInterval >= currentWeapon.shotInterval)
            {
                Bullet.Shot(enemyDetector.NearestShotEnemy.transform, playerController.playerWeaponTip, currentWeapon, BulletType.PlayerBullet);
                currentInterval = 0;
            }
        }
    }

    public void SwapWeapon()
    {
        currentWeaponIndex++;
        if (currentWeaponIndex + 1 > weapons.Count)
            currentWeaponIndex = 0;

        currentWeapon = weapons[currentWeaponIndex];
        currentInterval = 0;

        currentWeaponText.text = currentWeapon.weaponName;
        weaponTextCanvas.alpha = 1;
    }

}
