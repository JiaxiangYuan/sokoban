using System;
using UnityEngine;

public class Teleporter : Block
{
    public static event Action OnAnyTeleportHappened;
    
    [SerializeField]
    private Teleporter targetTeleporter;

    private bool isTeleporting = false;
    private bool hadBlockLastFrame = false;

    /// <summary>
    /// Removes this teleporter from occupying the cell after grid initialization.
    /// </summary>
    protected override void Start()
    {
        base.Start();

        if (transform.parent.TryGetComponent<Cell>(out Cell cell))
        {
            if (cell.ContainObj == gameObject)
            {
                cell.RemoveContainObj();
            }
        }
    }

    /// <summary>
    /// Checks whether a new block has just entered this teleporter cell and teleports it.
    /// </summary>
    protected override void GridChanged()
    {
        if (isTeleporting || targetTeleporter == null) return;

        Cell currentCell = gridManager.gridList[gridPos.x][gridPos.y].GetComponent<Cell>();
        bool hasBlockNow = currentCell.ContainObj != null;

        if (hasBlockNow && !hadBlockLastFrame)
        {
            Block enteringBlock = currentCell.ContainObj.GetComponent<Block>();

            if (enteringBlock != null && enteringBlock != this)
            {
                targetTeleporter.ReceiveBlock(enteringBlock);
            }
        }

        hadBlockLastFrame = hasBlockNow;
    }

    /// <summary>
    /// Receives a block from another teleporter and places it onto this teleporter cell.
    /// </summary>
    public void ReceiveBlock(Block block)
    {
        if (block == null) return;

        Cell targetCell = gridManager.gridList[gridPos.x][gridPos.y].GetComponent<Cell>();

        if (targetCell.CheckContainObj()) return;

        isTeleporting = true;

        if (block.transform.parent.TryGetComponent<Cell>(out Cell oldCell))
        {
            oldCell.RemoveContainObj();
        }

        block.SetNewGridPos(targetCell.gameObject, gridPos.x, gridPos.y);
        block.transform.position = targetCell.transform.position;
        block.targetPos = block.transform.position;
        block.State = MoveStates.idle;

        gridManager.UpdateGrid();

        hadBlockLastFrame = true;
        isTeleporting = false;
        
        OnAnyTeleportHappened?.Invoke();
    }
}