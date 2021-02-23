using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyList<T>: IList<T>, IReadOnlyList<T>
{
	T[] array;

	int capacity;
	
	public int Count    { get; private set; }
	public int Capacity => array.Length;
	
	public MyList(T[] arr = null)
	{
		array = arr ?? new T[]{};
		Count = array.Length;
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

	public bool IsReadOnly
	{
		get { return false; }
	}

	public void Add(T newElement)
	{
		SetMoreSpaciousArrayIfNeed();

		array[Count] = newElement;
		Count++;
	}
	
	public void Insert(int index, T elementToInsert)
	{
		if (index < 0 || index > Count)
			IndexOutOfRangeException();

		SetMoreSpaciousArrayIfNeed();
		T temp1 = array[index];
		array[index] = elementToInsert;
		var temp2= array[index+1];
		array[index+1] = temp1;
		
		for (int j = index+2; j < Count + 1; j++)
		{
			array[j] = temp2;
			if (j + 1 < array.Length)
				temp2 = array[j + 1];
		}

		Count++;
	}

	public void CopyTo(T[] arrayToCopy, int index)
	{
		if (index < 0 || index >= Count)
			IndexOutOfRangeException();
		
		int newCapacity = array.Length;
		int elementsCount = Count + arrayToCopy.Length;
		
		while (newCapacity < elementsCount)
			newCapacity *= 2;

		if (newCapacity != array.Length)
			array = GetNewArrayByLegacyArray(array, newCapacity);

		for (int i = index; i < Count; i++)
		{
			array[i + arrayToCopy.Length] = array[i];
		}

		int j = 0;
		for (int i = index; i < index + arrayToCopy.Length; i++)
		{
			array[i] = arrayToCopy[j];
			j++;
		}

		Count += arrayToCopy.Length;
	}

	public bool Remove(T elementToRemove)
	{
		int i = 0;
		bool isContained = false;
		for (i = 0; i < Count; i++)
		{
			if (array[i].Equals(elementToRemove))
			{
				isContained = true;
				break;
			}
		}

		if (isContained)
		{
			RemoveAt(i);
			return true;
		}

		return false;
	}
	
	public void RemoveAt(int index)
	{
		for (int j = index; j < Count-1; j++)
			array[j] = array[j + 1];

		array[Count - 1] = default;
		
		Count--;
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

	public bool Contains(T elementToFind)
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
			var newLength = array.Length == 0 ? 2 : array.Length * 2;
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

	public IEnumerator<T> GetEnumerator()
	{
		for (int i = 0; i < Count; i++)
		{
			yield return array[i];
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
