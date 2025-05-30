using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventBus<T> : ScriptableObject
{
    private event Action<T> listeners;


    public void Raise(T data) => listeners?.Invoke(data);

    public void Register(Action<T> listener) => listeners += listener;
    public void UnRegister(Action<T> listener) => listeners -= listener;





}
