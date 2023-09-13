using System;
using System.Collections.Generic;
using BooleanCircuits.Helper.HashMap;
using Xunit;
using Moq;

namespace Tests.BooleanCircuits.Helper.HashMap
{
    public class HashMapTest {
        [Fact]
        public void Get_NameDoesNotExist_ThrowsKeyNotFoundException()
        {
            HashMap<IHashable> hashMap = new HashMap<IHashable>();
            Assert.Throws<KeyNotFoundException>(() => hashMap.Get("Hash"));
        }
        
        [Fact]
        public void Get_OneHashableAdded_ReturnsNode()
        {
            HashMap<IHashable> hashMap = new HashMap<IHashable>();
            Mock<IHashable> hashable = CreateHashable("Hash");
            hashMap.Add(hashable.Object);
            Assert.Equal(hashable.Object, hashMap.Get("Hash"));
        }
        
        [Fact]
        public void Add_AddSameHashableTwice_HashableOnlyOnceAdded()
        {
            HashMap<IHashable> hashMap = new HashMap<IHashable>();
            Mock<IHashable> hashable = CreateHashable("Hash");
            hashMap.Add(hashable.Object);
            Assert.Throws<ArgumentException>(() => hashMap.Add(hashable.Object));
            hashMap.Remove(hashable.Object);
            Assert.False(hashMap.Contains("Hash"));
        }
        
        [Fact]
        public void Contains_NoHashableAdded_ReturnsFalse()
        {
            HashMap<IHashable> hashMap = new HashMap<IHashable>();
            Mock<IHashable> hashable = CreateHashable("Hash");
            Assert.False(hashMap.Contains("Hash"));
            Assert.False(hashMap.Contains(hashable.Object));
        }
    
        [Fact]
        public void Contains_HashableAdded_ReturnsTrue()
        {
            HashMap<IHashable> hashMap = new HashMap<IHashable>();
            Mock<IHashable> hashable = CreateHashable("Hash");
            hashMap.Add(hashable.Object);
            Assert.True(hashMap.Contains("Hash"));
            Assert.True(hashMap.Contains(hashable.Object));
        }
    
        [Fact]
        public void Remove_HashableAdded_RemovesHashable()
        {
            HashMap<IHashable> hashMap = new HashMap<IHashable>();
            Mock<IHashable> hashable = CreateHashable("Hash");
            
            // test remove with IHashable
            hashMap.Add(hashable.Object);
            Assert.True(hashMap.Contains("Hash"));
            
            hashMap.Remove(hashable.Object);
            Assert.False(hashMap.Contains("Hash"));
            
            // test remove with hash
            hashMap.Add(hashable.Object);
            Assert.True(hashMap.Contains("Hash"));
    
            hashMap.Remove("Hash");
            Assert.False(hashMap.Contains("Hash"));
        }
        
        [Fact]
        public void Remove_NoHashableAdded_DoesNotThrow()
        {
            HashMap<IHashable> hashMap = new HashMap<IHashable>();
            Mock<IHashable> hashable = CreateHashable("Hash");
            
            Assert.False(hashMap.Contains("Hash"));
            hashMap.Remove(hashable.Object);
            hashMap.Remove("Hash");
        }
        
        [Fact]
        public void ForEach_NoHashableAdded_DoesNotCallCalback()
        {
            HashMap<IHashable> hashMap = new HashMap<IHashable>();
            Mock<Action<IHashable>> mockCallback = new Mock<Action<IHashable>>();
            hashMap.ForEach(mockCallback.Object);
            mockCallback.Verify(c => c(It.IsAny<IHashable>()), Times.Never());
        }
    
        [Theory]
        [InlineData(1)]
        [InlineData(1000)]
        [InlineData(12317)]
        public void ForEach_NHashablesAdded_CallbackCalledNTimes(int n)
        {
            HashMap<IHashable> hashMap = new HashMap<IHashable>();
            
            for (int i = 0 ; i < n ; i++)
            {
                hashMap.Add(CreateHashable(i.ToString()).Object);
            }
            
            Mock<Action<IHashable>> mockCallback = new Mock<Action<IHashable>>();
            hashMap.ForEach(mockCallback.Object);
            mockCallback.Verify(c => c(It.IsAny<IHashable>()), Times.Exactly(n));
        }
        
        [Theory]
        [InlineData]
        [InlineData("Hash 1")]
        [InlineData("Hash 1", "Hash 2")]
        [InlineData("Hash 1", "Hash 2", "Hash 3", "Hash 4", "Hash 5")]
        public void GetHashList_NHashes_ExpectHashesToMach(params string[] hashes)
        {
            HashMap<IHashable> hashMap = new HashMap<IHashable>();
            
            foreach (var hash in hashes)
                hashMap.Add(CreateHashable(hash).Object);
            
            List<string> hashList = hashMap.GetHashList();
            
            for (int i = 0 ; i < hashes.Length ; i++)
            {
                Assert.Equal(hashes[i], hashList[i]);
            }
        }
    
        [Theory]
        [InlineData]
        [InlineData("Hash 1")]
        [InlineData("Hash 1", "Hash 2")]
        [InlineData("Hash 1", "Hash 2", "Hash 3", "Hash 4", "Hash 5")]
        public void Map_MapNHashesToInt_ReturnsListOfInt(params string[] hashes)
        {
            HashMap<IHashable> hashMap = new HashMap<IHashable>();
    
            foreach (var hash in hashes)
                hashMap.Add(CreateHashable(hash).Object);
            
            int index = 0;
            List<int> intList = hashMap.Map(_ => index++);
            
            for (int i = 0 ; i < hashes.Length ; i++)
            {
                Assert.Equal(i, intList[i]);
            }
        }
        
        private Mock<IHashable> CreateHashable(string hash)
        {
            Mock<IHashable> hashable = new Mock<IHashable>();
            hashable.Setup(h => h.Hash()).Returns(hash);
            return hashable;
        }
    }
}
