namespace UI.Modules
{
    using System.Threading.Tasks;

    partial class CustomeStack
    {
        string path, name;

        public string Path { get => path; set => path = value; }
        public string Name { get => name; set => name = value; }

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
         
        }

    }
}