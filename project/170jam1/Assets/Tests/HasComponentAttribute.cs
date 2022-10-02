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
public class HasComponentsAttribute : Attribute
{
    public Type[] RequiredComponents { get; private set; }
    public HasComponentsAttribute(params Type[] components)
    {
        RequiredComponents = components;
    }
    public bool Evaluate(object obj)
    {
        if (obj is not GameObject) throw new Exception("HasComponentsAttribute can only be applied to GameObjects.");
        GameObject gameObject = obj as GameObject;
        foreach (Type componentType in RequiredComponents) if (gameObject.GetComponent(componentType) is not null) return false;
        return true;
    }
}
/// <summary>
/// Just an alias for HasComponentsAttribute to make it more readable when only one component is required.
/// </summary>
public class HasComponentAttribute : HasComponentsAttribute
{
    public HasComponentAttribute(params Type[] types) : base(types) { }
}
