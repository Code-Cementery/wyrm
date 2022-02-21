using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Setup
{
    public static class DIInjector
    {

        static Dictionary<Type, object> Bindings = new Dictionary<Type, object>();

        public static HashSet<string> ExcludeFromInject = new HashSet<string>()
        {
            "EventSystem",
            "Camera",
            "Main Camera",

            "warehouse","granary",
            "lumberyard","stonemason","foundry","mill",
            "forum","castrum","bath","senate",
            "theatre","circus","colosseum",
            "pantheon"
        };

        public static string[] ExcludePatterns =
        {
            "- - ",
            "Context"
        };

        static BindingFlags _flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public static T Get<T>() where T : class
        {
            if (Bindings.TryGetValue(typeof(T), out object obj))
                return (T)obj;
            return null;
        }

        public static object Get(Type t)
        {
            if (Bindings.TryGetValue(t, out object obj))
                return obj;
            return null;
        }

        public static void Bind<T>(T o)
        {
            Bindings[typeof(T)] = o;
        }

        public static void Bind(object o)
        {
            Bindings[o.GetType()] = o;
        }

        public static void PopulateOneField(object component, string fieldType)
        {
            Type type = component.GetType();
            var field = type.GetField(fieldType, _flags);
            if (field == null)
                throw new Exception($"Field not found for {component}: {fieldType}");

            var binding = Get(field.FieldType);

            if (binding != null)
                field.SetValue(component, binding);
            else
                throw new Exception($"Binding not found for {component}: {fieldType} {field.Name}");
        }

        public static void Populate(object component)
        {
            Type type = component.GetType();

            //Debug.Log("[DI] Injecting " + component.name);


            // Find properties
            var properties = type.GetProperties(_flags).Where(prop => prop.IsDefined(typeof(InjectAttribute), false));

            foreach (var property in properties)
            {
                var binding = Get(property.PropertyType);

                if (binding != null)
                    property.SetValue(component, binding);
                else
                    throw new Exception($"Binding not found: {property.PropertyType.Name} {property.Name}");
            }

            // Find fields
            var fields = type.GetFields(_flags).Where(field => Attribute.IsDefined(field, typeof(InjectAttribute)));

            foreach (var field in fields)
            {
                var binding = Get(field.FieldType);

                if (binding != null)
                    field.SetValue(component, binding);
                else
                    Debug.LogError($"Binding not found for {component}: {field.FieldType.Name} {field.Name}");
                    //throw new Exception();
            }
        }

        public static void PopulateAll(IEnumerable<MonoBehaviour> components)
        {
            // todo: itt: nem lenni jo
            //var r = new Regex("[A-Z]{2,3}[0-9]{2,4}");

            foreach (var component in components)
            {
                var name = component.name.ToLower();

                // exclude a bunch of objects to optimize Reflection time on mobile phones
                if (ExcludeFromInject.Contains(name))
                    continue;
                bool ok = true;
                foreach (var pat in ExcludePatterns)
                    if (name.Contains(pat))
                    {
                        ok = false;
                        break;
                    }
                if (!ok)
                    continue;
                // filter out area names too
                //if (r.Match(name).Success)
                //    continue;

                Populate(component);
            }
        }

        internal static void Bind<T>(object receiver)
        {
            throw new NotImplementedException();
        }
    }
}
