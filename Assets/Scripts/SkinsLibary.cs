using System.Collections.Generic;
using UnityEngine;

public class SkinsLibary : MonoBehaviour
{
    [SerializeField] private List<Sprite> _skins;

    public Sprite GetSkin(int index) => _skins[index];
}