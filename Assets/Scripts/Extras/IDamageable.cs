public interface IDamageable
{
    Team Team { get; }
    void TakeDamage(int damage);
}
