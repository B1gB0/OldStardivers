using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Project.Game.Scripts.MVVM;
using UnityEngine;

public class ViewModel : MonoBehaviour
{
    private static readonly Dictionary<PropertyInfo, Property> _properties = new Dictionary<PropertyInfo, Property>();
    private static readonly Dictionary<MethodInfo, Command> _commands = new Dictionary<MethodInfo, Command>();

    public IEnumerable<IBindable> GetProperties()
    {
        Type type = GetType();

        foreach (var property in type.GetProperties())
        {
            if (_properties.TryGetValue(property, out Property domainProperty))
            {
                yield return domainProperty;
                continue;
            }

            if (!property.GetCustomAttributes<ProjectAttribute>().Any())
                continue;

            domainProperty = new Property(this, property.GetValue, property.Name);

            AddPropertyChangeEventListener(GetModels(), property, domainProperty.OnChanged);

            _properties.Add(property, domainProperty);
            yield return domainProperty;
        }
    }

    public IEnumerable<IBindable> GetCommands()
    {
        Type type = GetType();

        foreach (var method in type.GetMethods())
        {
            if (_commands.TryGetValue(method, out Command domainCommand))
            {
                yield return domainCommand;
                continue;
            }

            if (!method.GetCustomAttributes<CommandAttribute>().Any())
                continue;

            domainCommand = new Command(this, method.Invoke, method.Name);
            _commands.Add(method, domainCommand);

            yield return domainCommand;
        }
    }

    public IEnumerable<IBindable> GetBindables()
    {
        return GetCommands().Concat(GetProperties());
    }

    private IEnumerable<object> GetModels()
    {
        Type type = GetType();

        foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
        {
            if (!field.GetCustomAttributes<ModelAttribute>().Any())
                continue;

            yield return field.GetValue(this);
        }
    }

    private void AddPropertyChangeEventListener(IEnumerable<object> eventSources, PropertyInfo property, Action handler)
    {
        foreach (var eventSource in eventSources)
        {
            Type type = eventSource.GetType();
            EventInfo eventInfo = type.GetEvent($"{property.Name}Changed");

            if (eventInfo != null)
            {
                eventInfo.AddEventHandler(eventSource, handler);
                return;
            }
        }
    }
}
