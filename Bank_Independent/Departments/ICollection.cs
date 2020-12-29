using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Independent
{
    interface ICollection<T>
    {
        T this[int index] { get; set; }
        void Add(T item);        
        void Clear();
        void Remove(T item);
    }
}
