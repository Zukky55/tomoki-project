using UnityEngine;

namespace VRShooting
{
    /// <summary>
    /// Mono singleton.
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {

        /// <summary>
        /// The instance.
        /// </summary>
        protected static T m_instance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (m_instance != null) // m_instanceが存在するならそれを返す.
                {
                    return m_instance;
                }

                System.Type type = typeof(T); // Tのタイプ型

                T instance = FindObjectOfType(type) as T; // typeの型と一致するオブジェクトを検索してTに型変換して代入

                if (instance == null) // instanceがnullなら
                {
                    string typeName = type.ToString(); // typeの文字列

                    GameObject gameObject = new GameObject(typeName, type); // typeNameという名のゲームオブジェクトを生成してtypeという名のComponentをアタッチする
                    instance = gameObject.GetComponent<T>(); // 先程作ったオブジェクトのコンポーネントを参照

                    if (instance == null) // 生成に失敗したらエラーを返す
                    {
                        Debug.LogError("Problem during the creation of " + typeName, gameObject);
                    }
                }
                Initialize(instance);
                return m_instance;
            }
        }


        /// <summary>
        /// Initialize the specified instance.
        /// </summary>
        /// <param name="instance">Instance.</param>
        static void Initialize(T instance)
        {
            if (m_instance == null) // m_instanceがnullなら
            {
                m_instance = instance; // 代入

                m_instance.OnInitialize(); // 任意の処理を呼ぶ
            }
            else if (m_instance != instance) // 既にinstanceが他で作られていた場合
            {
                DestroyImmediate(instance.gameObject); // DestoryImmediateで完全に削除(UnityはDestroyを推奨してるみたいだけど、Destroyだと消した後も検索に引っかかってしまうらしい)
            }
        }

        /// <summary>
        /// Destroy the specified instance.
        /// </summary>
        /// <param name="instance">Instance.</param>
        static void Destroyed(T instance)
        {
            if (m_instance == instance) // 自分の意思で作ったオブジェクトを削除したなら
            {
                m_instance.OnFinalize(); // 任意の処理を呼ぶ

                m_instance = null; // nullに戻す
            }
        }

        /// <summary>
        /// instantiateした時に呼ばれる
        /// 継承先で任意の処理を書く
        /// Called when the instantiated.
        /// </summary>
        public virtual void OnInitialize() { }

        /// <summary>
        /// destroyした時に呼ばれる
        /// 継承先で任意の処理を書く
        /// Called when the Destoryed.
        /// </summary>
        public virtual void OnFinalize() { }

        /// <summary>
        /// Awake
        /// </summary>
        void Awake()
        {
            Initialize(this as T);
        }

        /// <summary>
        /// instanceを破棄した時に呼ばれる
        /// Called when destroying the instance
        /// </summary>
        void OnDestroy()
        {
            Destroyed(this as T);
        }

        /// <summary>
        /// Called when the application ends
        /// </summary>
        void OnApplicationQuit()
        {
            Destroyed(this as T);
        }
    }
}
