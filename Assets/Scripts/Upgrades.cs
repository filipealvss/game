using UnityEngine;

public static class Upgrades
{
    static readonly string[] All =
    {
        "+20% Dano", "+15% Velocidade de ataque", "+1 Projetil", "+10% Velocidade",
        "+25 HP maximo", "+50% Alcance de coleta", "Curar 50% HP"
    };

    public static string[] RollThree()
    {
        var pool = new System.Collections.Generic.List<string>(All);
        var result = new string[3];
        for (int i = 0; i < 3; i++)
        {
            int idx = Random.Range(0, pool.Count);
            result[i] = pool[idx];
            pool.RemoveAt(idx);
        }
        return result;
    }

    public static void Apply(string upgrade, Player p)
    {
        var w = p.GetComponent<Weapon>();
        switch (upgrade)
        {
            case "+20% Dano": w.Damage *= 1.2f; break;
            case "+15% Velocidade de ataque": w.Cooldown *= 0.85f; break;
            case "+1 Projetil": w.ProjectileCount++; break;
            case "+10% Velocidade": p.Speed *= 1.1f; break;
            case "+25 HP maximo": p.MaxHp += 25; p.Hp += 25; break;
            case "+50% Alcance de coleta": p.PickupRadius *= 1.5f; break;
            case "Curar 50% HP": p.Hp = Mathf.Min(p.MaxHp, p.Hp + p.MaxHp * 0.5f); break;
        }
    }
}
