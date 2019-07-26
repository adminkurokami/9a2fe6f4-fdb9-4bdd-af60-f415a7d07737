using FirebirdSql.Data.FirebirdClient;
using System;
using System.Linq;
using TestFirebird.Data;
using TestFirebird.Model;

namespace TestFirebird
{
    public class Controller
    {
        //global ApplicationDbContext + global FbConnection => keep incrasing transaction time (Configuration.AutoDetectChangesEnabled = false or .AsNoTracking() won't work)
        //local new ApplicationDbContext + global FbConnection => keep incrasing transaction time (Configuration.AutoDetectChangesEnabled = false or .AsNoTracking() won't work)
        //local new ApplicationDbContext + new FbConnection => error (contextOwnsConnection = false)
        private FbConnection _dbconnection;
        private string _connectionString;
        //  private ApplicationDbContext _context;
        private ApplicationDbContext Context()
        {
            ApplicationDbContext ctx = new ApplicationDbContext(_dbconnection, false);
          //  ApplicationDbContext ctx = new ApplicationDbContext(new FbConnection(_connectionString), false);
            ctx.Configuration.AutoDetectChangesEnabled = false;
            return ctx;
        }
        public Controller(string connectionString)
        {
            _connectionString = connectionString;
            _dbconnection = new FbConnection(connectionString);
            //  _context = new ApplicationDbContext(_dbconnection, false);
            //  _context.Configuration.AutoDetectChangesEnabled = false;
        }

        public Student AddStudent(Student student, string universityName)
        {
            ApplicationDbContext _context = Context();
            if (student.Id != 0)
            {

            }
            else if (_context.Students.Where(i => i.Id == student.Id).Any())
            {
                throw new ArgumentException("Already exist");
            }

            if (universityName == string.Empty)
            {
                throw new ArgumentNullException("null university");
            }
            else
            {
                var universityId = _context.Universities.Where(u => u.Name == universityName).Select(u => u.Id).First();
                student.UniversityId = universityId;
            }
            _context.Students.Add(student);
            _context.SaveChanges();
            _context.Dispose();
            return student;
        }
        public University AddUniversity(string universityName)
        {
            ApplicationDbContext _context = Context();
            if (universityName == null)
            {
                throw new ArgumentNullException();
            }
            else if (_context.Universities.Where(i => i.Name == universityName).Any())
            {
                throw new ArgumentException("Already exist");
            }
            University university = new University { Name = universityName };
            _context.Universities.Add(university);
            _context.SaveChanges();
            _context.Dispose();
            return university;
        }
        public University GetUniversity(string universityName)
        {
            ApplicationDbContext _context = Context();
            if (universityName == null)
            {
                throw new ArgumentNullException();
            }
            var university = _context.Universities.Where(i => i.Name == universityName).First();
            _context.Dispose();
            return university;
        }
        public int test()
        {
            ApplicationDbContext _context = Context();
            int count = _context.Students.Count();
            int countuniv = _context.Universities.Count();
            _context.SaveChanges();
            _context.Dispose();
            return count;
        }

    }
}
