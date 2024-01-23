using System.Collections.Generic;
using Xunit;

namespace CursoOnlineDDD.Dominio.Test.Cursos
{
    public class CursoTest
    {
        /*
         * Eu, enquanto administrador, quero criar e editar cursos para
         * que sejam abertas matrículas para o mesmo.
         * Critérios de aceite
         * - Criar um curso com nome, carga horária, público alvo e 
         * valor do curso
         * - As opções para público alvo são: Estudante, Universitário,
         * Empregado e Empregador
         * - Todos os campos de curso são obrigatórios
         */

        [Fact]
        public void DeveCriarCurso()
        {
            //Arrange
            const string nome = "Informática básica";
            const double cargaHoraria = 80;
            const string publicoAlvo = "Estudantes";
            const double valor = 950;

            //Action
            var curso = new Curso(nome, cargaHoraria, publicoAlvo, valor);

            //Assert
            Assert.Equal(nome, curso.Nome);
            Assert.Equal(cargaHoraria, curso.CargaHoraria);
            Assert.Equal(publicoAlvo, curso.PublicoAlvo);
            Assert.Equal(valor, curso.Valor);

        }
    }

    public class Curso
    {


        public Curso(string nome, double cargaHoraria, string publicoAlvo, double valor)
        {
            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public string Nome { get; set; }
        public double CargaHoraria { get; private set; }
        public string PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
    }
}
