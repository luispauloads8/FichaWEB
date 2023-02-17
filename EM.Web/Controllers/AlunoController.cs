using EM.Domain;
using EM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EM.Web.Controllers
{
    public class AlunoController : Controller
    {
        private readonly RepositorioAluno _repositorioAluno = new RepositorioAluno();

        // GET: Aluno
        public ActionResult Index(string searchString, int? opcao)
        {

            int? _opcao = opcao;
            string _searchString = searchString;
            IEnumerable<Aluno> alunos = _repositorioAluno.GetAll();
            List<Aluno> alunoss = new List<Aluno>();
            

            if (_opcao == 1)
            {
                if (!string.IsNullOrEmpty(_searchString))
                {
                    int matricula = 0;
                    try
                    {
                        matricula = int.Parse(_searchString);
                    }
                    catch (Exception)
                    {

                        return RedirectToAction("Index");
                    }

                    Aluno aluno = new Aluno();
                    aluno =  _repositorioAluno.GetByMatricula(matricula);
                    if (aluno.Matricula.Equals(0))
                    {
                        TempData["matriculanaoencontrada"] = "Matricula não encontrada!";
                        return View(_repositorioAluno.GetAll());
                    }
                    alunoss.Add(aluno);
                    alunos = alunoss;
                }
                else
                {
                    return View(_repositorioAluno.GetAll());
                }

            }
            else
            {
                if (_opcao == 2)
                {

                    if (!string.IsNullOrEmpty(_searchString))
                    {

                        alunos = _repositorioAluno.GetByContendoNome(_searchString);
                    }
                    else
                    {
                        return View(_repositorioAluno.GetAll());
                    }
                    
                }
            }

            return View(alunos.ToList());
        }

        // GET: Aluno
        public ActionResult EditarAluno(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            Aluno alunoatualizado = _repositorioAluno.GetByMatricula(id.Value);
            if (alunoatualizado == null)
            {
                return HttpNotFound();
            }

            return View(alunoatualizado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarAluno([Bind(Include = "Matricula,Nome,Sexo,CPF,Nascimento")] Aluno aluno)
        {

            if (ModelState.IsValid)
            {
                _repositorioAluno.Update(aluno);
                return RedirectToAction(nameof(Index));
            }

            return View(aluno);
        }

        // GET: Aluno
        public ActionResult CadastrarAluno()
        {
            Aluno aluno = new Aluno();
            ViewBag.DadosMatricula = UltimaMatricula();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarAluno([Bind(Include = "Matricula,Nome,Sexo,CPF,Nascimento")] Aluno aluno)
        {

            if (ModelState.IsValid)
            {
               
                if (_repositorioAluno.GetAll().Any(C => C.Matricula == aluno.Matricula))
                {
                    TempData["matriculaobrigatoria"] = "Matícula ja utlizada! Utilizar a matrícula " + UltimaMatricula() + " para realizar o cadastro!";
                }
                else if (_repositorioAluno.GetAll().Any(C => C.CPF == aluno.CPF))
                {
                    TempData["cpfduplicado"] = "CPF já cadastrado!";
                }
                else
                {
                    _repositorioAluno.Add(aluno);
                    return RedirectToAction(nameof(Index));
                }

            }

            return View(aluno);
        }

        private bool PedidoExists(Aluno aluno)
        {
            return _repositorioAluno.GetAll().Contains(aluno);
        }

        public int UltimaMatricula()
        {
            var dadosmatricula = 1;

            IEnumerable<Aluno> alunos = _repositorioAluno.GetAll().AsQueryable();
            try
            {
                dadosmatricula = alunos.OrderByDescending(x => x.Matricula).First().Matricula + 1;
                ViewBag.DadosMatricula = dadosmatricula;
            }
            catch (Exception)
            {

                ViewBag.DadosMatricula = dadosmatricula + 1;
            }
            return dadosmatricula;
        }

        // GET: Aluno
        public ActionResult DeletarAluno(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Aluno alunoexcluir = _repositorioAluno.GetByMatricula(id.Value);
            if (alunoexcluir == null)
            {
                return HttpNotFound();
            }
            return View(alunoexcluir);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarAluno(Aluno aluno)
        {
            _repositorioAluno.Remove(aluno);
            return RedirectToAction(nameof(Index));
        }

    }
}