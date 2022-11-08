using System;
using NSubstitute;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norbit.Crm.Hudehshenas.DynamicArrayLibrary;

namespace Norbit.Crm.Hodeshenas.TaskEleven
{
    /// <summary>
    /// Тесты для DynamicArray.
    /// </summary>
    [TestClass()]
    public class DynamicArrayTests
    {
        static DynamicArray<int> _dynamicArray;

        /// <summary>
        /// Инициализация объекта пустышки.
        /// </summary>
        [TestMethod]
        public void TestInitialize()
        {
            _dynamicArray = Substitute.For<DynamicArray<int>>();
        }

        /// <summary>
        /// Инициализация экземпляра класса DynamicArray.
        /// </summary>
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            _dynamicArray = new DynamicArray<int>() { 0x2, 0x4, 0x9, 0x14, 0x3, 0xA, 0x17 };
        }

        /// <summary>
        /// Добавляет элемент в конец массива.
        /// </summary>
        /// <param name="element">Элемент для добавления</param>
        [TestMethod]
        [DataRow(0xE)]
        [DataRow(0xA)]
        [DataRow(0xD)]
        [DataRow(0xC)]
        [DataRow(0xB)]
        public void AddElementTest(int element)
        {
            _dynamicArray.Add(element);
            Assert.AreEqual(element, _dynamicArray[--_dynamicArray.Length]);
        }

        /// <summary>
        /// Добавляет содержимое коллекции в конец массива.
        /// </summary>
        /// <param name="collection">Коллекция для добавления</param>
        [TestMethod()]
        [DataRow(new int[] { 0x2, 0x4, 0x9 })]
        [DataRow(new int[] { 0x14, 0x3, 0xA })]
        [DataRow(new int[] { 0x14, 0x3, 0xA })]
        [DataRow(new int[] { 0xA, 0x17, 0x2, 0x4, 0x9 })]
        public void DynamicArrayTest(int[] collection)
        {
            var collectionLength = collection.Count() + _dynamicArray.Count();

            _dynamicArray.AddRange(collection);
            Assert.AreEqual(collectionLength, _dynamicArray.Count());
        }

        /// <summary>
        /// Проверка на null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DynamicArrayCollectionTest() => _dynamicArray = new DynamicArray<int>(null);

        /// <summary>
        /// Добавляет элемент в конец массива.
        /// </summary>
        /// <param name="element">Элемент для добавления</param>
        [TestMethod]
        [DataRow(0x55)]
        [DataRow(0x44)]
        [DataRow(0x33)]
        [DataRow(0x22)]
        public void AddTest(int element)
        {
            _dynamicArray.Add(element);
            Assert.AreEqual(element, _dynamicArray[--_dynamicArray.Length]);
        }

        /// <summary>
        /// Добавляет содержимое коллекции в конец массива.
        /// </summary>
        /// <param name="collection">Коллекция для добавления.</param>
        [TestMethod]
        [DataRow(new int[] { 0x14, 0x3, 0xA })]
        [DataRow(new int[] { 0x14, 0x3, 0xA })]
        [DataRow(new int[] { 0xA, 0x17, 0x2, 0x4, 0x9 })]
        public void AddRangeTest(int[] collection)
        {
            var collectionLength = collection.Count() + _dynamicArray.Count();

            _dynamicArray.AddRange(collection);

            Assert.AreEqual(collectionLength, _dynamicArray.Count());
        }

        [TestMethod()]
        public void CompareTest() => Assert.IsTrue(_dynamicArray.Compare(_dynamicArray));

        /// <summary>
        /// Добавляет элемент в произвольную позицию массива.
        /// </summary>
        /// <param name="element">Элемент для добавления</param>
        /// <param name="index">Индекс для добавления</param>
        [TestMethod]
        [DataRow(55, 5)]
        [DataRow(44, 4)]
        [DataRow(33, 3)]
        [DataRow(22, 2)]
        public void InsertTest(int element, int index)
        {
            _dynamicArray.Insert(element, index);

            Assert.AreEqual(element, _dynamicArray[--index]);
        }

        /// <summary>
        /// Удаляет из коллекции указанный элемент.
        /// </summary>
        /// <param name="element">Элемент для удаления</param>
        [TestMethod]
        [DataRow(0x2)]
        [DataRow(0x4)]
        [DataRow(0x9)]
        [DataRow(0x17)]
        public void RemoveTest(int element) => Assert.IsTrue(_dynamicArray.Remove(element));
    }
}