using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ZCT.Data
{
    public class Row
    { //<Row ss:AutoFitHeight="0" ss:Height="19.5">
        //<Row ss:Index="12">
        int index = -1;
        /// <summary>
        /// 列
        /// </summary>
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        public int Height = -1;

        public string RowBegin()
        {
            StringBuilder str = new StringBuilder();
            str.Append("<Row ");
            if (index != -1)
            { str.Append("ss:Index=\"" + index.ToString() + "\" "); }
            str.Append("ss:AutoFitHeight=\"0\"");
            if (Height != -1)
            { str.Append("ss:Height=\"" + Height.ToString() + "\" "); }
            return str.ToString();
        }
        public string RowEnd()
        {
            return "</Row>";
        }
    }
}
