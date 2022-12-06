using System;
using UnityEngine;

namespace SnakesWithGuns.Utilities.Damage
{
    public interface ITarget
    {
        event Action<ITarget> Died;
        Vector3 Position { get; }   
    }
}