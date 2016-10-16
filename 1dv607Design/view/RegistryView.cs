using _1dv607Design.controller;
using _1dv607Design.model;
using System;

namespace _1dv607Design.view
{
    public class RegistryView
    {
        private RegistryController _controller;
        private ViewRenderer _render;

        public RegistryView()
        {
            Console.Title = "Member Registry";
            _controller = new RegistryController();
            _render = new ViewRenderer();
        }

        /// <summary>
        /// Main menu of application.
        /// </summary>
        /// <returns>MenuSelection ->Exit, ListMembers, AddMember</returns>
        public MenuSelection MainMenu()
        {
            _render.WelcomeMessage();
            
            var key = Console.ReadLine();
            int input;
            while (!int.TryParse(key, out input) || input > 3 || input < 1)
            {
                _render.WrongInput();
                key = Console.ReadLine();
                _render.WelcomeMessage();
            }
            switch (input)
            {
                case 1:
                    return MenuSelection.ListMembers;
                case 2:
                    return MenuSelection.AddMember;
                default:
                    return MenuSelection.Exit;
            }

        }

        public void ListMembers()
        {
            Console.Clear();
            Console.WriteLine(
                    "How do you want to display the members? " +
                    "\n(V)erbose or (C)ompact?"
                    );

            var typeInput = Console.ReadLine();
            while (typeInput == null || (typeInput.ToLower() != "v" && typeInput.ToLower() != "c"))
            {
                _render.WrongInput();
                typeInput = Console.ReadLine();
            }

            var listType = typeInput.ToLower() == "v" ? ListType.Verbose : ListType.Compact;
            var members = _controller.RetrieveAll();
            if (listType == ListType.Compact)
            {
                _render.MembersCompact(members);
            }
            else
            {
                _render.MembersVerbose(members);
            }

            //Select member to view
            var idInput = Console.ReadLine();
            int id;
            while (!string.IsNullOrWhiteSpace(idInput) && (!int.TryParse(idInput, out id)))
            {
                _render.WrongInput();
                idInput = Console.ReadLine();
            }

            if (int.TryParse(idInput, out id))
            {
                try
                {
                    ViewMember(id);
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine(
                        "Something went wrong while retrieving the member." +
                        "\nHit Enter to return to Main Menu..."
                        );
                    Console.ReadLine();
                }

            }
        }

        /// <summary>
        /// Handle interaction with selected member
        /// </summary>
        /// <param name="id">member id of member to be viewed</param>
        private void ViewMember(int id)
        {
            var member = _controller.Retrieve(id);
            _render.MemberInfo(member);

            var key = Console.ReadLine();
            int input;
            while (!int.TryParse(key, out input) || input > 7 || input < 1)
            {
                _render.WrongInput();
                key = Console.ReadLine();
            }
            switch (input)
            {
                case 1:
                    AddBoat(member);
                    break;
                case 2:
                    EditBoat(member);
                    break;
                case 3:
                    DeleteBoat(member);
                    break;
                case 4:
                    EditMember(member);
                    break;
                case 5:
                    _controller.Delete(member.Id);
                    break;
                case 6:
                    ListMembers();
                    break;
                case 7:
                    break;
            }
        }

        public void AddMember()
        {
            _render.CreateMemberView();
            Console.WriteLine("Full Name (Firstname Lastname):");
            var name = Console.ReadLine();
            Console.WriteLine("Personal Number (10 digits):");
            var numberInput = Console.ReadLine();
            long personalNumber;
            while (!long.TryParse(numberInput, out personalNumber) || personalNumber.ToString().Length != 10)
            {
                Console.WriteLine("No good, personal number must be a number with 10 digits!");
                numberInput = Console.ReadLine();
                Console.Clear();
            }

            try
            {
                _controller.Create(name, personalNumber);
            }
            catch (Exception)
            {
                Console.WriteLine(
                    "Something went wrong while creating the user! " +
                    "Return to (m)ain menu or try (a)gain?"
                    );
                var input = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(input) || (input.ToLower() != "m" && input.ToLower() != "a"))
                {
                    _render.WrongInput();
                    input = Console.ReadLine();
                }
                if (input.ToLower() == "a")
                {
                    AddMember();
                }
                else
                {
                    return;
                }
            }

