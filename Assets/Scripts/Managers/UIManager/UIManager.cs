using System;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class UIManager
{
    public enum Type
    {
        Menu,
        Scene,
        SubItem,
        WorldSpace,
        Count
    }

    public UI_Scene CurrentSceneUI { get; private set; }

    private Transform transform;

    private readonly Transform[] children = new Transform[(int)Type.Count];

    public void Initialize()
    {
        transform = new GameObject(nameof(UIManager), typeof(InputSystemUIInputModule)).transform;
        transform.SetParent(Managers.Instance.transform);

        var names = Enum.GetNames(typeof(Type));
        for (int i = 0; i < children.Length; i++)
        {
            var child = new GameObject(names[i]).transform;
            child.SetParent(transform);
            children[i] = child;
        }
    }

    public T Show<T>() where T : UI_Base
    {
        Type type = GetType(typeof(T));
        if (type == Type.Count)
        {
            Debug.LogWarning($"Failed to Show<{typeof(T).Name}>()");
            return null;
        }

        T @base = Managers.Resource.Instantiate<T>(children[(int)type]);
        if (@base == null)
        {
            return null;
        }

        switch (type)
        {
            case Type.Scene:
                CurrentSceneUI = @base as UI_Scene;
                break;
            case Type.WorldSpace:
                @base.GetComponent<Canvas>().worldCamera = Managers.Game.MainCamera;
                break;
        }

        return @base;
    }

    private Type GetType(System.Type type)
    {
        return type switch
        {
            var t when t.IsSubclassOf(typeof(UI_Menu)) => Type.Menu,
            var t when t.IsSubclassOf(typeof(UI_Scene)) => Type.Scene,
            var t when t.IsSubclassOf(typeof(UI_SubItem)) => Type.SubItem,
            var t when t.IsSubclassOf(typeof(UI_WorldSpace)) => Type.WorldSpace,
            _ => Type.Count
        };
    }
}