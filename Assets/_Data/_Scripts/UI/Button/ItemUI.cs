using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : BtnAbstract
{
    [SerializeField] private int _index;
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
    }

    private void LoadImage()
    {
        if (_image != null) return;
        _image = transform.Find("Image").GetComponent<Image>();
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

    public void SetIndex(int index) =>
        _index = index;

    public void SetAmount(int value)
    {
        _amount.enabled = value >= 2;
        _amount.text = value.ToString();
    }

    public void SetImage(Sprite sprite)
    {
        _image.enabled = sprite != null;
        _image.sprite = sprite;
    }

    public void SetItemId(string itemId) =>
        _itemId = itemId;

    public void SetDefaultBtn()
    {
        SetItemId(string.Empty);
        SetImage(null);
        SetAmount(0);
    }

    public bool IsEmptyBtn() => !_image.enabled;

    protected override void OnClick()
    {
        Debug.Log("btnItem clicked: " + _itemId);
    }
}
