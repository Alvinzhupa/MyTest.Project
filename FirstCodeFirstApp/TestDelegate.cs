using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCodeFirstApp
{
    public delegate string FirstD(int v, int c);

    class TestDelegate
    {
        /// <summary>
        /// 这里 Action<string> 相当于一个类型
        ///  Action<string> actionDo  相当于定义了一方法,但是并没有调用, 
        ///  在别的地方使用actionDo("abc") 才是真的调用该方法
        /// </summary>
        Action<string> actionDo = c =>
        {
            Console.WriteLine(c);
        };

        private PayWay _payWay;

        //泛型类或者泛型方法,只需要把T当作一个已经存在的类来看就行
        /// <summary>
        /// 推理路径:
        /// 外部看:方法doV的参数就是参数类型为PayWay方法委托,思考的是方法内部传入了一个PayWay对象到该委托中处理事情;外部看只有一种目的:就是在该方法doV所有的方法体中有众多流程,然后传一个方法参数action 给doV中执行
        /// 内部看:1.给外部程序一个操作的机会,2.交给外部程序处理该对象 
        /// 整体委托的作用:1.就是为了观察者模式的实现 
        /// 
        /// 得出结论:
        ///从内外看都可以得出委托方法的作用两个:
        ///1.把doV里面的数据传出到外部或者让外部做点啥操作 例如:lsit.ForEach()
        ///2.让外部处理某些事情 例如什么热水器响啊之类的
        ///3.让外部对参数对象做点处理,例如赋值等等 例如Webhost的创建,就是对
        /// </summary>
        /// <param name="action">这里应该理解为:传入一个参数为PayWay方法,</param>
        public Person doV(Action<PayWay> action)
        {
            //在走一个固定的流程,但是该流程不想在本内部操作
            _payWay.Id = 1;//模拟走了N步流程
            _payWay.Id = 2;//模拟走了N步流程
            _payWay.Id = 3;//模拟走了N步流程

            action(_payWay);//给外部方法执行一下,如果委托有返回值,那么就可以操作返回值,没返回值就纯粹让外部看看.

            Person person = new Person();

            person.UserName = _payWay.Name; //当外部操作完的对象,继续执行接下来的程序

            return person; //委托action的返回值和方法doV的返回值没有关系
        }


        public void dv()
        {
            string abc = string.Empty;

            //外部调用:就是想取点数据
            Person person = doV(c =>
              {
                  abc = c.Name;//1.想传出数据
                  Console.WriteLine(abc);//2.想做点啥
                  c.Name = "对内部对象改个名字";//3.对内部对象干点啥的
              });

            Console.WriteLine(person.UserName);
        }

    }
}
