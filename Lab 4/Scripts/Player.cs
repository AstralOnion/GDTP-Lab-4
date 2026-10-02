using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction fireAction;

    [Header("Prefabs")]
    public GameObject laserPrefab;
    public Rigidbody2D rb;

    [Header("Values")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float horizontalScreenLimit = 10f;
    [SerializeField] private float verticalScreenLimit = 6f;
    [SerializeField] private bool canShoot = true;
    Vector2 movementDirection = Vector2.zero;
    private PlayerInputs playerInput;

    private void Awake()
    {
        playerInput = new PlayerInputs();
    }

    private void OnEnable()
    {
        moveAction = playerInput.Player.Move;
        fireAction = playerInput.Player.Attack;
        moveAction.Enable();
        fireAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        fireAction.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movementDirection);
    }

    void Movement()
    {
        //changed if-else statement to switch statement.
        Vector2 currentPosition = transform.position;

        movementDirection = moveAction.ReadValue<Vector2>();

        switch (currentPosition.x)
        {
            case float x when x > horizontalScreenLimit:
                currentPosition.x = -horizontalScreenLimit;
                break;
            case float x when x < -horizontalScreenLimit:
                currentPosition.x = horizontalScreenLimit;
                break;
        }
        switch (currentPosition.y)
        {
            case float y when y > verticalScreenLimit:
                currentPosition.y = -verticalScreenLimit;
                break;
            case float y when y < -verticalScreenLimit:
                currentPosition.y = verticalScreenLimit;
                break;
        }
        transform.position = currentPosition;
    }

    void Shooting()
    {
        if (fireAction.triggered && canShoot)
        {
            Instantiate(laserPrefab, transform.position + Vector3.up, Quaternion.identity);
            canShoot = false;
            StartCoroutine(Cooldown()); //changed StartCoroutine call from a string to a method call
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }
}
