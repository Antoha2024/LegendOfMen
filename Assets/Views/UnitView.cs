using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    private static readonly int Up = Animator.StringToHash("anim_2");
    //private static readonly int Walk = Animator.StringToHash("walk");

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;

    public void Move(Vector3 translation)
    {
        transform.Translate(translation);
    }

    public void SetDirection(Vector3 velocity)
    {
        _spriteRenderer.flipX = velocity.x < 0;
        
    }

    public void UpdateAnimationState(Vector3 velocity)
    {
        _animator.SetBool(Up, velocity.x != 0 && true);
    }
}
