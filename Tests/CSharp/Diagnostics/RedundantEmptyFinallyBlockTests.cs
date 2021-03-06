using NUnit.Framework;
using RefactoringEssentials.CSharp.Diagnostics;

namespace RefactoringEssentials.Tests.CSharp.Diagnostics
{
    [TestFixture]
    [Ignore("TODO: Issue not ported yet")]
    public class RedundantEmptyFinallyBlockTests : CSharpDiagnosticTestBase
    {
        [Test]
        public void TestRedundantTry()
        {
            Test<RedundantEmptyFinallyBlockAnalyzer>(@"
using System;
class Test
{
	static void Main (string[] args)
	{
		try {
			Console.WriteLine (""1"");
			Console.WriteLine (""2"");
		} finally {
		}
	}
}
", @"
using System;
class Test
{
	static void Main (string[] args)
	{
		Console.WriteLine (""1"");
		Console.WriteLine (""2"");
	}
}
");
        }

        [Test]
        public void TestSimpleCase()
        {
            Test<RedundantEmptyFinallyBlockAnalyzer>(@"
using System;
class Test
{
	static void Main (string[] args)
	{
		try {
			Console.WriteLine (""1"");
			Console.WriteLine (""2"");
		} catch (Exception) {
		} finally {
		}
	}
}
", @"
using System;
class Test
{
	static void Main (string[] args)
	{
		try {
			Console.WriteLine (""1"");
			Console.WriteLine (""2"");
		} catch (Exception) {
		}  
	}
}
");
        }

        [Test]
        public void TestInvalid()
        {
            Analyze<RedundantEmptyFinallyBlockAnalyzer>(@"
using System;
class Test
{
	static void Main(string[] args)
	{
		try {
			Console.WriteLine(""1"");
		} finally {
			Console.WriteLine(""2"");
		}
	}
}
");
        }

        [Test]
        public void TestDisable()
        {
            Analyze<RedundantEmptyFinallyBlockAnalyzer>(@"
using System;
class Test
{
	static void Main(string[] args)
	{
		try {
			Console.WriteLine(""1"");
			Console.WriteLine(""2"");
		}
		// ReSharper disable once RedundantEmptyFinallyBlock
 		finally {
		}
	}
}
");
        }
    }
}

