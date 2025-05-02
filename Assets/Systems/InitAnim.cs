using Leopotam.EcsLite;
using UnityEngine;

namespace Client {
    sealed class InitAnim : IEcsInitSystem {
        public void Init (IEcsSystems systems) {
            var go = GameObject.FindGameObjectWithTag("Anim");

            var world = systems.GetWorld();
            var entity = world.NewEntity();

            ref var moveComp = ref world.GetPool<MoveAnim>().Add(entity);
            moveComp.Speed = 1;


            ref var viewComp = ref world.GetPool<ViewAnim>().Add(entity);
            viewComp.Transform = go.transform;

        }
    }
}