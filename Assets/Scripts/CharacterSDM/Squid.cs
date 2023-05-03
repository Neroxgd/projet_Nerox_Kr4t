using UnityEngine.InputSystem;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Squid : Character
{
    [SerializeField] private float dashForce;
    [SerializeField] private float timeToRechargeDash;
    private bool canDash = true;

    public void Dash(InputAction.CallbackContext context)
    {
        if (!context.started || direction == Vector2.zero || !canDash) return;
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.AddForce(direction * dashForce, ForceMode2D.Impulse);
        float baseSpeedCharacter = maxSpeedCharacter;
        maxSpeedCharacter *= 10;
        DOTween.To(() => maxSpeedCharacter, x => maxSpeedCharacter = x, baseSpeedCharacter, 0.5f)
        .SetEase(Ease.OutCirc);
        StartCoroutine(TimeToRechargeDash());
    }

    IEnumerator TimeToRechargeDash()
    {
        canDash = false;
        yield return new WaitForSeconds(timeToRechargeDash);
        canDash = true;
    }
}
