using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public InputSystem_Actions inputActions;
    public float moveSpeed = 5f;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    // Handles player movement based on input
    private void MovePlayer()
    {
        Vector2 movementInput = inputActions.Player.Move.ReadValue<Vector2>();
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y);

        transform.Translate(movement * Time.deltaTime * moveSpeed);
    }
}
