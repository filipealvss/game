using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 5f;
    public int MaxHp = 100;
    public float Hp = 100;
    public float PickupRadius = 2f;
    public int Level = 1;
    public int Xp;
    public int XpToNext = 5;

    float _hurtCooldown;
    SpriteRenderer _sr;

    void Awake() => _sr = GetComponent<SpriteRenderer>();

    void Update()
    {
        var dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (dir.sqrMagnitude > 1) dir.Normalize();
        transform.position += (Vector3)(dir * Speed * Time.deltaTime);

        _hurtCooldown -= Time.deltaTime;
        _sr.color = _hurtCooldown > 0.3f ? Color.red : new Color(0.3f, 0.7f, 1f);
    }

    public void TakeDamage(float dmg)
    {
        if (_hurtCooldown > 0 || GameManager.Instance.GameOver) return;
        _hurtCooldown = 0.5f;
        Hp -= dmg;
        if (Hp <= 0) { Hp = 0; GameManager.Instance.OnPlayerDied(); }
    }

    public void AddXp(int amount)
    {
        Xp += amount;
        if (Xp >= XpToNext)
        {
            Xp -= XpToNext;
            Level++;
            XpToNext = Mathf.RoundToInt(XpToNext * 1.4f) + 2;
            GameManager.Instance.OnLevelUp();
        }
    }
}
