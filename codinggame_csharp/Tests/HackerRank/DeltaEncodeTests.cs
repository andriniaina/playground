using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class DeltaEncodeTests
    {
        [Fact]
        public void Test2()
        {
            var nb =
            CustomerCapa.howManyAgentsToAdd(3, new List<List<int>>
            {
                new List<int>{1481122000, 1481122020},
                new List<int>{1481122000, 1481122040},
                new List<int>{1481122030, 1481122035},
            });

            Assert.Equal(1, nb);
        }
        [Fact]
        public void Test1()
        {
            Assert.Empty(DeltaEncode.delta_encode(new List<int> { }));
            Assert.Equal(new List<int> { 1 }, DeltaEncode.delta_encode(new List<int> { 1 }));
            Assert.Equal(new List<int> { 25626, -128, 131, 1, -128, -200, -127, 127 }, DeltaEncode.delta_encode(new List<int> { 25626, 25757, 25758, 25758 - 200, 25758 - 200 - 127, 25758 - 200 }));
        }
    }
}