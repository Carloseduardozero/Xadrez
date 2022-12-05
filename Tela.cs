using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using xadrez;

namespace Xadrez
{
     class Tela
    {
        public static void imprimirPartida(PartidaDeXadrez partida) 
        {
            imprimirTabuleiro(partida.tab);
            Console.WriteLine();

            imprimirpecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.turno);
            Console.WriteLine("Aguardando Jogador: " + partida.jogadorAtual);

            if (partida.xeque) 
            {
                Console.WriteLine("XEQUE! ");
            }
        }

        public static void imprimirpecasCapturadas(PartidaDeXadrez partida) 
        {
            Console.WriteLine("peças capturadas: ");
            Console.Write("Brancas: ");
            imprimirconjunto(partida.pecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            imprimirconjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void imprimirconjunto(HashSet<Peca> conjunto) 
        {
            Console.Write("[");
            foreach (Peca x in conjunto) 
            {
                Console.Write(x + "");

            }
            Console.Write("]");
        }

        public static void imprimirTabuleiro(Tabuleiro tab) 
        { 
            for (int i=0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {

                    imprimirPeca(tab.peca(i, j));
                        
                }
                Console.WriteLine();    
            }
            Console.WriteLine("  a b c d e f g h ");
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoePossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (posicoePossiveis[i, j]) 
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else 
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;

                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h ");
            Console.BackgroundColor = fundoOriginal;

        }

        public static PosicaoXadrez lerPocicaoXadrez() 
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

        public static void imprimirPeca(Peca peca) 
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {

                if (peca.cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
