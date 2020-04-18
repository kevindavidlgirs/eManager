using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using PRBD_Framework;

namespace prbd_1920_g04
{
    public enum Role {
        Member = 1,
        Admin = 2
    }

    public class Model : DbContext {
        public Model() : base("prbd_1920_g04") {
            // la base de données est supprimée et recréée quand le modèle change
            Database.SetInitializer<Model>(new DropCreateDatabaseIfModelChanges<Model>());
        }

        public DbSet<Member> Members { get; set; }

        public Member CreateMember(string pseudo, string password, Role role = Role.Member) {
            var member = Members.Create();
            member.Pseudo = pseudo;
            member.Password = password;
            member.Role = role;
            Members.Add(member);
            return member;
        }

        public void SeedData() {
            if (Members.Count() == 0) {
                Console.Write("Seeding members... ");
                var admin = CreateMember("admin", "admin", Role.Admin);
                var ben = CreateMember("ben", "ben");
                var bruno = CreateMember("bruno", "bruno");
                SaveChanges();
                Console.WriteLine("ok");
            }
        }
    }
}