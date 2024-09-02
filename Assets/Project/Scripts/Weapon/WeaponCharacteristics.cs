namespace Project.Game.Scripts
{
    public class WeaponCharacteristics
    {
        public float RangeAttack { get; private set; }

        public float FireRate { get; private set; }

        public float BulletSpeed { get; private set; }

        public float Damage { get; private set; }

        public void IncreaseDamage(float damageFactor)
        {
            Damage += Damage * damageFactor;
        }

        public void IncreaseFireRate(float fireRateFactor)
        {
            FireRate -= FireRate * fireRateFactor;
        }

        public void IncreaseBulletSpeed(float bulletSpeedFactor)
        {
            BulletSpeed += BulletSpeed * bulletSpeedFactor;
        }

        public void IncreaseRangeAttack(float rangeAttackFactor)
        {
            RangeAttack += RangeAttack * rangeAttackFactor;
        }

        protected void SetStartValues(float rangeAttack, float fireRate, float bulletSpeed, float damage)
        {
            RangeAttack = rangeAttack;
            FireRate = fireRate;
            BulletSpeed = bulletSpeed;
            Damage = damage;
        }
    }
}