using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCreator
{
    internal class ListUtils
    {
        public static bool IsGenericList(Type oType)
        {
            if (oType is null)
            {
                return false;
            }
            if (oType.IsGenericType)
            {
                return new List<Type> { typeof(List<>), typeof(ICollection<>), typeof(IEnumerable<>) }.Any(type => oType.GetGenericTypeDefinition() == type);
            }
            return false;
        }
    }
}
