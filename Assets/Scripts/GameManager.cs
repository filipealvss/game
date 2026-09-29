using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Player Player { get; private set; }
    public float TimeAlive { get; private set; }
    public int Kills;
    public bool GameOver { get; private set; }
    public bool ChoosingUpgrade { get; private set; }

    string[] _options;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
        var p = GameBootstrap.MakeSprite("Player", new Color(0.3f, 0.7f, 1f), 0.8f, 10);
        Player = p.AddComponent<Player>();
        p.AddComponent<Weapon>();
        gameObject.AddComponent<EnemySpawner>();
    }

    void Update()
    {
        if (GameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            return;
        }
        if (!ChoosingUpgrade) TimeAlive += Time.deltaTime;
    }

    void LateUpdate()
    {
        if (Player == null) return;
        var cam = Camera.main.transform;
        cam.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
    }

    public void OnLevelUp()
    {
        ChoosingUpgrade = true;
        Time.timeScale = 0;
        _options = Upgrades.RollThree();
    }

    public void OnPlayerDied()
    {
        GameOver = true;
        Time.timeScale = 0;
    }

    void OnGUI()
    {
        var style = new GUIStyle(GUI.skin.label) { fontSize = 22 };
        GUI.Label(new Rect(10, 10, 600, 30),
            $"HP {Mathf.CeilToInt(Player.Hp)}/{Player.MaxHp}   Lv {Player.Level}   XP {Player.Xp}/{Player.XpToNext}   Kills {Kills}", style);
        int t = (int)TimeAlive;
        GUI.Label(new Rect(Screen.width / 2 - 40, 10, 120, 30), $"{t / 60:00}:{t % 60:00}", style);

        if (ChoosingUpgrade)
        {
            var btn = new GUIStyle(GUI.skin.button) { fontSize = 20 };
            GUI.Box(new Rect(Screen.width / 2 - 220, Screen.height / 2 - 150, 440, 300), "LEVEL UP!");
            for (int i = 0; i < _options.Length; i++)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100 + i * 80, 400, 60), _options[i], btn))
                {
                    Upgrades.Apply(_options[i], Player);
                    ChoosingUpgrade = false;
                    Time.timeScale = 1;
                }
            }
        }

        if (GameOver)
        {
            var big = new GUIStyle(GUI.skin.label) { fontSize = 40, alignment = TextAnchor.MiddleCenter };
            GUI.Label(new Rect(0, Screen.height / 2 - 60, Screen.width, 120),
                $"GAME OVER\nSobreviveu {t / 60:00}:{t % 60:00} - pressione R", big);
        }
    }
}
