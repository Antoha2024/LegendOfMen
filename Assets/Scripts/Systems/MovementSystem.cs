using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class MovementSystem : IEcsRunSystem
{
    private EcsPoolInject<UnitCmp> _unitCmpPool;
    private EcsFilterInject<Inc<UnitCmp>> _unitCmpFilter;

    public void Run(EcsSystems systems)
    {
        foreach (var entity in _unitCmpFilter.Value)
        {
            ref var unitCmp = ref _unitCmpPool.Value.Get(entity);
            var velocity = unitCmp.Velocity;
            var view = unitCmp.View;

            if (view == null)
            {
                Debug.LogWarning($"MovementSystem: view == null для сущности {entity}");
                continue;
            }

            view.UpdateAnimationState(velocity);

            if (velocity == Vector3.zero)
                continue;

            var translation = velocity * Time.deltaTime;
            view.SetDirection(velocity);
            view.Move(translation);
        }
    }
}