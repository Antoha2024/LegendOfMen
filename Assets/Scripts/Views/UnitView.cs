using UnityEngine;
using Leopotam.EcsLite;

public class UnitView : MonoBehaviour
{
    private static readonly int Up = Animator.StringToHash("anim_2");

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;

    public EcsPackedEntity Entity;

    public void Move(Vector3 translation)
    {
        transform.Translate(translation);
    }

    public void SetDirection(Vector3 velocity)
    {
        if (_spriteRenderer != null)
            _spriteRenderer.flipX = velocity.x < 0;
    }

    public void UpdateAnimationState(Vector3 velocity)
    {
        if (_animator == null) return;
        _animator.SetBool(Up, velocity.x != 0);
    }

    public void PlayTestAnimation()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Attack2");
            Debug.Log("UnitView: Тестовая анимация удара запущена!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"=== UnitView: OnCollisionEnter2D вызван! ===");
        Debug.Log($"Объект: {collision.gameObject.name}, Тег: {collision.gameObject.tag}");

        if (!collision.gameObject.CompareTag("Item")) 
        {
            Debug.Log($"Не предмет, тег = {collision.gameObject.tag}");
            return;
        }
        
        var itemView = collision.gameObject.GetComponent<ItemView>();
        if (itemView == null)
        {
            Debug.LogWarning("UnitView: ItemView не найден!");
            return;
        }

        var ecsStartup = FindAnyObjectByType<Client.EcsStartup>();
        if (ecsStartup == null)
        {
            Debug.LogWarning("UnitView: EcsStartup не найден!");
            return;
        }

        var world = ecsStartup.GetWorld();
        if (world == null)
        {
            Debug.LogWarning("UnitView: World не найден!");
            return;
        }
        
        // Создаем событие подбора
        int pickupEntity = world.NewEntity();
        ref var pickupEvent = ref world.GetPool<ItemPickupEvent>().Add(pickupEntity);
        pickupEvent.PlayerEntity = Entity;
        pickupEvent.ItemEntity = itemView.Entity;
        
        Debug.Log($"Событие подбора создано! Player: {Entity}, Item: {itemView.Entity}");
        
        // ДЕАКТИВИРУЕМ ПРЕДМЕТ
        collision.gameObject.SetActive(false);
        Debug.Log($"Предмет {collision.gameObject.name} деактивирован!");
    }
}