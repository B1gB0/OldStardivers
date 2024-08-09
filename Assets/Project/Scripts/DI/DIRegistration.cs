using System;
using Object = UnityEngine.Object;

namespace DI
{
    public class DIRegistration
    {
        public Func<DIContainer, object> Factory { get; set; }
        
        public bool IsSingleton { get; set; }
        
        public object Instance { get; set; }
    }
}