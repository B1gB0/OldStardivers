namespace Project.Game.Scripts
{
    public class GunCharacteristics : WeaponCharacteristics
    {
        private const float RangeAttackStartValue = 5f;
        private const float FireRateStartValue = 2f;
        private const float BulletSpeedStartValue = 10f;
        private const float DamageStartValue = 2f;
        
        public GunCharacteristics()
        {
            SetStartValues(RangeAttackStartValue, FireRateStartValue, BulletSpeedStartValue, DamageStartValue);
        }
    }
}