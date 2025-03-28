namespace Exception_Handling_Search_Task
{
    class NoVowelsException : Exception
    {
        public NoVowelsException(string msg) : base(msg)
        {

        }
    }
    class DuplicateNumberException : Exception
    {
        public DuplicateNumberException(string msg) : base(msg)
        {

        }
    }
    internal class Program
    {
        static void AddNoDuplicates()
        {
            var values = new List<int>();
            int counter = 0, value;
            Console.WriteLine("Enter Number of Values");
            counter = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Values");
            while (counter > 0)
            {
                value = Convert.ToInt32(Console.ReadLine());
                if (!values.Contains(value))
                    values.Add(value);
                else
                    throw new DuplicateNumberException("Value is already is in the List");
                counter--;
            }
        }
        static void Vowels(string s)
        {
            s.ToLower();
            int counter = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'a' || s[i] == 'o' || s[i] == 'u' || s[i] == 'i' || s[i] == 'e')
                    counter++;
            }
            if (counter == 0)
                throw new NoVowelsException("String doesn't contain any Vowels");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a String that must contain a Vowel");
            string s = Console.ReadLine();
            try
            {
                AddNoDuplicates();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                Vowels(s);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
