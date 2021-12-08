using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 0.1f;
    
    private float playerHP = 200.0f;
    private float maxHP = 300.0f;
    private float playerDamage = 75.0f;
    private float attackSpeed = 0.75f;
    private float gravity;
    private float lastHitTime = 0f;
    private CharacterController characterController;
    private Vector3 moveDirection;
    private LevelManager levelManager;
    private Text hitPointsUI;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        hitPointsUI = GameObject.FindGameObjectWithTag("HP").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (playerHP >= 0)
        {
            hitPointsUI.text = playerHP.ToString();
        }
    }

    private void Move()
    {
        moveDirection = Vector3.zero;
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection *= speed;

        if (Vector3.Angle(Vector3.forward, moveDirection) > 1f || Vector3.Angle(Vector3.forward, moveDirection) == 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveDirection, speed, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
        }

        moveDirection.y = gravity * Time.deltaTime;

        if (!characterController.isGrounded)
        {
            gravity -= 20.0f * Time.deltaTime;
        }
        else
        {
            gravity = -1f * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Time.time - lastHitTime >= attackSpeed)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && other.gameObject.GetComponent<Spider>())
            {
                Spider spider = other.GetComponent<Spider>();
                spider.GetDamage(other.gameObject, playerDamage);
                lastHitTime = Time.time;
            }
        }
    }

    public void GetDamage(float damage)
    {
        playerHP -= damage;
        if (playerHP <= 0)
        {
            levelManager.NextLevel();
        }
        if (playerHP > maxHP)
        {
            playerHP = maxHP;
        }
    }

    public void LevelUp()
    {
        playerDamage += 20.0f;
        playerHP += 30.0f;
        maxHP += 30.0f;
    }
}
