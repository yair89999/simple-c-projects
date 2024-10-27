using System.IO;

class App
{
    public List<string> toDo = new List<string>();
    public List<string> Doing = new List<string>();
    public bool pressedToExit = false;

    public const string ToDoFilePath = "C:\\Code Connected\\Code\\c#\\ToDoListApp\\ToDoListApp\\todo.txt";
    public const string DoingFilePath = "C:\\Code Connected\\Code\\c#\\ToDoListApp\\ToDoListApp\\doing.txt";
    public App() {
        // reloading the file's info
        string toDoText = File.ReadAllText(ToDoFilePath);
        string DoingText = File.ReadAllText(DoingFilePath);

        // splits the text in the files by \n and transfering it into a list
        string[] lines = toDoText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        this.toDo = new List<string>(lines);

        lines = DoingText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        this.Doing = new List<string>(lines);
    }
    public void print()
    {
        Console.WriteLine(); // acts as a saperator

        Console.WriteLine("To Do:");
        for (int i =0; i < toDo.Count; i++)
        {
            Console.WriteLine((i+1) + ". " + this.toDo[i]);
        }
        Console.WriteLine("Doing:");
        for (int i = 0; i < Doing.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + this.Doing[i]);
        }

        Console.WriteLine(); // acts as a saperator
    }

    public void save()
    {
        // saving the to do
        if (!File.Exists(ToDoFilePath))
        {
            using (File.Create(ToDoFilePath)) { } // This creates and closes the file immediately
        }
        File.WriteAllText(ToDoFilePath, string.Join("\n", this.toDo));

        // saving the doing
        if (!File.Exists(DoingFilePath))
        {
            using (File.Create(DoingFilePath)) { } // This creates and closes the file immediately
        }
        File.WriteAllText(DoingFilePath, string.Join("\n", this.Doing));
    }
    public void handleInput()
    {
        try
        {
            Console.WriteLine("Choose An Action:");
            Console.WriteLine("1. Add A To Do Task");
            Console.WriteLine("2. Mark Task To Doing");
            Console.WriteLine("3. Mark Task As Done");
            Console.WriteLine("4. Print");
            Console.WriteLine("5. Exit");

            Console.Write("Action Number: ");
            int actionNum = Convert.ToInt32(Console.ReadLine());
            // add task
            if (actionNum == 1)
            {
                Console.Write("Enter The Task: ");
                string task = Console.ReadLine();
                bool isIn = false;

                foreach (string s in this.toDo)
                {
                    if (s == task)
                    {
                        isIn = true;
                        break;
                    }
                }

                if (isIn == true)
                {
                    Console.WriteLine("Task Is Already In The List");
                }
                else
                {
                    this.toDo.Add(task);
                    Console.WriteLine("Task Added");
                    this.print();
                }
            }
            // move task to doing
            else if (actionNum == 2)
            {
                Console.WriteLine("Choose Task Number:");
                for (int i = 0; i < toDo.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + this.toDo[i]);
                }
                Console.Write("Task Number: ");
                int taskNum = Convert.ToInt32(Console.ReadLine());

                this.Doing.Add(this.toDo[taskNum - 1]); // adding to doing
                this.toDo.Remove(this.toDo[taskNum - 1]); // removing from to do

                this.print();
            }
            // mark a doing task as done
            else if (actionNum == 3)
            {
                Console.WriteLine("Choose Task Number:");
                for (int i = 0; i < Doing.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + this.Doing[i]);
                }
                Console.Write("Task Number: ");
                int taskNum = Convert.ToInt32(Console.ReadLine());

                this.Doing.Remove(this.Doing[taskNum - 1]); // removing from to do

                Console.WriteLine("Task Marked As Finished");

                this.print();
            }
            // printing all
            else if (actionNum == 4)
            {
                this.print();
            }
            else
            {
                this.pressedToExit = true;
            }
            // if not printing or exiting we will check if it is needed to save the list
            if (actionNum < 4)
            {
                this.save();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("You Entered Invalid Input, Please Try Again");
        }
    }
    

}

namespace ToDoListApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            App app = new App();
            while (app.pressedToExit == false)
            {
                app.handleInput();
            }
        }
    }
}
