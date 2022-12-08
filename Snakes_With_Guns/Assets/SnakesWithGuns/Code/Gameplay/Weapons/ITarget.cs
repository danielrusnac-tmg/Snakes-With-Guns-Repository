using System;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Weapons
{
    public interface ITarget
    {
        int SourceID { get; }
        event Action<ITarget> Died;
        Vector3 Position { get; }   
    }
}