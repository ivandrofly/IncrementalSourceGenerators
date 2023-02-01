﻿using GeneratorLibrary;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Diagnostics;
using Xunit;

namespace GeneratorDebugging
{
    public class MyGeneratorTests
    {

        [Fact]
        public void DebugMarkerGenerator()
        {
            var markerGenerator = new MarkerGenerator();
            var result = GeneratorDebugger.RunDebugging(Array.Empty<SyntaxTree>(), new[] { markerGenerator });
            Debug.WriteLine(result.GeneratedTrees.Count());
        }

        [Fact]
        public void DebugOnDisposeGenerator()
        {
            var markerGenerator = new MarkerGenerator();
            var disposableGenerator = new OnDisposeGenerator();
            var ProgramCode = CSharpSyntaxTree.ParseText(@"
namespace GeneratorDebugConsumer
{
    public partial class Foobar
    {
        [OnDispose( 1 )]
        public void Free1()
        {
            Console.WriteLine(""Free1"");
        }

        [OnDispose]
        public void Free3()
        {
            Console.WriteLine(""Free3"");
        }

        [OnDisposeAttribute(CallOrder = 2)]
        public void Free2()
        {
            Console.WriteLine(""Free2"");
        }

    }
}

");
            //var result = GeneratorDebugger.RunDebugging(new[] { ProgramCode }, new IIncrementalGenerator[] { disposableGenerator });
            var result = GeneratorDebugger.RunDebugging(new[] { ProgramCode }, new IIncrementalGenerator[] { markerGenerator, disposableGenerator });
            Debug.WriteLine(result.GeneratedTrees.Count());
        }

        [Fact]
        public void CollectVsNotCollectGenerator()
        {
            var generator = new CollectVsNotCollectGenerator();
            var ProgramCode = CSharpSyntaxTree.ParseText(@"
namespace GeneratorDebugConsumer
{
    public partial class Foobar
    {

    }

    public partial class Bob
    {

    }

    public partial class Alice
    {

    }
}

");

            var result = GeneratorDebugger.RunDebugging(new[] { ProgramCode }, new IIncrementalGenerator[] { generator });
            Debug.WriteLine(result.GeneratedTrees.Count());
        }

        [Fact]
        public void SelectWhereGenerator()
        {
            var generator = new SelectAndWhereGenerator();
            var ProgramCode = CSharpSyntaxTree.ParseText(@"
namespace Tutorial
{
    public partial class Foobar
    {

    }

    public static class Bob
    {

    }

    public class Alice
    {

    }

    public abstract class Eric
    {

    }

    public partial class Betty
    {

    }
}

");

            var result = GeneratorDebugger.RunDebugging(new[] { ProgramCode }, new IIncrementalGenerator[] { generator });
            Debug.WriteLine(result.GeneratedTrees.Count());
        }

        [Fact]
        public void WithComparerGenerator()
        {
            var generator = new WithComparerGenerator();
            var ProgramCode = CSharpSyntaxTree.ParseText(@"
namespace GeneratorDebugConsumer
{
    public partial class Foobar : IInterfaceA
    {

    }

    public partial class Barbar : IInterfaceA, IInterfaceB, IInterfaceC
    {

    }

    public interface IInterfaceA
    {

    }

    public interface IInterfaceB
    {

    }

    public interface IInterfaceC
    {

    }
}

");

            var result = GeneratorDebugger.RunDebugging(new[] { ProgramCode }, new IIncrementalGenerator[] { generator });
            Debug.WriteLine(result.GeneratedTrees.Count());
        }
    }
}
