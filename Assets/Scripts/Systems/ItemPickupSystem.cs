using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class ItemPickupSystem : IEcsRunSystem
{
    private EcsFilterInject<Inc<ItemPickupEvent>> _eventFilter;
    private EcsPoolInject<ItemPickupEvent> _eventPool;
    private EcsPoolInject<ScoreComponent> _scorePool;
    private EcsPoolInject<ItemTag> _itemTagPool;

    public void Run(EcsSystems systems)
    {
        var world = systems.GetWorld();

        foreach (var entity in _eventFilter.Value)
        {
            ref var evt = ref _eventPool.Value.Get(entity);
            
            // Распаковываем сущность игрока
            if (evt.PlayerEntity.Unpack(world, out int playerEntity))
            {
                // Добавляем или обновляем счет у игрока
                if (!_scorePool.Value.Has(playerEntity))
                {
                    ref var newScore = ref _scorePool.Value.Add(playerEntity);
                    newScore.Value = 0;
                    Debug.Log("ScoreComponent добавлен игроку");
                }
                
                // Используем ДРУГОЕ имя переменной
                ref var playerScore = ref _scorePool.Value.Get(playerEntity);
                playerScore.Value++;
                Debug.Log($"Предмет подобран! Счёт: {playerScore.Value}");
            }

            // Распаковываем и удаляем сущность предмета
            if (evt.ItemEntity.Unpack(world, out int itemEntity))
            {
                world.DelEntity(itemEntity);
            }

            // Удаляем событие
            _eventPool.Value.Del(entity);
        }
    }
}