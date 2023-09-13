using System;
using BooleanCircuits.Helper.HashMap;
using Xunit;
using Moq;

namespace Tests.BooleanCircuits.Helper.HashMap
{
    public class ObservableHashMapTest {
        [Fact]
        public void Subscribe_Observer_ObserveOnRemoveByElement()
        {
            ObservableHashMap<IHashable> hashMap = new ObservableHashMap<IHashable>();
            Mock<Action<IHashable>> mockObserver = new Mock<Action<IHashable>>();
            Mock<IHashable> mockHashable = CreateHashable("Hash");
            
            hashMap.Subscribe(mockObserver.Object);
            
            hashMap.Add(mockHashable.Object);
            hashMap.Remove(mockHashable.Object);
            
            mockObserver.Verify(o => o(mockHashable.Object), Times.Once());
        }
    
        [Fact]
        public void Subscribe_Observer_ObserveOnRemoveByHash()
        {
            ObservableHashMap<IHashable> hashMap = new ObservableHashMap<IHashable>();
            Mock<Action<IHashable>> mockObserver = new Mock<Action<IHashable>>();
            Mock<IHashable> mockHashable = CreateHashable("Hash");
        
            hashMap.Subscribe(mockObserver.Object);
            
            hashMap.Add(mockHashable.Object);
            hashMap.Remove("Hash");
            
            mockObserver.Verify(o => o(mockHashable.Object), Times.Once());
        }
    
        [Fact]
        public void Dispose_Observer_StopObservingAfterDisposing()
        {
            ObservableHashMap<IHashable> hashMap = new ObservableHashMap<IHashable>();
            Mock<Action<IHashable>> mockObserver = new Mock<Action<IHashable>>();
            Mock<IHashable> mockHashable = CreateHashable("Hash");
    
            IDisposable subscription = hashMap.Subscribe(mockObserver.Object);
            
            hashMap.Add(mockHashable.Object);
            hashMap.Remove("Hash");
            
            mockObserver.Verify(o => o(mockHashable.Object), Times.Once());
            
            subscription.Dispose();
    
            hashMap.Add(mockHashable.Object);
            hashMap.Remove("Hash");
            
            mockObserver.Verify(o => o(mockHashable.Object), Times.Once());
        }
    
        [Fact]
        public void Subscribe_NoHashableAdded_DontObserveOnRemove()
        {
            ObservableHashMap<IHashable> hashMap = new ObservableHashMap<IHashable>();
            Mock<Action<IHashable>> mockObserver = new Mock<Action<IHashable>>();
            Mock<IHashable> mockHashable = CreateHashable("Hash");
    
            hashMap.Subscribe(mockObserver.Object);
    
            hashMap.Remove("Hash");
            
            mockObserver.Verify(o => o(mockHashable.Object), Times.Never());
            
            hashMap.Add(mockHashable.Object);
            hashMap.Remove("Hash");
            
            mockObserver.Verify(o => o(mockHashable.Object), Times.Once());
        }
        
        private Mock<IHashable> CreateHashable(string hash)
        {
            Mock<IHashable> hashable = new Mock<IHashable>();
            hashable.Setup(h => h.Hash()).Returns(hash);
            return hashable;
        }
    }
}
