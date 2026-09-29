using UnityEngine;

// Auto-fires at the nearest enemy, like the Magic Wand in Vampire Survivors.
public class Weapon : MonoBehaviour
{
    public float Damage = 5f;
    public float Cooldown = 0.8f;
    public int ProjectileCount = 1;
    public float Range = 12f;

    float _timer;

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0) return;

        Enemy target = null;
        float best = Range * Range;
        foreach (var e in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
        {
            float d = (e.transform.position - transform.position).sqrMagnitude;
            if (d < best) { best = d; target = e; }
        }
        if (target == null) return;
        _timer = Cooldown;

        Vector2 dir = (target.transform.position - transform.position).normalized;
        for (int i = 0; i < ProjectileCount; i++)
        {
            float spread = (i - (ProjectileCount - 1) / 2f) * 12f;
            var go = GameBootstrap.MakeSprite("Projectile", Color.yellow, 0.25f, 8);
            go.transform.position = transform.position;
            var p = go.AddComponent<Projectile>();
            p.Direction = Quaternion.Euler(0, 0, spread) * dir;
            p.Damage = Damage;
        }
    }
}
