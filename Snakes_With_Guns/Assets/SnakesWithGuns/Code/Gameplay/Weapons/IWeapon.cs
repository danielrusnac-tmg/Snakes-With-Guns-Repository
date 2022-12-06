namespace SnakesWithGuns.Gameplay.Weapons
{
    public interface IWeapon
    {
        bool IsFiring { get; set; }
        void Initialize(WeaponDefinition weaponDefinition);
    }
}