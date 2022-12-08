namespace SnakesWithGuns.Gameplay.Weapons
{
    public interface IDamageable
    {
        int SourceID { get; }
        void DealDamage(int amount);
    }
}