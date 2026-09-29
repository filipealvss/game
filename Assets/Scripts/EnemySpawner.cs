using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float _timer;

    void Update()
    {
        var gm = GameManager.Instance;
        float minutes = gm.TimeAlive / 60f;
        float interval = Mathf.Max(0.12f, 1.2f - minutes * 0.25f); // gets harder over time
        _timer -= Time.deltaTime;
        if (_timer > 0) return;
        _timer = interval;

        bool tank = Random.value < Mathf.Clamp01(minutes * 0.1f);
        var go = GameBootstrap.MakeSprite("Enemy", tank ? new Color(0.6f, 0.1f, 0.6f) : new Color(0.9f, 0.2f, 0.2f), tank ? 1.2f : 0.7f, 5);
        var angle = Random.value * Mathf.PI * 2;
        go.transform.position = gm.Player.transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * 14f;
        var e = go.AddComponent<Enemy>();
        e.Hp = (tank ? 40 : 10) * (1 + minutes * 0.5f);
        e.Speed = tank ? 1.3f : 2f + minutes * 0.1f;
        e.XpValue = tank ? 5 : 1;
    }
}
