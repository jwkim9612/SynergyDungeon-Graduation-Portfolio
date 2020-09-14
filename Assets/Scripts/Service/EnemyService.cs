public class EnemyService
{
    public const float KNOCKBACK_DISTANCE = 0.25f;
    public static float KNOCKBACK_SPEED = 0.01f;

    public const float ATTACK_ANIMATION_LENGTH = 2.0f;

    public static void SetDoubleSpeed()
    {
        KNOCKBACK_SPEED *= InGameService.DOUBLE_SPEED;
    }

    public static void SetDefaultSpeed()
    {
        KNOCKBACK_SPEED = 0.01f;
    }
}
