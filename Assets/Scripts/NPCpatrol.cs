using System.Collections;
using UnityEngine;

public class NPCpatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float movementSpeed;
    private int currentPatrolIndex = 0;
    private Animator animator;
    [SerializeField] private GameObject textContainer;
    [SerializeField] private GameObject interactText;
    private bool isMoving = true;

    private void Start()
    {
        transform.position = patrolPoints[currentPatrolIndex].position;
        animator = GetComponent<Animator>();
        interactText.SetActive(false);
        StartCoroutine(MoveToNextPatrolPoint());
    }

    private IEnumerator MoveToNextPatrolPoint()
    {
        while (true)
        {
            if (isMoving)
            {
                Vector3 targetPosition = patrolPoints[currentPatrolIndex].position;

                Vector3 direction = (targetPosition - transform.position).normalized;

                if (direction != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * movementSpeed);
                }

                animator.SetBool("isWalk", true);

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

                if (transform.position == targetPosition)
                {
                    animator.SetBool("isWalk", false);
                    yield return new WaitForSeconds(1.3f);
                    currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                }
            }

            yield return null;
        }
    }

    public void MoveAndText()
    {
        isMoving = false;
        animator.SetBool("isWalk", false);
        textContainer.SetActive(true);
        interactText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(false);
            textContainer.SetActive(false);
            isMoving = true;
            animator.SetBool("isWalk", true);

        }
    }
}