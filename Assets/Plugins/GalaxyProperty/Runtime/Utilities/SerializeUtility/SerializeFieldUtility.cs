namespace Game.Utilities
{
    public static partial class SerializeFieldUtility
    {
        // ReSharper disable once UnusedMember.Global
        public static string Serialize<T>(T value, bool prettyPrint)
            where T : class
        {
            using (EntryScope<T>.Create(out var entry))
            {
                return entry.ToJson(value, prettyPrint);
            }
        }

        public static T Deserialize<T>(string json)
            where T : class
        {
            using (EntryScope<T>.Create(out var entry))
            {
                return entry.FromJson(json);
            }
        }

        public static T Clone<T>(T value)
            where T : class
        {
            var json = Serialize(value, false);
            var result = Deserialize<T>(json);

            return result;
        }
    }
}
