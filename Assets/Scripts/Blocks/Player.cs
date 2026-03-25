using Unity.Mathematics;
using UnityEngine;

public class Player : Block
{

    [SerializeField]
    private Animator myAnim;

    private void Update()
    {
        if (State == MoveStates.idle) MoveInput();
    }

    private void MoveInput()
    {
        //Changed the input to fit my keyboard (My "d" key broke)
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CheckMove(-1, 0)) transform.rotation = Quaternion.LookRotation(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CheckMove(1, 0)) transform.rotation = Quaternion.LookRotation(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CheckMove(0, -1)) transform.rotation = Quaternion.LookRotation(Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CheckMove(0, 1)) transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
    }

    protected override void StartMove(Cell newParent, int _deltaX, int _deltaY)
    {
        myAnim.SetBool("isMoving", true);
        base.StartMove(newParent, _deltaX, _deltaY);
    }

    protected override void FinishMove()
    {
        myAnim.SetBool("isMoving", false);
        base.FinishMove();
    }

}
