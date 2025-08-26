using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projetomvc.Models;

namespace projetomvc.context
{
    public class AgendaContext : DbContext
    {
        // DbContextOptions<AgendaContext> significa que você está passando 
        // as opções de configuração para a sua classe AgendaContext.
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {

        }

        public DbSet<Contato> Contatos { get; set; }
    }   // pega do model Contato e transforma numa tabela contatos
}