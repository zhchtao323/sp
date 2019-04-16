using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ZCT.Data
{
    public class Cell
    {
        object _value = null;
        ContentType Content = ContentType.None;
        int index = -1;
        /// <summary>
        /// 列
        /// </summary>
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        int styleId = -1;
        /// <summary>
        /// 样式字段
        /// </summary>
        public int StyleId
        {
            get { return styleId; }
            set { styleId = value; }
        }
        public int MergeAcross = -1;
        public int MergeDown = -1;
        string formula = "";

        public string Formula
        {
            get { return formula; }
            set { formula = value; }
        }
        public string ExportString(object va)
        {
            Value = va;
            StringBuilder str = new StringBuilder();
            //str.Append("<Cell ss:Index=\"5\" ss:Formula=\"=R[-1]C+1\"><Data ss:Type=\"Number\">10</Data></Cell>");
            str.Append("<Cell ");
            if (MergeAcross != -1)//ss:MergeAcross="8" 
            {
                { str.Append("ss:MergeAcross=\"" + MergeAcross.ToString() + "\" "); }
            }
            if (MergeDown != -1)//ss:MergeAcross="8" 
            {
                { str.Append("ss:MergeDown=\"" + MergeDown.ToString() + "\" "); }
            }
            if (index != -1)
            { str.Append("ss:Index=\"" + index.ToString() + "\" "); }
            if (styleId != -1)// ss:StyleID="s23"
            { str.Append("ss:StyleID=\"s" + styleId.ToString() + "\" "); }
            if (formula != "")//ss:Formula="=SQRT(RC[-1])"
            { str.Append("ss:Formula=\"s" + formula + "\" "); }
            str = str.Append(">");
            //
            GetData(ref str);
            str = str.Append("</Cell>\r\n");
            return str.ToString();
        }
        private void GetData(ref StringBuilder str)
        {
            switch (Content)
            {
                case ContentType.Boolean:
                    {
                        if ((bool)_value)
                            str.Append("<Data ss:Type=\"Boolean\">1</Data>");//<Data ss:Type="Boolean">1</Data>
                        else
                            str.Append("<Data ss:Type=\"Boolean\">0</Data>");
                        break;
                    }
                case ContentType.DateTime:
                    {//<Data ss:Type="DateTime">2009-12-14T13:50:00.000</Data>
                        str.Append("<Data ss:Type=\"DateTime\">" + ((DateTime)_value).ToString("yyyy-MM-dd\\Thh:mm:ss.fff",
                            CultureInfo.InvariantCulture) + "</Data>");
                        //writer.WriteValue();
                        break;
                    }
                case ContentType.Number:
                    {
                        decimal d = Convert.ToDecimal(_value, CultureInfo.InvariantCulture);
                        //writer.WriteValue());
                        str.Append("<Data ss:Type=\"Number\">" + d.ToString(new CultureInfo("en-US")) + "</Data>");
                        break;
                    }
                case ContentType.String:
                    {
                        //writer.WriteValue();
                        str.Append("<Data ss:Type=\"String\">" + (string)_value + "</Data>");
                        break;
                    }
            }
        }

        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                switch (value.GetType().FullName)
                {
                    case "System.DateTime":
                        {
                            _value = value;
                            Content = ContentType.DateTime;
                            break;
                        }
                    case "System.Byte":
                    case "System.SByte":
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.UInt16":
                    case "System.UInt32":
                    case "System.UInt64":
                    case "System.Single":
                    case "System.Double":
                    case "System.Decimal":
                        {
                            _value = value;
                            Content = ContentType.Number;
                            break;
                        }
                    case "System.Boolean":
                        {
                            _value = value;
                            Content = ContentType.Boolean;
                            break;
                        }
                    case "System.String":
                        {
                            _value = value;
                            Content = ContentType.String;
                            break;
                        }
                    default:
                        _value = value;
                        Content = ContentType.String;
                        break;
                }
            }
        }
    }
    /// <summary>
    /// The cell content type
    /// </summary>
}
