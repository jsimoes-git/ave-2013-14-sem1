using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public struct Student
{
    public readonly int nr;
    public readonly String name;

    public Student(int nr, String name)
    {
        this.nr = nr;
        this.name = name;
    }

    public override String ToString()
    {
        return String.Format("{0} {1}", nr, name);
    }
    public void Print()
    {
        Console.WriteLine(this);
    }
}

public class Sorter
{

    private static readonly String STUDENTS_FILE = "i41n.txt";

    List<Student> stds = new List<Student>();
    Random r = new Random();

    static IEnumerable<String> WithLines(string path)
    {
        string line;
        using (StreamReader file = new StreamReader(path, Encoding.UTF8))
        {
            while ((line = file.ReadLine()) != null)
            {
                yield return line;
            }

        }
        yield break;
    }

    static Student? toStudent(String line)
    {
        if (line.StartsWith("#")) return new Nullable<Student>();
        int nr = int.Parse(line.Substring(0, 5));
        String name = line.Substring(6);
        return new Nullable<Student>(new Student(nr, name));
    }

    public static Sorter Load()
    {
        Sorter srt = new Sorter();
        WithLines(STUDENTS_FILE).Select(toStudent).ToList().ForEach(
            std => { if (std.HasValue) srt.stds.Add(std.Value); }
        );
        return srt;
    }

    public void Print()
    {
        stds.ForEach(s => Console.WriteLine(s));
    }

    public Student Rand()
    {
        return stds[r.Next(stds.Count)];
    }
}

class App
{
    static void Main()
    {
        Sorter.Load().Rand().Print();
    }
}