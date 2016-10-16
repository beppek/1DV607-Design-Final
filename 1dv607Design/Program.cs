using _1dv607Design.view;
using System;

namespace _1dv607Design
{
    public class Program
    {
        
        private static void Main()
        {
            var view = new RegistryView();

            var menuSelection = view.MainMenu();

            while (menuSelection != MenuSelection.Exit)
            {
                switch (menuSelection)
                {
                    case MenuSelection.AddMember:
                        view.AddMember();
                        break;
                    case MenuSelection.ListMembers:
                        view.ListMembers();
                        break;
                }

                menuSelection = view.MainMenu();
            }
        }
    }
}
