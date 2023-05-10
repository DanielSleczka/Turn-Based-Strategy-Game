using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private Animator moveAnimator;
    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPosition;
    private Unit unit;
    private bool isActive;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if(!isActive) 
        { 
            return; 
        }

        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        Debug.Log(moveDirection);
        float stoppingDistance = 0.02f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            moveAnimator.SetBool("IsWalking", true);
        }
        else
        {
            moveAnimator.SetBool("IsWalking", false);
            isActive = false;
        }

        float rotateSpeed = 15f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

    }

    public void Move(GridPosition gridPosition)
    {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        isActive = true;
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition) 
    {
        List<GridPosition> validGridPositionsList = GetValidActionGridPositionList();
        return validGridPositionsList.Contains(gridPosition);
    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionsList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();
        
        for(int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                if(unitGridPosition == testGridPosition)
                {
                    // Same Grid Position where the unit is already at\
                    continue;
                }
                if(LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Grid Position already occupied with another Unit
                    continue;
                }

                validGridPositionsList.Add(testGridPosition);
            }
        }

        return validGridPositionsList;
    }
}
