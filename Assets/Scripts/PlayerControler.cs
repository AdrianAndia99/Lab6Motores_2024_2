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
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
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

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {

    }
}