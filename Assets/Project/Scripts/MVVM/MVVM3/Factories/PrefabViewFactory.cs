using UnityEngine;

namespace Project.Game.Scripts.MVVM.MVVM1.UI.Views.Factories
{
    public class PrefabViewFactory
    {
        public T Create<T>(string path = "") where T : Object
        {
            return Object.Instantiate(Resources.Load<T>(path + typeof(T).Name));
        } 
    }
}