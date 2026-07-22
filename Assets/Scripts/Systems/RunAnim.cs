using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class RunAnim : IEcsRunSystem
{
    private EcsFilterInject<Inc<MoveAnim, ViewAnim>> _filter;

    public void Run(EcsSystems systems)
    {
        foreach (var entity in _filter.Value)
        {
            ref var move = ref _filter.Pools.Inc1.Get(entity);
            ref var view = ref _filter.Pools.Inc2.Get(entity);

            if (view.Transform != null)
            {
                view.Transform.position += Vector3.right * move.Speed * Time.deltaTime;
            }
        }
    }
}