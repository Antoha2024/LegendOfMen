using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem, IEcsInitSystem
{
	// Инжектим нужные зависимости
    private EcsWorldInject _world;
    private EcsPoolInject<UnitCmp> _unitCmpPool;
    private EcsPoolInject<PlayerTag> _playerTagPool;
    private EcsCustomInject<SceneService> _sceneData;

    private int _playerEntity;

    public void Init(IEcsSystems systems)
    {
        //Создаём сущность игрока
        _playerEntity = _world.Value.NewEntity();

        //Добавляем PlayerTag и UnitCmp на сущность игрока
        _playerTagPool.Value.Add(_playerEntity);
        ref var playerCmp = ref _unitCmpPool.Value.Add(_playerEntity);

        //Прокидываем UnitView в UnitCmp
        playerCmp.View = _sceneData.Value.PlayerView;
    }

    public void Run(IEcsSystems systems)
    {
        //Находим скорость игрока
        var playerMoveSpeed = _sceneData.Value.PlayerMoveSpeed;
        var x = Input.GetAxisRaw("Horizontal");
        var direction = new Vector3(x, 0).normalized;
        var velocity = direction * playerMoveSpeed;

        // Проверяем что на сущности игрока точно есть UnitCmp
        if (!_unitCmpPool.Value.Has(_playerEntity))
            return;

        // Устанавливаем скорость
        ref var playerCmp = ref _unitCmpPool.Value.Get(_playerEntity);
        playerCmp.Velocity = velocity;
    }
}
