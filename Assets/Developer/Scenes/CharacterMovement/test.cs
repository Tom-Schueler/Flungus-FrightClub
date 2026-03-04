using Team3.Movement;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class cdjkscn : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    [SerializeField] private Rigidbody body;
    [SerializeField] private Transform headTransform;

    private Vector3 moveInput;
    private Vector2 lookInput;
    private bool jumpInput;

    public void OnMove(CallbackContext callbackContext)
    {
        Vector2 tmpVector = callbackContext.ReadValue<Vector2>();
        moveInput = new Vector3(tmpVector.x, 0, tmpVector.y);
    }

    public void OnLook(CallbackContext callbackContext)
    {
        lookInput = callbackContext.ReadValue<Vector2>();
    }

    void Update()
    {
        FirstPersonMovement.Look(lookInput, body, 0.5f, headTransform);
        var yawRotation = Quaternion.Euler(0f, body.transform.rotation.eulerAngles.y, 0f);
        Vector3 moveDir = yawRotation * moveInput;
        character.SimpleMove(moveDir * 10);
    }
}
