using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 Direction;
    public float Speed = 12f;
    public float Damage = 5f;
    float _life = 2f;

    void Update()
    {
        transform.position += (Vector3)(Direction * Speed * Time.deltaTime);
        _life -= Time.deltaTime;
        if (_life <= 0) { Destroy(gameObject); return; }

        foreach (var e in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
        {
            if ((e.transform.position - transform.position).sqrMagnitude < 0.36f)
            {
                e.TakeDamage(Damage);
                Destroy(gameObject);
                return;
            }
        }
    }
}
