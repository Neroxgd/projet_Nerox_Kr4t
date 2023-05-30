using DG.Tweening;
using UnityEngine;

public class PlateformMove : MonoBehaviour
{
    [SerializeField] private Transform[] positionsToMove;
    [SerializeField] private float speed;
    [SerializeField] private Ease ease;

    void Start()
    {
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < positionsToMove.Length; i++)
            sequence.Append(transform.DOMove(positionsToMove[i].position, speed).SetSpeedBased(true).SetEase(ease));
        sequence.SetLoops(-1);
    }
}
