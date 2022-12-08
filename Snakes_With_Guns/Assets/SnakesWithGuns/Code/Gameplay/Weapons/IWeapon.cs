namespace SnakesWithGuns.Gameplay.Weapons
{
    public interface IWeapon
    {
        int SourceID { get; set; }
        bool IsFiring { get; set; }
        void Initialize(WeaponDefinition weaponDefinition);
    }
}