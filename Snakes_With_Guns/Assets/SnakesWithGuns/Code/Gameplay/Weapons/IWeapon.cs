namespace SnakesWithGuns.Gameplay.Weapons
{
    public interface IWeapon
    {
        int DamageLayer { get; set; }
        bool IsFiring { get; set; }
        void Initialize(WeaponDefinition weaponDefinition);
    }
}