using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public float mouseSensitivity = 100f;
    
    private CharacterController controller;
    private Vector3 velocity;
    private Transform cameraTransform;
    private float xRotation = 0f;
    public Text mensajeFinal; // Asigna un Text de UI en el Inspector
    public GameObject pantallaFinal; // Asigna un panel de UI en el Inspector

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        if (pantallaFinal != null)
        {
            pantallaFinal.SetActive(false); // Ocultar la pantalla final al inicio
        }
    }

    void Update()
    {
        MovePlayer();
        RotateCamera();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Restringir el movimiento solo a las teclas WASD
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && 
            !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            moveX = 0;
            moveZ = 0;
        }
        
        Vector3 moveDirection = Vector3.ProjectOnPlane(transform.right * moveX + transform.forward * moveZ, Vector3.up);
        controller.Move(moveDirection * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meta"))
        {
            if (mensajeFinal != null)
            {
                mensajeFinal.text = "¡Ganaste!";
            }
            if (pantallaFinal != null)
            {
                pantallaFinal.SetActive(true); // Mostrar la pantalla final
            }
        }
    }
}
