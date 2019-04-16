//=====================================================================================
// ���ߣ���ʮ�Ļ���
// Email��mailto:esshs@tom.com
// Blog��http://esshs.cnblogs.com
//=====================================================================================

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;

namespace ZCT.Data
{
	/// <summary>
	/// Provider������Ϣ�ࣨProvider���������ṩ�ߣ��ö����ʵ������һ��Provider��������Ϣ���ϣ��磺���ݿ����Provider��
	/// </summary>
	public class ProviderConfiguration
	{
		private Hashtable _Providers = new Hashtable();
		private string _DefaultProvider;

		/// <summary>
		/// Ĭ��Provider����
		/// </summary>
		public string DefaultProvider
		{
			get
			{
				return _DefaultProvider;
			}
		}

		/// <summary>
		/// ȫ��Providers��Hashtable
		/// </summary>
		public Hashtable Providers
		{
			get
			{
				return _Providers;
			}
		}

		/// <summary>
		/// ��ȡָ��Provider��������Ϣ
		/// </summary>
		/// <param name="strProvider"></param>
		/// <returns></returns>
		public static ProviderConfiguration GetProviderConfiguration(string strProvider)
		{
			//GetConfig���������û��������ýڵ�����ݣ����ø÷���ʱ�ᴥ��ʵ��IConfigurationSectionHandler�ӿڵ���
			return (ProviderConfiguration)ConfigurationSettings.GetConfig("esshs/"+strProvider);
		}

		/// <summary>
		/// ��ȡָ�����ýڵ������
		/// </summary>
		/// <param name="node"></param>
		internal void LoadValuesFromConfigurationXml(XmlNode node)
		{
			XmlAttributeCollection attributeCollection = node.Attributes;
			// ��ȡ��Ӧ�ڵ�Ĭ�ϵ�provider
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
		/// ��ȡָ�������provider�����пɹ�ʹ�õ�provider
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
	/// Provider��
	/// </summary>
	public class Provider
	{
		private string _ProviderName;
		private string _ProviderType;
		private NameValueCollection _ProviderAttributes = new NameValueCollection();

		/// <summary>
		/// ���캯��
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

		#region ����

		/// <summary>
		/// Provider����
		/// </summary>
		public string Name
		{
			get
			{
				return _ProviderName;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public string Type
		{
			get
			{
				return _ProviderType;
			}
		}

		/// <summary>
		/// ���Լ���
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
	/// ʵ��IConfigurationSectionHandler�ӿڣ�����ɲμ�MSDN�С������µ����ýڡ�һ�ĵ����ݣ�
	/// </summary>
	internal class ProviderConfigurationHandler : IConfigurationSectionHandler
	{
		#region IConfigurationSectionHandler ��Ա

		public object Create(object parent, object configContext, XmlNode section)
		{
			ProviderConfiguration objProviderConfiguration = new ProviderConfiguration();
			objProviderConfiguration.LoadValuesFromConfigurationXml(section);
			return objProviderConfiguration;
		}

		#endregion

	}

}
