using System;
using System.Collections;
using System.Collections.Generic;
using MVVM;
using R3;
using UnityEngine;
using UnityEngine.UI;

public sealed class TextBinder : IBinder, IObserver<string>
{
    private readonly Text _view;
    private readonly ReadOnlyReactiveProperty<string> _property;
    private IDisposable _handle;

    public TextBinder(Text view, ReadOnlyReactiveProperty<string> property)
    {
        _view = view;
        _property = property;
    }

    public void Bind()
    {
        OnNext(_property.CurrentValue);
        _handle = _property.Subscribe();
    }

    public void Unbind()
    {
        _handle?.Dispose();
        _handle = null;
    }

    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public void OnNext(string value)
    {
        _view.text = value;
    }
}
