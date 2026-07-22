using Leopotam.EcsLite;

public struct ItemPickupEvent
{
    public EcsPackedEntity PlayerEntity;
    public EcsPackedEntity ItemEntity;
}