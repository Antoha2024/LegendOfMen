using UnityEngine;
using Leopotam.EcsLite;
using Client;

public class Monobeh : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private EcsWorld GetWorld()
    {
        var startup = FindAnyObjectByType<EcsStartup>();
        if (startup != null)
        {
            return startup.GetWorld();
        }
        return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Monobeh: OnCollisionEnter2D вызван!");

        var world = GetWorld();
        if (world == null)
        {
            Debug.LogWarning("Monobeh: EcsStartup не найден!");
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            var playerView = collision.gameObject.GetComponent<UnitView>();
            if (playerView == null) return;

            PlayAttackAnimation();
            
            var attackPool = world.GetPool<AttackEvent>();
            int attackEntity = world.NewEntity();
            ref var attackEvent = ref attackPool.Add(attackEntity);
            attackEvent.Target = playerView.Entity;
            
            Debug.Log("Monobeh: Событие удара отправлено!");
        }
        else if (collision.gameObject.CompareTag("Item"))
        {
            var itemView = collision.gameObject.GetComponent<ItemView>();
            if (itemView == null)
            {
                Debug.LogWarning("Monobeh: ItemView не найден на предмете!");
                return;
            }

            var playerObject = GameObject.FindWithTag("Player");
            if (playerObject == null)
            {
                Debug.LogWarning("Monobeh: Игрок не найден по тегу!");
                return;
            }

            var playerView = playerObject.GetComponent<UnitView>();
            if (playerView == null)
            {
                Debug.LogWarning("Monobeh: UnitView не найден на игроке!");
                return;
            }

            var pickupPool = world.GetPool<ItemPickupEvent>();
            int pickupEntity = world.NewEntity();
            ref var pickupEvent = ref pickupPool.Add(pickupEntity);
            pickupEvent.ItemEntity = itemView.Entity;
            pickupEvent.PlayerEntity = playerView.Entity;

            Debug.Log("Monobeh: Событие подбора предмета отправлено!");
        }
        else
        {
            Debug.Log("Monobeh: Столкновение с неизвестным объектом: " + collision.gameObject.tag);
        }
    }

    public void PlayAttackAnimation()
    {
        Debug.Log("Monobeh: PlayAttackAnimation вызван!");

        if (_animator != null)
        {
            Debug.Log("Monobeh: Animator найден, отправляем триггер Attack2");
            _animator.SetTrigger("Attack2");
            Debug.Log("Monobeh: Триггер Attack2 отправлен!");
        }
        else
        {
            Debug.LogError("Monobeh: Animator НЕ назначен в инспекторе! Перетащите его.");
        }
    }
}