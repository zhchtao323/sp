using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Collections;

namespace ZCT.Pattern
{
    /// <summary>
    /// 反射工厂
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Factory<T>
    {        

        /// <summary>
        /// 取满足接口的所有类型字典
        /// </summary>
        /// <param name="classname"></param>
        /// <param name="path"></param>
        public Dictionary<string, System.Type> GetInstance(string classname, string path,string inetrfaceName)
        {
            Dictionary<string,System.Type> objs = new Dictionary<string, System.Type>();
            foreach (string filename in Directory.GetFiles(path, "zct.*.dll"))
            {
                Assembly asm = null;
                try
                {
                    asm = Assembly.Load(filename);
                }
                catch { }
                { }
                if (asm != null)
                {
                    try
                    { 
                        foreach(Type type in asm.GetTypes())
                        {
                            if (type.IsClass && !type.IsAbstract && type.GetInterface("inetrfaceName") != null)
                            {
                                ConstructorInfo ci = type.GetConstructor(System.Type.EmptyTypes);
                                T t = (T)ci.Invoke(null);
                                objs.Add(type.Name,type);//name 必须唯一
                            }
                        }
                    }
                    catch{}
                }
            }
            return objs;
        }
        /// <summary>
        /// 取满足接口的所有实例字典
        /// </summary>
        /// <param name="classname"></param>
        /// <param name="path"></param>
        public Dictionary<string, T> GetInstances(string classname, string path, string inetrfaceName)
        {
            Dictionary<string, T> objs = new Dictionary<string, T>();
            foreach (string filename in Directory.GetFiles(path, "zct.*.dll"))
            {
                Assembly asm = null;
                try
                {
                    asm = Assembly.Load(filename);
                }
                catch { }
                { }
                if (asm != null)
                {
                    try
                    {
                        foreach (Type type in asm.GetTypes())
                        {
                            if (type.IsClass && !type.IsAbstract && type.GetInterface("inetrfaceName") != null)
                            {
                                ConstructorInfo ci = type.GetConstructor(System.Type.EmptyTypes);
                                T t = (T)ci.Invoke(null);
                                objs.Add(type.Name, t);//name 必须唯一
                            }
                        }
                    }
                    catch { }
                }
            }
            return objs;
        }
        /// <summary>
        /// 取该类名的实例
        /// </summary>
        /// <param name="classname"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T GetInstanceOne(string classname, string path, string inetrfaceName)
        {
            // Dictionary<string, object> objs = new Dictionary<string, object>();
            foreach (string filename in Directory.GetFiles(path, "zct.*.dll"))
            {
                Assembly asm = null;
                try
                {
                    asm = Assembly.Load(filename);
                }
                catch { }
                { }
                if (asm != null)
                {
                    try
                    {
                        Type type = asm.GetType(classname,true);
                        if (type.IsClass && !type.IsAbstract && type.GetInterface("inetrfaceName") != null)
                        {
                            ConstructorInfo ci = type.GetConstructor(System.Type.EmptyTypes);
                            T t = (T)ci.Invoke(null);
                            return t;
                        }                    
                    }
                    catch { }
                }
            }
            throw new Exception("没有该类");
        }
    }
}
