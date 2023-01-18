using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float sensitivity = 2f;
    public int maxJump = 1;
    [SerializeField]
    private GameObject playerCamera;
    private int currentJump;
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private float xRot;
    private bool isGrounded = true;
    private bool isRunning = false;
    private bool canVoice = true;
    AudioSource audioSource;
    Rigidbody rb;
    Animator animator;

    void Start()
    {
        currentJump = maxJump;
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        playerMovementInput = new Vector3(Input.GetAxis("Horizontal") / 5, 0, Input.GetAxis("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (speed == 3f)
            {
                isRunning = true;
                speed = 10f;
            }
            else
            {
                isRunning = false;
                speed = 3f;
            }
        }
        MovePlayer();
        MovePlayerCamera();
    }


    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(playerMovementInput) * speed;
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
        animator.SetFloat("Speed", playerMovementInput.z * speed);
        if (playerMovementInput.z > 0.5f && isGrounded && canVoice/* && !audioSource.isPlaying*/)
        {
            canVoice = false;
            if (isRunning)
            {
                StartCoroutine(StepSound(0.3f));
            }
            else
            {
                StartCoroutine(StepSound(0.5f));
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && currentJump > 0)
        {
            currentJump--;
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("Jump", true);
        }
    }

    private void MovePlayerCamera()
    {
        //Commented lines using for rotating camera around x axis

        xRot -= playerMouseInput.y * sensitivity;
        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            currentJump = maxJump;
            animator.SetBool("Jump", false);
            isGrounded = true;
        }
    }

    IEnumerator StepSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.volume = Random.Range(0.8f, 1f);
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
        canVoice = true;
    }
}
