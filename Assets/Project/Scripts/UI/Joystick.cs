using System;
using System.Collections;
using System.Collections.Generic;
using Project.Game.Scripts.MVVM.MVVM1.UI.Views;
using UnityEngine;

public class Joystick : MonoBehaviour, IView
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
