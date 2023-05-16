using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

//ce script gère les mouvements de base des personnages, donc déplacement et jump
public abstract class Character : MonoBehaviour
{
    protected Vector2 direction;
    private float puissanceJump;
    private float AccelerationSpeedCharacter;
    protected float maxSpeedCharacter;
    private float airControlSpeed;
    protected bool canUseAbility = true;
    private float gravityPower;
    protected Rigidbody2D rigidBody2D;
    public float timeToRechargeAbility;
    private bool IsGrounded { get { return Physics2D.Raycast(transform.position + Vector3.down, Vector2.down, 0.1f); } }
    private bool[] IsOnWall { get { return new bool[] { Physics2D.Raycast(transform.position + Vector3.right, Vector2.right, 0.05f), Physics2D.Raycast(transform.position + Vector3.left, Vector2.left, 0.05f) }; } }
    private bool IsOnWalls { get { return IsOnWall[0] || IsOnWall[1]; } }
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        puissanceJump = 20;
        AccelerationSpeedCharacter = 5;
        maxSpeedCharacter = 10;
        airControlSpeed = 0.2f;
        gravityPower = -1;
    }

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
        if (context.started && IsGrounded)
            rigidBody2D.AddForce(Vector2.up * puissanceJump, ForceMode2D.Impulse);
        else if (context.started && IsOnWalls)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, rigidBody2D.velocity.y / 2);
            rigidBody2D.AddForce((IsOnWall[0] ? new Vector2(-1, 1).normalized : new Vector2(1, 1).normalized) * puissanceJump, ForceMode2D.Impulse);
        }
    }

    protected IEnumerator TimeToRechargeAbility()
    {
        canUseAbility = false;
        yield return new WaitForSeconds(timeToRechargeAbility);
        canUseAbility = true;
    }
}
