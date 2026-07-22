using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {
        [SerializeField] private SceneService _sceneService;
        EcsWorld _world;
        EcsSystems _systems;

        void Start() {
            _world = new EcsWorld();
            
            _systems = new EcsSystems(_world)
                .Add(new InitItemsSystem())
                .Add(new PlayerInputSystem())
                .Add(new MovementSystem())
                .Add(new ItemPickupSystem())
                // .Add(new ScoreSystem()) // ← можно убрать, т.к. счет обновляется в ItemPickupSystem
                .Add(new InitAnim())
                .Add(new RunAnim())
                .Inject(_sceneService);
            
            _systems.Init();
            Debug.Log("EcsStartup: Системы инициализированы!");
        }

        void Update() {
            _systems?.Run();
        }

        void OnDestroy() {
            _systems?.Destroy();
            _world?.Destroy();
        }

        public EcsWorld GetWorld() => _world;
    }
}