            Console.Clear();
            Console.WriteLine("Member successfully created!");
            Console.WriteLine("Hit any key to go back to the menu...");
            Console.ReadLine();
        }

        /// <summary>
        /// Add boat
        /// </summary>
        /// <param name="member"></param>
        private void AddBoat(Member member)
        {
            _render.RegisterBoatView();
            var boatType = GetBoatType();
            var boatLength = GetBoatLength();
            _controller.RegisterBoat(boatType, boatLength, member);

        }

        /// <summary>
        /// Delete Boat
        /// </summary>
        /// <param name="member">Owner of the boat</param>
        private void DeleteBoat(Member member)
        {
            var boats = member.BoatsOwned;
            Console.Clear();
            Console.WriteLine("Which boat would you like to delete?");
            _render.Boats(boats);
            var key = Console.ReadLine();
            int input;
            while (!int.TryParse(key, out input) && (input > boats.Count || input < 1))
            {
                _render.WrongInput();
                key = Console.ReadLine();
            }
            try
            {
                _controller.DeleteBoat(input - 1, member);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to delete boat...");
                Console.WriteLine("Hit Enter to return to menu...");
                Console.ReadLine();
            }

        }

        /// <summary>
        /// Edit boat
        /// </summary>
        /// <param name="member">Owner of the boat</param>
        private void EditBoat(Member member)
        {
            var boats = member.BoatsOwned;
            Console.Clear();
            Console.WriteLine("Which boat would you like to edit?");
            _render.Boats(boats);
            var key = Console.ReadLine();
            int input;
            while (!int.TryParse(key, out input) && (input > boats.Count || input < 1))
            {
                _render.WrongInput();
                key = Console.ReadLine();
            }

            var index = input - 1;

            _render.RegisterBoatView();
            var boatType = GetBoatType();
            var boatLength = GetBoatLength();
            _controller.UpdateBoat(index, member, boatType, boatLength);
        }

        /// <summary>
        /// Lets user select boat type
        /// </summary>
        /// <returns></returns>
        private BoatType GetBoatType()
        {
            BoatType boatType;
            var key = Console.ReadLine();
            int input;
            while (!int.TryParse(key, out input) || input > 4 || input < 1)
            {
                _render.WrongInput();
                key = Console.ReadLine();
            }

            switch (input)
            {
                case 1:
                    boatType = BoatType.Sailboat;
                    break;
                case 2:
                    boatType = BoatType.Motorsailer;
                    break;
                case 3:
                    boatType = BoatType.Kayak;
                    break;
                default:
                    boatType = BoatType.Other;
                    break;
            }

            return boatType;
        }

        /// <summary>
        /// Lets user input boat length
        /// </summary>
        /// <returns></returns>
        private double GetBoatLength()
        {
            Console.WriteLine("Now we need the length of the boat in metres. " +
                              "\nExample input: 3, 4.2, 5.75");
            var key = Console.ReadLine();
            double length;
            while (!double.TryParse(key, out length) || length <= 0)
            {
                _render.WrongInput();
                key = Console.ReadLine();
            }
            return length;
        }

        /// <summary>
        /// Edit the member
        /// </summary>
        /// <param name="member">Member to be edited</param>
        private void EditMember(Member member)
        {
            Console.Clear();
            _render.EditMemberView();
            Console.WriteLine("Full Name (Firstname Lastname):");
            var name = Console.ReadLine();

            //Assign original name if nothing new
            if (string.IsNullOrWhiteSpace(name))
            {
                name = member.Name;
            }

            Console.WriteLine("Personal Number (10 digits):");
            var numberInput = Console.ReadLine();
            long personalNumber;

            //Assign original personal number if nothing new
            if (string.IsNullOrWhiteSpace(numberInput))
            {
                personalNumber = member.PersonalNumber;
            }
            else
            {
                while (!long.TryParse(numberInput, out personalNumber))
                {
                    Console.WriteLine("No good, personal number must be a number!");
                    numberInput = Console.ReadLine();
                    Console.Clear();
                }
            }

            _controller.Update(name, personalNumber, member);
            Console.Clear();
            Console.WriteLine("Member successfully updated!");
            Console.WriteLine("Hit any key to go back to the menu...");
            Console.ReadLine();
        }
    
    }
}