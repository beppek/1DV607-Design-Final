using _1dv607Design.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1dv607Design.view
{
    class ViewRenderer
    {
        /// <summary>
        /// Display welcome message
        /// </summary>
        public void WelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine(
                "Welcome to the boat club member registry. " +
                "\nSelect from the menu below by inputting corresponding number." +
                "\n1 List Members" +
                "\n2 Add New Member" +
                "\n3 Exit Program"
              );
        }

        public void WrongInput()
        {
            Console.WriteLine("Wrong input, try again...");
        }

        /// <summary>
        /// Render view to Create new member
        /// </summary>
        public void CreateMemberView()
        {
            Console.Clear();
            Console.WriteLine(
                "Create New Member" +
                "\nInput member name and personal number below"
                );
        }

        public void RegisterBoatView()
        {
            Console.Clear();
            Console.WriteLine(
                "Select boat type and length (in meters)" +
                "\nStart with the type" +
                "\n1 Sailboat" +
                "\n2 Motorsailer" +
                "\n3 Kayak/Canoe" +
                "\n4 Other"
                );
        }

        public void Boats(List<Boat> boats)
        {
            var i = 1;
            foreach (var boat in boats)
            {
                Console.WriteLine($"{i} Type: {boat.Type}, Length: {boat.Length} meters");
                i += 1;
            }
        }

        public void EditMemberView()
        {
            Console.WriteLine(
                    "Input new information. " +
                    "If you don't want to change a field just leave it blank and hit enter."
                    );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        public void MemberInfo(Member member)
        {
            var boats = string.Join("\n\t", member.BoatsOwned.Select(boat => $"Type: {boat.Type}, Length: {boat.Length} meters").ToList());
            Console.Clear();
            Console.WriteLine($"{"|Name|",15} {"|Personal Number|",25} {"|ID|",10} {"|Boats|",10}");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine(
                    $"Name: {member.Name}\n" +
                    $"Personal Number: {member.PersonalNumber}\n" +
                    $"ID: {member.Id}\n" +
                    $"Boats: \t{boats}\n"
                    );
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine(
                "1 Register Boat to Member" +
                "\n2 Edit Boat" +
                "\n3 Delete Boat" +
                "\n4 Edit Member Info" +
                "\n5 Delete Member" +
                "\n6 Return to List" +
                "\n7 Return to Main Menu"
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberList"></param>
        public void MembersCompact(List<Member> memberList)
        {
            Console.Clear();
            Console.WriteLine($"{"|Name|",15} {"|ID|",21} {"|Number of Boats|",24}");
            Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (var member in memberList)
            {
                Console.WriteLine($"{member.Name,20} {member.Id,15} {member.BoatsOwned.Count,14}\n");
            }
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("Input member id to view info or press enter to return to menu.");
        }

        public void MembersVerbose(List<Member> memberList)
        {
            Console.Clear();
            foreach (var member in memberList)
            {
                var boats = string.Join("\n\t", member.BoatsOwned.Select(boat => $"Type: {boat.Type}, Length: {boat.Length} meters").ToList());
                Console.WriteLine(
                    $"Name: {member.Name}\n" +
                    $"Personal Number: {member.PersonalNumber}\n" +
                    $"ID: {member.Id}\n" +
                    $"Boats: \t{boats}\n"
                    );
                Console.WriteLine("*************************\n");
            }

            Console.WriteLine("Input member id to view info or press enter to return to menu.");
        }

    }
}
