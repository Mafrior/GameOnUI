using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu]
public class CraftObject : InventoryItem
{
    public List<Case> cases = new List<Case>();
    public List<string> haracteristics = new List<string>();
    public static bool operator ==(CraftObject left, CraftObject right)
    {
        if (left.Name == right.Name)
        {
            for (int i = 0; i < left.haracteristics.Count; i++)
            {
                if (right.haracteristics.Any(x => x == left.haracteristics[i]))
                {
                    continue;
                }
                return false;
            }
            return true;
        }
        return false;
    }

    public static bool operator !=(CraftObject left, CraftObject right) { return (Object)left != (Object)right; }
    public override bool Equals(object obj) { return base.Equals((Object)obj); }
    public override int GetHashCode() { return base.GetHashCode(); }
}
