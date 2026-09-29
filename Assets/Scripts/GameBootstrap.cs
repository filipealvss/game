using UnityEngine;

// Builds the whole game at runtime, so any empty scene works (no scene assets needed).
public static class GameBootstrap
{
    static Sprite _square;
    public static Sprite Square
    {
        get
        {
            if (_square == null)
            {
                var tex = new Texture2D(1, 1);
                tex.SetPixel(0, 0, Color.white);
                tex.Apply();
                _square = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
            }
            return _square;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Init()
    {
        if (Object.FindAnyObjectByType<GameManager>() != null) return;

        var cam = Camera.main;
        if (cam == null)
        {
            cam = new GameObject("Main Camera").AddComponent<Camera>();
            cam.tag = "MainCamera";
        }
        cam.orthographic = true;
        cam.orthographicSize = 8;
        cam.backgroundColor = new Color(0.08f, 0.1f, 0.08f);
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.transform.position = new Vector3(0, 0, -10);

        new GameObject("GameManager").AddComponent<GameManager>();
    }

    public static GameObject MakeSprite(string name, Color color, float size, int order = 0)
    {
        var go = new GameObject(name);
        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = Square;
        sr.color = color;
        sr.sortingOrder = order;
        go.transform.localScale = Vector3.one * size;
        return go;
    }
}
