using System.Collections;

namespace Chess.ChessPieces
{
    public class Path : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public bool IsRecursive { get; set; }
    }
}