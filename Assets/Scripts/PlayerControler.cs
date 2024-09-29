using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerControler : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 moveInput;
    public float speed = 5f;
    private float velocidadMovimiento = 3.0f;
    private float velocidadRotacion;
    private float footstepTimer;
    private float footstepInterval = 0.5f;
    [SerializeField] private AudioClipSO clipSO1;
    [SerializeField] private AudioClipSO clipSO2;
    [SerializeField] private AudioClipSO steps;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (context.performed)
        {
            MovingSound();
        }
    }

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref velocidadRotacion, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            rb.MovePosition(transform.position + moveDirection * velocidadMovimiento * Time.deltaTime);

        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Room1"))
        {
            clipSO1.PlayLoop();
        }
        if (other.gameObject.CompareTag("Room2"))
        {
            clipSO2.PlayLoop();
        }
        if (other.gameObject.CompareTag("Room3"))
        {
            clipSO1.PlayLoop();
        }
        if (other.gameObject.CompareTag("Room4"))
        {
            clipSO2.PlayLoop();
        }
        if (other.gameObject.CompareTag("Door"))
        {
            clipSO1.StopPlay();
            clipSO2.StopPlay();
        }
    }

    private bool IsMoving()
    {
        return moveInput.magnitude > 0;
    }
    private void MovingSound()
    {
        if (IsMoving())
        {
            if (Time.time >= footstepTimer)
            {
                steps.PlayOneShoot();
                footstepTimer = Time.time + footstepInterval;
            }
        }
        else
        {
            steps.StopPlay();
        }
    }
}