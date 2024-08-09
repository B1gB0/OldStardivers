using System.Collections;
using System.Collections.Generic;
using Build.Game.Scripts;
using Project.Scripts.MVP.Presenters;
using UnityEngine;

public class PresentersFactory
{
    public HealthBarPresenter CreateHealthBarPresenter(HealthBar healthBar, Health health)
    {
        HealthBarPresenter healthBarPresenter = new HealthBarPresenter(healthBar, health);

        return healthBarPresenter;
    }
}
