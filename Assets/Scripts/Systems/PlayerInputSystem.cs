using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem, IEcsInitSystem
{
    private EcsWorldInject _world;
    private EcsPoolInject<UnitCmp> _unitCmpPool;
    private EcsPoolInject<PlayerTag> _playerTagPool;
    private EcsPoolInject<ScoreComponent> _scorePool;
    private EcsCustomInject<SceneService> _sceneData;
    private int _playerEntity;

    public void Init(EcsSystems systems)
    {
        _playerEntity = _world.Value.NewEntity();
        _playerTagPool.Value.Add(_playerEntity);
        
        ref var playerCmp = ref _unitCmpPool.Value.Add(_playerEntity);
        playerCmp.View = _sceneData.Value.PlayerView;
        
        if (playerCmp.View == null)
        {
            Debug.LogError("PlayerInputSystem: PlayerView == null! Назначьте PlayerView в SceneService.");
        }
        else
        {
            playerCmp.View.Entity = _world.Value.PackEntity(_playerEntity);
            Debug.Log($"PlayerInputSystem: PlayerView назначен: {playerCmp.View.gameObject.name}");
        }
        
        ref var scoreCmp = ref _scorePool.Value.Add(_playerEntity);
        scoreCmp.Value = 0;
        
        Debug.Log("PlayerInputSystem: Игрок создан в ECS! Счёт: 0");
    }

    public void Run(EcsSystems systems)
    {
        if (!_unitCmpPool.Value.Has(_playerEntity))
        {
            Debug.LogError("PlayerInputSystem: Игрок потерян!");
            return;
        }

        ref var playerCmp = ref _unitCmpPool.Value.Get(_playerEntity);
        
        var x = Input.GetAxisRaw("Horizontal");
        var direction = new Vector3(x, 0).normalized;
        var velocity = direction * _sceneData.Value.PlayerMoveSpeed;

        playerCmp.Velocity = velocity;

        if (Mathf.Abs(x) > 0.01f)
        {
            Debug.Log($"PlayerInputSystem: Движение x={x}, velocity={velocity}");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("PlayerInputSystem: Нажата E для подбора");
        }
    }
}