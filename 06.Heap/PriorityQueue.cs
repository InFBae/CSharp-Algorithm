using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    internal class PriorityQueue<TElement, TPriority>
    {
        private struct Node
        {
            public TElement element;
            public TPriority priority;
        }

        private List<Node> nodes;
        private IComparer<TPriority> comparer;
        public int Count { get { return nodes.Count; } }
        public PriorityQueue()
        {
            this.nodes = new List<Node>();
            this.comparer = Comparer<TPriority>.Default;
        }
        public PriorityQueue(IComparer<TPriority> comparer)
        {
            this.nodes = new List<Node>();
            this.comparer= comparer;
        }

        public void Enqueue(TElement element, TPriority priority)
        {
            Node newNode = new Node() { element = element, priority = priority };

            // 1 가장 뒤에 데이터 추가
            this.nodes.Add(newNode);
            int newNodeIndex = Count - 1;

            // 2 새로운 노드를 힙상태가 유지되도록 승격 작업 반복
            while (newNodeIndex > 0)
            {
                // 2-1 부모 노드 확인
                int parentIndex = GetParentIndex(newNodeIndex);
                Node parentNode = nodes[parentIndex];

                // 2-2 자식 노드가 부모 노드보다 우선 순위가 높으면 교체
                if (comparer.Compare(newNode.priority, parentNode.priority) < 0)
                {
                    nodes[parentIndex] = newNode;
                    nodes[newNodeIndex] = parentNode;
                    newNodeIndex = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        public TElement Dequeue()
        {
            Node rootNode = nodes[0];

            // 1 가장 마지막 노드를 최상단으로 위치
            Node lastNode = nodes[nodes.Count - 1];
            nodes[0] = lastNode;
            nodes.RemoveAt(nodes.Count - 1);

            int index = 0;
            while(index < nodes.Count)
            {
                int leftChildIndex = GetLeftChildIndex(index);
                int rightChildIndex = GetRightChildIndex(index);

                // 2-1 자식이 둘 다 있는 경우
                if (rightChildIndex < nodes.Count)
                {
                    // 2-1-1 왼쪽 자식과 오른쪽 자식을 비교하여 더 우선순위가 더 높은 자식을 선정
                    int lessChildIndex = comparer.Compare(nodes[leftChildIndex].priority, nodes[rightChildIndex].priority) > 0 ? leftChildIndex : rightChildIndex;

                    // 2-1-2 더 우선순위가 높은 자식과 부모 노드를 비교하여 부모가 더 낮을 경우 교체
                    if (comparer.Compare(nodes[lessChildIndex].priority, nodes[index].priority) < 0)
                    {
                        nodes[index] = nodes[lessChildIndex];
                        nodes[lessChildIndex] = lastNode;
                        index = lessChildIndex;
                    }
                    else break;
                }
                // 2-2 자식이 하나만 있는 경우 == 왼쪽 자식만 있는 경우
                else if (leftChildIndex < nodes.Count)
                {
                    if (comparer.Compare(nodes[leftChildIndex].priority, nodes[index].priority) < 0)
                    {
                        nodes[index] = nodes[leftChildIndex];
                        nodes[leftChildIndex] = lastNode;
                        index = leftChildIndex;
                    }
                    else break;
                }
                // 2-3 자식이 없는 경우
                else
                {
                    break;
                }

            }
            return rootNode.element;
        }

        public TElement Pick()
        {
            return nodes[0].element;
        }
        private int GetParentIndex(int childIndex)
        {
             return (childIndex - 1) / 2;
        }

        private int GetLeftChildIndex(int parentIndex)
        {
            return parentIndex * 2 + 1;
        }
        private int GetRightChildIndex(int parentIndex)
        {
            return parentIndex * 2 + 2;
        }
    }
}
