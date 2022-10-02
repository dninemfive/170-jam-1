using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// (incomplete) Attribute which can be applied to fields and properties to tell the Tests class to check whether required components are present.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class HaveComponentsAttribute : Attribute
{
    public Type[] RequiredComponents { get; private set; }
    public HaveComponentsAttribute(params Type[] components)
    {
        RequiredComponents = components;
    }
    public bool Evaluate(object obj)
    {
        if (obj is not IEnumerable<GameObject>) throw new Exception("HaveComponentsAttribute can only be applied to IEnumerables containing GameObjects.");
        IEnumerable<GameObject> enumerable = obj as IEnumerable<GameObject>;
        foreach(GameObject gameObject in enumerable)
            foreach (Type componentType in RequiredComponents) 
                if (gameObject.GetComponent(componentType) is not null) return false;      
        return true;
    }
}
/// <summary>
/// Just an alias for HaveComponentsAttribute to make it more readable when only one component is required.
/// </summary>
public class HaveComponentAttribute : HasComponentsAttribute
{
    public HaveComponentAttribute(params Type[] types) : base(types) { }
}
