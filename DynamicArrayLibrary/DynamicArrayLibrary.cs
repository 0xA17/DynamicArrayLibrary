using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Norbit.Crm.Hudehshenas.DynamicArrayLibrary
{

    /// <summary>
    /// Динамический массив.
    /// </summary>
    /// <typeparam name="T">Тип параметра.</typeparam>
    public class DynamicArray<T> : IEnumerable, IEnumerable<T>
    {
        #region Свойства и поля

        T[] TempArray;
        public int Length;
        public int Capacity;
        const int DefaultArrayCapacity = 8;

        #endregion

        /// <summary>
        /// Меняет значение массива по задаваемому индексу.
        /// </summary>
        /// <param name="index">Индекс для изменения</param>
        /// <returns>Результат изменения</returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Length)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return TempArray[index];
            }
            set
            {
                if (index < 0 || index > Length)
                {
                    throw new ArgumentOutOfRangeException();
                }
                TempArray[index] = value;
            }
        }

        #region Конструкторы


        /// <summary>
        /// Создает массив стандартной длинны.
        /// </summary>
        public DynamicArray()
            : this(DefaultArrayCapacity)
        {

        }

        /// <summary>
        /// Создает массив указанной ёмкости.
        /// </summary>
        /// <param name="capacity">Емоксть массива</param>
        public DynamicArray(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), message: "Емкость массива не должна быть отрицательной");
            }

            TempArray = new T[capacity];
            Capacity = capacity;
            Length = 0;
        }

        /// <summary>
        /// Создает массив на основе переданной коллекции.
        /// </summary>
        /// <param name="collection"> Коллекция для создания.</param>
        public DynamicArray(IEnumerable<T> collection)
        {
            TempArray = new T[Capacity];
            AddRange(collection);
        }

        #endregion

        /// <summary>
        /// Возвращает массив.
        /// </summary>
        /// <returns>Массив</returns>
        public IEnumerator GetEnumerator()
        {
            return TempArray.GetEnumerator();
        }

        /// <summary>
        /// Возвращает массив типа IEnumerable.
        /// </summary>
        /// <returns>Массив типа IEnumerable</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)TempArray).GetEnumerator();
        }

        #region Методы

        /// <summary>
        /// Добавляет элемент в конец массива.
        /// </summary>
        /// <param name="element">Элемент для добавления.</param>
        public void Add(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element), message: "Элемент не имеет значений!");
            }

            AddRange(new T[] { element });
        }

        /// <summary>
        /// Добавляет содержимое коллекции в конец массива.
        /// </summary>
        /// <param name="collection">Коллекция для добавления.</param>
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection), message: "Коллекция не имеет значений!");
            }

            var arr = new T[Length];

            Array.Copy(TempArray, arr, Length);

            var newValues = arr.ToList();

            foreach (var item in collection)
            {
                newValues.Add(item);
            }

            TempArray = newValues.ToArray();
            Length = TempArray.Length;
            Capacity = TempArray.Length;
        }

        /// <summary>
        /// Сравнивает экземпляры различных типов.
        /// </summary>
        /// <param name="anObject">Экземпляр для сравнения</param>
        /// <returns>Результат сравнения</returns>
        public bool Compare(object anObject)
        {
            if (anObject == null)
            {
                return false;
            }

            var newArray = anObject as DynamicArray<T>;

            if (newArray == null)
            {
                return false;
            }

            return (Capacity.CompareTo(newArray.Length) >= 0);
        }

        /// <summary>
        /// Добавляет элемент в произвольную позицию массива.
        /// </summary>
        /// <param name="element">Элемент для добавления</param>
        /// <param name="index">Индекс для добавления</param>
        /// <returns>True, если добавление прошло успешно, False - если нет</returns>
        public bool Insert(T element, int index)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element), message: "Переменная не имеет значений!");
            }

            if ((index <= 0) || (index > Capacity))
            {
                throw new ArgumentOutOfRangeException();
            }

            try
            {
                var beforeIndex = TempArray.Take(index).ToArray();

                beforeIndex[index - 1] = element;

                var afterIndex = TempArray.Skip(index - 1).ToArray();

                var tempArray = new DynamicArray<T>(Length);

                tempArray.AddRange(beforeIndex);
                tempArray.AddRange(afterIndex);

                TempArray = tempArray.ToArray();
                ++Length;
                Capacity++;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Удаляет из коллекции указанный элемент.
        /// </summary>
        /// <param name="element">Элемент для удаления</param>
        /// <returns>True, если добавление прошло успешно, False - если нет</returns>
        public bool Remove(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element), message: "Переменная не имеет значений!");
            }

            var listArray = TempArray.ToList();

            if (listArray.Remove(element))
            {
                TempArray = listArray.ToArray();
                --Length;

                return true;
            }

            return false;
        }

        #endregion
    }
}