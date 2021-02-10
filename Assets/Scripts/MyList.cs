using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyList<T>
{
	T[] array;

	int capacity;
	
	public int Count    { get; private set; }
	public int Capacity => array.Length;
	
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
		if (index < 0 || index >= Count)
			IndexOutOfRangeException();

		SetMoreSpaciousArrayIfNeed();
		
		int i = 0;
		for (int j = 0; j < Count; j++)
		{
			if (j == index)
			{
				array[i] = elementToInsert;
				i++;
			}

			array[i] = array[j];
			i++;
		}

		Count++;
	}

	public bool Remove(T elementToRemove)
	{
		return TryRemoveElement(elementToRemove);
	}
	
	public bool RemoveAt(int index)
	{
		if (index < 0 || index >= Count)
			IndexOutOfRangeException();

		var elementToRemove = array[index];

		return TryRemoveElement(elementToRemove);
	}

	bool TryRemoveElement(T elementToRemove)
	{
		int i = 0;
		bool removed = false;

		for (int j = 0; j < Count; j++)
		{
			if (!removed && array[j].Equals(elementToRemove))
			{
				removed = true;
				continue;
			}

			array[i] = array[j];
			i++;
		}

		if (removed)
			Count--;

		return removed;
	}

	public int LastIndexOf(T elementToFind)
	{
		return LastIndexOf(elementToFind, Count-1, Count);
	}
	
	public int LastIndexOf(T elementToFind, int indexStart)
	{
		var elementsAmount = Count - indexStart;
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
		return IndexOf(elementToFind, 0, Count);
	}
	
	public int IndexOf(T elementToFind, int indexStart)
	{
		int elementsAmount = Count - indexStart;
		return IndexOf(elementToFind, indexStart, elementsAmount);
	}
	
	public int IndexOf(T elementToFind, int indexStart, int elementsAmount)
	{
		if(elementToFind == null)
			throw new ArgumentNullException();
		
		if(elementsAmount > (Count - indexStart))
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
		for (int i = 0; i < Count; i++)
			array[i] = default;
		
		Count = 0;
	}

	public void TrimExcess()
	{
		if (array.Length != Count)
			array = GetNewArrayByLegacyArray(array, Count);
	}

	public bool Contain(T elementToFind)
	{
		for (int i = 0; i < Count; i++)
		{
			if (array[i].Equals(elementToFind))
				return true;
		}

		return false;
	}

	void SetMoreSpaciousArrayIfNeed()
	{
		if (Count == array.Length)
		{
			var newLength = array.Length == 0 ? 1 : array.Length * 2;
			array = GetNewArrayByLegacyArray(array, newLength);
		}
	}

	T[] GetNewArrayByLegacyArray(T[] legacyArray, int length)
	{
		T[] newArray = new T[length];
		
		for (int i = 0; i < Count; i++)
			newArray[i] = legacyArray[i];

		return newArray;
	}

	IndexOutOfRangeException IndexOutOfRangeException()
	{
		return new IndexOutOfRangeException($"The collection hold only {Count} elements.");
	}
}
