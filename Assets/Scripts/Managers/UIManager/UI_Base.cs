using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VInspector;

public enum UIState
{
    Closed,
    Open,
    Opened,
    Close
}

public abstract class UI_Base : MonoBehaviour
{
    #region Inspector  
#if UNITY_EDITOR
    [ShowInInspector]
    public UIState CurrentState
    {
        get
        {
            if (EditorApplication.isPlaying == false)
            {
                return UIState.Closed;
            }

            EditorUtility.SetDirty(this);
            return state;
        }
    }
#endif
    #endregion

    public event Action OnClosed;

    protected CanvasGroup canvasGroup;

    private UIState state;

    private readonly SequenceHandler sequenceHandler = new();
    private readonly List<RectTransform> children = new();

    private void Awake() => Initialize();
    private void OnDestroy() => Deinitialize();
    private void OnDisable() => Clear();

    protected RectTransform Get(int index) => children[index];
    protected T Get<T>(int index) where T : Component => Get(index).GetComponent<T>();
    protected void BindSequences(UIState type, params Func<Sequence>[] sequences) => sequenceHandler.Bind(type, sequences);

    protected virtual void Initialize()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        sequenceHandler.Initialize();
    }

    protected virtual void Deinitialize()
    {
        sequenceHandler.Deinitialize();
    }

    public virtual void Clear()
    {
        OnClosed = null;
    }

    public virtual void Open()
    {
        canvasGroup.interactable = false;
        state = UIState.Open;

        sequenceHandler.Open.Restart();
    }

    public virtual void Opened()
    {
        canvasGroup.interactable = true;
        state = UIState.Opened;

        sequenceHandler.Opened.Restart();
    }

    public virtual void Close()
    {
        canvasGroup.interactable = false;
        state = UIState.Close;

        sequenceHandler.Opened.Pause();
        sequenceHandler.Close.Restart();
    }

    public virtual void Destroy()
    {
        canvasGroup.interactable = false;
        state = UIState.Closed;

        sequenceHandler.Opened.Pause();

        OnClosed?.Invoke();
        Managers.Resource.Destroy(gameObject);
    }

    protected void BindChildren(Type enumType)
    {
        var names = Enum.GetNames(enumType);
        foreach (var name in names)
        {
            RectTransform child = gameObject.FindComponent<RectTransform>(name);
            children.Add(child);
        }
    }
}