using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    PlayerBullet,
    EnemyBullet
}

public class Bullet : MonoBehaviour
{
    float bulletSpeed;
    int bulletDamage;
    BulletType bulletType;

    public float BulletDamage => bulletDamage;

    public void SetValues(WeaponSO weapon, BulletType bulletType)
    {
        bulletSpeed = weapon.bulletSpeed;
        bulletDamage = weapon.bulletDamage;
        this.bulletType = bulletType;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward , Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        if (other.tag == "Enemy" || other.tag == "Walls" || other.tag == "Player")
        {
            if(tag == "Enemy" && bulletType == BulletType.PlayerBullet)
                other.GetComponent<EnemyBase>().TakeDamage(bulletDamage);

            if (tag == "Player" && bulletType == BulletType.EnemyBullet)
                PlayerManager.Instance.ChangeHealth(-bulletDamage);

            Destroy(gameObject);
        }
    }

    public static void Shot(Transform target, Transform weaponTip, WeaponSO weapon, BulletType bulletType)
    {
        GameObject bullet = Instantiate(weapon.bulletObject);

        bullet.transform.position = weaponTip.position;
        bullet.transform.LookAt(target.position);

        Bullet[] bulletObjects = bullet.GetComponentsInChildren<Bullet>();
        foreach (var b in bulletObjects)
            b.SetValues(weapon, bulletType);
    }
}
