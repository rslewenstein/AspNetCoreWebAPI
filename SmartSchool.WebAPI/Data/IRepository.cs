using System.Threading.Tasks;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         bool SaveChanges();

         //Alunos
         Task<Aluno[]> GetAllAlunosAsync(bool includeProfessor = false);
         Aluno[] GetAllAlunos(bool includeProfessor = false);
         Aluno[] GetAllAlunosByDisciplinaId(int disciplinaid, bool includeProfessor = false);
         Aluno GetAlunoById(int alunoid, bool includeProfessor = false);

         //Professores
         Professor[] GetAllProfessores(bool includeProfessor = false);
         Professor[] GetAllProfessoresByDisciplinaId(int disciplinaid, bool includeAluno = false);
         Professor GetProfessorById(int professorid, bool includeProfessor = false);
    }
}