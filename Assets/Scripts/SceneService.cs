using UnityEngine;

public class SceneService : MonoBehaviour
{
    [SerializeField] private UnitView _playerView;
    [SerializeField] private float _playerMoveSpeed = 5f;

    public UnitView PlayerView => _playerView;
    public float PlayerMoveSpeed => _playerMoveSpeed;
}