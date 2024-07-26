using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Joystick playerJoystick;
    public Transform playerWeaponTip;
    [SerializeField] Transform playerModel;
    [Space(5)]

    [Header("Player Settings")]
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float maxPushbackSpeed;
    [SerializeField] float pushbackTime;
    [SerializeField] float gravityScale;

    Vector3 pushbackDirection;
    bool duringPushback;
    float currentPushbackTime;

    EnemyDetector enemyDetector;
    CharacterController characterController;
    public Vector3 playerDirection => ConvertDirection(playerJoystick.Direction).normalized;

    bool playerIsMoving;

    public bool isMoving => playerIsMoving;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        enemyDetector = GetComponent<EnemyDetector>();
    }

    private void Update()
    {
        playerIsMoving = playerDirection.magnitude > 0;

        MovePlayer();
        RotatePlayer();
        PlayerGravity();
    }

    void MovePlayer()
    {
        if (!duringPushback)
            characterController.Move(playerDirection * movementSpeed * Time.deltaTime);
        else
        {
            float pushbackSpeed = Mathf.Lerp(0, maxPushbackSpeed, currentPushbackTime / pushbackTime);
            characterController.Move(pushbackDirection * pushbackSpeed * Time.deltaTime);
            currentPushbackTime -= Time.deltaTime;

            if(currentPushbackTime <=0)
                duringPushback = false;
        }
    }

    void RotatePlayer()
    {
        if(playerIsMoving)
            playerModel.rotation = Quaternion.RotateTowards(playerModel.rotation, Quaternion.LookRotation(playerDirection), rotationSpeed * Time.deltaTime * 100);
        else
        {
            if (enemyDetector.NearestShotEnemy != null)
            {
                GameObject enemy = enemyDetector.NearestShotEnemy;

                Vector3 enemyPos = new Vector3(enemy.transform.position.x, playerModel.position.y , enemy.transform.position.z);
                playerModel.rotation = Quaternion.RotateTowards(playerModel.rotation, Quaternion.LookRotation(enemyPos - transform.position), rotationSpeed * Time.deltaTime * 100);
            }
        }
    }

    void PlayerGravity()
    {
        characterController.Move(Vector3.down * gravityScale * Time.deltaTime);
    }

    public void EnablePushback(Vector3 direction)
    {
        if (duringPushback) return;

        duringPushback = true;
        currentPushbackTime = pushbackTime;
        pushbackDirection = direction;
    }

    Vector3 ConvertDirection(Vector2 direction) => new Vector3(direction.x, 0, direction.y);
}