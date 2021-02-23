using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEditor.VersionControl;
using UnityEngine;

namespace GeniusTests
{
    public class MyListAutoTest : MonoBehaviour
    {
        MyList<int> myList;
        
        [Test]
        public void CountAndCapacity()
        {
            myList = new MyList<int>(new[] {1,2,3,4,5,6,7,8,9});
            Assert.IsTrue(myList.Count == 9);
            Assert.IsTrue(myList.Capacity == 9);

            myList.Remove(0);
            Assert.IsTrue(myList.Count == 9);
            Assert.IsTrue(myList.Capacity == 9);

            myList.Remove(1);
            Assert.IsTrue(myList.Count == 8);
            Assert.IsTrue(myList.Capacity == 9);

            myList.RemoveAt(0);
            Assert.IsTrue(myList.Count == 7);
            Assert.IsTrue(myList.Capacity == 9);

            myList.Add(10);
            Assert.IsTrue(myList.Count == 8);
            Assert.IsTrue(myList.Capacity == 9);

            myList.CopyTo(new[] {0, 1}, 0);
            Assert.IsTrue(myList.Count == 10);
            Assert.IsTrue(myList.Capacity == 18);
            
            myList.Insert(5, 5);
            Assert.IsTrue(myList.Count == 11);
            Assert.IsTrue(myList.Capacity == 18);

            myList.TrimExcess();
            Assert.IsTrue(myList.Count == 11);
            Assert.IsTrue(myList.Capacity == 11);

            myList.Clear();
            Assert.IsTrue(myList.Count == 0);
            Assert.IsTrue(myList.Capacity == 11);
        }

        [Test]
        public void Indexer()
        {
            myList = new MyList<int>(new[] {1,2,3});
            Assert.IsTrue(myList[0] == 1); 
            Assert.IsTrue(myList[1] == 2); 
            Assert.IsTrue(myList[2] == 3); 
        }

        [Test]
        public void Add()
        {
            myList = new MyList<int>();
        
            Assert.IsTrue(myList.Count == 0);
            Assert.IsTrue(myList.Capacity == 0);
            
            myList.Add(0);
            Assert.IsTrue(myList.Count == 1);
            Assert.IsTrue(myList.Capacity == 2);
            Assert.IsTrue(myList[0] == 0);
        }
        
        [Test]
        public void Insert()
        {
            myList = new MyList<int>();
            
            myList.Insert(0, 1);
            Assert.IsTrue(myList[0] == 1);
            Assert.IsTrue(myList.Count == 1);
            Assert.IsTrue(myList.Capacity == 2);
            
            myList = new MyList<int>(new []{1,2,3,4,5});
            
            myList.Insert(3, 69);
            MyList<int> targetList = new MyList<int>(new []{1, 2, 3, 69, 4, 5});
            for (int i = 0; i < myList.Count; i++)
                Assert.IsTrue(myList[i] == targetList[i]);
            
            Assert.IsTrue(myList.Count == 6);
            Assert.IsTrue(myList.Capacity == 10);
        }

        [Test]
        public void RemoveAt()
        {
            myList = new MyList<int>(new[] {1,2,3,4,5,6,3,8,9});
            myList.RemoveAt(0);
            myList.RemoveAt(7);
            myList.RemoveAt(4);
            
            MyList<int> targetList = new MyList<int>(new []{2,3,4,5,3,8});
            for (int i = 0; i < myList.Count; i++)
                Assert.IsTrue(myList[i] == targetList[i]);
            
            Assert.IsTrue(myList.Count == 6);
        }

        [Test]
        public void Remove()
        {
            myList = new MyList<int>(new[] {1,2,3,4,5,5,7,8,9});
            
            Assert.IsFalse(myList.Remove(22));
            Assert.IsTrue(myList.Count == 9);
            
            Assert.IsTrue(myList.Remove(5));
            Assert.IsTrue(myList.Count == 8);
        }

        [Test]
        public void LastIndexOf()
        {
            myList = new MyList<int>(new[] {1,2,3,4,5,6,7,8,9});
            
            Assert.IsTrue(myList.LastIndexOf(5) == 4);
            myList.Add(5);
            Assert.IsTrue(myList.LastIndexOf(5) == 9);
            Assert.IsTrue(myList.LastIndexOf(5, 5) == 4);
            Assert.IsTrue(myList.LastIndexOf(5, 7, 2) == -1);
        }

        [Test]
        public void IndexOf()
        {
            myList = new MyList<int>(new[] {1,2,3,4,5,1,7,8,9});
            Assert.IsTrue(myList.IndexOf(1) == 0);
            Assert.IsTrue(myList.IndexOf(1, 1) == 5);
            Assert.IsTrue(myList.IndexOf(1, 5, 2) == 5);
            Assert.IsTrue(myList.IndexOf(1, 6, 2) == -1);
        }

        [Test]
        public void Clear()
        {
            myList = new MyList<int>(new[] {1,2,3,4,5,1,7,8,9});
            
            Assert.IsTrue(myList.Count == 9);
            myList.Clear();
            Assert.IsTrue(myList.Count == 0);
        }
        
        [Test]
        public void TrimExcess()
        {
            myList = new MyList<int>(new[] {1,2,3,4,5,1,7,8,9});
            
            Assert.IsTrue(myList.Capacity == 9);
            myList.TrimExcess();
            Assert.IsTrue(myList.Capacity == 9);
            myList.Clear();
            myList.TrimExcess();
            Assert.IsTrue(myList.Capacity == 0);
        }

        [Test]
        public void Contains()
        {
            myList = new MyList<int>(new[] {1,2,3,4,5,1,7,8,9});
            Assert.IsTrue(myList.Contains(1));
            Assert.IsFalse(myList.Contains(11));
        }
    }
}
