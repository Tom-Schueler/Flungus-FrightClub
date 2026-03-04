using System.Collections.Generic;
using UnityEngine;
using Team3.CustomStructs;

namespace Team3.Weapons
{
    public interface IGunPerk
    {
        abstract void OnShoot();
        abstract void OnUpdate();
        abstract void OnReload();
        
    }
}