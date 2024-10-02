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
    private AudioSource audioSource;
    private float footstepInterval = 0.5f;
    private float footstepTimer;
    [SerializeField] private AudioClipSO clipSO1;
    [SerializeField] private AudioClipSO clipSO2;
    [SerializeField] private AudioClip footstepClip;
    public NPCpatrol npc;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            npc.MoveAndText();
        }
    }
    private void Update()
    {
        if (IsMoving())
        {
            if (Time.time >= footstepTimer)
            {
                PlayFootstepSound();
                footstepTimer = Time.time + footstepInterval;
            }
        }
        else
        {
            StopFootstepSound();
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
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            SceneManager.LoadScene("Game2");
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
    private void PlayFootstepSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = footstepClip;
            audioSource.Play();
        }
    }
    private void StopFootstepSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}