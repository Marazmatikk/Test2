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
			if (i > Count)
				IndexOutOfRangeException();
			return array[i]; 
		}
		set
		{
			if (i > Count)
				IndexOutOfRangeException();
			array[i] = value; }
	}

	public void Add(T newElement)
	{
		if (Count == Capacity)
		{
			Capacity *= 2;
			array = GetNewArray(array, Capacity);
		}

		array[Count] = newElement;
		Count++;
	}
	
	// public void Insert()
	// {
	// 	
	// }

	// public bool Remove(T elementToRemove)
	// {
	// 	
	// 	foreach (var element in array)
	// 	{
	// 		if (element.Equals(elementToRemove))
	// 		{
	// 			
	// 		}
	// 	}
	//
	// 	return false;
	// }
	//
	// public void RemoveAt()
	// {
	// 	
	// }

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
			array    = GetNewArray(array, Capacity);
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

	T[] GetNewArray(T[] legacyArray, int newCapacity)
	{
		T[] newArray = new T[newCapacity];
		var count= newCapacity < legacyArray.Length ? newCapacity : legacyArray.Length;
		
		for (int i = 0; i < count; i++)
			newArray[i] = legacyArray[i];

		return newArray;
	}

	IndexOutOfRangeException IndexOutOfRangeException()
	{
		return new IndexOutOfRangeException($"The collection hold only {array.Length} elements.");
	}
}
