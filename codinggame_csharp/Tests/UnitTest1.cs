using System;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var s = ArrayTools.join(new int[] { 1, 2, 3, 4, 5, 6 });
            Assert.Equal("1,2,3,4,5,6", s);
        }
    }
}
