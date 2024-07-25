using System;
using System.Collections;
using System.Collections.Generic;
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
    }

    void MovePlayer()
    {
        if (!playerIsMoving) return;

        if (!duringPushback)
            characterController.Move(playerDirection * movementSpeed * Time.deltaTime);
        else
        {
            float pushbackSpeed = Mathf.Lerp(maxPushbackSpeed, 0, currentPushbackTime / pushbackTime);  
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
            if (enemyDetector.NearestEnemy != null)
            {
                GameObject enemy = enemyDetector.NearestEnemy;

                Vector3 enemyPos = new Vector3(enemy.transform.position.x, playerModel.position.y , enemy.transform.position.z);
                playerModel.rotation = Quaternion.RotateTowards(playerModel.rotation, Quaternion.LookRotation(enemyPos - transform.position), rotationSpeed * Time.deltaTime * 100);
            }
        }
    }

    public void EnablePushback()
    {
        duringPushback = true;
        currentPushbackTime = pushbackTime;
    }

    Vector3 ConvertDirection(Vector2 direction) => new Vector3(direction.x, 0, direction.y);


}