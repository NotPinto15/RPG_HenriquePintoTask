using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryElementUI _elementPrefab;
    [SerializeField] private RectTransform _elementsParent;

    public void Open(IEnumerable<Ingredient> items)
    {
        this.gameObject.SetActive(true);

        foreach (var item in items)
        {
            var elementInstance = Instantiate(_elementPrefab, _elementsParent);
            elementInstance.Initialize(item);
        }
    }
}
