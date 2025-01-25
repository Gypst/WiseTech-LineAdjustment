using NUnit.Framework;

namespace LineAdjustment.Tests
{
    public class LineAdjustmentAlgorithmTests
    {
        [Test]
        [TestCase(null, 5, "")]
        [TestCase("", 5, "")]
        [TestCase("test", 5, "test ")]
        [TestCase("Lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", 12,
            "Lorem  ipsum\ndolor    sit\namet        \nconsectetur \nadipiscing  \nelit  sed do\neiusmod     \ntempor      \nincididunt  \nut labore et\ndolore magna\naliqua      ")]
        [TestCase("Lorem     ipsum    dolor", 17, "Lorem ipsum dolor")]

        //My tests
        [TestCase("LoremLoremipsumLoremdolor", 5, "Lorem\nLorem\nipsum\nLorem\ndolor")]
        [TestCase("Lorem test", 7, "Lorem  \ntest   ")]
        [TestCase("   test   ", 7, "test   ")]
        [TestCase("  Lorem   test   ", 7, "Lorem  \ntest   ")]
        [TestCase("  test   test   test", 12, "test    test\ntest        ")]
        public void Simple(string input, int lineWidth, string expected)
        {
            var algorithm = new LineAdjustmentAlgorithm();
            var output = algorithm.Transform(input, lineWidth);
            Assert.That(expected, Is.EqualTo(output));
        }
    }
}