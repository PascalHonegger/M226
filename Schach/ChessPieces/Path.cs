using System;
using System.Collections;

namespace Chess.ChessPieces
{
    public class Path : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool IsRecursive { get; set; }
    }
}