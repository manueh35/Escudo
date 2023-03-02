using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new color", menuName = "color")]
public class ScriptableColor : ScriptableObject
{
    [SerializeField] private Color _color;

    public Color myColor { get { return _color; } }
}
