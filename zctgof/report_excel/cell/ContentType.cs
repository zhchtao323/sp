using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ZCT.Data
{
  /// <summary>
    /// The cell content type
    /// </summary>
    public enum ContentType
    {
        /// <summary>
        /// Cell does not contain anything
        /// </summary>
        None,
        /// <summary>
        /// Cell contains a string
        /// </summary>
        String,
        /// <summary>
        /// Cell contains a number
        /// </summary>
        Number,
        /// <summary>
        /// Cell contains a DateTime value
        /// </summary>
        DateTime,
        /// <summary>
        /// Cell contains a bool value
        /// </summary>
        Boolean,
        /// <summary>
        /// Cell contains a formula
        /// </summary>
        Formula,
        /// <summary>
        /// Cell contains a formula which cannot be resolved
        /// </summary>
        UnresolvedValue
    }
}
