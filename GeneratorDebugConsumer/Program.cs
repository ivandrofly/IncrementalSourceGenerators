﻿#pragma warning disable CA1822 // Mark members as static

// https://www.youtube.com/watch?v=yn3S3ClmA_I&ab_channel=Stefan%27sTechLog
namespace GeneratorDebugConsumer
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, World!");
            using (var foobar = new Foobar())
            {
                Console.WriteLine("in scope");
                foobar.NotCollected();
                foobar.GetNumberOfInterfaces();
            }
            Console.WriteLine("out of scope");

            var bob = new Bob();
            bob.NotCollected();
            bob.GetNumberOfInterfaces();

            var alice = new Alice();
            alice.NotCollected();
            alice.GetNumberOfInterfaces();

            ClassHelper.GetAllClasses();
            ElementHelper.AllDistinctElements();
            bob.PrintText();
            MyOptionsHelper.AllErrors();
            MyOptionsHelper.AllFeatures();
            MyOptionsHelper.AllPreprocessorSymbols();
            MyOptionsHelper.Language();

            MetadataReferenceHelper.AllReferences();
            Console.WriteLine(bob.GetAsSerialized());
        }
    }

    //public partial class Juicebox { }
    //public partial class Kilohertz { }
    //public partial class Lemontree { }


    public interface IInterface1 { }
    public interface IInterface2 { }
    public interface IInterface3 { }


    public partial class Betty { }

    [Serializable]
    public partial class Bob : IInterface1
    {
        public int Age { get; set; } = 31;
        public string HairColor { get; set; } = "brown";
        public bool IsMarried { get; set; } = false;
    }
    public partial class Alice : IInterface1, IInterface2 { }

    public partial class Foobar : IInterface1, IInterface2, IInterface3
    {
        [OnDispose(2)]
        public void Free2()
        {
            Console.WriteLine("Free2");
        }

        [OnDispose(1)]
        public void Free1()
        {
            Console.WriteLine("Free1");
        }
        [OnDispose]
        public void Free3()
        {
            Console.WriteLine("Free3");
        }

        [OnDisposeAttribute(CallOrder = 0)]
        public void Free0()
        {
            Console.WriteLine("Free0");
        }

        public void NotFree()
        {
            Console.WriteLine("This will not be called");
        }
    }
}
#pragma warning restore CA1822 // Mark members as static