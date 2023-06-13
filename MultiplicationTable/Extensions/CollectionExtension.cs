namespace MultiplicationTable.Extensions
{
    public static class CollectionExtension
    {
        public static List<T> Shuffle<T>(this List<T> list)
        {
            if (list != null && list.Count > 1)
            {
                Random random = new Random();
                int n = list.Count;
                for (int i = n - 1; i > 0; i--)
                {
                    int j = random.Next(0, i + 1);
                    T temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }
            return list;
        }
    }
}