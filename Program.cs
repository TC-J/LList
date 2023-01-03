using System.CommandLine;
using System.Text.Json;
using System.Text;

namespace list;

class LLNode
{
    private LLNode? Next
    {
        get; set;
    }

    private LLNode? Previous
    {
        get; set;
    }

    public string Type
    {
        get; set;
    }

    public string Value
    {
        get; set;
    }

    public LLNode()
    {
        this.Type = "TBA";
        this.Value = "TBA";

    }

    public LLNode(string dataType, string dataValue)
    {
        this.Type = dataType;
        this.Value = dataValue;
    }

}

class LList
{
    public string Id
    {
        get; set;
    }

    public LLNode Head
    {
        get; set;
    }

    public LList(string id)
    {
        this.Id = id;
        this.Head = new LLNode("TBA", "TBA");
    }
}

class App
{
    public static void EnsureDir(string path)
    {
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
    }

    public static string MakePath(string[] pathComponents)
    {
        return String.Join(Path.DirectorySeparatorChar, pathComponents);
    }

}

class Program
{

    // Command: llist create <names>..
    static void AddSubcommandCreate(RootCommand program)
    {
        var createCmd = new Command("create", "create a new list");

        var createNameArg = new Argument<IEnumerable<string>>("name", "name of list");

        createCmd.AddArgument(createNameArg);

        createCmd.SetHandler((names) =>
        {
            createCmdHandler(names.ToList());
        }, createNameArg);

        program.AddCommand(createCmd);
    }

    static void AppInvariants()
    {
        App.EnsureDir("ll.d");
    }

    static int Main(string[] argv)
    {
        // Program Root Command
        var program = new RootCommand("Create & Manage Lists");

        AddSubcommandCreate(program);

        AppInvariants();

        return program.Invoke(argv);
    }


    static void createCmdHandler(List<string> names)
    {
        foreach (string name in names)
        {
            var ll = new LList(name);

            var jsonStr = JsonSerializer.Serialize(ll);

            var fPath = App.MakePath(new[] { "ll.d", name + ".json" });

            var fStream = File.Create(fPath);


            fStream.Write(Encoding.ASCII.GetBytes(jsonStr));

            fStream.Close();
        }
    }
}