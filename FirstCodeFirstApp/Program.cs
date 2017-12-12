using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCodeFirstApp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var init = new Initializer();
    //        //init.InitializeDatabase(new Context());
    //        //Database.SetInitializer();

    //        using (var db = new Context())
    //        {

    //            var province = db.Provinces.FirstOrDefault(c => c.Id == 1);
    //            foreach (var item in province.Donators)
    //            {
    //                Console.WriteLine(item.Name);
    //            }

    //        }
    //        Console.WriteLine("DB create");
    //        //Console.Read();
    //    }
    //}

    class Program
    {
        static void Main(string[] args)
        {

            TestDelegate.doV(c => c.Id == 1);

            //.WriteLine("DB create");
            Console.Read();
        }
    }
}
