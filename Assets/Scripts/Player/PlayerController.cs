using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cameraTransform;

    private CharacterController characterController;

    public Interactor Interactor;

    public event UnityAction OnInteractioned;
    public event UnityAction OnInventoryToggled;
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
           
        Move();

        if (Input.GetKeyDown(KeyCode.E) && Interactor != null)
        {
            OnInteractioned?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            OnInventoryToggled?.Invoke();
        }
    }


    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 move = camForward * v + camRight * h;
        if (move.magnitude > 1f) move.Normalize();

        characterController.SimpleMove(move * moveSpeed);

        if (move != Vector3.zero)
            transform.forward = move;
    }

    public void Teleport(Vector3 teleportPos)
    {
        transform.position = teleportPos;
    }
}