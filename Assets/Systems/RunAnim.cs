using Leopotam.EcsLite;
using UnityEngine;

namespace Client {
    sealed class RunAnim : IEcsRunSystem {        
        public void Run (IEcsSystems systems) {
            EcsWorld world = systems.GetWorld();

            // Мы хотим получить все сущности с компонентом "MoveAnim".
           var filter = world.Filter<MoveAnim>().End();

            // Запросим и закешируем пул компонентов "MoveAnim".
            var movePool = world.GetPool<MoveAnim>();
            var viewPool = world.GetPool<ViewAnim>();





            foreach (int entity in filter)
            {
                ref MoveAnim moveComp = ref movePool.Get(entity);
                ref ViewAnim viewComp = ref viewPool.Get(entity);

                viewComp.Transform.position += Vector3.left * moveComp.Speed * Time.deltaTime;

            }
        
    }
    }
}