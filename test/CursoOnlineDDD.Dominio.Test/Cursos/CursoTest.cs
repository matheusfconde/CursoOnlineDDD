using System;
using CursoOnlineDDD.Dominio.Test._Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnlineDDD.Dominio.Test.Cursos
{
    public class CursoTest :IDisposable
    {
        /*
         * Eu, enquanto administrador, quero criar e editar cursos para que sejam abertas matrículas para o mesmo.
         * Critérios de aceite:
         * - Criar um curso com nome, carga horária, público alvo e valor do curso;
         * - As opções para público alvo são: Estudante, Universitário, Empregado e Empregador;
         * - Todos os campos de curso são obrigatórios.
        */

        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;

        public CursoTest(ITestOutputHelper output)
        {
                //A cada método de teste, o construtor é executado.
                _output = output;
                _output.WriteLine("Construtor sendo executado.");

                _nome = "Informática básica";
                _cargaHoraria = 80;
                _publicoAlvo = PublicoAlvo.Estudante;
                _valor = 950;
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado.");
        }

        [Fact]
        public void DeveCriarCurso()
        {
            //Arrange
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor
            };

            //Action
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            //Assert
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            //Assert
            Assert.Throws<ArgumentException>(() =>
                 new Curso(nomeInvalido, _cargaHoraria, _publicoAlvo, _valor)).ComMensagem("Nome inválido");
        }

        //Os dois testes abaixos, estão sendo substituídos pelo de cima com a notação [Theory].
        //[Fact]
        //public void NaoDeveCursoTerUmNomeVazio()
        //{
        //    //Arrange
        //    var cursoEsperado = new
        //    {
        //        Nome = "Informática básica",
        //        CargaHoraria = (double)80,
        //        PublicoAlvo = PublicoAlvo.Estudante,
        //        Valor = (double)950
        //    };

        //    //Assert
        //    Assert.Throws<ArgumentException>(() =>
        //        new Curso(string.Empty, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor));

        //}

        //[Fact]
        //public void NaoDeveCursoTerUmNomeNulo()
        //{
        //    //Arrange
        //    var cursoEsperado = new
        //    {
        //        Nome = "Informática básica",
        //        CargaHoraria = (double)80,
        //        PublicoAlvo = PublicoAlvo.Estudante,
        //        Valor = (double)950
        //    };

        //    //Assert
        //    Assert.Throws<ArgumentException>(() =>
        //        new Curso(null, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor));

        //}

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {
            //Assert
            Assert.Throws<ArgumentException>(() =>
                    new Curso(_nome, cargaHorariaInvalida, _publicoAlvo, _valor)).ComMensagem("Carga horária inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmValorMenorQue1(double valorInvalido)
        {
            //Assert
            Assert.Throws<ArgumentException>(() =>
                new Curso(_nome, _cargaHoraria, _publicoAlvo, valorInvalido)).ComMensagem("Valor inválido");
        }
    }

    public enum PublicoAlvo
    {
        Estudante,
        Universitario,
        Empregado,
        Empreendedor
    }

    public class Curso
    {
        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty((nome)))
                throw new ArgumentException("Nome inválido");

            if (cargaHoraria < 1)
                throw new ArgumentException("Carga horária inválida");

            if (valor < 1)
                throw new ArgumentException("Valor inválido");

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public string Nome { get; set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
    }
}
