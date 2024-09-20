using UnityEngine;
using UnityEngine.UI;

public class InventoryElementUI : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void Initialize(Ingredient item)
    {
        _image.sprite = item.Sprite;
    }
}
