using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : GameMonoBehaviour
{
    [SerializeField] private string _itemId;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _amount;
    public string ItemId => _itemId;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAmount();
        LoadImage();
        SetDefault();
    }

    private void LoadImage()
    {
        if (_image != null) return;
        _image = GetComponentInChildren<Image>();
        Debug.Log("LoadImage", gameObject);
    }

    private void LoadAmount()
    {
        if (_amount != null) return;
        _amount = GetComponentInChildren<TextMeshProUGUI>();
        _amount.fontStyle = FontStyles.Bold;
        _amount.alignment = TextAlignmentOptions.MidlineRight;
        _amount.fontSize = 25;
        Debug.Log("LoadAmount", gameObject);
    }

    #endregion

    public void SetAmount(int value)
    {
        _amount.gameObject.SetActive(value >= 2);
        _amount.text = value.ToString();
    }

    public void SetImage(Sprite sprite)
    {
        _image.gameObject.SetActive(sprite != null);
        _image.sprite = sprite;
    }

    public void SetItemId(string itemId) =>
        _itemId = itemId;

    public void SetDefault()
    {
        SetItemId(string.Empty);
        SetImage(null);
        SetAmount(0);
    }

    public bool CheckEmptyItem() => _image.sprite == null;

}
