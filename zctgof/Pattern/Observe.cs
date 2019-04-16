using System;
using System.Collections.Generic;
using System.Text;

namespace ZCT.Pattern
{
    /// <summary>
    /// 观察者模式是主题=被观察对象去通知所有观察者 ： 数据分析中使用观察着
    /// </summary>
    public  class  subject
    {
        public delegate void Send(int i, string b);
        public event Send se;
        public  void tr()
        {
            if(se!=null)
            {se(1,"5");}
        }
    }
    public class ob
    {
        public void g(int i,string b)
        {}
    }
    public class client
    {
        private void d()
        {
            subject s = new subject();
            s.se += new subject.Send(s_se);
            ob  o=new ob();
            s.se+=o.g;//增加观察着  要避免循环死锁
            s.se += (new ob()).g;
            s.se -= o.g;//取消观察着
        }

        void s_se(int i, string b)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
