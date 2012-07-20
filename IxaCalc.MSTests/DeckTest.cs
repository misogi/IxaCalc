using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IxaCalc.MSTests
{
    using IxaCalc.Model;
    [TestClass]
    public class DeckTest
    {
        [TestMethod]
        public void AddTest()
        {
            var deck = new Deck();
            Assert.AreEqual(0, deck.Bushos.Count);
            var busho = new Busho(1001, "天", "織田信長", 3.5, 3500, "A", "B", "A", ".S");
            deck.Add(busho);
            Assert.AreEqual(1, deck.Bushos.Count);
            deck.Add(new Busho(3010, "特", "本願寺顕如", 3, 2610, "B", "C", "C", "A"));
            Assert.AreEqual(2, deck.Bushos.Count);
            deck.Add(new Busho(3011, "特", "榊原康政", 2.5, 2270, "A", "B", "B", "D"));
            Assert.AreEqual(3, deck.Bushos.Count);
            deck.Add(new Busho(3012, "特", "井伊直政", 2.5, 2270, "A", "C", "A", "D"));
            Assert.AreEqual(4, deck.Bushos.Count);
            deck.Add(new Busho(3019, "特", "武田信繁", 3, 2640, "B", "A", "A", "D"));
            Assert.AreEqual(4, deck.Bushos.Count);
        }

        [TestMethod]
        public void RemoveTest()
        {
            var deck = new Deck();
            Assert.AreEqual(0, deck.Bushos.Count);
            deck.Add(new Busho(1001, "天", "織田信長", 3.5, 3500, "A", "B", "A", ".S"));
            deck.Add(new Busho(3010, "特", "本願寺顕如", 3, 2610, "B", "C", "C", "A"));
            deck.Add(new Busho(3011, "特", "榊原康政", 2.5, 2270, "A", "B", "B", "D"));
            deck.Add(new Busho(3012, "特", "井伊直政", 2.5, 2270, "A", "C", "A", "D"));
            Assert.AreEqual(4, deck.Bushos.Count);
            deck.Remove(0);
            Assert.AreEqual(3, deck.Bushos.Count);
            Assert.AreEqual(3010, deck.Bushos[0].Id);
            deck.Remove(4);
            Assert.AreEqual(3, deck.Bushos.Count);
            deck.Remove(0);
            Assert.AreEqual(2, deck.Bushos.Count);
            deck.Remove(0);
            Assert.AreEqual(1, deck.Bushos.Count);
            deck.Remove(0);
            Assert.AreEqual(0, deck.Bushos.Count);
            deck.Remove(0);
            Assert.AreEqual(0, deck.Bushos.Count);
        }

        [TestMethod]
        public void TotalSoldierNumTest()
        {
            var deck = new Deck();
            deck.Add(new Busho(1001, "天", "織田信長", 3.5, 3500, "A", "B", "A", ".S"));
            deck.Add(new Busho(3010, "特", "本願寺顕如", 3, 2610, "B", "C", "C", "A"));
            Assert.AreEqual(6110, deck.TotalSoldierNum());
            deck.Add(new Busho(3011, "特", "榊原康政", 2.5, 2270, "A", "B", "B", "D"));
            Assert.AreEqual(8380, deck.TotalSoldierNum());
        }
    }
}
