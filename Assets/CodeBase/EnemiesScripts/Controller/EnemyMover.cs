using CodeBase.EnemiesScripts.Controller;
using CodeBase.Logic.Utilities;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _moveSpeed;
    private HorizontalDirection _horizontalDirection;
    private EnemyAnimationsController _enemyAnimationsController;

    public void Construct(float moveSpeed) => 
        _moveSpeed = moveSpeed;

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _enemyAnimationsController = gameObject.GetComponent<EnemyAnimationsController>();

        _horizontalDirection = HorizontalDirection.Right;
    }

    public void Move(Transform target)
    {
        Vector2 vectorToKnight = target.transform.position - transform.position;
        
        transform.position = Vector2.Lerp(transform.position, target.position, _moveSpeed * Time.deltaTime);

        if (vectorToKnight.x > 0 && _horizontalDirection != HorizontalDirection.Right)
        {
            _horizontalDirection = HorizontalDirection.Right;
            _enemyAnimationsController.Turn();
        }
        else if (vectorToKnight.x < 0 && _horizontalDirection != HorizontalDirection.Left)
        {
            _horizontalDirection = HorizontalDirection.Left;
            _enemyAnimationsController.Turn();
        }
    }
}
