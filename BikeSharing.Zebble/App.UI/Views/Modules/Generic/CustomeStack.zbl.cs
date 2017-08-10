namespace UI.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;
    using Domain;

    partial class CustomeStack
    {
        string path, name;

        public string Path { get => path; set => path = value; }
        public string Name { get => name; set => name = value; }

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
        }

    }
}