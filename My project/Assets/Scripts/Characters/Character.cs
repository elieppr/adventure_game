using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    CharactorAnimator animator;
    public float moveSpeed;
    public bool IsMoving { get; private set; }
    private void Awake()
    {
        animator = GetComponent<CharactorAnimator>();
    }
    public IEnumerator Move(Vector2 moveVect)
    {

        animator.MoveX = Mathf.Clamp(moveVect.x, -1f, 1f);
        animator.MoveY = Mathf.Clamp(moveVect.y, -1f, 1f);

        var targetPos = transform.position;
        targetPos.x += moveVect.x;
        targetPos.y += moveVect.y;
        
        
        if(!IsWalkable(targetPos))
        {
            yield break;
        } 

        IsMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        IsMoving = false;
    }

    public void HandleUpdate()
    {
        animator.IsMoving = IsMoving;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.i.SolidLayer | GameLayers.i.Interactable) != null)
        {

            return false;
        }
        return true;
    }
    public CharactorAnimator Animator
    {
        get => animator;
    }
}
