namespace AnimalSanctuaryAPI.Extensions
{
    public static class GenericExtension
    {
        public static IEnumerable<T> FilterBy<T>(this IEnumerable<T> data, string? filter = "null")
        {
            if (data?.Any() != true)
            {
                return Enumerable.Empty<T>();
            }
            if (!string.IsNullOrEmpty(filter) && filter != "null")
            {
                data = data.Where(e => e!.GetType().GetProperties().Where(p => p.GetValue(e) != null).Select(p => p.GetValue(e)!.ToString()!.ToLower()).Any(p => p.Contains(filter.ToLower())));
            }

            return data;
        }

        public static IEnumerable<T> SortBy<T>(this IEnumerable<T> data, string? field, string? order)
        {
            if (data?.Any() != true)
            {
                return Enumerable.Empty<T>();
            }

            field ??= "Name";

            var prop = Array.Find(data.First()!.GetType().GetProperties(), p => string.Equals(p.Name, field, StringComparison.CurrentCultureIgnoreCase));

            if (prop == null)
            {
                return data;
            }

            return order switch
            {
                "desc" => data.OrderByDescending(p => prop.GetValue(p)),
                _ => data.OrderBy(p => prop.GetValue(p))
            };
        }
    }
}