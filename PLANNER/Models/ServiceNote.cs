using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PLANNER.Models
{
    public class ServiceNote
    {
        private readonly AppDbContext _context;
        public ServiceNote(AppDbContext context)
        {
            _context = context;
        }
        public void CreateNote(Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();

        }
        public void UpdateNote(Note note)
        {
            _context.Notes.Update(note);
            _context.SaveChanges();
        }
        public void DeleteNote(int id)
        {
            var note = _context.Notes.Find(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges();
            }
        }
        public List<Note> GetNotes()
        {
            return _context.Notes.ToList();
        }
        public Note GetNote(int id)
        {
            var note = _context.Notes.Find(id);
            return note;
        }
    }
}
