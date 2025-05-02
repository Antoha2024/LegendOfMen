using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

using UnityEngine;

public class MovementSystem : IEcsRunSystem
{
    private EcsPoolInject<UnitCmp> _unitCmpPool;
    private EcsFilterInject<Inc<UnitCmp>> _unitCmpFilter;

    public void Run(IEcsSystems systems)
    {
        //Бежим по всем сущностям с UnitCmp
        foreach (var entity in _unitCmpFilter.Value)
        {
            //Получаем текущую скорость и отображение
            var unitCmp = _unitCmpPool.Value.Get(entity);
            var velocity = unitCmp.Velocity;
            var view = unitCmp.View;

            view.UpdateAnimationState(velocity);

            if (velocity == Vector3.zero)
                continue;

            // Двигаем отображение
            var translation = velocity * Time.deltaTime;
            view.SetDirection(velocity);
            view.Move(translation);
        }
    }
}
