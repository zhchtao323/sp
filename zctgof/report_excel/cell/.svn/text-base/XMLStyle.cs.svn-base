using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ZCT.Data
{
    public class xmlStyle
    {
        //       <Style ss:ID="m21470784">
        // <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
        // <Borders>
        //  <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
        //  <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="2"/>
        //  <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="2"/>
        // </Borders>
        // <Font ss:FontName="宋体" x:CharSet="134" ss:Size="11"/>
        //</Style>
        //      <Style ss:ID="s27">
        // <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
        // <Borders>
        //  <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
        //  <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
        //  <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
        //  <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="2"/>
        // </Borders>
        // <Font ss:FontName="宋体" x:CharSet="134" ss:Size="11"/>
        //</Style>
        DisplayFormatType d;
        string numberformat = "";
        string fontString = "";
        private string QtString = "";
        public string ExportStyle(int id)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<Style ss:ID=\"s" + id.ToString() + "\">");
            if (numberformat != "")//<NumberFormat ss:Format="AM/PMh&quot;时&quot;mm&quot;分&quot;ss&quot;秒&quot;"/>
            { str.Append("<NumberFormat ss:Format=\"" + numberformat + "\"/>"); }
            if (fontString != "") //<Font ss:FontName="宋体" x:CharSet="134" ss:Size="12" ss:Color="#00FFFF"    ss:Underline="Double"/>
            { str.Append("<Font " + fontString + " />"); }
            if (QtString != "")
            { str.Append(QtString); }
            str.Append("</Style>");
            return str.ToString();
        }
        /// <summary>
        /// Xml文件增加样式
        /// </summary>
        /// <param name="str_xml"></param>
        /// <returns></returns>
        public static string AddStyle(string str_xml, string style_str)
        {
            int begin = str_xml.IndexOf(@"</Styles>") - 1;
            str_xml = str_xml.Insert(begin, style_str);
            return str_xml;
        }
    }
  
}
