using System;
using UnityEngine;

[Serializable]
public struct Appearance
{
    [SerializeField] private string _name;
    [SerializeField] private int _skin;
    
    public string Name => _name;
    public int Skin => _skin;

    public Appearance(string name, int skin)
    {
        _name = name;
        _skin = skin;
    }
}