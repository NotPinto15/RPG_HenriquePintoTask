using UnityEngine;

[CreateAssetMenu(menuName = "Core/Ingredient")]
public class Ingredient : ScriptableObject
{
    public Sprite Sprite => _sprite;

    [SerializeField] private Sprite _sprite;
}
