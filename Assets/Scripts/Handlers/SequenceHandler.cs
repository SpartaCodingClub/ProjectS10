using DG.Tweening;
using System;

public class SequenceHandler
{
    public Sequence Open { get; private set; }
    public Sequence Opened { get; private set; }
    public Sequence Close { get; private set; }

    public void Initialize()
    {
        Open = Utility.RecyclableSequence();
        Opened = Utility.RecyclableSequence().SetLoops(-1);
        Close = Utility.RecyclableSequence();
    }

    public void Deinitialize()
    {
        Open.Kill();
        Opened.Kill();
        Close.Kill();
    }

    public void Bind(UIState type, params Func<Sequence>[] sequences)
    {
        Sequence sequence = sequences[0]();
        for (int i = 1; i < sequences.Length; i++)
        {
            sequence.Join(sequences[i]());
        }

        switch (type)
        {
            case UIState.Closed:
                Debug.LogWarning($"Failed to BindSequences({type})");
                break;
            case UIState.Open:
                Open.Append(sequence);
                break;
            case UIState.Opened:
                Opened.Append(sequence);
                break;
            case UIState.Close:
                Close.Append(sequence);
                break;
        }
    }
}