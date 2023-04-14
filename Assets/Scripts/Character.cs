using UnityEngine;
using UnityEngine.InputSystem;

//ce script gère les mouvements de base des personnages, donc déplacement et jump
public class Character : MonoBehaviour
{
    private Vector2 direction;
    public float puissanceJump = 1;
    public float AccelerationSpeedCharacter = 1;
    public float maxSpeedCharacter = 20;
    public float airControlSpeed = 0.2f;
    public float gravityPower = -9.81f;
    [SerializeField] private Rigidbody2D rigidBody2D;
    private bool IsGrounded { get { return Physics2D.Raycast(transform.position + Vector3.down, Vector2.down, 0.1f); } }
    private bool[] IsOnWall { get { return direction.x == 0 ? new bool[] { false, false } : new bool[] { Physics2D.Raycast(transform.position + new Vector3(direction.x, 0, 0), new Vector2(direction.x, 0), 0.05f), Physics2D.Raycast(transform.position + new Vector3(-direction.x, 0, 0), new Vector2(-direction.x, 0), 0.05f) }; } }
    private bool IsOnWalls { get { return IsOnWall[0] || IsOnWall[1]; } }
    
    public void GetInputsDeplacement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Deplacements();
    }

    private void Deplacements()
    {
        rigidBody2D.velocity += new Vector2((IsGrounded ? direction.x : direction.x * airControlSpeed) * AccelerationSpeedCharacter, IsOnWalls ? gravityPower / 2 : gravityPower);
        rigidBody2D.velocity = new Vector2(Mathf.Clamp(rigidBody2D.velocity.x, -maxSpeedCharacter, maxSpeedCharacter), rigidBody2D.velocity.y);
        if (IsGrounded && direction.x == 0)
            rigidBody2D.velocity = new Vector2(Mathf.Lerp(rigidBody2D.velocity.x, 0, 0.25f), rigidBody2D.velocity.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && (IsGrounded || IsOnWalls))
            rigidBody2D.AddForce((IsOnWalls ? IsOnWall[0] ? (Vector2.up + new Vector2(-direction.x, 0)).normalized : (Vector2.up + new Vector2(direction.x, 0)).normalized : Vector2.up) * puissanceJump, ForceMode2D.Impulse);
    }
}
