public class ItemMovement : RandomDownMovement
{
    private ItemCtrl _itemCtrl;

    public void Initialize(ItemCtrl itemCtrl)
    {
        _itemCtrl = itemCtrl;
    }

    protected override void Start()
    {

    }

    public void SetSpeedAndRotation(float speed, float rotation)
    {
        maxRandomSpeed = speed;
        maxRandomRotationSpeed = rotation;
    }
}