using System.Linq;

namespace TextAdventureJimmySkinnari
{
    static class ArrayExtensions
    {
        public static string[] RemoveAll(this string[] items, params string[] valuesToRemove) // Let user remove strings from given array
        {
            if ( items.Except(valuesToRemove).ToArray().Length > 1 )
            {
                return items.Except(valuesToRemove).ToArray();
            }
            else
            {
                return items;
            }

        }
    }
}

