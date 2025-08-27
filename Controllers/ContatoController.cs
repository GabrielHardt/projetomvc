using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projetomvc.context;
using projetomvc.Models;

namespace projetomvc.Controllers
{
    public class ContatoController : Controller
    {
        // Declara uma variável para guardar a referência objeto que gerencia a conexão com o banco de dados.
        private readonly AgendaContext _context;

        // O construtor recebe o contexto do banco de dados via injeção de dependência
        public ContatoController(AgendaContext context)
        {
            _context = context; // Atribui o contexto recebido à variável de classe
        }

        public IActionResult index()
        {
            var contatos = _context.Contatos.ToList(); //pegando todos os contatos e pondo em lista
            return View(contatos); // carregando a view, com a lista de contatos
        }

        public IActionResult Criar() //pra tela inicial do criar, primeira vez criando
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Contato contato) // pega a instancia do contato gerada na view
        {
            if (ModelState.IsValid)
            {
                _context.Contatos.Add(contato);
                _context.SaveChanges(); // logica do EF pra ligar ao banco (que fica no context)
                return RedirectToAction(nameof(index));
            }
            return View(contato);
        }

        public IActionResult Editar(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
                return NotFound();

            return View(contato);
        }

        [HttpPost]
        public IActionResult Editar(Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(contato.Id); // contatoBanco 

            contatoBanco.Nome = contato.Nome; // contato banco recebe as informacoes passadas por contato
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco); // salvamos o contato editado na lista de contatos
            _context.SaveChanges(); // salvamos no banco

            return RedirectToAction(nameof(index));
        }

        public IActionResult Detalhes(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
                return RedirectToAction(nameof(index));
            return View(contato);
        }

        public IActionResult Deletar(int id)
        {
            var contato = _context.Contatos.Find(id);

            return View(contato);
        }

        [HttpPost]
        public IActionResult Deletar(Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(contato.Id);

            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(index));
        }
    }
}