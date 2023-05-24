using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.UI;

public class Denis : Character
{
    private bool isVisible;
    [SerializeField] private float invisibleTime;
    [SerializeField] private Image barreTimeInvisible;

    protected override void Start()
    {
        base.Start();
        isVisible = true;

    }

    public void Invisible(InputAction.CallbackContext context)
    {
        if (!context.started || !photonView.IsMine) return;
        if (isVisible && canUseAbility)
        {
            spriteRenderer.DOFade(0, 1);
            InvisibleBarre();
            isVisible = false;
            spriteRenderer.DOFade(1, 1)
            .SetDelay(invisibleTime)
            .SetId("TweenInvisibleTime")
            .OnComplete(() => isVisible = true);
            StartCoroutine(TimeToRechargeAbility());
        }
        else if (!isVisible)
        {
            InvisibleBarre();
            spriteRenderer.DOFade(1, 1);
            isVisible = true;
            DOTween.Kill("TweenInvisibleTime");
        }
    }

    private void InvisibleBarre()
    {
        if (isVisible)
        {
            barreTimeInvisible.fillAmount = 1;
            barreTimeInvisible.DOFade(1, 0.5f);
            barreTimeInvisible.DOFillAmount(0, invisibleTime)
            .OnComplete(() => barreTimeInvisible.DOFade(0, 0.5f));
        }
        else
            barreTimeInvisible.DOFade(0, 0.5f);
    }
}
