using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animator playerAnim;
    float inputX, inputZ;
    bool pressed = true;
    public float turnSmoothTime = 2, turnSmoothVelocity;
    Vector3 movement;
    public float moveSpeed;
    public List<Transform> points;
    public GameManager gameManager;
    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        Move();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && !gameManager.enemyList.Contains(other.transform))
        {
            gameManager.enemyList.Add(other.transform);
        }
    }
    void Move()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) ||
    Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            pressed = true;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            pressed = false;
        if (!pressed)
        {
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(inputX, 0, inputZ).normalized;
            float targetAngle = 0;
            if (direction.magnitude >= .1f)
            {
                targetAngle = (float)Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
            movement = transform.forward;
            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
            playerAnim.SetBool("Walk", true);
        }
        else if (pressed)
        {
            playerAnim.SetBool("Walk", false);
        }
    }
}
