using System;
using System.Text;
using UnityEngine;


[Serializable]
public enum RepuType
{
    None,
    Repellent,
    Attractant,
}

[Serializable]
public enum ChemoType
{
    None,
    Gustatory,
    Olfactory,
    Pheromone,
    Other,
}

public class Stimuli : ScriptableObject
{
    public string Name => name;

    public ReceptorType receptorType = ReceptorType.Chemo;

    public ChemoType chemoType = ChemoType.None;

    public RepuType attractDirection = RepuType.None;

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(Name);
        sb.Append(' ');


        if (receptorType == ReceptorType.Chemo && chemoType != ChemoType.None)
        {
            sb.Append('(');
            sb.Append(chemoType);
        }
        else if (receptorType != ReceptorType.Other)
        {
            sb.Append('(');
            sb.Append(receptorType);
        }
        else 
            return sb.ToString();

        if (attractDirection != RepuType.None)
        {
            sb.Append(' ');
            sb.Append(attractDirection);
        }

        sb.Append(')');

        return sb.ToString();
    }
}
