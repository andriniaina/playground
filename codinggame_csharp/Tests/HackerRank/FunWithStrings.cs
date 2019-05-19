using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace Tests
{
    public class FunWithStringsTests
    {
        [Fact]
        public void TestZ()
        {
            Assert.Equal(76428576, FunWithStrings.countPerms(10000));
        }
        [Fact]
        public void Test1()
        {
            Assert.Equal(5, FunWithStrings.countPerms(1));
        }
        [Fact]
        public void Test2()
        {
            Assert.Equal(10, FunWithStrings.countPerms(2));
        }
        [Fact]
        public void Test3()
        {
            Assert.Equal(19, FunWithStrings.countPerms(3));
        }
    }
}
