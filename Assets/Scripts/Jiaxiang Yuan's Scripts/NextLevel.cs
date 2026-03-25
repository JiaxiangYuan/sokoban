using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : Block
{
    [SerializeField]
    private string targetSceneName;

    private bool isSwitching = false;
    
    /// <summary>
    /// Removes this NextLevelTrigger from occupying the cell after grid initialization.
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
    /// Called whenever the grid updates. Checks if any block is on this cell and loads the target scene.
    /// </summary>
    protected override void GridChanged()
    {
        if (isSwitching) return;
        if (string.IsNullOrEmpty(targetSceneName)) return;

        Cell currentCell = gridManager.gridList[gridPos.x][gridPos.y].GetComponent<Cell>();

        if (currentCell.ContainObj != null)
        {
            Block enteringBlock = currentCell.ContainObj.GetComponent<Block>();
            if (enteringBlock != null && enteringBlock != this)
            {
                if (enteringBlock.CompareTag("Player"))
                {
                    isSwitching = true;
                    SceneManager.LoadScene(targetSceneName);
                }
            }
        }
    }
}