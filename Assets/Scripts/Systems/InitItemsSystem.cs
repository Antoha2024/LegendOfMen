using Leopotam.EcsLite;
using UnityEngine;

public class InitItemsSystem : IEcsInitSystem
{
    public void Init(EcsSystems systems)
    {
        var world = systems.GetWorld();
        var itemPool = world.GetPool<ItemTag>();
        var unitCmpPool = world.GetPool<UnitCmp>();

        var itemObjects = GameObject.FindGameObjectsWithTag("Item");
        
        if (itemObjects.Length == 0)
        {
            Debug.LogWarning("InitItemsSystem: Нет объектов с тегом 'Item' на сцене!");
            return;
        }

        foreach (var go in itemObjects)
        {
            int entity = world.NewEntity();
            itemPool.Add(entity);

            var itemView = go.GetComponent<ItemView>();
            if (itemView == null)
            {
                itemView = go.AddComponent<ItemView>();
            }
            
            itemView.Entity = world.PackEntity(entity);

            var view = go.GetComponent<UnitView>();
            if (view == null)
            {
                view = go.AddComponent<UnitView>();
            }
            
            ref var unitCmp = ref unitCmpPool.Add(entity);
            unitCmp.View = view;

            go.SetActive(true);
            Debug.Log($"InitItemsSystem: Предмет создан: {go.name}");
        }
    }
}