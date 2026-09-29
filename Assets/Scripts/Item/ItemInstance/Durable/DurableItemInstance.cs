/// <summary>
/// 耐久値のあるアイテムのインスタンス用
/// </summary>
public class DurableItemInstance : ItemInstance
{
    public int CurrentRemainingAmmunition;

    // : base(引数)で親のコンストラクタも初期化してくれる。
    // DurableItemInstance(string id, int currentRemainingAmmo)で書いた親の引数を : base(id)がそのまま親に渡してる。
    public DurableItemInstance(string id, int currentRemainingAmmo) : base(id)
    {
        this.CurrentRemainingAmmunition = currentRemainingAmmo;
    }
}
