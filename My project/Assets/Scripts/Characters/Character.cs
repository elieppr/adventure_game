using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    CharactorAnimator animator;
    public float moveSpeed;
    public bool IsMoving { get; private set; }
    public float OffsetY { get; private set; } = 0.3f; 

    private void Awake()
    {
        animator = GetComponent<CharactorAnimator>();
        SetPositionAndSetToTile(transform.position);
    }
    public IEnumerator Move(Vector2 moveVect, Action OnMoveOver=null)
    {

        animator.MoveX = Mathf.Clamp(moveVect.x, -1f, 1f);
        animator.MoveY = Mathf.Clamp(moveVect.y, -1f, 1f);

        var targetPos = transform.position;
        targetPos.x += moveVect.x;
        targetPos.y += moveVect.y;
        
        
        if(!IsPathClear(targetPos))
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
        OnMoveOver?.Invoke();
    }

    public void HandleUpdate()
    {
        animator.IsMoving = IsMoving;
    }

    public void SetPositionAndSetToTile(Vector2 pos) 
    {
        pos.x = Mathf.Floor(pos.x) + 0.5f;
        pos.y = Mathf.Floor(pos.y) + 0.5f + OffsetY;

        transform.position = pos;
    }

    public bool IsPathClear(Vector3 targetPos) 
    {
        var diff = targetPos - transform.position;
        var dir = diff.normalized;
        if (Physics2D.BoxCast(transform.position + dir, new Vector2(0.2f, 0.2f), 0f, dir, diff.magnitude - 1, GameLayers.i.SolidLayer | GameLayers.i.Interactable | GameLayers.i.PlayerLayer) == true)
        {
            return false;
        }
        return true;

    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.i.SolidLayer | GameLayers.i.Interactable) != null)
        {

            return false;
        }
        return true;
    }

    public void LookTowards(Vector3 targetPos) 
    {
        var xdiff = Mathf.Floor(targetPos.x) - Mathf.Floor(transform.position.x);
        var ydiff = Mathf.Floor(targetPos.y) - Mathf.Floor(transform.position.y);

        if (xdiff == 0 || ydiff == 0)
        {
            animator.MoveX = Mathf.Clamp(xdiff, -1f, 1f);
            animator.MoveY = Mathf.Clamp(ydiff, -1f, 1f);
        }
    }

    public CharactorAnimator Animator
    {
        get => animator;
    }
}
