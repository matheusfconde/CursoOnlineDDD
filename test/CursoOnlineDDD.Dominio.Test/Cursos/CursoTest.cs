using System;
using Bogus;
using CursoOnlineDDD.Dominio.Test._Builders;
using CursoOnlineDDD.Dominio.Test._Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnlineDDD.Dominio.Test.Cursos
{
    public class CursoTest : IDisposable
    {
        /*
         * Eu, enquanto administrador, quero criar e editar cursos para que sejam abertas matrículas para o mesmo.
         * Critérios de aceite:
         * - Criar um curso com nome, carga horária, público alvo e valor do curso;
         * - As opções para público alvo são: Estudante, Universitário, Empregado e Empregador;
         * - Todos os campos de curso são obrigatórios.
         * - Curso deve ter uma descrição (não obrigatório)
        */

        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public CursoTest(ITestOutputHelper output)
        {
            //A cada método de teste, o construtor é executado.
            _output = output;
            _output.WriteLine("Construtor sendo executado.");

            var faker = new Faker();

            _nome = faker.Random.Word();
            _cargaHoraria = faker.Random.Double(50,1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = faker.Random.Double(100,1000);
            _descricao = faker.Lorem.Paragraph();
            
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
                Valor = _valor,
                Descricao = _descricao
            };

            //Action
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

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
                CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem("Nome inválido");
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
                CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                 .ComMensagem("Carga horária inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmValorMenorQue1(double valorInvalido)
        {
            //Assert
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComValor(valorInvalido).Build())
                .ComMensagem("Valor inválido");
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
        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty((nome)))
                throw new ArgumentException("Nome inválido");

            if (cargaHoraria < 1)
                throw new ArgumentException("Carga horária inválida");

            if (valor < 1)
                throw new ArgumentException("Valor inválido");

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
    }
}
