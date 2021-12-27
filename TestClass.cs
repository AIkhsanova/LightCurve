using System;
namespace LightCurve
{
    public class TestClass
    {
        public TestClass()
        {
        }

        public virtual void test() {
            Console.WriteLine("Я метод родителя");
        }

    }

    public class TestClass2:TestClass
    {
        public TestClass2()
        {
        }

        public override void test()
        {
            Console.WriteLine("Я метод наследника");
        }

    }
}
