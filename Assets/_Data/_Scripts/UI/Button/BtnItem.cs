using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnItem : BtnAbstract
{
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private Image _image;
    [SerializeField] private string _itemId;

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

    public void SetAmount(int value) =>
        _amount.text = value.ToString();

    public void SetImage(Sprite sprite) => 
        _image.sprite = sprite;

    public void SetItemId(string itemId) =>
        _itemId = itemId;

    protected override void OnClick()
    {
        Debug.Log("BtnItem Clicked");
    }
}
