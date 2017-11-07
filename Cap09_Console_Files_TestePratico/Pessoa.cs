using System;

/*
    Classes com construtores
    Construtores são métodos especiais que permitem criar objetos com dados
    Neste exemplo temos:
        - 3 Atributos: Nome, idade e género
        - Construtores: Default e 3 especificos
        - Metodos Locais
        - Getters & Setters

    Finalmente: para os objetos de uma classe poderem ser serializaveis, 
    temos que dotar a classe dessa capacidade:
    Adicionamos o atirbuto [Serializable] sobre a declaração de CLASSE
*/

namespace Cap09_Console_Files_TestePratico
{
    [Serializable]          // Atributo especial para serializar as classes
    class Pessoa
    {

        #region Atributos

        private string nome;
        private int idade;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor default
        /// Nada devolve
        /// Nada recebe
        /// Se não for declarado pelo programador, o java cria-o por defeito
        /// É por isso que podemos criar objetos das classes sem declarar um unico construtor -> Pessoa P = new Pessoa();
        /// </summary>
        public Pessoa()
        {

        }

        /// <summary>
        /// Construtor que recebe a string para o atributo nome
        /// </summary>
        /// <param name="name"> nome da pessoa </param>
        public Pessoa(string name)
        {
            // com "nome" nos parametros -> this.nome = nome;
            // ou com name nos parametros

            nome = name;
        }

        /// <summary>
        /// Construtor que recebe a string para o atributo nome
        /// </summary>
        /// <param name="name"> nome da pessoa </param>
        /// <param name="age"> idade da pessoa </param>
        public Pessoa(string name, int age)
        {
            // com "nome" nos parametros -> this.nome = nome;
            // ou com name nos parametros

            nome = name;
            idade = age;
        }

        #endregion

        #region Metodos Locais

        #endregion

        #region Getters & Setters

        public string getNome()
        {
            return nome;
        }

        public void setNome(string nome)
        {
            this.nome = nome;
        }

        public int getIdade()
        {
            return idade;
        }

        public void setIdade(int idade)
        {
            this.idade = idade;
        }

        #endregion


    }
}
