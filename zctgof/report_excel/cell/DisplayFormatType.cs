using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ZCT.Data
{
    /// <summary>
    /// The default display format of the cell in excel
    /// </summary>
    public enum DisplayFormatType
    {
        /// <summary>
        /// General format
        /// </summary>
        None,
        /// <summary>
        /// Displays anything as text (i.e. Left aligned without formatting)
        /// </summary>
        Text,
        /// <summary>
        /// Displays numeric values with two fixed decimals
        /// </summary>
        Fixed,
        /// <summary>
        /// Displays numeric values with two fixed decimals and digit grouping
        /// </summary>
        Standard,
        /// <summary>
        /// Displays numeric values as percentage values
        /// </summary>
        Percent,
        /// <summary>
        /// Displays numeric values in scientific notation
        /// </summary>
        Scientific,
        /// <summary>
        /// Displays numeric or date values as formatted date values
        /// </summary>
        GeneralDate,
        /// <summary>
        /// Displays numeric or date values as short date format
        /// </summary>
        ShortDate,
        /// <summary>
        /// Displays numeric or date values as long date format
        /// </summary>
        LongDate,
        /// <summary>
        /// Displays numeric or date values in time format
        /// </summary>
        Time,
        /// <summary>
        /// Custom defined format
        /// </summary>
        Custom
    }
  }
