using Item.Instance;

namespace Inventory
{
    /// <summary>
    /// UIのプロバイダー（UIから情報を 渡す/受け取る）
    /// </summary>
    public interface IInventoryUIProvider 
    {
        // インターフェースはプロパティと抽象メソッドを保持できる。

        //TODO : 下記を実装
        //[o]インベントリの中身を全渡しししてくれるメソッド
        //[x]インベントリの特定の場所を置き換える
        //[x]軽いトランザクション処理（マネージャー側）
        
        //TODO:インベントリのマネージャーを作って、そこが全部管理、プロバイダーは更新を受け入れるだけ。
        
        //TODO:
        // アイテム管理	アイテムの追加・削除
        // スタック	スタック数の変更・上限管理
        // 移動	スロット間の移動
        // 整理	スタックの結合・分割
         
        /// <summary>
        /// [インターフェースにより実装]
        /// インベントリの中身をGetします。
        /// </summary>
        /// <returns>インベントリの配列</returns>
        public ItemInstance[] GetAllInventoryItems();
        
        /// <summary>
        /// [インターフェースにより実装]
        /// インベントリの指定indexの値を置き換えます。
        /// </summary>
        /// <param name="item">アイテムインスタンス</param>
        /// <param name="index">書き換える場所</param>
        /// <returns>成功したか</returns>
        public bool SetItem(ItemInstance item, int index);
        
    }
}