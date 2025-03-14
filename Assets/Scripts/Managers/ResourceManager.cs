using System;
using UnityEngine;
using UnityEngine.U2D;
using Object = UnityEngine.Object;

public enum SpriteType
{
    Item,
    Count
}

public class ResourceManager
{
    private readonly SpriteAtlas[] atlas = new SpriteAtlas[(int)SpriteType.Count];

    public Sprite GetSprite(SpriteType type, int id) => atlas[(int)type].GetSprite(id.ToString());

    public void Initialize()
    {
        string[] names = Enum.GetNames(typeof(SpriteType));
        for (int i = 0; i < atlas.Length; i++)
        {
            SpriteAtlas atlas = Resources.Load<SpriteAtlas>($"{Define.PATH_ATLAS}/Atlas_{names[i]}");
            this.atlas[i] = atlas;
        }
    }

    public T Instantiate<T>(Transform parent) where T : UI_Base
    {
        string key = typeof(T).Name;
        GameObject gameObject = Instantiate(key, Vector3.zero, Define.PATH_UI);
        gameObject.transform.SetParent(parent);

        UI_Base @base = gameObject.GetComponent<T>();
        @base.Open();

        return @base as T;
    }

    public GameObject Instantiate(string key, Vector3 position, string pathType = Define.PATH_OBJECT)
    {
        GameObject gameObject = Managers.Pool.TryPop(key);
        if (gameObject == null)
        {
            string path = $"{pathType}/{key}";
            GameObject original = Resources.Load<GameObject>(path);
            if (original == null)
            {
                Debug.LogWarning($"Failed to Load<GameObject>({path})");
                return null;
            }

            gameObject = Instantiate(original);
        }

        gameObject.transform.SetPositionAndRotation(position, Quaternion.identity);
        return gameObject;
    }

    public GameObject Instantiate(GameObject original)
    {
        GameObject gameObject = Object.Instantiate(original);
        gameObject.name = original.name;

        return gameObject;
    }

    public void Destroy(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Poolable>(out var poolable))
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(gameObject);
    }
}