using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace _09.DesignTechnique
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hanoi hanoi = new Hanoi(5);

            hanoi.Move(hanoi.Size, 0, 2, 1);
            Console.WriteLine("Completed");
        }

        public class Hanoi
        {
            const int DefaultSize = 5;
            private int size;
            public int Size { get { return size; } }
            Stack<int>[] hanoi = new Stack<int>[3];
            public Hanoi(int size = DefaultSize)
            {
                this.size = size;
                for (int i = 0; i < 3; i++)
                {
                    hanoi[i] = new Stack<int>();
                }

                for (int i = size; i > 0; i--)
                {
                    hanoi[0].Push(i);
                }
            }

            public void Move(int count, int source, int dest, int other)
            {
                if (count == 1)
                {
                    MoveBoard(source, dest);
                    return;
                }
                Move(count - 1, source, other, dest);
                Move(count - 1, other, dest, source);
            }
            private void MoveBoard(int source, int dest)
            {
                hanoi[dest].Push(hanoi[source].Pop());
            }
        }
       
    }
}