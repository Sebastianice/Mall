namespace ConsoleApp1 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
            List<Student> list = new List<Student>();

            list.Add(new Student() { Name = "aa" });
            var st2 = new Student() { Name = "bb" };
            list.Add(st2);
            list.Add(new Student() { Name = "cc" });
            int i = 0;
            foreach (var st in list) {
                i++;
                if (i == 2) {
                    st.Name="gg";
                }
                Console.WriteLine(st.Name);
            }
            Console.WriteLine(st2.Name);
        }

        public class Student {
            public string Name { get; set; }
        }
    }
}