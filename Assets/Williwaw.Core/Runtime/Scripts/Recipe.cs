using UnityEngine;

[CreateAssetMenu(menuName = "Core/Recipe")]
public class Recipe : ScriptableObject
{
    public string Name => _name;
    public Ingredient[] Ingredients => _ingredients;

    [SerializeField] private string _name;
    [SerializeField] private Ingredient[] _ingredients;
}
