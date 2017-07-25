/// <summary>
/// 继承以获得Singleton功能，T类型必须为自己
/// </summary>
public abstract class Singleton<T> where T : Singleton<T>, new()
{
    public static T instance
    {
        get
        {
            if (_instance == null)
                _instance = new T();
            return _instance;
        }
    }
    static T _instance;
}