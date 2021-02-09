using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyList<T>
{
	T[] array;

	int capacity;
	
	public int Count    { get; private set; }
	public int Capacity
	{
		get => capacity;
		private set => capacity = capacity == 0 ? 1 : value;
	}
	
	public MyList()
	{
		array = new T[0];
	}

	public T this[int i]
	{
		get
		{
			if (i < 0 || i > Count)
				IndexOutOfRangeException();
			return array[i]; 
		}
		set
		{
			if (i < 0 || i > Count)
				IndexOutOfRangeException();
			array[i] = value; }
	}

	public void Add(T newElement)
	{
		SetMoreSpaciousArrayIfNeed();

		array[Count] = newElement;
		Count++;
	}
	
	public void Insert(int index, T elementToInsert)
	{
		if (index < 0 || index >= array.Length)
			IndexOutOfRangeException();

		SetMoreSpaciousArrayIfNeed();
		
		int i = 0;
		T[] newArray = new T[array.Length];

		for (int j = 0; j < array.Length; j++)
		{
			if (j == index)
			{
				newArray[i] = elementToInsert;
				i++;
			}

			newArray[i] = array[j];
			i++;
		}

		array = newArray;
		Count++;
	}

	public bool Remove(T elementToRemove)
	{
		if (Contain(elementToRemove))
			return TryRemoveElement(elementToRemove);

		return false;
	}
	
	public bool RemoveAt(int index)
	{
		if (index < 0 || index >= array.Length)
			IndexOutOfRangeException();

		var elementToRemove = array[index];

		return TryRemoveElement(elementToRemove);
	}

	bool TryRemoveElement(T elementToRemove)
	{
		int i = 0;
		bool removed  = false;
		T[]  newArray = new T[array.Length];
		
		foreach (var element in array)
		{
			if (!removed && element.Equals(elementToRemove))
			{
				removed = true;
				Count--;
				continue;
			}
			newArray[i] = element;
		}

		return removed;
	}

	public int LastIndexOf(T elementToFind)
	{
		return LastIndexOf(elementToFind, array.Length-1, array.Length);
	}
	
	public int LastIndexOf(T elementToFind, int indexStart)
	{
		var elementsAmount = array.Length - indexStart;
		return LastIndexOf(elementToFind, indexStart, elementsAmount);
	}
	
	public int LastIndexOf(T elementToFind, int indexStart, int elementsAmount)
	{
		if(indexStart < 0 || elementsAmount < 0 || (indexStart+1) - elementsAmount < 0)
			throw new ArgumentOutOfRangeException();
		
		for (int i = indexStart; i > indexStart - elementsAmount; i--)
		{
			if (array[i].Equals(elementToFind))
				return i;
		}

		return -1;
	}

	public int IndexOf(T elementToFind)
	{
		return IndexOf(elementToFind, 0, array.Length);
	}
	
	public int IndexOf(T elementToFind, int indexStart)
	{
		int elementsAmount = array.Length - indexStart;
		return IndexOf(elementToFind, indexStart, elementsAmount);
	}
	
	public int IndexOf(T elementToFind, int indexStart, int elementsAmount)
	{
		if(elementToFind == null)
			throw new ArgumentNullException();
		
		if(indexStart < 0 || elementsAmount < 0 || indexStart > array.Length || elementsAmount > (array.Length - indexStart))
			throw new ArgumentOutOfRangeException();
		
		for (int i = indexStart; i < indexStart + elementsAmount; i++)
		{
			if (array[i].Equals(elementToFind))
				return i;
		}

		return -1;
	}

	public void Clear()
	{
		array = new T[0];
		Count = 0;
	}

	public void TrimExcess()
	{
		if (Capacity != Count)
		{
			Capacity = Count;
			array = GetNewArrayByLegacyArray(array, Capacity);
		}
	}

	public bool Contain(T elementToFind)
	{
		foreach (var element in array)
		{
			if (element.Equals(elementToFind))
				return true;
		}

		return false;
	}

	void SetMoreSpaciousArrayIfNeed()
	{
		if (Count == Capacity)
		{
			Capacity *= 2;
			array = GetNewArrayByLegacyArray(array, Capacity);
		}
	}

	T[] GetNewArrayByLegacyArray(T[] legacyArray, int newCapacity)
	{
		T[] newArray = new T[newCapacity];
		var count = newCapacity < legacyArray.Length ? newCapacity : legacyArray.Length;
		
		for (int i = 0; i < count; i++)
			newArray[i] = legacyArray[i];

		return newArray;
	}

	IndexOutOfRangeException IndexOutOfRangeException()
	{
		return new IndexOutOfRangeException($"The collection hold only {array.Length} elements.");
	}
}
