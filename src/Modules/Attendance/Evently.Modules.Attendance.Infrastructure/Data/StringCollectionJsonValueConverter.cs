using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Evently.Modules.Attendance.Infrastructure.Data;

public class StringCollectionJsonValueConverter() : ValueConverter<ICollection<string>, string>(
    valueTo => JsonConvert
        .SerializeObject(valueTo.Select(e => e.ToString()).ToList()),
    valueFrom => JsonConvert
        .DeserializeObject<ICollection<string>>(valueFrom)!.ToList());
