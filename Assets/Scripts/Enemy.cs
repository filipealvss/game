using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 2f;
    public float Hp = 10;
    public float ContactDamage = 10;
    public int XpValue = 1;

    Player _player;

    void Start() => _player = GameManager.Instance.Player;

    void Update()
    {
        var toPlayer = (Vector2)(_player.transform.position - transform.position);
        float dist = toPlayer.magnitude;
        if (dist > 0.01f) transform.position += (Vector3)(toPlayer / dist * Speed * Time.deltaTime);
        if (dist < 0.7f) _player.TakeDamage(ContactDamage);
        if (dist > 30f) Destroy(gameObject); // despawn far-away enemies
    }

    public void TakeDamage(float dmg)
    {
        Hp -= dmg;
        if (Hp > 0) return;
        GameManager.Instance.Kills++;
        var gem = GameBootstrap.MakeSprite("XpGem", new Color(0.3f, 1f, 0.5f), 0.3f, 1);
        gem.transform.position = transform.position;
        gem.AddComponent<XpGem>().Value = XpValue;
        Destroy(gameObject);
    }
}
