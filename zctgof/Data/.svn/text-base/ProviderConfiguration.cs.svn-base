//=====================================================================================
// 作者：二十四画生
// Email：mailto:esshs@tom.com
// Blog：http://esshs.cnblogs.com
//=====================================================================================

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;

namespace ZCT.Data
{
	/// <summary>
	/// Provider配置信息类（Provider：可理解成提供者；该对象的实例，是一类Provider的配置信息集合，如：数据库访问Provider）
	/// </summary>
	public class ProviderConfiguration
	{
		private Hashtable _Providers = new Hashtable();
		private string _DefaultProvider;

		/// <summary>
		/// 默认Provider名称
		/// </summary>
		public string DefaultProvider
		{
			get
			{
				return _DefaultProvider;
			}
		}

		/// <summary>
		/// 全部Providers的Hashtable
		/// </summary>
		public Hashtable Providers
		{
			get
			{
				return _Providers;
			}
		}

		/// <summary>
		/// 获取指定Provider的配置信息
		/// </summary>
		/// <param name="strProvider"></param>
		/// <returns></returns>
		public static ProviderConfiguration GetProviderConfiguration(string strProvider)
		{
			//GetConfig用来调用用户定义配置节点的内容，调用该发放时会触发实现IConfigurationSectionHandler接口的类
			return (ProviderConfiguration)ConfigurationSettings.GetConfig("esshs/"+strProvider);
		}

		/// <summary>
		/// 读取指定配置节点的内容
		/// </summary>
		/// <param name="node"></param>
		internal void LoadValuesFromConfigurationXml(XmlNode node)
		{
			XmlAttributeCollection attributeCollection = node.Attributes;
			// 获取相应节点默认的provider
			_DefaultProvider = attributeCollection["defaultProvider"].Value;

			foreach(XmlNode child in node.ChildNodes)
			{
				if(child.Name == "providers")
				{
					GetProviders(child);
				}
			}
		}

		/// <summary>
		/// 获取指定类类别provider中所有可供使用的provider
		/// </summary>
		/// <param name="node"></param>
		internal void GetProviders(XmlNode node)
		{
			foreach(XmlNode Provider in node.ChildNodes)
			{
				switch(Provider.Name)
				{
					case "add":
						Providers.Add(Provider.Attributes["name"].Value, new Provider(Provider.Attributes));
						break;
					case "remove":
						Providers.Remove(Provider.Attributes["name"].Value);
						break;
					case "clear":
						Providers.Clear();
						break;
				}
			}
		}
	}

	/// <summary>
	/// Provider类
	/// </summary>
	public class Provider
	{
		private string _ProviderName;
		private string _ProviderType;
		private NameValueCollection _ProviderAttributes = new NameValueCollection();

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="Attributes"></param>
		public Provider(XmlAttributeCollection Attributes)
		{
			_ProviderName = Attributes["name"].Value;
			_ProviderType = Attributes["type"].Value;

			foreach(XmlAttribute Attribute in Attributes)
			{
				if (Attribute.Name != "name" && Attribute.Name != "type")
				{
					_ProviderAttributes.Add(Attribute.Name, Attribute.Value);
				}
			}
		}

		#region 属性

		/// <summary>
		/// Provider名称
		/// </summary>
		public string Name
		{
			get
			{
				return _ProviderName;
			}
		}

		/// <summary>
		/// 类型名称
		/// </summary>
		public string Type
		{
			get
			{
				return _ProviderType;
			}
		}

		/// <summary>
		/// 属性集合
		/// </summary>
		public NameValueCollection Attributes
		{
			get
			{
				return _ProviderAttributes;
			}
		}

		#endregion
	}

	/// <summary>
	/// 实现IConfigurationSectionHandler接口（具体可参见MSDN中“创建新的配置节”一文的内容）
	/// </summary>
	internal class ProviderConfigurationHandler : IConfigurationSectionHandler
	{
		#region IConfigurationSectionHandler 成员

		public object Create(object parent, object configContext, XmlNode section)
		{
			ProviderConfiguration objProviderConfiguration = new ProviderConfiguration();
			objProviderConfiguration.LoadValuesFromConfigurationXml(section);
			return objProviderConfiguration;
		}

		#endregion

	}

}
