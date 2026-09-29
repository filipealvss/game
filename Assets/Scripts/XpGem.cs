using UnityEngine;

public class XpGem : MonoBehaviour
{
    public int Value = 1;

    void Update()
    {
        var player = GameManager.Instance.Player;
        var to = player.transform.position - transform.position;
        float dist = to.magnitude;
        if (dist < player.PickupRadius)
            transform.position += to.normalized * 10f * Time.deltaTime;
        if (dist < 0.4f)
        {
            player.AddXp(Value);
            Destroy(gameObject);
        }
    }
}
