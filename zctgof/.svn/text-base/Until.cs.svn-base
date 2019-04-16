using System;
using System.Collections.Generic;
using System.Text;

namespace ZCT
{
    class Until
    {
        // IEEE754规定了如下的几种四舍五入的标准，分别举例说明其意义： 
        //“away from zero” （中间值时，远离零，即取绝对值最大） 例：3.215 －> 3.22，-3.215 －> -3.22 
        //“towards zero” （中间值时，接近零，即绝对值最小） 例：3.215 －> 3.21，-3.215 －> -3.21 
        //“to even” （中间值时，接近相邻的偶数） 例：3.215 －> 3.22，3.245 －> 3.24 
        //“towards positive infinity”（中间值时，向正无穷大方向接近） 例：3.215 －> 3.22，-3.215 －> -3.21 
        //“towards negative infinity” （中间值时，向负无穷大方向接近） 例：3.215 －> 3.21，-3.215 －> -3.22 
        //我们常规意义上的“四舍五入”是“away from zero”的方式。 
        //在Ｃ#中，Math.Round (Decimal)采用的是四舍六入五成双，即 “to even” ，要想控制舍入类型，需要调用Math.Round(Decimal, MidpointRounding)，其中MidpointRounding枚举只有两种，请参考：MSDN-MidpointRounding 枚举，其实MSDN中下面的解释的错的，真想不到，ＭＳ会出现这样的错误，误导的后果一定很严重。 
        public void round()
        {
            Math.Round(2.345, 2, MidpointRounding.AwayFromZero);
            Math.Round(2.345, 2, MidpointRounding.ToEven);
        }
    }
}
