using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace Tests
{
    public class CompressionsTests
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal("a13b5c56", LitchiSolution.betterCompression("a12c56a1b5"));
        }
    }
}
