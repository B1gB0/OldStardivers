using System;
using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuRootBinder : MonoBehaviour
{
    [field: SerializeField] public Button PlayButton { get; private set; }
    [field: SerializeField] public Button SettingsButton { get; private set; }
    [field: SerializeField] public Button ExitButton { get; private set; }
    
    private Subject<Unit> _exitSceneSubjectSignal;

    private void OnEnable()
    {
        PlayButton.onClick.AddListener(HandleGoToGameplayButtonClick);
    }

    private void OnDisable()
    {
        PlayButton.onClick.RemoveListener(HandleGoToGameplayButtonClick);
    }

    public void HandleGoToGameplayButtonClick()
    {
        _exitSceneSubjectSignal?.OnNext(Unit.Default);
    }

    public void Bind(Subject<Unit> exitSceneSignalSubject)
    {
        _exitSceneSubjectSignal = exitSceneSignalSubject;
    }
}
 