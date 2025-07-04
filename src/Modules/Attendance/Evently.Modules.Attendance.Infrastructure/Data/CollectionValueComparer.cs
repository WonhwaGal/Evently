using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Evently.Modules.Attendance.Infrastructure.Data;

public class CollectionValueComparer<T>() : ValueComparer<ICollection<T>>(
    (c1, c2) => c1!.SequenceEqual(c2!),
    c => c.Aggregate(0, (a, v) => 
        HashCode.Combine(a, v!.GetHashCode())), c => (ICollection<T>)c.ToHashSet());
