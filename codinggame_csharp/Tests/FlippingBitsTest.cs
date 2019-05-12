using System;
using System.Collections;
using System.Linq;
using Xunit;

namespace Tests
{
    public class FlippingBitsTests
    {
        [Fact]
        public void ToStringTest()
        {
            var ba = new BitArray(new byte[] { 0, 0, 0, 9 });
            var n_xor = BitArrayUtils.ToString(ba);
            Assert.Equal("00000000000000000000000010010000", n_xor);
        }
        [Fact]
        public void ToStringTest2()
        {
            var n = 9U;
            var n_expected = 4294967286U;
            var i = Convert.ToInt32(n);
            Assert.Equal(i, 9);
            var bytes = BitConverter.GetBytes(i);
            var ba = new BitArray(bytes);
            Assert.Equal("10010000000000000000000000000000", BitArrayUtils.ToString(ba));
            var b1 = new BitArray(Enumerable.Range(0, 32).Select(x => true).ToArray());
            var ba_xor = ba.Xor(b1);
            Assert.Equal("01101111111111111111111111111111", BitArrayUtils.ToString(ba_xor));
            var ba_xor_long = BitArrayUtils.BitArrayToByteArray(ba_xor);
            var n_xor = BitConverter.ToUInt32(ba_xor_long);
            Assert.Equal("0110111111111111111111111111111100000000000000000000000000000000", BitArrayUtils.ToString(n_expected));
            Assert.Equal("0110111111111111111111111111111100000000000000000000000000000000", BitArrayUtils.ToString(n_xor));
            Assert.Equal(n_expected, n_xor);
        }
        [Fact]
        public void test()
        {
            Assert.Equal(4294967286U, FlippingBits.flippingBits(9U));
            Assert.Equal(4294967295U, FlippingBits.flippingBits(0U));
            Assert.Equal(3492223820U, FlippingBits.flippingBits(802743475U));
            Assert.Equal(0U, FlippingBits.flippingBits(4294967295U));
        }
    }
}