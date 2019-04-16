//=====================================================================================
// 作者：二十四画生
// Email：mailto:esshs@tom.com
// Blog：http://esshs.cnblogs.com
//=====================================================================================

using System;


namespace ZCT.Pattern
{
	/// <summary>
	/// 利用反射动态的创建对象
	/// </summary>
	public class Reflection
	{

		/// <summary>
		/// 根据Provider类型创建对象实例
		/// </summary>
		/// <param name="ObjectProviderType"></param>
		/// <returns></returns>
        //public static object CreateObject(string ObjectProviderType)
        //{
        //    return CreateObject(ObjectProviderType, "", "");
        //}

		/// <summary>
		/// 根据Provider类型和指定的命名空间和程序集名称创建对象实例
		/// </summary>
		/// <param name="ObjectProviderType"></param>
		/// <param name="ObjectNamespace"></param>
		/// <param name="ObjectAssemblyName"></param>
		/// <returns></returns>
        //public static object CreateObject(string ObjectProviderType , string ObjectNamespace, string ObjectAssemblyName)
        //{
        //    string TypeName = "";
        //    string CacheKey = "";
        //    ProviderConfiguration objProviderConfiguration = ProviderConfiguration.GetProviderConfiguration(ObjectProviderType);
        //    if (ObjectNamespace != "" && ObjectAssemblyName != "")
        //    {
        //        TypeName = ObjectNamespace + "." + objProviderConfiguration.DefaultProvider + ", " + ObjectAssemblyName + "." + objProviderConfiguration.DefaultProvider;
        //        CacheKey = ObjectNamespace + "." + ObjectProviderType + "provider";
        //    }
        //    else
        //    {
        //        TypeName = ((Provider)objProviderConfiguration.Providers[objProviderConfiguration.DefaultProvider]).Type;
        //        CacheKey = ObjectProviderType + "provider";
        //    }
        //    return CreateObject(TypeName, CacheKey);
        //}

		/// <summary>
		/// 利用反射动态构建指定TypeName的对象实例，并将创建的对象存到缓存中（利用缓存可减轻由于反射引起的性能损耗）
		/// </summary>
		/// <param name="TypeName"></param>
		/// <param name="CacheKey"></param>
		/// <returns></returns>
        //public static object CreateObject(string TypeName, string CacheKey)
        //{
        //    if (CacheKey == "")
        //    {
        //        CacheKey = TypeName;
        //    }

        //    // 先从缓存中获取该类型
        //    Type objType = (Type)DataCache.GetCache(CacheKey);

        //    if (objType == null)
        //    {
        //        try
        //        {
        //            objType = Type.GetType(TypeName, true);
        //            // 写入缓存
        //            DataCache.SetCache(CacheKey, objType);
        //        }
        //        catch
        //        {
        //            // 记录错误日志
        //        }
        //    }

        //    // 使用与指定参数匹配程度最高的构造函数来创建指定类型的实例
        //    return Activator.CreateInstance(objType);
        //}
	}
}
