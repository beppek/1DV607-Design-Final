using System.Collections.Generic;
using _1dv607Design.model;

namespace _1dv607Design.controller
{
    public class RegistryController
    {
        private readonly Registry _db = new Registry();
        
        public void Delete(int id)
        {
            _db.Delete(id);
        }

        public void Create(string name, long personalNumber)
        {
            var id = _db.GetUniqueId();
            var member = new Member(name, personalNumber, id);
            _db.Add(member);
        }

        public void Update(string name, long personalNumber, Member member)
        {
            member.Update(name, personalNumber);
            _db.Save();
        }

        public Member Retrieve(int id)
        {
            var member = _db.Retrieve(id);
            return member;
        }

        public List<Member> RetrieveAll()
        {
            var members = _db.RetrieveAll();
            return members;
        }

        public void RegisterBoat(BoatType boatType, double length, Member member)
        {
            var boat = new Boat(boatType, length);
            member.RegisterBoat(boat);
            _db.Save();
        }

        public void DeleteBoat(int index, Member member)
        {
            member.DeleteBoat(index);
            _db.Save();
        }

        public void UpdateBoat(int index, Member member, BoatType boatType, double length)
        {
            member.UpdateBoat(index, boatType, length);
            _db.Save();
        }
    }
}