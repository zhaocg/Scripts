using UnityEngine;
using System.Collections;
/// <summary>
/// 从下面这个MonoSingletion类派生的所有类，将自动获得单件功能
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MonoSingletion<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// 创建一个MonoSingletionRoot的空物体
    /// </summary>
    private static string rootName = "MonoSingletionRoot";
    private static GameObject monoSingletionRoot;

    private static T instance;
    public static T Instance
    {
        get
        {
            if (monoSingletionRoot == null)
            {
                monoSingletionRoot = GameObject.Find(rootName);
                if (monoSingletionRoot == null) Debug.Log("please create a gameobject named " + rootName);
            }
            if (instance == null)
            {
                instance = monoSingletionRoot.GetComponent<T>();
                if (instance == null) instance = monoSingletionRoot.AddComponent<T>();
            }
            return instance;
        }
    }  
}
